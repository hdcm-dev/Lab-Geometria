using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GeometriaFactory.Api.Composition;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Contracts.Works;
using GeometriaFactory.Domain.Values;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// El límite de tasa del contrato (`BT-00029`): por persona autenticada, por dirección de origen sin
/// acceso, con la cuota propia del canje, el `429` con `Retry-After`, y `/salud` afuera. Y desde
/// `ADR-00011`, los fallos por cuenta y los topes por origen que dejan entrar a una comisión detrás
/// de una sola dirección.
/// </summary>
/// <remarks>
/// SE MIDE CON LOS UMBRALES POR OMISIÓN Y NO CON UNOS BAJADOS PARA LA PRUEBA. Las cifras que estas
/// pruebas exceden —60 por persona, 1200 por origen, 600 canjes por origen, 10 fallos por cuenta— son las que
/// `Contratos-REST.md` §4.1 declara y las que `appsettings.json` trae; una batería que bajara los
/// umbrales a tres verificaría que el limitador existe, no que el producto tiene la cuota que dice
/// tener. Y acá eso importa más que en ningún lado: el defecto que corrigió `ADR-00011` fue una cifra
/// por origen demasiado baja para una clase, y sólo una prueba con la cifra real lo ve. El servidor en
/// memoria responde en milisegundos, así que las mil doscientas peticiones caben de sobra en una
/// ventana de un minuto.
///
/// LA DIRECCIÓN DEL ZÓCALO SE SIMULA, PORQUE EL SERVIDOR EN MEMORIA NO TIENE NINGUNA. `TestServer`
/// deja `RemoteIpAddress` en nulo, y sin dirección no hay forma de mostrar que dos orígenes tienen
/// cuotas separadas ni que `X-Forwarded-For` se honra sólo desde un proxy conocido. El harness de
/// abajo inserta, **antes de todo lo demás**, un intermediario que toma la dirección de una cabecera
/// de prueba y la pone donde Kestrel la pondría. Es lo único que se sustituye, y no es un servicio
/// del producto: es el zócalo.
///
/// POR QUÉ LOS ACCESOS SE OBTIENEN ANTES DE CONTAR. Los canjes de la preparación también gastan cuota
/// —la del canje, por origen—, y una prueba que los mezclara con la medición contaría mal. Cada
/// prueba prepara su mundo primero y recién después empieza a contar sobre otra partición.
/// </remarks>
public sealed class RateLimitingTests : IDisposable
{
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";
    private const string SetupOrigin = "192.0.2.1";
    private const string StudentPassword = "la-que-eligio-la-alumna";

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly List<OriginAwareHarness> _harnesses = [];

    public void Dispose()
    {
        foreach (var harness in _harnesses)
        {
            harness.Dispose();
        }

        DataServiceHarness.DiscardStore(_storePath);
    }

    // ---------------------------------------------------- los umbrales, tal como se declaran ---

    /// <summary>
    /// Los valores por omisión son los que `Contratos-REST.md` §4.1 y `appsettings.json` dicen. Si
    /// alguien cambia una cifra en un lado y no en los otros, falla acá.
    /// </summary>
    [Fact]
    public void TheDefaultThresholdsAreTheOnesTheContractDeclares()
    {
        var defaults = new RateLimitingOptions();

        Assert.Equal(60, defaults.WindowSeconds);
        Assert.Equal(60, defaults.PermitsPerPerson);
        Assert.Equal(1200, defaults.PermitsPerAddress);
        Assert.Equal(600, defaults.CredentialExchangePermitsPerAddress);
        Assert.Equal(10, defaults.CredentialFailuresPerAccount);
        Assert.Equal(900, defaults.CredentialFailureWindowSeconds);
    }

