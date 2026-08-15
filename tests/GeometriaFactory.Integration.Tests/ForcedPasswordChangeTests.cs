using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.Security;
using Microsoft.Data.Sqlite;
using Xunit;
using Xunit.Abstractions;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// EL RECORRIDO DEL ALUMNO AL QUE LE RESETEARON LA CONTRASEÑA, de punta a punta y contra la
/// pieza de datos real: entra con la provisoria, **no obtiene sesión de trabajo**, cambia la
/// contraseña con la provisoria como credencial, y recién el ingreso siguiente le da la sesión.
/// </summary>
/// <remarks>
/// QUÉ RESUELVE. `PRODUCT-INTAKE` **1.34**: la operación de cambio de contraseña admite **dos
/// formas de autenticarse** —con sesión de trabajo, el cambio corriente; con la contraseña
/// actual, el cambio forzado—. Sin la segunda, RN-13 le niega la sesión a la cuenta marcada y la
/// pantalla del cambio queda **inalcanzable**: el alumno reseteado quedaba fuera del laboratorio.
///
/// DE DÓNDE SALE LA CUENTA MARCADA. El reseteo (`CU-13`) y la habilitación (`CU-02`) son de la
/// etapa `d` y todavía no existen como operación. La cuenta de alumno con la marca puesta se
/// escribe **directamente en el almacén**, que es exactamente el estado que esos dos actos van a
/// dejar: cuenta `Enabled`, credencial derivada de la provisoria y marca en verdadero. Lo que
/// esta batería ejercita es lo que ocurre **después** de la marca, que es lo que la etapa `c`
/// construye.
///
/// LO QUE NO SE AFLOJA, Y SE COMPRUEBA ACÁ:
///   · **RN-13** — con la marca puesta, ningún camino entrega credencial de sesión;
///   · **INV-09** — la marca la levanta ÚNICAMENTE el cambio efectivo hecho por la propia cuenta:
///     ni el intento fallido, ni el canje, ni el paso del tiempo;
///   · la forma sin sesión **sólo procede sobre una cuenta marcada**, y la contraseña vigente
///     sigue siendo obligatoria: no aparece ninguna escritura de contraseña sin credencial;
///   · **la contraseña no se guarda en claro ni viaja en ninguna respuesta.**
/// </remarks>
public sealed class ForcedPasswordChangeTests : IDisposable
{
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";

    private const string StudentEmail = "alumna@frre.utn.edu.ar";
    private const string ProvisionalPassword = "la-provisoria-que-me-paso-el-docente";
    private const string ChosenPassword = "la-que-elijo-yo-ahora";

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly ITestOutputHelper _output;

    /// <summary>
    /// El recorrido deja RASTRO LEGIBLE. Cada paso escribe su código de respuesta y su cuerpo,
    /// para que el recorrido se pueda leer entero sin volver a correrlo y para que la salida
    /// cruda sea inspeccionable: es donde se ve que ningún paso previo al cambio entrega
    /// credencial de sesión.
    /// </summary>
    public ForcedPasswordChangeTests(ITestOutputHelper output) => _output = output;

    public void Dispose() => DataServiceHarness.DiscardStore(_storePath);

    private void Trace(string step, HttpResponseMessage response, string body) =>
        _output.WriteLine($"{step,-46} {(int)response.StatusCode} {body}");

    // ---- EL RECORRIDO COMPLETO ----

    [Fact]
    public async Task TheStudentWhosePasswordWasResetReachesTheChangeAndOnlyThenGetsAWorkingSession()
    {
        using var service = new DataServiceHarness(_storePath);
        using var client = service.CreateClient();

        await ConfigureAdministratorAsync(client);
        await WriteResetStudentAsync();

        // 1 · Entra con la provisoria. La credencial SE RECONOCE y no se admite: `403` con el
        //     motivo del cambio requerido, y CERO accesos emitidos (`Api CU-01` CA-05).
        var diverted = await client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(StudentEmail, ProvisionalPassword));
        var divertedBody = await diverted.Content.ReadAsStringAsync();

