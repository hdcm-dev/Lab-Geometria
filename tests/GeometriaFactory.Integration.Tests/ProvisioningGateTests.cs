using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Contracts.Service;
using GeometriaFactory.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// EL GUARDIÁN 1 DE `Web ADR-03` §2, ejercido sobre HTTP de verdad y con sus DOS MITADES: sin
/// administrador toda ruta desvía al aprovisionamiento, y con administrador el aprovisionamiento
/// deja de armar formulario y desvía de forma neutra.
/// </summary>
/// <remarks>
/// LA PRUEBA QUE MÁS IMPORTA ES LA DE LA TRANSICIÓN, y conviene decir por qué. Las dos mitades por
/// separado las cierra cualquier implementación; lo que ocurre **una sola vez en la vida de la
/// instancia** es el cruce del medio —el laboratorio pasa de no configurado a configurado
/// **mientras la pieza pública está viva**— y es exactamente el caso que un caché con vencimiento
/// rompe, de forma intermitente e irreproducible. Acá se ejerce sin reiniciar nada: la misma pieza
/// pública, el mismo proceso, y el guardián cambia de comportamiento en la petición siguiente.
///
/// LAS DOS PIEZAS SON DE VERDAD. El estado del laboratorio no se simula: el administrador se
/// configura golpeando `A-03` contra el servicio de datos real, y el guardián lo averigua por
/// `A-17`, también real. Con un doble de prueba esta batería no diría nada del producto.
///
/// NO SE SIGUEN LAS REDIRECCIONES, porque **la redirección es lo que se verifica**, y el navegador
/// de la prueba no guarda cookies: cada petición dice qué lleva.
/// </remarks>
public sealed class ProvisioningGateTests : IDisposable
{
    private const string Email = "docente@frre.utn.edu.ar";
    private const string Password = "la-que-elegi-para-el-guardian-1";

    private const string ProvisioningRoute = "/aprovisionamiento-inicial";

    /// <summary>
    /// Rutas de distinta clase —raíz, acceso, panel y estado—, para que «cualquier ruta pedida»
    /// se mida sobre más de una familia y no sobre una sola.
    /// </summary>
    private static readonly string[] AssortedRoutes =
    [
        "/", "/ingreso", "/registro-de-cuenta", "/mis-trabajos", "/cuentas", "/estado",
    ];

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly PublicPieceHarness _publicPiece;
    private readonly List<IDisposable> _disposables = [];

    public ProvisioningGateTests()
    {
        _dataService = new DataServiceHarness(_storePath);
        _publicPiece = new PublicPieceHarness(_dataService.Server.CreateHandler());
    }

    // ------------------------------------------------------------------ el punto `A-17` ------

    [Fact]
    public async Task TheProvisioningPointAnswersAnonymouslyAndWithASingleFact()
    {
        using var data = _dataService.CreateClient();

        using var before = await data.GetAsync("/aprovisionamiento");
        Assert.Equal(HttpStatusCode.OK, before.StatusCode);
        Assert.False((await before.Content.ReadFromJsonAsync<LaboratoryProvisioning>())!.AdministratorConfigured);

        // Y NO LLEVA NINGÚN OTRO DATO: ni correo, ni nombre, ni fecha, ni cantidad de cuentas. Se
        // mide sobre el cuerpo crudo y no sobre el tipo, que es donde se notaría un campo de más.
        await ConfigureAdministratorAsync();

        using var after = await data.GetAsync("/aprovisionamiento");
        var body = await after.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, after.StatusCode);
        Assert.True((await after.Content.ReadFromJsonAsync<LaboratoryProvisioning>())!.AdministratorConfigured);
        Assert.DoesNotContain(Email, body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Rossi", body, StringComparison.OrdinalIgnoreCase);
        Assert.Single(Regex.Matches(body, ":"));
    }

    // ---------------------------------------------------------------------- mitad 1 ---------

    [Fact]
    public async Task WithoutAdministratorEveryRouteDetoursToTheProvisioning()
    {
        using var browser = BrowserOf(_publicPiece);

        foreach (var route in AssortedRoutes)
        {
            using var response = await browser.GetAsync(route);

            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
            Assert.Equal(ProvisioningRoute, response.Headers.Location?.OriginalString);
        }
    }

