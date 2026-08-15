using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// EL GUARDIÁN 2 DE `Web ADR-03` §2, ejercido sobre HTTP de verdad: «ninguna ruta del panel es
/// accesible sin sesión», y la puerta de servicio que sólo existe en desarrollo.
/// </summary>
/// <remarks>
/// LO QUE ESTA BATERÍA MIRA ES EL CÓDIGO DE RESPUESTA Y LA CABECERA `Location`, no el código que
/// los produce: se pide cada dirección como la pediría un navegador, con marca y sin ella. Y no se
/// siguen las redirecciones, porque **la redirección es lo que se verifica**.
///
/// LA CUARTA PRUEBA ES LA QUE IMPORTA MÁS. La puerta de servicio abre el paseo sin sesión, y su
/// límite —que fuera de desarrollo no rige **aunque la opción esté puesta**— es una afirmación que
/// no se puede dejar en un comentario: acá se levanta la pieza en `Production` con la opción
/// puesta en `true` y se comprueba que las siete rutas desvían igual.
/// </remarks>
public sealed class PanelSessionGateTests : IDisposable
{
    private const string Email = "docente@frre.utn.edu.ar";
    private const string Password = "la-que-elegi-para-el-guardian";

    /// <summary>Las siete rutas del panel de `ADR-03` §2, con las dos formas de `/trabajos`.</summary>
    private static readonly string[] PanelRoutes =
    [
        "/mis-trabajos", "/trabajo-nuevo", "/trabajos/T-1", "/trabajos/T-1/editar",
        "/cuentas", "/entrega-comision", "/mi-contrasena",
    ];

    /// <summary>
    /// Las que NO son del panel y siguen siendo públicas. `/credencial-propia/cambio-obligado` está
    /// acá a propósito: se llega **sin sesión de trabajo** (`INV-09`), y gatearla rompe `RN-13`.
    /// </summary>
    private static readonly string[] PublicRoutes =
    [
        "/", "/aprovisionamiento-inicial", "/registro-de-cuenta", "/ingreso",
        "/credencial-propia/establecer", "/credencial-propia/cambio-obligado",
        "/estado", "/no-encontrado",
    ];

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly PublicPieceHarness _publicPiece;
    private readonly List<IDisposable> _disposables = [];

    public PanelSessionGateTests()
    {
        _dataService = new DataServiceHarness(_storePath);
        _publicPiece = new PublicPieceHarness(_dataService.Server.CreateHandler());
    }

    [Fact]
    public async Task WithoutTheMarkTheSevenRoutesOfThePanelDetourToSignIn()
    {
        using var browser = BrowserOf(_publicPiece);

        foreach (var route in PanelRoutes)
        {
            using var response = await GetAsync(browser, route, mark: null);

            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
            Assert.Equal("/ingreso", response.Headers.Location?.OriginalString);
        }
    }

    [Fact]
    public async Task WithoutTheMarkThePublicRoutesStillAnswer()
    {
        using var browser = BrowserOf(_publicPiece);

        foreach (var route in PublicRoutes)
        {
            using var response = await GetAsync(browser, route, mark: null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    [Fact]
    public async Task WithAValidMarkTheSevenRoutesOfThePanelAnswer()
    {
        using var browser = BrowserOf(_publicPiece);
        await ConfigureAdministratorAsync();
        var mark = await SignInAsync(browser);

        Assert.NotEmpty(mark);

        foreach (var route in PanelRoutes)
        {
            using var response = await GetAsync(browser, route, mark);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    [Fact]
    public async Task TheServiceDoorDoesNotOpenAnythingOutsideDevelopment()
    {
        // LA OPCIÓN PUESTA Y EL ENTORNO EQUIVOCADO: la opción sola no abre nada.
        using var browser = BrowserOf(HarnessIn("Production", walkthrough: true));

        foreach (var route in PanelRoutes)
        {
            using var response = await GetAsync(browser, route, mark: null);

            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
            Assert.Equal("/ingreso", response.Headers.Location?.OriginalString);
        }
    }

    [Fact]
    public async Task TheServiceDoorOpensTheWalkthroughOnlyInDevelopment()
    {
        // Y con los dos puestos abre, que es lo que `scripts/verify-navigation.sh` necesita para
        // recorrer las trece pantallas sin entrar.
        using var browser = BrowserOf(HarnessIn("Development", walkthrough: true));

        foreach (var route in PanelRoutes)
        {
            using var response = await GetAsync(browser, route, mark: null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
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

    /// <summary>
    /// El navegador de la prueba no guarda cookies y no sigue redirecciones, por lo mismo que en
    /// `SessionCookieTests`: cada petición dice qué marca lleva, y el desvío es lo que se mira.
    /// </summary>
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

    private static async Task<string> SignInAsync(HttpClient browser)
    {
        using var page = await browser.GetAsync("/ingreso");
        var html = await page.Content.ReadAsStringAsync();

        using var request = new HttpRequestMessage(HttpMethod.Post, "/ingreso")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "sign-in",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
                ["Input.Email"] = Email,
                ["Input.Password"] = Password,
            }),
        };
        request.Headers.Add("Cookie", AntiforgeryMarkOf(page));

        using var response = await browser.SendAsync(request);

        return response.Headers.TryGetValues("Set-Cookie", out var cookies)
            ? cookies.Select(cookie => cookie.Split(';')[0])
                .Single(cookie => cookie.StartsWith(SessionCookieDefaults.CookieName + "=", StringComparison.Ordinal))
            : string.Empty;
    }

    private static async Task<HttpResponseMessage> GetAsync(HttpClient browser, string route, string? mark)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, route);

        if (mark is not null)
        {
            request.Headers.Add("Cookie", mark);
        }

        return await browser.SendAsync(request);
    }

    private static string AntiforgeryTokenOf(string html) =>
        Regex.Match(html, "name=\"__RequestVerificationToken\" value=\"(?<token>[^\"]+)\"").Groups["token"].Value;

    private static string AntiforgeryMarkOf(HttpResponseMessage response) =>
        response.Headers.TryGetValues("Set-Cookie", out var cookies)
            ? string.Join("; ", cookies.Select(cookie => cookie.Split(';')[0])
                .Where(cookie => cookie.Contains("Antiforgery", StringComparison.Ordinal)))
            : string.Empty;
}
