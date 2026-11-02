using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Works;
using GeometriaFactory.Domain.Values;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// `POST /v1/interpretaciones/pistas` sobre el servicio real (expediente 0002, tramo T5).
/// </summary>
public sealed class HintsEndpointTests : IDisposable
{
    private const string HintsRoute = "/v1/interpretaciones/pistas";
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";
    private const string StudentEmail = "alumna@frre.utn.edu.ar";

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();

    public void Dispose() => DataServiceHarness.DiscardStore(_storePath);

    /// <summary>
    /// Sin configuración de IA —el flag apagado por defecto— la ruta responde 200 y **todas** las
    /// pistas salen de plantilla (REQ-IA-07, REQ-IA-15, D-FLAG).
    /// </summary>
    [Fact]
    public async Task WithoutConfiguration_EveryHintIsTemplate()
    {
        using var harness = new AiDataServiceHarness(_storePath);
        using var client = harness.CreateClient();
        var token = await StudentTokenAsync(client);

        var body = await HintsAsync(client, token, Scenarios.E1);

        Assert.Equal(2, body.Hints.Count);
        Assert.All(body.Hints, hints =>
        {
            Assert.Equal(nameof(HintOrigin.Template), hints.Origin);
            Assert.Equal("Warning", body.Observations[hints.ObservationIndex].Kind);
        });
    }

    /// <summary>REQ-IA-16 · Las observaciones de la respuesta de pistas son las de A-18, idénticas.</summary>
    [Theory]
    [InlineData(Scenarios.E1)]
    [InlineData(Scenarios.E3)]
    [InlineData(Scenarios.E5)]
    [InlineData("esto no es json")]
    public async Task Hints_DoNotChangeObservations(string text)
    {
        using var harness = new AiDataServiceHarness(_storePath);
        using var client = harness.CreateClient();
        var token = await StudentTokenAsync(client);

        using var interpreted = await client.SendAsync(Authorized(HttpMethod.Post, "/v1/interpretaciones", token, new WorkInterpretationRequest(text)));
        var a18 = (await interpreted.Content.ReadFromJsonAsync<WorkInterpretationResponse>())!;

        var hints = await HintsAsync(client, token, text);

        Assert.Equal(a18.Observations, hints.Observations);
    }

    /// <summary>Con el doble del modelo registrado, las pistas son del modelo.</summary>
    [Fact]
    public async Task WithAModel_HintsComeFromTheModel()
    {
        var fake = FakeChatClient.Answering(FakeChatClient.ValidAnswer);
        using var harness = new AiDataServiceHarness(_storePath, services => services.AddSingleton<IChatClient>(fake));
        using var client = harness.CreateClient();
        var token = await StudentTokenAsync(client);

        var body = await HintsAsync(client, token, Scenarios.E3);

        Assert.Equal(nameof(HintOrigin.Model), Assert.Single(body.Hints).Origin);
        Assert.Single(fake.Calls);
    }

    /// <summary>REQ-IA-09 · Después de pedir pistas, el almacén no cambia.</summary>
    [Fact]
    public async Task Hints_AreNotPersisted()
    {
        using var harness = new AiDataServiceHarness(_storePath, services =>
            services.AddSingleton<IChatClient>(FakeChatClient.Answering(FakeChatClient.ValidAnswer)));
        using var client = harness.CreateClient();
        var token = await StudentTokenAsync(client);
        var before = await StoreFingerprintAsync();

        await HintsAsync(client, token, Scenarios.E1);
        await HintsAsync(client, token, Scenarios.E3);

        Assert.Equal(before, await StoreFingerprintAsync());
        Assert.Equal(0L, await ScalarAsync("select count(*) from Work"));
    }

