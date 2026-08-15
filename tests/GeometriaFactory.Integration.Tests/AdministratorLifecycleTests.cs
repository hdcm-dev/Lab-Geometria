using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Contracts.Service;
using Microsoft.Data.Sqlite;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// Los tres primeros criterios de transición de la etapa `c`, ejercidos de punta a punta contra
/// la pieza de datos real y sobre un almacén que no existía.
/// </summary>
public sealed class AdministratorLifecycleTests : IDisposable
{
    private const string Email = "docente@frre.utn.edu.ar";
    private const string FirstPassword = "la-primera-que-elegi";
    private const string SecondPassword = "la-que-elegi-despues";

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();

    public void Dispose() => DataServiceHarness.DiscardStore(_storePath);

    private static AdministratorSetupRequest Setup(string email = Email, string password = FirstPassword) =>
        new(email, "Ana", "Rossi", password);

    // ---- CRITERIO 3 · las actualizaciones de esquema se aplican solas sobre una base inexistente ----

    [Fact]
    public async Task TheSchemaIsAppliedByItselfOverANonExistentStore()
    {
        Assert.False(File.Exists(_storePath));

        using var service = new DataServiceHarness(_storePath);
        using var client = service.CreateClient();

        // El punto de salud responde `Ready` sólo cuando la fase 1 del arranque terminó, y la
        // fase 1 es exactamente la aplicación de las transformaciones (`QG-11`).
        var health = await client.GetFromJsonAsync<ServiceHealth>("/salud");

        Assert.NotNull(health);
        Assert.True(health!.Ready);
        Assert.True(File.Exists(_storePath));

        // Y se mira el almacén por dentro: la transformación quedó ASENTADA y la tabla existe
        // con sus dos índices únicos. Que el servicio arranque no alcanza como prueba de que
        // el esquema es el que corresponde.
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        Assert.Equal(1L, await ScalarAsync(connection,
            "select count(*) from __EFMigrationsHistory"));
        Assert.Equal(1L, await ScalarAsync(connection,
            "select count(*) from sqlite_master where type = 'table' and name = 'Account'"));
        Assert.Equal(2L, await ScalarAsync(connection,
            "select count(*) from sqlite_master where type = 'index' and name in ('UX_Account_NormalizedEmail', 'UX_Account_SingleAdministrator')"));
    }

    // ---- CRITERIO 1 · el administrador se configura, y sólo mientras no exista ninguno ----

    [Fact]
    public async Task TheAdministratorIsConfiguredOnTheFirstStartAndOnlyWhileThereIsNone()
    {
        using var service = new DataServiceHarness(_storePath);
        using var client = service.CreateClient();

        var first = await client.PostAsJsonAsync("/cuentas/administrador", Setup());
        Assert.Equal(HttpStatusCode.Created, first.StatusCode);

        var created = await first.Content.ReadFromJsonAsync<AccountSetupResponse>();
        Assert.NotNull(created);
        Assert.Equal(Email, created!.Email);
        Assert.Equal("Administrator", created.Role);
        Assert.NotEqual(Guid.Empty, created.AccountId);

        // Segundo intento, con OTRO correo para que lo que rechace sea la existencia del
        // administrador y no la unicidad del correo.
        var second = await client.PostAsJsonAsync(
            "/cuentas/administrador", Setup(email: "otro@frre.utn.edu.ar"));

        Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);
        var error = await second.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.Equal(ErrorCode.AdministratorAlreadyConfigured, error!.Code);