        Trace("1 · canje con la provisoria", diverted, divertedBody);
        Assert.Equal(HttpStatusCode.Forbidden, diverted.StatusCode);
        Assert.Equal(ErrorCode.PasswordChangeRequired, (await Error(diverted)).Code);
        Assert.DoesNotContain("accessToken", divertedBody, StringComparison.OrdinalIgnoreCase);

        // 2 · Es llevada al cambio, y ahí cambia: SIN credencial de sesión, con la provisoria
        //     como contraseña vigente. Es la segunda forma de autenticarse del intake 1.34.
        var changed = await client.PostAsJsonAsync(
            "/cuenta/contrasena",
            new OwnPasswordChangeRequest(ProvisionalPassword, ChosenPassword, StudentEmail));
        var changedBody = await changed.Content.ReadAsStringAsync();

        Trace("2 · cambio forzado, sin sesión", changed, changedBody);
        Assert.Equal(HttpStatusCode.OK, changed.StatusCode);

        // EL CAMBIO NO EMITE NINGUNA SESIÓN: la respuesta no trae credencial de ninguna clase.
        Assert.DoesNotContain("accessToken", changedBody, StringComparison.OrdinalIgnoreCase);

        // 3 · La marca quedó levantada, y la levantó el cambio de la propia cuenta (INV-09).
        _output.WriteLine($"{"3 · marca en el almacén tras el cambio",-46} {await MarkOfAsync(StudentEmail)}");
        Assert.False(await MarkOfAsync(StudentEmail));

