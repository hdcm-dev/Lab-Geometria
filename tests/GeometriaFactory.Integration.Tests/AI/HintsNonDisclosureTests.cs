using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Works;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// PRUEBA NEGATIVA DE NO PUBLICACIÓN (expediente 0004, orden 5; el par de Geometría de K-7).
///
/// `POST /v1/interpretaciones/pistas` no expone hacia afuera el prompt de sistema, la
/// configuración del modelo (nombre, opciones de Ollama, permanencia) ni la dirección del
/// servidor de modelos: ni en la respuesta correcta, ni en los errores, ni con la capa apagada,
/// ni al anónimo o a un papel que no corresponde. La dirección y el modelo son CANARIOS
/// inventados para la prueba (dominio `.invalid`, RFC 2606): no se usa ni se escribe la dirección
/// real del servidor de IA (R-PRIV).
///
/// EL PAR ROJO/VERDE está en <see cref="Detector_FlagsALeakingBody_AndAcceptsACleanOne"/>: el
/// mismo detector que juzga las respuestas reales marca un cuerpo fabricado con fuga (rojo) y
/// acepta uno limpio (verde). Si el detector dejara de detectar, esa prueba falla primero.
/// </summary>
public sealed class HintsNonDisclosureTests : IDisposable
{
    private const string HintsRoute = "/v1/interpretaciones/pistas";
    private const string CanaryHost = "canario-servidor-ia.invalid";
    private const string CanaryPort = "47311";
    private const string CanaryEndpoint = "http://" + CanaryHost + ":" + CanaryPort;
    private const string CanaryModel = "modelo-canario-geo:9b";
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";
    private const string StudentEmail = "alumna@frre.utn.edu.ar";

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();

    public void Dispose() => DataServiceHarness.DiscardStore(_storePath);

    private static readonly Dictionary<string, string?> Configured = new()
    {
        ["AI:Endpoint"] = CanaryEndpoint,
        ["AI:Model"] = CanaryModel,
        ["AI:HintsEnabled"] = "true",
    };

    /// <summary>Lo que nunca puede aparecer en un cuerpo de respuesta de la ruta.</summary>
    internal static IReadOnlyList<string> Forbidden { get; } = BuildForbidden();