    /// <summary>
    /// REQ-IA-08 · A-18 no resuelve el puerto de pistas: con un doble que lanza si lo llaman,
    /// A-18 responde igual y su batería no cambia.
    /// </summary>
    [Fact]
    public async Task InterpretationsEndpoint_DoesNotUseHintPort()
    {
        using var harness = new AiDataServiceHarness(_storePath, services =>
            services.AddScoped<IObservationHintWriter, ExplodingWriter>());
        using var client = harness.CreateClient();
        var token = await StudentTokenAsync(client);

        using var response = await client.SendAsync(Authorized(HttpMethod.Post, "/v1/interpretaciones", token, new WorkInterpretationRequest(Scenarios.E1)));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, (await response.Content.ReadFromJsonAsync<WorkInterpretationResponse>())!.Observations.Count);
    }

    [Fact]
    public async Task AnEmptyTextGetsTheSameErrorAsA18()
    {
        using var harness = new AiDataServiceHarness(_storePath);
        using var client = harness.CreateClient();
        var token = await StudentTokenAsync(client);

        using var a18 = await client.SendAsync(Authorized(HttpMethod.Post, "/v1/interpretaciones", token, new WorkInterpretationRequest("  ")));
        using var hints = await client.SendAsync(Authorized(HttpMethod.Post, HintsRoute, token, new WorkHintsRequest("  ")));

        Assert.Equal(a18.StatusCode, hints.StatusCode);
        Assert.Equal(await a18.Content.ReadFromJsonAsync<Contracts.Errors.ErrorResponse>() is { } e ? e.Code : null,
            await hints.Content.ReadFromJsonAsync<Contracts.Errors.ErrorResponse>() is { } h ? h.Code : null);
    }

    [Fact]
    public async Task TheRouteNeedsASignedStudentAccess()
    {
        using var harness = new AiDataServiceHarness(_storePath);
        using var client = harness.CreateClient();
        var administrator = await AdministratorTokenAsync(client);

        using var anonymous = await client.PostAsJsonAsync(HintsRoute, new WorkHintsRequest(Scenarios.E3));
        using var asAdministrator = await client.SendAsync(Authorized(HttpMethod.Post, HintsRoute, administrator, new WorkHintsRequest(Scenarios.E3)));

        Assert.Equal(HttpStatusCode.Unauthorized, anonymous.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, asAdministrator.StatusCode);
    }

    /// <summary>
    /// REQ-IA-11 · Con el servicio en `Production` —como se despliega— la traza dice la categoría y
    /// la latencia, y no trae ni el mensaje al modelo ni su respuesta.
    /// </summary>
    [Fact]
    public async Task HintTelemetry_ContentOnlyInDevelopment_ProductionCarriesNoMessages()
    {
        using var harness = new AiDataServiceHarness(
            _storePath,
            services => services.AddSingleton<IChatClient>(FakeChatClient.Answering(FakeChatClient.ValidAnswer)),
            environment: "Production");
        using var client = harness.CreateClient();
        var token = await StudentTokenAsync(client);

        await HintsAsync(client, token, Scenarios.E3);

        var trace = string.Join("\n", harness.Logs.Lines);
        Assert.Contains("Pistas: Ok", trace, StringComparison.Ordinal);
        Assert.DoesNotContain("Valor declarado por el programa", trace, StringComparison.Ordinal);
        Assert.DoesNotContain("Mira como calcula", trace, StringComparison.Ordinal);
        Assert.DoesNotContain("\"Tipo\"", trace, StringComparison.Ordinal);
    }

    private static async Task<WorkHintsResponse> HintsAsync(HttpClient client, string token, string text)
    {
        using var response = await client.SendAsync(Authorized(HttpMethod.Post, HintsRoute, token, new WorkHintsRequest(text)));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return (await response.Content.ReadFromJsonAsync<WorkHintsResponse>())!;
    }

    private static async Task<string> AdministratorTokenAsync(HttpClient client)
    {
        using var setup = await client.PostAsJsonAsync("/v1/cuentas/administrador",
            new AdministratorSetupRequest(AdministratorEmail, "Ana", "Rossi", AdministratorPassword));
        Assert.Equal(HttpStatusCode.Created, setup.StatusCode);

        return await TokenAsync(client, AdministratorEmail, AdministratorPassword);
    }

    private static async Task<string> StudentTokenAsync(HttpClient client)
    {
        var administrator = await AdministratorTokenAsync(client);

        using var registration = await client.PostAsJsonAsync("/v1/cuentas", new AccountRegistrationRequest(StudentEmail, "Ana", "Diaz"));
        var id = (await registration.Content.ReadFromJsonAsync<AccountRegistrationResponse>())!.AccountId;

        using var enabled = await client.SendAsync(Authorized(HttpMethod.Post, $"/v1/cuentas/{id}/situacion", administrator,
            new AccountStatusChangeRequest(id, nameof(AccountStatus.Enabled))));
        var provisional = (await enabled.Content.ReadFromJsonAsync<AccountStatusChangeResponse>())!.ProvisionalPassword!;

        const string Chosen = "la-que-eligio-la-alumna";
        using var change = await client.PostAsJsonAsync("/v1/cuenta/contrasena", new OwnPasswordChangeRequest(provisional, Chosen, StudentEmail));
        Assert.Equal(HttpStatusCode.OK, change.StatusCode);

        return await TokenAsync(client, StudentEmail, Chosen);
    }

    private static async Task<string> TokenAsync(HttpClient client, string email, string password)
    {
        using var response = await client.PostAsJsonAsync("/v1/auth/token", new CredentialExchangeRequest(email, password));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return (await response.Content.ReadFromJsonAsync<SessionResponse>())!.AccessToken;
    }

    private static HttpRequestMessage Authorized(HttpMethod method, string route, string token, object body) =>
        new(method, route)
        {
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
            Content = JsonContent.Create(body, body.GetType()),
        };

    /// <summary>Cuántas filas hay en cada tabla del almacén: si algo se escribiera, cambiaría.</summary>
    private async Task<string> StoreFingerprintAsync()
    {
        using var connection = new SqliteConnection($"Data Source={_storePath};Pooling=False");
        await connection.OpenAsync();
        using var tables = connection.CreateCommand();
        tables.CommandText = "select name from sqlite_master where type = 'table' order by name";
        var names = new List<string>();
        await using (var reader = await tables.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                names.Add(reader.GetString(0));
            }
        }

        var counts = new List<string>();
        foreach (var name in names)
        {
            using var count = connection.CreateCommand();
            count.CommandText = $"select count(*) from \"{name}\"";
            counts.Add($"{name}={await count.ExecuteScalarAsync()}");
        }

        return string.Join(";", counts);
    }

    private async Task<object?> ScalarAsync(string sql)
    {
        using var connection = new SqliteConnection($"Data Source={_storePath};Pooling=False");
        await connection.OpenAsync();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        return await command.ExecuteScalarAsync();
    }

    private sealed class ExplodingWriter : IObservationHintWriter
    {
        public Task<HintResult> WriteAsync(ObservationHintRequest request, CancellationToken cancellationToken = default) =>
            throw new InvalidOperationException("A-18 no tiene que llamar al redactor de pistas.");

        public Hints TemplateFor(ObservationHintRequest request) =>
            throw new InvalidOperationException("A-18 no tiene que llamar al redactor de pistas.");
    }
}
