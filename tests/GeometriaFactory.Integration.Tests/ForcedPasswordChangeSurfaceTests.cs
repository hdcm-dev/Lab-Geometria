using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.Security;
using GeometriaFactory.Web.Services;
using Microsoft.Data.Sqlite;
using Xunit;
using Xunit.Abstractions;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// EL RECORRIDO DEL ALUMNO RESETEADO, SOBRE HTTP DE VERDAD Y ATRAVESANDO LA REDIRECCIÓN, que es
/// exactamente lo que ninguna prueba miraba y por eso el defecto pasó.
/// </summary>
/// <remarks>
/// QUÉ REEMPLAZA, Y POR QUÉ NO SE PODÍA CONSERVAR. La versión anterior de esta batería armaba el
/// componente en memoria con <c>HtmlRenderer</c>, le anotaba el desvío a mano en un
/// <see cref="SessionState"/> construido por la prueba, y miraba el marcado. Fijaba el mecanismo
/// viejo —el ingreso anotaba el correo en el estado de ámbito y la pantalla lo leía— y **por
/// construcción no podía ver el defecto**: la prueba nunca cruzaba una petición, y el defecto
/// vivía exactamente ahí. El ingreso es una superficie estática; el envío es UNA petición y la
/// redirección abre OTRA, con un `SessionState` nuevo y vacío. Anotado y leído en la misma
/// instancia, todo cerraba; sobre HTTP, el correo llegaba nulo y la pantalla mostraba su callejón.
///
/// DE MODO QUE ACÁ NO SE ARMA NINGÚN COMPONENTE. Se levantan **las dos piezas de verdad** —la
/// pública y la de datos—, se pide por HTTP, se envían formularios con la antifalsificación
/// puesta, se siguen las redirecciones **a mano** para poder mirarlas, y la marca de cambio
/// pendiente se lee **del almacén**, nunca de una respuesta.
///
/// EL NAVEGADOR DE LA PRUEBA NO GUARDA COOKIES SOLO NI SIGUE REDIRECCIONES, igual que en
/// <see cref="SessionCookieTests"/>: cada petición declara qué lleva, y la redirección es lo que
/// varias de estas pruebas verifican.
/// </remarks>
public sealed class ForcedPasswordChangeSurfaceTests : IDisposable
{
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";

    private const string StudentEmail = "alumna@frre.utn.edu.ar";
    private const string ProvisionalPassword = "la-provisoria-que-me-paso-el-docente";
    private const string ChosenPassword = "la-que-elijo-yo-ahora";

    private const string ForcedChangeRoute = "/credencial-propia/cambio-obligado";

    /// <summary>Forma de un acceso firmado: tres tramos separados por punto, el primero `eyJ`.</summary>
    private static readonly Regex SignedAccessShape =
        new(@"eyJ[A-Za-z0-9_-]{6,}\.[A-Za-z0-9_-]{6,}\.", RegexOptions.None, TimeSpan.FromSeconds(1));

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly PublicPieceHarness _publicPiece;
    private readonly HttpClient _browser;
    private readonly ITestOutputHelper _output;

