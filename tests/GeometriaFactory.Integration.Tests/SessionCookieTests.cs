using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Web.Services;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// LA MARCA DE SESIÓN DEL NAVEGADOR, ejercida sobre las dos piezas corriendo: qué lleva adentro,
/// qué NO lleva, y qué pasa cuando el almacén del servidor ya no está (`Web ADR-03` §2 y §6.1).
/// </summary>
/// <remarks>
/// LO QUE ESTA BATERÍA MIRA ES LA CABECERA REAL Y EL CUERPO REAL, no el código que los produce.
/// Se entra con un correo y una contraseña de verdad, el servicio de datos emite su testigo
/// firmado de verdad, y después se busca ese testigo —el literal exacto, y también su forma— en
/// todo lo que el navegador recibió. La métrica §8 de `ADR-03` fija el objetivo en exactamente 0.
/// </remarks>
public sealed class SessionCookieTests : IDisposable
{
    private const string Email = "docente@frre.utn.edu.ar";
    private const string Password = "la-que-elegi-para-la-bateria";

    /// <summary>Forma de un acceso firmado: tres tramos separados por punto, el primero `eyJ`.</summary>
    private static readonly Regex SignedAccessShape =
        new(@"eyJ[A-Za-z0-9_-]{6,}\.[A-Za-z0-9_-]{6,}\.", RegexOptions.None, TimeSpan.FromSeconds(1));

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly PublicPieceHarness _publicPiece;
    private readonly HttpClient _browser;