    /// <summary>
    /// Un umbral en cero no es «sin límite»: detiene el arranque nombrando la llave, como la clave
    /// de firma. Y una red de proxy que no se puede leer, también.
    /// </summary>
    [Theory]
    [InlineData("RateLimiting:PermitsPerPerson", "0")]
    [InlineData("RateLimiting:PermitsPerAddress", "-5")]
    [InlineData("RateLimiting:CredentialExchangePermitsPerAddress", "0")]
    [InlineData("RateLimiting:WindowSeconds", "0")]
    [InlineData("RateLimiting:CredentialFailuresPerAccount", "0")]
    [InlineData("RateLimiting:CredentialFailureWindowSeconds", "-1")]
    public void AThresholdBelowOneStopsTheStartupNamingTheKey(string key, string value)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { [key] = value })
            .Build();

        var failure = Assert.Throws<InvalidOperationException>(
            () => new ServiceCollection().AddContractRateLimiting(configuration));

        Assert.Contains(key, failure.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void AKnownNetworkThatIsNotCidrStopsTheStartupNamingTheKey()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [$"{ContractRateLimiting.KnownNetworksSetting}:0"] = "el-tunel",
            })
            .Build();

        // La opción se valida al construirse, que es cuando el marco la pide por primera vez.
        var services = new ServiceCollection().AddContractRateLimiting(configuration);
        using var provider = services.BuildServiceProvider();

        var failure = Assert.Throws<InvalidOperationException>(
            () => provider.GetRequiredService<Microsoft.Extensions.Options.IOptions<ForwardedHeadersOptions>>().Value);

        Assert.Contains(ContractRateLimiting.KnownNetworksSetting, failure.Message, StringComparison.Ordinal);
        Assert.Contains("el-tunel", failure.Message, StringComparison.Ordinal);
    }

    // ------------------------------------------------------------------- por persona ---------

    /// <summary>
    /// Criterio 1 y 2 de la ficha, por persona: la sexagésima primera petición de la misma persona
    /// en la ventana recibe `429` con `Retry-After`, sin cuerpo; otra persona sigue en `200`; y la
    /// misma persona desde otra dirección sigue en `429`, porque la partición es el reclamo de
    /// identidad y no el zócalo.
    /// </summary>
    [Fact]
    public async Task APersonWhoExceedsTheQuotaGets429WithRetryAfterAndNobodyElseDoes()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        var world = await WorldAsync(client, "primera@frre.utn.edu.ar", "segunda@frre.utn.edu.ar");
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        for (var i = 0; i < options.PermitsPerPerson; i++)
        {
            using var allowed = await client.SendAsync(Request(HttpMethod.Get, "/v1/trabajos", "198.51.100.1", world.FirstToken));
            Assert.Equal(HttpStatusCode.OK, allowed.StatusCode);
        }

        using var rejected = await client.SendAsync(Request(HttpMethod.Get, "/v1/trabajos", "198.51.100.1", world.FirstToken));
        Assert.Equal(HttpStatusCode.TooManyRequests, rejected.StatusCode);
        AssertRetryAfter(rejected, options.WindowSeconds);
        Assert.Empty(await rejected.Content.ReadAsStringAsync());

        using var otherPerson = await client.SendAsync(Request(HttpMethod.Get, "/v1/trabajos", "198.51.100.1", world.SecondToken));
        Assert.Equal(HttpStatusCode.OK, otherPerson.StatusCode);

        using var samePersonElsewhere = await client.SendAsync(Request(HttpMethod.Get, "/v1/trabajos", "198.51.100.2", world.FirstToken));
        Assert.Equal(HttpStatusCode.TooManyRequests, samePersonElsewhere.StatusCode);
    }

    /// <summary>
    /// Criterio 3 de la ficha: la carga sintética que el límite deja pasar —la cuota entera de una
    /// persona, en ráfaga concurrente y toda de escritura— llega al único escritor y **cada
    /// escritura queda**: ninguna respuesta es `5xx` y el almacén tiene exactamente las sesenta.
    /// La sexagésima primera no llega al escritor.
    /// </summary>
    [Fact]
    public async Task ABurstOfTheWholePersonQuotaOfWritesLandsEntirelyOnTheSingleWriter()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        var world = await WorldAsync(client, "escritora@frre.utn.edu.ar", "otra@frre.utn.edu.ar");
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        var burst = Enumerable.Range(1, options.PermitsPerPerson).Select(async i =>
        {
            using var response = await client.SendAsync(Request(
                HttpMethod.Post, "/v1/trabajos", "198.51.100.1", world.FirstToken,
                new WorkSubmissionRequest(null, $"Entrega {i}", "2026-09-12", null, Scenarios.E5)));
            return response.StatusCode;
        });

        var statuses = await Task.WhenAll(burst);

        Assert.All(statuses, status => Assert.Equal(HttpStatusCode.Created, status));

        using var oneMore = await client.SendAsync(Request(
            HttpMethod.Post, "/v1/trabajos", "198.51.100.1", world.FirstToken,
            new WorkSubmissionRequest(null, "Entrega de más", "2026-09-12", null, Scenarios.E5)));
        Assert.Equal(HttpStatusCode.TooManyRequests, oneMore.StatusCode);

        using var scope = harness.Services.CreateScope();
        var works = scope.ServiceProvider.GetRequiredService<IWorkRepository>();
        Assert.Equal(options.PermitsPerPerson, (await works.ListOwnedByAsync(world.FirstId)).Count);
    }

    // ------------------------------------------------------------------- por origen ----------

    /// <summary>
    /// Sin acceso, la partición es la dirección: la petición ciento veintiuna de un origen recibe
    /// `429`, y otro origen sigue en `200`.
    /// </summary>
    [Fact]
    public async Task AnOriginWithoutAccessThatExceedsTheQuotaGets429AndAnotherOriginDoesNot()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        for (var i = 0; i < options.PermitsPerAddress; i++)
        {
            using var allowed = await client.SendAsync(Request(HttpMethod.Get, "/v1/aprovisionamiento", "203.0.113.10"));
            Assert.Equal(HttpStatusCode.OK, allowed.StatusCode);
        }

        using var rejected = await client.SendAsync(Request(HttpMethod.Get, "/v1/aprovisionamiento", "203.0.113.10"));
        Assert.Equal(HttpStatusCode.TooManyRequests, rejected.StatusCode);
        AssertRetryAfter(rejected, options.WindowSeconds);

        using var otherOrigin = await client.SendAsync(Request(HttpMethod.Get, "/v1/aprovisionamiento", "203.0.113.11"));
        Assert.Equal(HttpStatusCode.OK, otherOrigin.StatusCode);
    }

    /// <summary>
    /// El limitador corre antes de autorizar: quien tantea la guardia sin acceso gasta la cuota
    /// de su origen y, agotada, recibe `429` y no otro `401`.
    /// </summary>
    [Fact]
    public async Task ARequestRejectedByTheGuardAlsoSpendsTheOriginQuota()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        for (var i = 0; i < options.PermitsPerAddress; i++)
        {
            using var refused = await client.SendAsync(Request(HttpMethod.Get, "/v1/trabajos", "203.0.113.12"));
            Assert.Equal(HttpStatusCode.Unauthorized, refused.StatusCode);
        }

        using var limited = await client.SendAsync(Request(HttpMethod.Get, "/v1/trabajos", "203.0.113.12"));
        Assert.Equal(HttpStatusCode.TooManyRequests, limited.StatusCode);
    }

    /// <summary>
    /// El defecto de `BT-00029`, que `ADR-00011` corrige: **una comisión entera detrás de un origen**
    /// —el front, o el NAT de la facultad— canjea cuentas distintas y ninguna recibe `429` hasta el
    /// tope holgado del origen. Con la cuota de treinta por origen, el trigésimo primer alumno de la
    /// clase se quedaba afuera. Recién el intento que excede el tope recibe `429`, y otro origen sigue
    /// en `401`.
    /// </summary>
    [Fact]
    public async Task AWholeCommissionBehindOneOriginIsNotLimitedUntilTheGenerousOriginCap()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        // La clase más grande que el tope tiene que dejar entrar: trescientos alumnos con un error
        // de tipeo cada uno (`RateLimitingOptions.CredentialExchangePermitsPerAddress`).
        Assert.True(options.CredentialExchangePermitsPerAddress >= 600);

        for (var i = 0; i < options.CredentialExchangePermitsPerAddress; i++)
        {
            using var refused = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", "203.0.113.20",
                body: new CredentialExchangeRequest($"alumno{i}@frre.utn.edu.ar", "no-es")));
            Assert.Equal(HttpStatusCode.Unauthorized, refused.StatusCode);
        }

        using var limited = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", "203.0.113.20",
            body: new CredentialExchangeRequest("uno-mas@frre.utn.edu.ar", "no-es")));
        Assert.Equal(HttpStatusCode.TooManyRequests, limited.StatusCode);
        AssertRetryAfter(limited, options.WindowSeconds);

        using var elsewhere = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", "203.0.113.21",
            body: new CredentialExchangeRequest("uno-mas@frre.utn.edu.ar", "no-es")));
        Assert.Equal(HttpStatusCode.Unauthorized, elsewhere.StatusCode);
    }

    /// <summary>
    /// Y la clase entra de verdad: cuentas reales, habilitadas, canjeando con su contraseña correcta
    /// desde el mismo origen, **más que la cuota vieja de treinta**, todas en `200`.
    /// </summary>
    [Fact]
    public async Task RealAccountsBehindOneOriginSignInBeyondTheOldQuotaOfThirty()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        await WorldAsync(client, "primera@frre.utn.edu.ar", "segunda@frre.utn.edu.ar");

        var statuses = new List<HttpStatusCode>();

        for (var i = 0; i < 40; i++)
        {
            var email = i % 2 == 0 ? "primera@frre.utn.edu.ar" : "segunda@frre.utn.edu.ar";
            using var response = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", "172.23.0.2",
                body: new CredentialExchangeRequest(email, StudentPassword)));
            statuses.Add(response.StatusCode);
        }

        Assert.All(statuses, status => Assert.Equal(HttpStatusCode.OK, status));
    }

    /// <summary>
    /// La fuerza bruta sobre **una cuenta** se corta aunque cada intento venga de otro origen: la
    /// partición es el correo normalizado (`ADR-06003`), así que mayúsculas y espacios no la esquivan.
    /// Después del décimo fallo, **ni la contraseña correcta** entra —recibe `429`, sin cuerpo y con
    /// `Retry-After`—; otra cuenta, desde el mismo origen, sigue entrando.
    /// </summary>
    [Fact]
    public async Task TheSameAccountIsLimitedAfterItsFailuresFromAnyOrigin()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        await WorldAsync(client, "primera@frre.utn.edu.ar", "segunda@frre.utn.edu.ar");
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        for (var i = 0; i < options.CredentialFailuresPerAccount; i++)
        {
            var written = i % 2 == 0 ? "primera@frre.utn.edu.ar" : "  PRIMERA@frre.UTN.edu.ar ";
            using var refused = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", $"198.51.100.{i + 1}",
                body: new CredentialExchangeRequest(written, "no-es")));
            Assert.Equal(HttpStatusCode.Unauthorized, refused.StatusCode);
        }

        using var limited = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", "198.51.100.200",
            body: new CredentialExchangeRequest("primera@frre.utn.edu.ar", StudentPassword)));
        Assert.Equal(HttpStatusCode.TooManyRequests, limited.StatusCode);
        AssertRetryAfter(limited, options.CredentialFailureWindowSeconds);
        Assert.Empty(await limited.Content.ReadAsStringAsync());

        using var otherAccount = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", "198.51.100.200",
            body: new CredentialExchangeRequest("segunda@frre.utn.edu.ar", StudentPassword)));
        Assert.Equal(HttpStatusCode.OK, otherAccount.StatusCode);
    }

    /// <summary>
    /// R-03 — EL RESETEO DEL DOCENTE LIBERA LA CUOTA DE INTENTOS FALLIDOS DE LA CUENTA. Una cuenta
    /// que agotó sus intentos recibe `429` aunque traiga la contraseña correcta; cuando el docente
    /// le resetea la contraseña, el ingreso con la provisoria **ya no se limita**: llega al cambio
    /// obligado. Sin esto, la mitigación que `ADR-00011` §6 declara —«el docente puede resetear»—
    /// no existía, y el único remedio era reiniciar el servicio (mesa
    /// `SDD/Docs/Audit/Mesa-2026-09-14.md`).
    /// </summary>
    [Fact]
    public async Task TheAdministratorResetReleasesTheAccountFromItsFailedAttemptQuota()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        var world = await WorldAsync(client, "primera@frre.utn.edu.ar", "segunda@frre.utn.edu.ar");
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();
        var administrator = await TokenAsync(client, AdministratorEmail, AdministratorPassword);

        for (var i = 0; i < options.CredentialFailuresPerAccount; i++)
        {
            using var refused = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", $"198.51.100.{i + 1}",
                body: new CredentialExchangeRequest("primera@frre.utn.edu.ar", "no-es")));
            Assert.Equal(HttpStatusCode.Unauthorized, refused.StatusCode);
        }

        using var limited = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", "198.51.100.200",
            body: new CredentialExchangeRequest("primera@frre.utn.edu.ar", StudentPassword)));
        Assert.Equal(HttpStatusCode.TooManyRequests, limited.StatusCode);

        // EL DOCENTE RESETEA LA CONTRASEÑA DE LA CUENTA LIMITADA.
        using var reset = await client.SendAsync(Request(
            HttpMethod.Post, $"/v1/cuentas/{world.FirstId}/reseteo-de-contrasena", SetupOrigin, administrator));
        Assert.Equal(HttpStatusCode.OK, reset.StatusCode);
        var provisional = (await reset.Content.ReadFromJsonAsync<PasswordResetResponse>())!.ProvisionalPassword!;

        // CON LA PROVISORIA, EL INGRESO YA NO SE LIMITA: la credencial se reconoce y lleva al
        // cambio obligado, que es lo que el reseteo produce.
        using var afterTheReset = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", "198.51.100.201",
            body: new CredentialExchangeRequest("primera@frre.utn.edu.ar", provisional)));
        Assert.NotEqual(HttpStatusCode.TooManyRequests, afterTheReset.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, afterTheReset.StatusCode);
        Assert.Contains(ErrorCode.PasswordChangeRequired, await afterTheReset.Content.ReadAsStringAsync(), StringComparison.Ordinal);
    }

    /// <summary>
    /// El `429` por cuenta **no dice si la cuenta existe**: una cuenta registrada y un correo que
    /// nadie registró reciben el rechazo en el mismo intento, con el mismo código, la misma cabecera
    /// y el mismo cuerpo vacío.
    /// </summary>
    [Fact]
    public async Task TheAccountRefusalIsIndistinguishableForAnExistingAndAnUnknownAccount()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        await WorldAsync(client, "primera@frre.utn.edu.ar", "segunda@frre.utn.edu.ar");
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        var existing = await ExhaustAsync(client, "primera@frre.utn.edu.ar", "198.51.100.10", options.CredentialFailuresPerAccount + 1);
        var unknown = await ExhaustAsync(client, "nadie@frre.utn.edu.ar", "198.51.100.11", options.CredentialFailuresPerAccount + 1);

        Assert.Equal(existing, unknown);
        Assert.Equal(options.CredentialFailuresPerAccount, existing.FirstLimitedAttempt);
        Assert.Equal((int)HttpStatusCode.TooManyRequests, existing.Status);
        Assert.True(existing.HasRetryAfter);
        Assert.Equal(string.Empty, existing.Body);
    }

    /// <summary>
    /// Los ingresos correctos **no gastan** la cuota por cuenta: una persona —o un guion propio del
    /// docente— que canjea bien muchas veces no se bloquea, y después sigue teniendo todos sus fallos.
    /// </summary>
    [Fact]
    public async Task SuccessfulSignInsDoNotSpendTheAccountQuota()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        await WorldAsync(client, "primera@frre.utn.edu.ar", "segunda@frre.utn.edu.ar");
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        for (var i = 0; i < options.CredentialFailuresPerAccount * 2; i++)
        {
            using var allowed = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", "198.51.100.20",
                body: new CredentialExchangeRequest("primera@frre.utn.edu.ar", StudentPassword)));
            Assert.Equal(HttpStatusCode.OK, allowed.StatusCode);
        }

        var outcome = await ExhaustAsync(client, "primera@frre.utn.edu.ar", "198.51.100.20", options.CredentialFailuresPerAccount + 1);
        Assert.Equal(options.CredentialFailuresPerAccount, outcome.FirstLimitedAttempt);
    }

    /// <summary>
    /// La forma sin sesión del cambio de contraseña propia comprueba la contraseña vigente por correo,
    /// y **comparte la cuota de la cuenta** con el canje: sin eso sería la puerta lateral para
    /// tantear lo que el canje ya no deja tantear. Diez vigentes equivocadas ahí, y el canje correcto
    /// recibe `429`.
    /// </summary>
    [Fact]
    public async Task TheCredentialFormOfThePasswordChangeSharesTheAccountQuota()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        await WorldAsync(client, "primera@frre.utn.edu.ar", "segunda@frre.utn.edu.ar");
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        for (var i = 0; i < options.CredentialFailuresPerAccount; i++)
        {
            using var refused = await client.SendAsync(Request(HttpMethod.Post, "/v1/cuenta/contrasena", $"198.51.100.{i + 30}",
                body: new OwnPasswordChangeRequest("no-es", "otra-que-elegiria", "primera@frre.utn.edu.ar")));
            Assert.Equal(HttpStatusCode.Unauthorized, refused.StatusCode);
        }

        using var limitedChange = await client.SendAsync(Request(HttpMethod.Post, "/v1/cuenta/contrasena", "198.51.100.99",
            body: new OwnPasswordChangeRequest("no-es", "otra-que-elegiria", "primera@frre.utn.edu.ar")));
        Assert.Equal(HttpStatusCode.TooManyRequests, limitedChange.StatusCode);

        using var limitedExchange = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", "198.51.100.99",
            body: new CredentialExchangeRequest("primera@frre.utn.edu.ar", StudentPassword)));
        Assert.Equal(HttpStatusCode.TooManyRequests, limitedExchange.StatusCode);
    }

    // ------------------------------------------------------------------- lo que queda afuera --

    /// <summary>
    /// `/salud` no se limita: más peticiones que cualquiera de las dos cuotas por origen, todas
    /// `200`. Es lo que sondea el `healthcheck` de las composiciones (`ADR-00007` §2 punto 3).
    /// </summary>
    [Fact]
    public async Task HealthIsNeverLimited()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        for (var i = 0; i <= options.PermitsPerAddress + options.CredentialExchangePermitsPerAddress; i++)
        {
            using var probe = await client.SendAsync(Request(HttpMethod.Get, "/salud", "203.0.113.30"));
            Assert.Equal(HttpStatusCode.OK, probe.StatusCode);
            Assert.False(probe.Headers.Contains("Retry-After"));
        }
    }

    // ------------------------------------------------------------------- X-Forwarded-For ------

    /// <summary>
    /// Con la red del proxy declarada, la dirección que cuenta es la de `X-Forwarded-For`: dos
    /// navegadores detrás del mismo túnel tienen cuotas separadas.
    /// </summary>
    [Fact]
    public async Task BehindADeclaredProxyTheForwardedAddressIsTheOneThatCounts()
    {
        var harness = Harness(new Dictionary<string, string?>
        {
            [$"{ContractRateLimiting.KnownNetworksSetting}:0"] = "172.23.0.0/16",
        });
        using var client = harness.CreateClient();
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        for (var i = 0; i < options.PermitsPerAddress; i++)
        {
            using var allowed = await client.SendAsync(Request(HttpMethod.Get, "/v1/aprovisionamiento", "172.23.0.3", forwardedFor: "198.51.100.1"));
            Assert.Equal(HttpStatusCode.OK, allowed.StatusCode);
        }

        using var rejected = await client.SendAsync(Request(HttpMethod.Get, "/v1/aprovisionamiento", "172.23.0.3", forwardedFor: "198.51.100.1"));
        Assert.Equal(HttpStatusCode.TooManyRequests, rejected.StatusCode);

        using var otherBrowser = await client.SendAsync(Request(HttpMethod.Get, "/v1/aprovisionamiento", "172.23.0.3", forwardedFor: "198.51.100.2"));
        Assert.Equal(HttpStatusCode.OK, otherBrowser.StatusCode);
    }

    /// <summary>
    /// Sin red de proxy declarada, la cabecera se ignora: un cliente no puede elegir su partición
    /// escribiendo `X-Forwarded-For`. Es el estado en que arranca el servicio si quien despliega no
    /// declara `ForwardedHeaders__KnownNetworks`, y por eso el despliegue tiene que declararla.
    /// </summary>
    [Fact]
    public async Task WithoutADeclaredProxyTheForwardedHeaderIsIgnored()
    {
        var harness = Harness();
        using var client = harness.CreateClient();
        var options = harness.Services.GetRequiredService<RateLimitingOptions>();

        for (var i = 0; i < options.PermitsPerAddress; i++)
        {
            using var allowed = await client.SendAsync(Request(HttpMethod.Get, "/v1/aprovisionamiento", "203.0.113.40", forwardedFor: $"198.51.100.{i % 200 + 1}"));
            Assert.Equal(HttpStatusCode.OK, allowed.StatusCode);
        }

        using var rejected = await client.SendAsync(Request(HttpMethod.Get, "/v1/aprovisionamiento", "203.0.113.40", forwardedFor: "198.51.100.250"));
        Assert.Equal(HttpStatusCode.TooManyRequests, rejected.StatusCode);
    }

    // ----------------------------------------------------------------------- ayudantes --------

    private OriginAwareHarness Harness(IReadOnlyDictionary<string, string?>? settings = null)
    {
        var harness = new OriginAwareHarness(_storePath, settings);
        _harnesses.Add(harness);
        return harness;
    }

    private static HttpRequestMessage Request(
        HttpMethod method,
        string route,
        string origin,
        string? token = null,
        object? body = null,
        string? forwardedFor = null)
    {
        var request = new HttpRequestMessage(method, route);
        request.Headers.Add(OriginAwareHarness.OriginHeader, origin);

        if (token is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        if (forwardedFor is not null)
        {
            request.Headers.Add("X-Forwarded-For", forwardedFor);
        }

        if (body is not null)
        {
            request.Content = JsonContent.Create(body, body.GetType());
        }

        return request;
    }

    private static void AssertRetryAfter(HttpResponseMessage response, int windowSeconds)
    {
        var retryAfter = Assert.Single(response.Headers.GetValues("Retry-After"));
        var seconds = int.Parse(retryAfter, System.Globalization.CultureInfo.InvariantCulture);

        Assert.InRange(seconds, 1, windowSeconds);
    }

    private sealed record World(string FirstToken, Guid FirstId, string SecondToken);

    private sealed record Exhaustion(int FirstLimitedAttempt, int Status, bool HasRetryAfter, string Body);

    /// <summary>
    /// Canjea con una contraseña equivocada hasta <paramref name="attempts"/> veces y devuelve en qué
    /// intento —contando desde cero— llegó el primer `429`, y cómo era esa respuesta.
    /// </summary>
    private static async Task<Exhaustion> ExhaustAsync(HttpClient client, string email, string origin, int attempts)
    {
        for (var i = 0; i < attempts; i++)
        {
            using var response = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", origin,
                body: new CredentialExchangeRequest(email, "no-es")));

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                return new Exhaustion(i, (int)response.StatusCode, response.Headers.Contains("Retry-After"),
                    await response.Content.ReadAsStringAsync());
            }

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        return new Exhaustion(-1, 0, false, string.Empty);
    }

    /// <summary>
    /// Un administrador y dos alumnas habilitadas con acceso, preparados desde un origen que ninguna
    /// prueba usa después para contar. Diez peticiones anónimas y cuatro canjes: lejos de toda cuota.
    /// </summary>
    private static async Task<World> WorldAsync(HttpClient client, string firstEmail, string secondEmail)
    {
        using var setup = await client.SendAsync(Request(HttpMethod.Post, "/v1/cuentas/administrador", SetupOrigin,
            body: new AdministratorSetupRequest(AdministratorEmail, "Fernando", "Filipuzzi", AdministratorPassword)));
        Assert.Equal(HttpStatusCode.Created, setup.StatusCode);

        var administrator = await TokenAsync(client, AdministratorEmail, AdministratorPassword);
        var first = await EnrolAsync(client, administrator, firstEmail);
        var second = await EnrolAsync(client, administrator, secondEmail);

        return new World(first.Token, first.Id, second.Token);
    }

    private static async Task<(Guid Id, string Token)> EnrolAsync(HttpClient client, string administratorToken, string email)
    {
        using var registration = await client.SendAsync(Request(HttpMethod.Post, "/v1/cuentas", SetupOrigin,
            body: new AccountRegistrationRequest(email, "Ana", "Diaz")));
        Assert.Equal(HttpStatusCode.Created, registration.StatusCode);
        var id = (await registration.Content.ReadFromJsonAsync<AccountRegistrationResponse>())!.AccountId;

        using var enabled = await client.SendAsync(Request(HttpMethod.Post, $"/v1/cuentas/{id}/situacion", SetupOrigin, administratorToken,
            new AccountStatusChangeRequest(id, nameof(AccountStatus.Enabled))));
        Assert.Equal(HttpStatusCode.OK, enabled.StatusCode);
        var provisional = (await enabled.Content.ReadFromJsonAsync<AccountStatusChangeResponse>())!.ProvisionalPassword!;

        const string Chosen = StudentPassword;
        using var change = await client.SendAsync(Request(HttpMethod.Post, "/v1/cuenta/contrasena", SetupOrigin,
            body: new OwnPasswordChangeRequest(provisional, Chosen, email)));
        Assert.Equal(HttpStatusCode.OK, change.StatusCode);

        return (id, await TokenAsync(client, email, Chosen));
    }

    private static async Task<string> TokenAsync(HttpClient client, string email, string password)
    {
        using var response = await client.SendAsync(Request(HttpMethod.Post, "/v1/auth/token", SetupOrigin,
            body: new CredentialExchangeRequest(email, password)));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return (await response.Content.ReadFromJsonAsync<SessionResponse>())!.AccessToken;
    }

    /// <summary>
    /// El harness de la pieza de datos con **el zócalo simulado**: la dirección de origen de cada
    /// petición se toma de una cabecera de prueba y se pone en `RemoteIpAddress`, antes de todo lo
    /// demás, que es donde Kestrel la pondría. Ningún servicio del producto se sustituye.
    /// </summary>
    private sealed class OriginAwareHarness(string storePath, IReadOnlyDictionary<string, string?>? settings)
        : DataServiceHarness(storePath, settings)
    {
        public const string OriginHeader = "X-Prueba-Origen";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services => services.AddSingleton<IStartupFilter, SimulatedSocket>());
        }

        private sealed class SimulatedSocket : IStartupFilter
        {
            public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
            {
                app.Use((context, following) =>
                {
                    if (context.Request.Headers.TryGetValue(OriginHeader, out var origin)
                        && IPAddress.TryParse(origin.ToString(), out var address))
                    {
                        context.Connection.RemoteIpAddress = address;
                    }

                    return following(context);
                });

                next(app);
            };
        }
    }
}