    public ForcedPasswordChangeSurfaceTests(ITestOutputHelper output)
    {
        _output = output;
        _dataService = new DataServiceHarness(_storePath);
        _publicPiece = new PublicPieceHarness(_dataService.Server.CreateHandler());
        _browser = _publicPiece.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            HandleCookies = false,
            AllowAutoRedirect = false,
        });
    }

    public void Dispose()
    {
        _browser.Dispose();
        _publicPiece.Dispose();
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }

    // ---- LA PANTALLA SE ALCANZA DE FRENTE, Y EL CAMBIO SE COMPLETA ----

    /// <summary>
    /// SIN HABER PASADO POR EL INGRESO. Es el caso que la pantalla vieja no podía atender —no
    /// tenía de dónde sacar de qué cuenta se trata— y es también el de la recarga y el del enlace
    /// guardado, que son la misma cosa vistas desde el servidor: una petición sin estado previo.
    /// </summary>
    [Fact]
    public async Task TheSurfaceIsReachedHeadOnWithoutSigningInAndTheChangeCompletes()
    {
        await ConfigureAdministratorAsync();
        await WriteResetStudentAsync();

        // 1 · Se pide la pantalla DE FRENTE: sin marca de sesión y sin haber pedido `/ingreso`.
        using var page = await _browser.GetAsync(ForcedChangeRoute);
        var html = await page.Content.ReadAsStringAsync();

        Trace("1 · GET de frente", page);
        Assert.Equal(HttpStatusCode.OK, page.StatusCode);

        // Los CUATRO campos, y el del correo es el apartamiento declarado en la cabecera del
        // componente: es lo que hace que esta pantalla no dependa de ningún estado previo.
        Assert.Contains("forced-email", html, StringComparison.Ordinal);
        Assert.Contains("forced-provisional", html, StringComparison.Ordinal);
        Assert.Contains("forced-new", html, StringComparison.Ordinal);
        Assert.Contains("forced-new-repeat", html, StringComparison.Ordinal);

        // Y NO dibuja el callejón que la versión anterior mostraba a quien llegaba así.
        Assert.DoesNotContain("Ir a ingresar", html, StringComparison.Ordinal);

        // Shell de acceso, sin barra lateral, y sin jerga en pantalla.
        Assert.Contains("Elegí una contraseña nueva", html, StringComparison.Ordinal);
        Assert.Contains("Volver al ingreso", html, StringComparison.Ordinal);
        Assert.DoesNotContain("gf-shell-sidebar", html, StringComparison.Ordinal);
        Assert.DoesNotContain("Cancelar", html, StringComparison.Ordinal);
        Assert.DoesNotContain("SUP-", html, StringComparison.Ordinal);
        Assert.DoesNotContain("CMP-", html, StringComparison.Ordinal);
        Assert.DoesNotContain("NAV-", html, StringComparison.Ordinal);

        // 2 · Y el cambio se completa desde acá, con la marca todavía puesta antes de enviar.
        Assert.True(await MarkOfAsync(StudentEmail));

        using var changed = await PostForcedChangeAsync(page, html, StudentEmail, ProvisionalPassword, ChosenPassword, ChosenPassword);
        Trace("2 · POST del cambio", changed);

        Assert.Equal(HttpStatusCode.Found, changed.StatusCode);
        Assert.Equal("/ingreso?estado=confirmacion-contrasena", changed.Headers.Location?.PathAndQuery);

        // La marca se levantó, leída DEL ALMACÉN (INV-09).
        Assert.False(await MarkOfAsync(StudentEmail));

        // Y el cambio no emitió ninguna sesión: la respuesta no escribe marca de sesión.
        Assert.Empty(SessionCookiesOf(changed));
    }

    // ---- EL RECORRIDO ENTERO, ATRAVESANDO LA REDIRECCIÓN DEL INGRESO ----

    /// <summary>
    /// PRESENTA LA PROVISORIA → RECIBE EL DESVÍO → LLEGA A LA PANTALLA → CAMBIA → VUELVE A ENTRAR,
    /// y recién ahí hay sesión de trabajo. Es el recorrido que el defecto rompía en su tercer paso.
    /// </summary>
    [Fact]
    public async Task TheResetStudentWalksTheWholeWayThroughTheRedirectAndOnlyThenGetsASession()
    {
        await ConfigureAdministratorAsync();
        await WriteResetStudentAsync();

        // 1 · Entra con la provisoria por la superficie de ingreso, con antifalsificación.
        var (diverted, divertedMark) = await SignInAsync(StudentEmail, ProvisionalPassword);
        using (diverted)
        {
            Trace("1 · POST /ingreso con la provisoria", diverted);

            // EL DESVÍO ES UNA REDIRECCIÓN DE VERDAD, y no una banda dibujada.
            Assert.Equal(HttpStatusCode.Found, diverted.StatusCode);
            Assert.Equal(ForcedChangeRoute, diverted.Headers.Location?.AbsolutePath);

            // Y NO ABRE SESIÓN (RN-13): ninguna marca de sesión se escribe.
            Assert.Equal(string.Empty, divertedMark);
        }

        // 2 · Se sigue la redirección a mano, que es lo que hace el navegador, y SIN llevar nada:
        //     es una petición nueva, con su propio `SessionState` vacío. Acá se rompía.
        using var page = await _browser.GetAsync(ForcedChangeRoute);
        var html = await page.Content.ReadAsStringAsync();

        Trace("2 · GET del destino del desvío", page);
        Assert.Equal(HttpStatusCode.OK, page.StatusCode);
        Assert.Contains("forced-provisional", html, StringComparison.Ordinal);
        Assert.DoesNotContain("Ir a ingresar", html, StringComparison.Ordinal);

        // 3 · Cambia la contraseña desde la pantalla a la que la redirección la trajo.
        using var changed = await PostForcedChangeAsync(page, html, StudentEmail, ProvisionalPassword, ChosenPassword, ChosenPassword);
        Trace("3 · POST del cambio", changed);

        Assert.Equal(HttpStatusCode.Found, changed.StatusCode);
        Assert.Equal("/ingreso?estado=confirmacion-contrasena", changed.Headers.Location?.PathAndQuery);
        Assert.False(await MarkOfAsync(StudentEmail));

        // 4 · La provisoria ya no sirve: vuelve a la pantalla de ingreso con el rechazo dibujado,
        //     y sigue sin haber sesión.
        var (withProvisional, provisionalMark) = await SignInAsync(StudentEmail, ProvisionalPassword);
        using (withProvisional)
        {
            Trace("4 · POST /ingreso con la provisoria ya cambiada", withProvisional);
            Assert.Equal(HttpStatusCode.OK, withProvisional.StatusCode);
            Assert.Equal(string.Empty, provisionalMark);
        }

        // 5 · Y RECIÉN ACÁ HAY SESIÓN DE TRABAJO: con la contraseña nueva, y al panel del alumno.
        var (session, mark) = await SignInAsync(StudentEmail, ChosenPassword);
        using (session)
        {
            Trace("5 · POST /ingreso con la contraseña elegida", session);
            Assert.Equal(HttpStatusCode.Found, session.StatusCode);
            Assert.Equal("/mis-trabajos", session.Headers.Location?.AbsolutePath);
            Assert.NotEmpty(mark);
        }

        // Y la sesión sirve: el panel del alumno responde y dibuja su identidad.
        using var panel = await GetAsync("/mis-trabajos", mark);
        var panelHtml = await panel.Content.ReadAsStringAsync();

        Trace("6 · GET /mis-trabajos con la marca", panel);
        Assert.Equal(HttpStatusCode.OK, panel.StatusCode);
        Assert.Contains(StudentEmail, panelHtml, StringComparison.Ordinal);
    }

    // ---- RN-13 · MIENTRAS LA MARCA ESTÉ PUESTA, NINGÚN CUERPO TRAE ACCESO FIRMADO ----

    [Fact]
    public async Task WhileTheMarkIsSetNoBodyCarriesASignedAccess()
    {
        await ConfigureAdministratorAsync();
        await WriteResetStudentAsync();

        var seen = new List<(string Step, string Payload)>();

        // Todo lo que el navegador recibe mientras la marca sigue puesta: el desvío del ingreso,
        // la pantalla del cambio, y el rechazo de una provisoria equivocada.
        var (diverted, divertedMark) = await SignInAsync(StudentEmail, ProvisionalPassword);
        using (diverted)
        {
            seen.Add(("desvío del ingreso", await diverted.Content.ReadAsStringAsync()));
            seen.Add(("cabeceras del desvío", HeadersOf(diverted)));
            Assert.Equal(string.Empty, divertedMark);
        }

        using var page = await _browser.GetAsync(ForcedChangeRoute);
        var html = await page.Content.ReadAsStringAsync();
        seen.Add(("pantalla del cambio", html));
        seen.Add(("cabeceras de la pantalla", HeadersOf(page)));

        using var refused = await PostForcedChangeAsync(page, html, StudentEmail, "no-es-la-que-me-paso", ChosenPassword, ChosenPassword);
        seen.Add(("rechazo de la provisoria", await refused.Content.ReadAsStringAsync()));
        seen.Add(("cabeceras del rechazo", HeadersOf(refused)));

        // La marca sigue puesta en el almacén después del intento fallido.
        Assert.True(await MarkOfAsync(StudentEmail));

        foreach (var (step, payload) in seen)
        {
            _output.WriteLine($"{step,-30} {payload.Length} caracteres");
            Assert.DoesNotMatch(SignedAccessShape, payload);
            Assert.DoesNotContain("accessToken", payload, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain(ProvisionalPassword, payload, StringComparison.Ordinal);
            Assert.DoesNotContain(ChosenPassword, payload, StringComparison.Ordinal);
        }

        // LA PRUEBA NO PASA POR AUSENCIA: el mismo instrumento SÍ reconoce un acceso firmado real,
        // el que la pieza de datos emite para una cuenta que no tiene la marca puesta.
        using var data = _dataService.CreateClient();
        using var exchange = await data.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(AdministratorEmail, AdministratorPassword));

        Assert.Equal(HttpStatusCode.OK, exchange.StatusCode);
        Assert.Matches(SignedAccessShape, await exchange.Content.ReadAsStringAsync());
    }

    // ---- INV-09 · LA MARCA SE LEE DEL ALMACÉN ----

    [Fact]
    public async Task TheMarkIsReadFromTheStoreAndOnlyTheEffectiveChangeLiftsIt()
    {
        await ConfigureAdministratorAsync();
        await WriteResetStudentAsync();

        Assert.True(await MarkOfAsync(StudentEmail));

        using var page = await _browser.GetAsync(ForcedChangeRoute);
        var html = await page.Content.ReadAsStringAsync();

        // Intento fallido: la provisoria no corresponde. La marca SIGUE PUESTA.
        using var refused = await PostForcedChangeAsync(page, html, StudentEmail, "no-es-la-que-me-paso", ChosenPassword, ChosenPassword);
        var refusedHtml = await refused.Content.ReadAsStringAsync();

        Trace("intento fallido", refused);
        Assert.Equal(HttpStatusCode.OK, refused.StatusCode);
        Assert.Contains("La contraseña provisoria que escribiste no corresponde.", refusedHtml, StringComparison.Ordinal);
        Assert.True(await MarkOfAsync(StudentEmail));

        // Y el correo vuelve escrito, que es lo que evita reescribirlo; las contraseñas no.
        Assert.Contains(StudentEmail, refusedHtml, StringComparison.Ordinal);

        // Cambio efectivo: la marca queda LEVANTADA, leída del almacén.
        using var changed = await PostForcedChangeAsync(page, html, StudentEmail, ProvisionalPassword, ChosenPassword, ChosenPassword);
        Trace("cambio efectivo", changed);

        Assert.Equal(HttpStatusCode.Found, changed.StatusCode);
        Assert.False(await MarkOfAsync(StudentEmail));
    }

    // ---- Andamiaje ----

    private void Trace(string step, HttpResponseMessage response) =>
        _output.WriteLine($"{step,-40} {(int)response.StatusCode} {response.Headers.Location?.OriginalString}");

    private static string HeadersOf(HttpResponseMessage response) =>
        string.Join("\n", response.Headers.Select(header => $"{header.Key}: {string.Join(", ", header.Value)}"));

    private static IEnumerable<string> SessionCookiesOf(HttpResponseMessage response) =>
        response.Headers.TryGetValues("Set-Cookie", out var cookies)
            ? cookies.Where(cookie => cookie.StartsWith(SessionCookieDefaults.CookieName + "=", StringComparison.Ordinal))
            : [];

    private async Task ConfigureAdministratorAsync()
    {
        using var data = _dataService.CreateClient();

        using var setup = await data.PostAsJsonAsync(
            "/cuentas/administrador",
            new AdministratorSetupRequest(AdministratorEmail, "Ana", "Rossi", AdministratorPassword));

        Assert.Equal(HttpStatusCode.Created, setup.StatusCode);
    }

    /// <summary>
    /// Escribe la cuenta de alumno TAL COMO LA VA A DEJAR EL RESETEO de la etapa `d`: habilitada,
    /// con la credencial derivada de la provisoria y con la marca puesta. Es el mismo andamiaje
    /// que <see cref="ForcedPasswordChangeTests"/>, y por el mismo motivo: el reseteo todavía no
    /// existe como operación.
    /// </summary>
    private async Task WriteResetStudentAsync()
    {
        var derived = new PasswordDerivation(iterations: 1).Derive(ProvisionalPassword);
        Assert.NotNull(derived);

        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = """
            insert into Account (Id, Email, NormalizedEmail, FirstName, LastName, Role, Status, PasswordHash, MustChangePassword, CreatedAt)
            select $id, $email, $normalized, 'Ana', 'Diaz', 'Student', 'Enabled', $hash, 1, CreatedAt from Account limit 1
            """;
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
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "select MustChangePassword from Account where NormalizedEmail = $email";
        command.Parameters.AddWithValue("$email", EmailIdentity.Normalize(email));

        var value = await command.ExecuteScalarAsync();
        return value?.ToString() == "1";
    }

    /// <summary>Entra por la superficie de ingreso, con antifalsificación, y devuelve la marca.</summary>
    private async Task<(HttpResponseMessage Response, string Mark)> SignInAsync(string email, string password)
    {
        using var page = await _browser.GetAsync("/ingreso");
        var html = await page.Content.ReadAsStringAsync();

        using var request = new HttpRequestMessage(HttpMethod.Post, "/ingreso")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "sign-in",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
                ["Input.Email"] = email,
                ["Input.Password"] = password,
            }),
        };
        request.Headers.Add("Cookie", AntiforgeryMarkOf(page));

        var response = await _browser.SendAsync(request);
        var mark = response.Headers.TryGetValues("Set-Cookie", out var cookies)
            ? cookies.Select(cookie => cookie.Split(';')[0])
                .FirstOrDefault(cookie => cookie.StartsWith(SessionCookieDefaults.CookieName + "=", StringComparison.Ordinal))
                ?? string.Empty
            : string.Empty;

        return (response, mark);
    }

    /// <summary>Envía el formulario de la pantalla del cambio forzado, con sus cuatro campos.</summary>
    private async Task<HttpResponseMessage> PostForcedChangeAsync(
        HttpResponseMessage page,
        string html,
        string email,
        string provisional,
        string chosen,
        string repeated)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, ForcedChangeRoute)
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "forced-password-change",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
                ["Input.Email"] = email,
                ["Input.ProvisionalPassword"] = provisional,
                ["Input.NewPassword"] = chosen,
                ["Input.NewPasswordRepeat"] = repeated,
            }),
        };
        request.Headers.Add("Cookie", AntiforgeryMarkOf(page));

        return await _browser.SendAsync(request);
    }

    private async Task<HttpResponseMessage> GetAsync(string route, string? mark)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, route);

        if (!string.IsNullOrEmpty(mark))
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
}