    public SessionCookieTests()
    {
        _dataService = new DataServiceHarness(_storePath);
        _publicPiece = new PublicPieceHarness(_dataService.Server.CreateHandler());

        // EL NAVEGADOR DE LA PRUEBA NO GUARDA COOKIES SOLO, Y ES DELIBERADO: cada petición dice
        // exactamente qué marca lleva, de modo que «con marca» y «sin marca» son dos peticiones
        // distintas y no dos estados de un contenedor que nadie ve. Y no sigue redirecciones,
        // porque la redirección ES lo que varias de estas pruebas verifican.
        _browser = _publicPiece.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            HandleCookies = false,
            AllowAutoRedirect = false,
        });
    }

    [Fact]
    public async Task TheMarkOfSessionCarriesTheThreeAttributesAndNotTheToken()
    {
        var token = await ConfigureAdministratorAndTakeItsTokenAsync();
        var (response, mark) = await SignInAsync();

        Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        Assert.Equal("/entrega-comision", response.Headers.Location?.AbsolutePath);

        var setCookie = response.Headers.GetValues("Set-Cookie")
            .Single(header => header.StartsWith(SessionCookieDefaults.CookieName + "=", StringComparison.Ordinal));

        // Los TRES atributos que el intake §17.6 declara, sobre la cabecera real.
        Assert.Contains("httponly", setCookie, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("secure", setCookie, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("samesite=strict", setCookie, StringComparison.OrdinalIgnoreCase);

        // Y LO QUE NO LLEVA: ni el testigo literal, ni nada con su forma.
        Assert.DoesNotContain(token, setCookie, StringComparison.Ordinal);
        Assert.DoesNotMatch(SignedAccessShape, setCookie);
        Assert.DoesNotContain(token, mark, StringComparison.Ordinal);

        // La prueba NO es vacía: la marca existe y el testigo tampoco es vacío.
        Assert.NotEmpty(mark);
        Assert.NotEmpty(token);
    }

    [Fact]
    public async Task TheSessionSurvivesASeparateRequest()
    {
        var token = await ConfigureAdministratorAndTakeItsTokenAsync();
        var (_, mark) = await SignInAsync();

        // PETICIÓN NUEVA, con la marca y nada más: es lo que hace una recarga o una pestaña nueva.
        // Con la sesión viviendo sólo en el estado del circuito, acá se llegaba sin sesión.
        using var panel = await GetAsync("/entrega-comision", mark);
        var html = await panel.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, panel.StatusCode);
        Assert.Contains(Email, html, StringComparison.Ordinal);
        Assert.Contains("Administrador", html, StringComparison.Ordinal);

        // Y el testigo sigue sin aparecer en lo que el navegador recibe.
        Assert.DoesNotContain(token, html, StringComparison.Ordinal);
        Assert.DoesNotMatch(SignedAccessShape, html);
    }

    [Fact]
    public async Task NoSurfaceOfThePanelEmitsTheTokenIntoItsMarkup()
    {
        var token = await ConfigureAdministratorAndTakeItsTokenAsync();
        var (_, mark) = await SignInAsync();

        string[] routes =
        [
            "/", "/ingreso", "/entrega-comision", "/cuentas", "/mis-trabajos",
            "/trabajo-nuevo", "/mi-contrasena", "/trabajos/T-1", "/credencial-propia/cambio-obligado",
        ];

        foreach (var route in routes)
        {
            using var response = await GetAsync(route, mark);
            var html = await response.Content.ReadAsStringAsync();

            Assert.DoesNotContain(token, html, StringComparison.Ordinal);
            Assert.DoesNotMatch(SignedAccessShape, html);
        }
    }

    [Fact]
    public async Task WithoutTheMarkThereIsNoSession()
    {
        await ConfigureAdministratorAndTakeItsTokenAsync();
        var (_, mark) = await SignInAsync();

        // Con marca, la superficie ofrece el formulario.
        using var withMark = await GetAsync("/mi-contrasena", mark);
        var offered = await withMark.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, withMark.StatusCode);
        Assert.Contains("credential-current", offered, StringComparison.Ordinal);

        // SIN MARCA NO SE LLEGA A LA SUPERFICIE, Y ESTA AFIRMACIÓN CAMBIÓ DE FORMA. Mientras el
        // guardián 2 de `ADR-03` §2 estuvo abierto, `/mi-contrasena` se dibujaba sin sesión y
        // decía qué hacer, y esta prueba fijaba ese texto. Cerrado el guardián, esa pantalla ya
        // no existe para quien no entró: la respuesta es el desvío a `Ingreso`. Lo que se
        // verificaba —que sin sesión no se ofrece nada— se sigue verificando, y más fuerte.
        using var without = await GetAsync("/mi-contrasena", mark: null);
        var notOffered = await without.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Found, without.StatusCode);
        Assert.Equal("/ingreso", without.Headers.Location?.OriginalString);
        Assert.DoesNotContain("credential-current", notOffered, StringComparison.Ordinal);

        // Y sin marca ninguna superficie del panel dibuja identidad de nadie.
        using var panel = await GetAsync("/entrega-comision", mark: null);
        Assert.Equal(HttpStatusCode.Found, panel.StatusCode);
        Assert.DoesNotContain(Email, await panel.Content.ReadAsStringAsync(), StringComparison.Ordinal);
    }

    [Fact]
    public async Task AValidMarkWithAnEmptyStoreGoesBackToSignInWithTheReasonDeclared()
    {
        await ConfigureAdministratorAndTakeItsTokenAsync();
        var (_, mark) = await SignInAsync();

        // EL RECICLADO DEL PROCESO, EJERCIDO: la marca sobrevive en el navegador y el almacén no.
        // Es el costo que `ADR-03` §6.1 aceptó por escrito.
        _publicPiece.Tokens.Clear();

        using var response = await GetAsync("/entrega-comision", mark);

        // NO es una excepción y NO es una pantalla rota: se vuelve a `Ingreso` con el motivo.
        Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        Assert.Equal("/ingreso?estado=sesion-no-restablecible", response.Headers.Location?.OriginalString);

        // Y la marca que ya no abre nada se borra, para que el desvío no se repita.
        var deletion = response.Headers.GetValues("Set-Cookie")
            .Single(header => header.StartsWith(SessionCookieDefaults.CookieName + "=", StringComparison.Ordinal));
        Assert.Contains("expires=Thu, 01 Jan 1970", deletion, StringComparison.OrdinalIgnoreCase);

        // El texto que la persona ve es el de la maqueta aprobada, y llega sin sesión.
        using var signIn = await GetAsync("/ingreso?estado=sesion-no-restablecible", mark: null);
        Assert.Contains(
            "Tu sesión no se pudo restablecer y volviste acá.",
            await signIn.Content.ReadAsStringAsync(),
            StringComparison.Ordinal);
    }

    [Fact]
    public async Task SigningOutDiscardsTheTokenAndErasesTheMark()
    {
        await ConfigureAdministratorAndTakeItsTokenAsync();
        var (_, mark) = await SignInAsync();

        using var panel = await GetAsync("/entrega-comision", mark);
        var html = await panel.Content.ReadAsStringAsync();

        using var request = new HttpRequestMessage(HttpMethod.Post, "/entrega-comision")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "sign-out",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
            }),
        };
        request.Headers.Add("Cookie", Join(mark, AntiforgeryMarkOf(panel)));

        using var response = await _browser.SendAsync(request);

        Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        Assert.Equal("/ingreso?estado=sesion-cerrada", response.Headers.Location?.PathAndQuery);

        // Cerrar es un acto del lado del servidor: el testigo se descartó, y por eso volver con
        // la marca vieja no restablece nada.
        using var afterwards = await GetAsync("/mi-contrasena", mark);
        Assert.DoesNotContain(
            "credential-current",
            await afterwards.Content.ReadAsStringAsync(),
            StringComparison.Ordinal);
    }

    [Fact]
    public async Task TheForcedPasswordChangeDetourStillWorksWithoutASession()
    {
        await ConfigureAdministratorAndTakeItsTokenAsync();
        var (_, mark) = await SignInAsync();

        // El desvío del cambio forzado se llega SIN sesión de trabajo (`ADR-03` §2, guardián 4),
        // así que tiene que responder igual con marca y sin ella.
        foreach (var carried in new[] { mark, null })
        {
            using var response = await GetAsync("/credencial-propia/cambio-obligado", carried);
            var html = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("Elegí una contraseña nueva", html, StringComparison.Ordinal);
            Assert.Contains("forced-provisional", html, StringComparison.Ordinal);

            // Y en el shell de acceso, que no promete navegación.
            Assert.DoesNotContain("gf-shell-sidebar", html, StringComparison.Ordinal);
        }
    }

    public void Dispose()
    {
        _browser.Dispose();
        _publicPiece.Dispose();
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }

    /// <summary>Configura el administrador y devuelve el testigo firmado REAL que el canje emite.</summary>
    private async Task<string> ConfigureAdministratorAndTakeItsTokenAsync()
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

        using var exchange = await data.PostAsJsonAsync("/auth/token", new { email = Email, password = Password });
        exchange.EnsureSuccessStatusCode();

        var body = await exchange.Content.ReadAsStringAsync();
        var token = Regex.Match(body, "\"accessToken\":\"(?<token>[^\"]+)\"").Groups["token"].Value;

        Assert.NotEmpty(token);
        return token;
    }

    /// <summary>Entra por la superficie de ingreso, con antifalsificación, y devuelve la marca.</summary>
    private async Task<(HttpResponseMessage Response, string Mark)> SignInAsync()
    {
        using var page = await _browser.GetAsync("/ingreso");
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

        var response = await _browser.SendAsync(request);
        var mark = response.Headers.TryGetValues("Set-Cookie", out var cookies)
            ? cookies.Select(cookie => cookie.Split(';')[0])
                .Single(cookie => cookie.StartsWith(SessionCookieDefaults.CookieName + "=", StringComparison.Ordinal))
            : string.Empty;

        return (response, mark);
    }

    private async Task<HttpResponseMessage> GetAsync(string route, string? mark)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, route);

        if (mark is not null)
        {
            request.Headers.Add("Cookie", mark);
        }

        return await _browser.SendAsync(request);
    }

    private static string AntiforgeryTokenOf(string html) =>
        Regex.Match(html, "name=\"__RequestVerificationToken\" value=\"(?<token>[^\"]+)\"").Groups["token"].Value;

    private static string AntiforgeryMarkOf(HttpResponseMessage response) =>
        response.Headers.TryGetValues("Set-Cookie", out var cookies)
            ? string.Join("; ", cookies.Select(cookie => cookie.Split(';')[0])
                .Where(cookie => cookie.Contains("Antiforgery", StringComparison.Ordinal)))
            : string.Empty;

    private static string Join(params string[] cookies) =>
        string.Join("; ", cookies.Where(cookie => !string.IsNullOrEmpty(cookie)));
}