    [Fact]
    public async Task WithoutAdministratorTheProvisioningItselfIsServed()
    {
        using var browser = BrowserOf(_publicPiece);

        // LA ÚNICA SALIDA DEL ESTADO NO SE DESVÍA A SÍ MISMA: sin esto el producto no tendría
        // cómo configurarse nunca, que es el lazo cerrado que `RN-13` prohíbe con otro disfraz.
        using var response = await browser.GetAsync(ProvisioningRoute);
        var html = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("id=\"provisioning-email\"", html, StringComparison.Ordinal);
        Assert.Contains("id=\"provisioning-password\"", html, StringComparison.Ordinal);
    }

    [Fact]
    public async Task TheStaticResourcesAreNotDetoured()
    {
        using var browser = BrowserOf(_publicPiece);

        // Y EL GUION DEL NAVEGADOR TAMPOCO. Si el guardián los desviara, la pantalla a la que
        // manda quedaría sin sistema visual y sin la mejora de superficie autorizada.
        foreach (var resource in new[] { "/css/app.css", "/interaction/surface-interaction.js" })
        {
            using var response = await browser.GetAsync(resource);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    // ---------------------------------------------------------------------- mitad 2 ---------

    [Fact]
    public async Task WithAdministratorTheProvisioningDetoursNeutrally()
    {
        using var browser = BrowserOf(_publicPiece);
        await ConfigureAdministratorAsync();

        using var response = await browser.GetAsync(ProvisioningRoute);
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        Assert.Equal("/ingreso", response.Headers.Location?.OriginalString);

        // NI UN CAMPO DE FORMULARIO NI UN TEXTO QUE EXPLIQUE POR QUÉ (`ADR-03` §2 y §6.4). El
        // desvío es NEUTRO: la respuesta no dice que ya hay administrador, y la dirección de
        // destino tampoco lleva ningún motivo colgado.
        Assert.DoesNotContain("<input", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<form", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("administrador", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("?", response.Headers.Location!.OriginalString, StringComparison.Ordinal);
    }

    [Fact]
    public async Task WithAdministratorTheOtherRoutesAreNoLongerDetoured()
    {
        using var browser = BrowserOf(_publicPiece);
        await ConfigureAdministratorAsync();

        // La mitad 1 deja de regir, y el guardián 2 pasa a ser el que decide sobre el panel: por
        // eso acá se piden las rutas que NO son del panel.
        foreach (var route in new[] { "/ingreso", "/registro-de-cuenta", "/estado" })
        {
            using var response = await browser.GetAsync(route);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // **[RELEVO DE LA ETAPA `g`, DECLARADO.]** La raíz salía de esta lista, y respondía `200`
        // porque nadie la desviaba. Era la mitad de `NAV-03` que faltaba: `Linea-Base-Visual.md` §5
        // declara que **con administrador constituido la entrada va al ingreso**, y hasta la etapa
        // `f` quien escribía `/` en un laboratorio ya configurado se quedaba mirando el marcador de
        // posición de la etapa `b`.
        //
        // Que deje de responder `200` NO es la mitad 1 volviendo: el desvío es al **ingreso** y no
        // al aprovisionamiento, y esa diferencia es justamente lo que esta prueba comprueba.
        using var root = await browser.GetAsync("/");

        Assert.Equal(HttpStatusCode.Redirect, root.StatusCode);
        Assert.EndsWith("/ingreso", root.Headers.Location!.ToString(), StringComparison.Ordinal);
    }

    // --------------------------------------------------------------------- la transición ----

    [Fact]
    public async Task TheGateChangesBehaviourWithoutRestartingThePublicPiece()
    {
        using var browser = BrowserOf(_publicPiece);

        // ANTES: el aprovisionamiento se sirve y `/ingreso` desvía hacia él.
        using (var provisioningBefore = await browser.GetAsync(ProvisioningRoute))
        {
            Assert.Equal(HttpStatusCode.OK, provisioningBefore.StatusCode);
        }

        using (var signInBefore = await browser.GetAsync("/ingreso"))
        {
            Assert.Equal(HttpStatusCode.Found, signInBefore.StatusCode);
            Assert.Equal(ProvisioningRoute, signInBefore.Headers.Location?.OriginalString);
        }

        // EL CRUCE. Se configura el administrador contra el servicio de datos, exactamente como lo
        // hace la pantalla. LA PIEZA PÚBLICA NO SE REINICIA, NO SE RECONSTRUYE Y NO SE LE AVISA
        // NADA: es la misma instancia, el mismo proceso y el mismo `ProvisioningStateProbe`.
        await ConfigureAdministratorAsync();

        // DESPUÉS, y en la petición siguiente: las dos mitades cambiaron de lado.
        using (var provisioningAfter = await browser.GetAsync(ProvisioningRoute))
        {
            Assert.Equal(HttpStatusCode.Found, provisioningAfter.StatusCode);
            Assert.Equal("/ingreso", provisioningAfter.Headers.Location?.OriginalString);
        }

        using (var signInAfter = await browser.GetAsync("/ingreso"))
        {
            Assert.Equal(HttpStatusCode.OK, signInAfter.StatusCode);
        }
    }

    [Fact]
    public async Task TheServiceStillRejectsASecondAdministratorForcingTheRequest()
    {
        await ConfigureAdministratorAsync();

        using var data = _dataService.CreateClient();

        // SIN PASAR POR LA PANTALLA, que es la única forma de comprobar que el guardián no se
        // estaba usando como si fuera una defensa. La negativa la produce el servicio de datos.
        using var second = await data.PostAsJsonAsync("/cuentas/administrador", new
        {
            email = "otro@frre.utn.edu.ar",
            firstName = "Otro",
            lastName = "Docente",
            password = Password,
        });

        Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);
    }

    // ------------------------------------------------------------- la puerta de servicio ----

    [Fact]
    public async Task TheServiceDoorDoesNotOpenTheGateOutsideDevelopment()
    {
        // LA OPCIÓN PUESTA Y EL ENTORNO EQUIVOCADO: la opción sola no abre nada, igual que con el
        // guardián 2. Sin administrador, en `Production`, el desvío rige aunque esté puesta.
        using var browser = BrowserOf(HarnessIn("Production", walkthrough: true));

        using var response = await browser.GetAsync("/ingreso");

        Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        Assert.Equal(ProvisioningRoute, response.Headers.Location?.OriginalString);
    }

    [Fact]
    public async Task TheServiceDoorOpensTheWalkthroughOnlyInDevelopment()
    {
        // Y con los dos puestos abre, que es lo que `scripts/verify-navigation.sh` necesita: el
        // paseo pide las trece pantallas SIN servicio de datos levantado, y sin esta salvedad el
        // guardián 1 desviaría doce de ellas o desviaría el aprovisionamiento, según cómo le
        // respondiera el laboratorio consultado.
        using var browser = BrowserOf(HarnessIn("Development", walkthrough: true));

        foreach (var route in new[] { ProvisioningRoute, "/ingreso", "/registro-de-cuenta" })
        {
            using var response = await browser.GetAsync(route);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    // ------------------------------------------------------- el servicio que no responde ----

    [Fact]
    public async Task WhenTheDataServiceDoesNotAnswerNothingIsDetoured()
    {
        // «NO SE SABE» NO ES «NO». Con el servicio de datos caído, desviar sobre una suposición
        // taparía `/estado`, que es la única pantalla desde la que se diagnostica justamente eso.
        using var unreachable = new PublicPieceHarness(new SocketsHttpHandler
        {
            ConnectTimeout = TimeSpan.FromMilliseconds(200),
        });
        _disposables.Add(unreachable);

        using var browser = BrowserOf(unreachable);

        using var response = await browser.GetAsync("/estado");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    public void Dispose()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }

        _publicPiece.Dispose();
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }

    /// <summary>La misma pieza pública, levantada en otro entorno y con la puerta de servicio puesta.</summary>
    private WebApplicationFactory<SessionTokenStore> HarnessIn(string environment, bool walkthrough)
    {
        var variant = _publicPiece.WithWebHostBuilder(builder => builder
            .UseEnvironment(environment)
            .UseSetting(
                PanelSessionGateMiddleware.WalkthroughSetting,
                walkthrough ? "true" : "false"));

        _disposables.Add(variant);
        return variant;
    }

    private static HttpClient BrowserOf(WebApplicationFactory<SessionTokenStore> harness) =>
        harness.CreateClient(new WebApplicationFactoryClientOptions
        {
            HandleCookies = false,
            AllowAutoRedirect = false,
        });

    private async Task ConfigureAdministratorAsync()
    {
        using var data = _dataService.CreateClient();

        using var setup = await data.PostAsJsonAsync("/cuentas/administrador", new
        {
            email = Email,
            firstName = "Ana",
            lastName = "Rossi",
            password = Password,
        });
        setup.EnsureSuccessStatusCode();
    }
}