    private static List<string> BuildForbidden()
    {
        var list = new List<string>
        {
            CanaryHost, CanaryPort, CanaryModel, "modelo-canario",
            "num_ctx", "num_predict", "keep_alive", "keepalive", "\"think\"", "temperature", "Endpoint",
            "ollama",
        };

        // Cada cláusula del prompt de sistema de más de 20 caracteres, tal como está en el código.
        list.AddRange(ModelObservationHintWriter.SystemPrompt
            .Split(['.', ';', ':'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Where(s => s.Length > 20));

        return list;
    }

    internal static IReadOnlyList<string> Leaks(string body) =>
        [.. Forbidden.Where(f => body.Contains(f, StringComparison.OrdinalIgnoreCase))];

    [Fact]
    public void Detector_FlagsALeakingBody_AndAcceptsACleanOne()
    {
        var leaking = $$"""{"error":"no se pudo llegar a {{CanaryEndpoint}}","model":"{{CanaryModel}}","prompt":"{{ModelObservationHintWriter.SystemPrompt}}","options":{"num_ctx":4096} }""";
        var clean = """{"observations":[],"hints":[{"observationIndex":0,"level1":"Mira el area.","level2":"Suma las caras.","level3":"Contalas.","origin":"Template"}]}""";

        Assert.True(Leaks(leaking).Count >= 5, "rojo: el detector tiene que marcar la fuga fabricada");
        Assert.Empty(Leaks(clean));
    }

    public static TheoryData<string, string> Scripts => new()
    {
        { "modelo-valido", "Development" },
        { "modelo-valido", "Production" },
        { "modelo-lanza", "Development" },
        { "modelo-lanza", "Production" },
        { "modelo-repite-el-prompt", "Development" },
        { "modelo-repite-el-prompt", "Production" },
        { "servidor-inalcanzable", "Development" },
        { "servidor-inalcanzable", "Production" },
    };

    /// <summary>Respuesta correcta, error del modelo, intento de eco del prompt y servidor caído.</summary>
    [Theory]
    [MemberData(nameof(Scripts))]
    public async Task HintsResponse_NeverCarriesPromptConfigurationOrAddress(string script, string environment)
    {
        Action<IServiceCollection>? services = script switch
        {
            "modelo-valido" => s => s.AddSingleton<IChatClient>(FakeChatClient.Answering(FakeChatClient.ValidAnswer)),
            "modelo-lanza" => s => s.AddSingleton<IChatClient>(new FakeChatClient((_, _) =>
                throw new HttpRequestException($"connection refused {CanaryEndpoint} model {CanaryModel}"))),
            "modelo-repite-el-prompt" => s => s.AddSingleton<IChatClient>(FakeChatClient.Answering(
                $$"""{"nivel1":"{{ModelObservationHintWriter.SystemPrompt}}","nivel2":"{{CanaryEndpoint}}","nivel3":"{{CanaryModel}}"}""")),
            _ => null, // la composición real, apuntando al canario inalcanzable
        };

        using var harness = new AiDataServiceHarness(_storePath, services, environment, Configured);
        using var client = harness.CreateClient();
        var token = await StudentTokenAsync(client);

        foreach (var text in new[] { Scenarios.E1, Scenarios.E3, "esto no es json", "  " })
        {
            using var response = await client.SendAsync(Authorized(HintsRoute, token, new WorkHintsRequest(text)));
            var body = await Everything(response);

            Assert.True(response.StatusCode is HttpStatusCode.OK or HttpStatusCode.BadRequest, $"{script}: {response.StatusCode}");
            Assert.Empty(Leaks(body));
        }
    }

    /// <summary>
    /// Con la capa apagada (sin dirección ni modelo, el valor por omisión de main) el estado
    /// esperado es 200 con plantillas (D-FLAG), y el cuerpo no dice nada de la configuración.
    /// </summary>
    [Fact]
    public async Task WithTheLayerOff_TheExpectedStateCarriesNothing()
    {
        using var harness = new AiDataServiceHarness(_storePath);
        using var client = harness.CreateClient();
        var token = await StudentTokenAsync(client);

        using var response = await client.SendAsync(Authorized(HintsRoute, token, new WorkHintsRequest(Scenarios.E1)));
        var body = await Everything(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.All((await response.Content.ReadFromJsonAsync<WorkHintsResponse>())!.Hints,
            h => Assert.Equal("Template", h.Origin));
        Assert.Empty(Leaks(body));
    }

    /// <summary>Lo restringido no sale al anónimo ni al administrador: 401/403 sin pistas ni configuración.</summary>
    [Fact]
    public async Task RestrictedCallers_GetNoHintsAndNoConfiguration()
    {
        using var harness = new AiDataServiceHarness(_storePath,
            s => s.AddSingleton<IChatClient>(FakeChatClient.Answering(FakeChatClient.ValidAnswer)), settings: Configured);
        using var client = harness.CreateClient();
        var administrator = await AdministratorTokenAsync(client);

        using var anonymous = await client.PostAsJsonAsync(HintsRoute, new WorkHintsRequest(Scenarios.E3));
        using var forged = await client.SendAsync(Authorized(HintsRoute, "no-es-un-acceso-firmado", new WorkHintsRequest(Scenarios.E3)));
        using var asAdministrator = await client.SendAsync(Authorized(HintsRoute, administrator, new WorkHintsRequest(Scenarios.E3)));

        Assert.Equal(HttpStatusCode.Unauthorized, anonymous.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, forged.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, asAdministrator.StatusCode);

        foreach (var response in new[] { anonymous, forged, asAdministrator })
        {
            var body = await Everything(response);
            Assert.Empty(Leaks(body));
            Assert.DoesNotContain("Mira como calcula", body, StringComparison.Ordinal);
            Assert.DoesNotContain("level1", body, StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>El documento público del contrato tampoco publica nada de la configuración.</summary>
    [Fact]
    public async Task TheContractDocument_PublishesNoConfiguration()
    {
        using var harness = new AiDataServiceHarness(_storePath, settings: Configured);
        using var client = harness.CreateClient();

        using var response = await client.GetAsync("/openapi/v1.json");
        var body = await Everything(response);

        Assert.DoesNotContain(CanaryHost, body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(CanaryModel, body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Sos un asistente", body, StringComparison.Ordinal);
    }

    /// <summary>Cuerpo más encabezados: una fuga en un encabezado también cuenta.</summary>
    private static async Task<string> Everything(HttpResponseMessage response) =>
        string.Join("\n", response.Headers.Concat(response.Content.Headers).Select(h => $"{h.Key}: {string.Join(",", h.Value)}"))
        + "\n" + await response.Content.ReadAsStringAsync();

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

        using var enabled = await client.SendAsync(Authorized($"/v1/cuentas/{id}/situacion", administrator,
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

    private static HttpRequestMessage Authorized(string route, string token, object body) =>
        new(HttpMethod.Post, route)
        {
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
            Content = JsonContent.Create(body, body.GetType()),
        };
}