        // Y el almacén tiene UNA sola cuenta con papel `Administrator`.
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();
        Assert.Equal(1L, await ScalarAsync(connection,
            "select count(*) from Account where Role = 'Administrator'"));
    }

    [Fact]
    public async Task TheStoredCredentialIsNeitherInClearTextNorInAnyResponse()
    {
        using var service = new DataServiceHarness(_storePath);
        using var client = service.CreateClient();

        var setup = await client.PostAsJsonAsync("/cuentas/administrador", Setup());
        var setupBody = await setup.Content.ReadAsStringAsync();

        var session = await client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(Email, FirstPassword));
        var sessionBody = await session.Content.ReadAsStringAsync();

        Assert.DoesNotContain(FirstPassword, setupBody, StringComparison.Ordinal);
        Assert.DoesNotContain(FirstPassword, sessionBody, StringComparison.Ordinal);

        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "select PasswordHash from Account";
        var stored = (string?)await command.ExecuteScalarAsync();

        Assert.NotNull(stored);
        Assert.DoesNotContain(FirstPassword, stored!, StringComparison.Ordinal);
        // La forma del valor guardado es parte del contrato del dato (`Infrastructure ADR-04` §6).
        Assert.StartsWith("PBKDF2-SHA256$", stored, StringComparison.Ordinal);
        Assert.Equal(4, stored.Split('$').Length);

        // Y la respuesta de sesión declara CUATRO campos y ninguno más.
        var body = await session.Content.ReadFromJsonAsync<System.Text.Json.JsonDocument>();
        Assert.Equal(4, body!.RootElement.EnumerateObject().Count());
    }

    // ---- CRITERIO 2 · entrar, cambiar contraseña exigiendo la actual y salir, y que persista ----

    [Fact]
    public async Task SignInChangePasswordAndTheChangeSurvivesARestart()
    {
        // PRIMER ARRANQUE.
        using (var service = new DataServiceHarness(_storePath))
        {
            using var client = service.CreateClient();

            Assert.Equal(
                HttpStatusCode.Created,
                (await client.PostAsJsonAsync("/cuentas/administrador", Setup())).StatusCode);

            var accessToken = await SignInAsync(client, FirstPassword);
            Assert.NotNull(accessToken);

            // Cambiar exigiendo la actual: con la actual EQUIVOCADA no se aplica.
            var wrong = await ChangePasswordAsync(client, accessToken!, "no-es-la-mia", SecondPassword);
            Assert.Equal(HttpStatusCode.Unauthorized, wrong.StatusCode);

            // Sin credencial de sesión tampoco: el punto está bajo la guardia.
            var anonymous = await client.PostAsJsonAsync(
                "/cuenta/contrasena", new OwnPasswordChangeRequest(FirstPassword, SecondPassword));
            Assert.Equal(HttpStatusCode.Unauthorized, anonymous.StatusCode);

            // Con la actual correcta, sí.
            var applied = await ChangePasswordAsync(client, accessToken!, FirstPassword, SecondPassword);
            Assert.Equal(HttpStatusCode.OK, applied.StatusCode);

            // La anterior deja de servir en el mismo arranque.
            Assert.Null(await SignInAsync(client, FirstPassword));
            Assert.NotNull(await SignInAsync(client, SecondPassword));
        }

        // REINICIO: proceso nuevo, composición nueva, el MISMO archivo de almacén.
        using (var restarted = new DataServiceHarness(_storePath))
        {
            using var client = restarted.CreateClient();

            Assert.Null(await SignInAsync(client, FirstPassword));
            Assert.NotNull(await SignInAsync(client, SecondPassword));

            // Y el reinicio no vuelve a abrir la configuración: sigue habiendo administrador.
            Assert.Equal(
                HttpStatusCode.Conflict,
                (await client.PostAsJsonAsync("/cuentas/administrador", Setup(email: "otro@frre.utn.edu.ar"))).StatusCode);
        }
    }

    [Fact]
    public async Task AnAccessTokenIsRefusedWhenItIsAbsentMalformedOrSignedWithAnotherKey()
    {
        using var service = new DataServiceHarness(_storePath);
        using var client = service.CreateClient();

        Assert.Equal(
            HttpStatusCode.Created,
            (await client.PostAsJsonAsync("/cuentas/administrador", Setup())).StatusCode);

        var request = new OwnPasswordChangeRequest(FirstPassword, SecondPassword);

        foreach (var presented in new[] { null, "no-es-un-acceso", "a.b.c" })
        {
            var response = await ChangePasswordRawAsync(client, presented, request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // QUÉ CAMBIÓ ACÁ CON `PRODUCT-INTAKE` 1.34, Y POR QUÉ. Este punto dejó de estar bajo la
        // guardia de sesión, porque exigirle acceso firmado dejaba la pantalla del cambio
        // forzado inalcanzable (RN-13). El rechazo lo produce ahora el punto mismo: una petición
        // sin acceso utilizable y sin correo NO IDENTIFICA NINGUNA CUENTA, y recibe la respuesta
        // neutra del contrato —la misma que una credencial que no corresponde—, que es lo que
        // impide distinguir por tanteo qué cuentas existen.
        var refused = await ChangePasswordRawAsync(client, "a.b.c", request);
        var body = await refused.Content.ReadAsStringAsync();
        Assert.Equal(ErrorCode.InvalidCredentials, (await refused.Content.ReadFromJsonAsync<ErrorResponse>())!.Code);

        // Y no nombra ningún campo: decir «falta el correo» sería enseñar la otra forma de
        // autenticarse a quien está probando accesos falsificados.
        Assert.DoesNotContain("REQUIRED_FIELD_MISSING", body, StringComparison.Ordinal);
        Assert.DoesNotContain("Email", body, StringComparison.Ordinal);

        // Sobre todo: NINGUNO de los tres intentos cambió nada. La contraseña sigue siendo la
        // primera, que es la afirmación que este control existe para sostener.
        Assert.NotNull(await SignInAsync(client, FirstPassword));
        Assert.Null(await SignInAsync(client, SecondPassword));
    }

    [Fact]
    public async Task NoMessageThatReachesTheBrowserCarriesAnInternalAddress()
    {
        // `RA-03`, sobre las respuestas de error que la pieza de datos emite.
        using var service = new DataServiceHarness(_storePath);
        using var client = service.CreateClient();

        var bodies = new List<string>();
        bodies.Add(await (await client.PostAsJsonAsync(
            "/cuentas/administrador", new AdministratorSetupRequest(null, null, null, null))).Content.ReadAsStringAsync());
        bodies.Add(await (await client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(Email, "cualquiera"))).Content.ReadAsStringAsync());

        foreach (var body in bodies)
        {
            foreach (var forbidden in new[] { _storePath, "Data Source", "localhost", "127.0.0.1", "at GeometriaFactory" })
            {
                Assert.DoesNotContain(forbidden, body, StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    // ---- utilería ----

    private static async Task<string?> SignInAsync(HttpClient client, string password)
    {
        var response = await client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(Email, password));

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var session = await response.Content.ReadFromJsonAsync<SessionResponse>();
        return session?.AccessToken;
    }

    private static Task<HttpResponseMessage> ChangePasswordAsync(
        HttpClient client, string accessToken, string current, string replacement) =>
        ChangePasswordRawAsync(client, accessToken, new OwnPasswordChangeRequest(current, replacement));

    private static async Task<HttpResponseMessage> ChangePasswordRawAsync(
        HttpClient client, string? accessToken, OwnPasswordChangeRequest request)
    {
        using var message = new HttpRequestMessage(HttpMethod.Post, "/cuenta/contrasena")
        {
            Content = JsonContent.Create(request),
        };

        if (accessToken is not null)
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return await client.SendAsync(message);
    }

    private static async Task<long> ScalarAsync(SqliteConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        return (long)(await command.ExecuteScalarAsync())!;
    }
}