        // 4 · La provisoria dejó de servir y la nueva sí entrega sesión de trabajo.
        var withProvisional = await client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(StudentEmail, ProvisionalPassword));
        Trace("4a · canje con la provisoria, ya cambiada", withProvisional, await withProvisional.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.Unauthorized, withProvisional.StatusCode);

        var session = await client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(StudentEmail, ChosenPassword));
        Trace("4b · canje con la contraseña elegida", session, Redacted(await session.Content.ReadAsStringAsync()));
        Assert.Equal(HttpStatusCode.OK, session.StatusCode);

        var opened = await session.Content.ReadFromJsonAsync<SessionResponse>();
        Assert.NotNull(opened);
        Assert.False(string.IsNullOrWhiteSpace(opened!.AccessToken));
        Assert.Equal(nameof(Role.Student), opened.Role);
        Assert.Equal(StudentEmail, opened.Email);

        // 5 · Y ninguna de las dos contraseñas quedó en claro en ninguna parte.
        foreach (var body in new[] { divertedBody, changedBody, await session.Content.ReadAsStringAsync() })
        {
            Assert.DoesNotContain(ProvisionalPassword, body, StringComparison.Ordinal);
            Assert.DoesNotContain(ChosenPassword, body, StringComparison.Ordinal);
        }

        var stored = await ScalarTextAsync("select PasswordHash from Account where NormalizedEmail = $email",
            EmailIdentity.Normalize(StudentEmail));
        Assert.StartsWith("PBKDF2-SHA256$", stored, StringComparison.Ordinal);
        Assert.DoesNotContain(ProvisionalPassword, stored!, StringComparison.Ordinal);
        Assert.DoesNotContain(ChosenPassword, stored!, StringComparison.Ordinal);
    }

    // ---- RN-13 · CON LA MARCA PUESTA, NINGÚN CAMINO ENTREGA SESIÓN DE TRABAJO ----

    [Fact]
    public async Task WhileTheMarkIsSetNoPathHandsOutAWorkingSession()
    {
        using var service = new DataServiceHarness(_storePath);
        using var client = service.CreateClient();

        await ConfigureAdministratorAsync(client);
        await WriteResetStudentAsync();

        // El canje con la provisoria correcta: reconocida y NO admitida.
        var diverted = await client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(StudentEmail, ProvisionalPassword));
        Assert.Equal(HttpStatusCode.Forbidden, diverted.StatusCode);

        // El cambio con la provisoria equivocada: `401`, y LA MARCA SIGUE PUESTA.
        var refused = await client.PostAsJsonAsync(
            "/cuenta/contrasena",
            new OwnPasswordChangeRequest("no-es-la-que-me-paso", ChosenPassword, StudentEmail));

        Assert.Equal(HttpStatusCode.Unauthorized, refused.StatusCode);
        Assert.Equal(ErrorCode.InvalidCredentials, (await Error(refused)).Code);
        Assert.True(await MarkOfAsync(StudentEmail));

        // Y después del intento fallido la provisoria SIGUE siendo la vigente: el intento no
        // cambió nada. El canje vuelve a dar el desvío y ningún acceso.
        var again = await client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(StudentEmail, ProvisionalPassword));
        Assert.Equal(HttpStatusCode.Forbidden, again.StatusCode);
        Assert.DoesNotContain("accessToken", await again.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
    }

    // ---- LA SEGUNDA FORMA NO ES UNA PUERTA NUEVA ----

    [Fact]
    public async Task TheCredentialFormOnlyServesTheForcedChangeAndStillRequiresTheCurrentPassword()
    {
        using var service = new DataServiceHarness(_storePath);
        using var client = service.CreateClient();

        await ConfigureAdministratorAsync(client);

        // La cuenta del administrador NO tiene la marca: su contraseña se cambia con sesión, y
        // la forma sin sesión no le sirve aunque presente la contraseña correcta.
        var withoutSession = await client.PostAsJsonAsync(
            "/cuenta/contrasena",
            new OwnPasswordChangeRequest(AdministratorPassword, ChosenPassword, AdministratorEmail));

        Assert.Equal(HttpStatusCode.Unauthorized, withoutSession.StatusCode);

        // Un correo que no existe responde EXACTAMENTE igual: la marca de una cuenta ajena no
        // es averiguable desde afuera.
        var unknown = await client.PostAsJsonAsync(
            "/cuenta/contrasena",
            new OwnPasswordChangeRequest(AdministratorPassword, ChosenPassword, "nadie@frre.utn.edu.ar"));

        Assert.Equal(withoutSession.StatusCode, unknown.StatusCode);
        Assert.Equal((await Error(withoutSession)).Code, (await Error(unknown)).Code);

        // Ni acceso firmado ni correo: nada identifica a la cuenta, y responde `401` como
        // respondía la guardia.
        var anonymous = await client.PostAsJsonAsync(
            "/cuenta/contrasena", new OwnPasswordChangeRequest(AdministratorPassword, ChosenPassword));
        Assert.Equal(HttpStatusCode.Unauthorized, anonymous.StatusCode);

        // Y la contraseña del administrador no cambió por ninguno de los tres intentos: sigue
        // entrando con la suya, y por la forma CON sesión el cambio sí procede.
        var session = await client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(AdministratorEmail, AdministratorPassword));
        Assert.Equal(HttpStatusCode.OK, session.StatusCode);

        var opened = await session.Content.ReadFromJsonAsync<SessionResponse>();
        using var authorized = new HttpRequestMessage(HttpMethod.Post, "/cuenta/contrasena")
        {
            Content = JsonContent.Create(new OwnPasswordChangeRequest(AdministratorPassword, ChosenPassword)),
        };
        authorized.Headers.Authorization = new AuthenticationHeaderValue("Bearer", opened!.AccessToken);

        var applied = await client.SendAsync(authorized);
        Assert.Equal(HttpStatusCode.OK, applied.StatusCode);
    }

    [Fact]
    public async Task TheCredentialFormNeverAcceptsANewPasswordWithoutTheCurrentOne()
    {
        using var service = new DataServiceHarness(_storePath);
        using var client = service.CreateClient();

        await ConfigureAdministratorAsync(client);
        await WriteResetStudentAsync();

        // Sin la vigente no hay cambio, y el motivo es el campo ausente: `400`. La cuenta
        // marcada tampoco puede saltearse la provisoria.
        var withoutCurrent = await client.PostAsJsonAsync(
            "/cuenta/contrasena", new OwnPasswordChangeRequest(null, ChosenPassword, StudentEmail));

        Assert.Equal(HttpStatusCode.BadRequest, withoutCurrent.StatusCode);
        Assert.Equal(ErrorCode.RequiredFieldMissing, (await Error(withoutCurrent)).Code);
        Assert.True(await MarkOfAsync(StudentEmail));

        // Y con la provisoria correcta el cambio sí procede: la prueba no pasa por ausencia.
        var changed = await client.PostAsJsonAsync(
            "/cuenta/contrasena",
            new OwnPasswordChangeRequest(ProvisionalPassword, ChosenPassword, StudentEmail));

        Assert.Equal(HttpStatusCode.OK, changed.StatusCode);
        Assert.False(await MarkOfAsync(StudentEmail));
    }

    // ---- Andamiaje ----

    private static async Task ConfigureAdministratorAsync(HttpClient client)
    {
        var setup = await client.PostAsJsonAsync(
            "/cuentas/administrador",
            new AdministratorSetupRequest(AdministratorEmail, "Ana", "Rossi", AdministratorPassword));

        Assert.Equal(HttpStatusCode.Created, setup.StatusCode);
    }

    /// <summary>
    /// La credencial de sesión NO se escribe en el rastro: que el recorrido sea legible no es
    /// motivo para dejar un acceso firmado en la salida de la batería.
    /// </summary>
    private static string Redacted(string body) =>
        System.Text.RegularExpressions.Regex.Replace(body, "\"accessToken\":\"[^\"]*\"", "\"accessToken\":\"<credencial de sesión emitida>\"");

    private static async Task<ErrorResponse> Error(HttpResponseMessage response)
    {
        var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(error);
        return error!;
    }

    /// <summary>
    /// Escribe la cuenta de alumno TAL COMO LA VA A DEJAR EL RESETEO de la etapa `d`: habilitada,
    /// con la credencial derivada de la provisoria y con la marca puesta. El momento de alta se
    /// copia de la fila que el producto ya escribió, para no inventar un formato de fecha.
    /// </summary>
    private async Task WriteResetStudentAsync()
    {
        // El coste de derivación es el de la batería: los parámetros viajan con el valor
        // guardado, de modo que el servicio lo verifica sin conocer esta elección.
        var derived = new PasswordDerivation(iterations: 1).Derive(ProvisionalPassword);
        Assert.NotNull(derived);

        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = """
            insert into Account (Id, Email, NormalizedEmail, FirstName, LastName, Role, Status, PasswordHash, MustChangePassword, CreatedAt)
            select $id, $email, $normalized, 'Ana', 'Diaz', 'Student', 'Enabled', $hash, 1, CreatedAt from Account limit 1
            """;
        // El identificador se escribe con la MISMA forma con la que el producto lo escribe:
        // el motor de persistencia guarda el identificador como texto en mayúsculas, y una fila
        // escrita de otra forma no se deja actualizar después.
        command.Parameters.AddWithValue("$id", Guid.NewGuid().ToString().ToUpperInvariant());
        command.Parameters.AddWithValue("$email", StudentEmail);
        command.Parameters.AddWithValue("$normalized", EmailIdentity.Normalize(StudentEmail));
        command.Parameters.AddWithValue("$hash", derived!);

        Assert.Equal(1, await command.ExecuteNonQueryAsync());
        Assert.True(await MarkOfAsync(StudentEmail));
    }

    /// <summary>La marca, leída del almacén y no de ninguna respuesta.</summary>
    private async Task<bool> MarkOfAsync(string email)
    {
        var mark = await ScalarTextAsync(
            "select MustChangePassword from Account where NormalizedEmail = $email",
            EmailIdentity.Normalize(email));

        return mark == "1";
    }

    private async Task<string?> ScalarTextAsync(string sql, string email)
    {
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("$email", email);

        var value = await command.ExecuteScalarAsync();
        return value?.ToString();
    }
}
