using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// LA INTERACCIÓN DE SUPERFICIE AUTORIZADA, MIRADA POR LO QUE NO PUEDE ROMPER.
/// </summary>
/// <remarks>
/// QUÉ SE VERIFICA ACÁ, Y QUÉ NO SE PUEDE VERIFICAR ACÁ. El Product Owner autorizó un guion del
/// navegador acotado a cuatro cosas —copiar al portapapeles, dibujar un estado en curso, mantener
/// una acción inhabilitada hasta que lo tecleado coincide, y cerrar un diálogo con la tecla de
/// escape confinando el foco—. **Nada de eso se ejercita en esta batería, y no podría**: no hay
/// motor de guiones en el proceso de prueba, y comprobarlo exige un navegador conducido. Lo que
/// esta batería mide es lo otro, que es lo que importa que no se rompa:
///
///   1. **Las cuatro superficies siguen funcionando SIN el guion**, sobre HTTP de verdad. Y no es
///      una simulación de «sin guion»: es literalmente lo que pasa acá, porque el cliente pide,
///      envía formularios y lee respuestas **sin ejecutar una sola línea de guion**. Que
///      `AccountLifecycleWebSurfaceTests` siga entera en verde es la mitad de esta afirmación; la
///      otra mitad es lo que se comprueba abajo: el marcado servido deja los controles en su
///      estado utilizable —la acción destructiva **habilitada**— y no injerta nada que dependa
///      del guion.
///   2. **La baja se sigue rechazando del lado del servidor** con un correo que no coincide,
///      **forzando la solicitud sin pasar por la pantalla**. Que el botón espere a que coincida es
///      comodidad de superficie; la defensa es ésta, y por eso se prueba así.
///   3. **Ninguna respuesta servida al navegador lleva el testigo de sesión**, **el guion
///      incluido**, comparado contra el testigo literal.
/// </remarks>
public sealed class SurfaceInteractionTests : IDisposable
{
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";

    private const string StudentEmail = "alumna@frre.utn.edu.ar";

    private const string AccountsRoute = "/cuentas";
    private const string RegistrationRoute = "/registro-de-cuenta";
    private const string ScriptRoute = "/interaction/surface-interaction.js";

    /// <summary>Forma de un acceso firmado: tres tramos separados por punto, el primero `eyJ`.</summary>
    private static readonly Regex SignedAccessShape =
        new(@"eyJ[A-Za-z0-9_-]{6,}\.[A-Za-z0-9_-]{6,}\.", RegexOptions.None, TimeSpan.FromSeconds(1));

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly PublicPieceHarness _publicPiece;
    private readonly HttpClient _browser;
    private readonly ITestOutputHelper _output;

    private string _antiforgeryMark = string.Empty;

    public SurfaceInteractionTests(ITestOutputHelper output)
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

    // ---- LAS CUATRO SUPERFICIES FUNCIONAN SIN EL GUION ----

    /// <summary>
    /// El guion es MEJORA PROGRESIVA: lo que llega al navegador es marcado utilizable tal cual, y
    /// ninguna de las cuatro superficies depende de que el guion cargue.
    /// </summary>
    [Fact]
    public async Task TheFourSurfacesAreUsableWithTheScriptNeverExecuted()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();

        var mark = await SignInAsAdministratorAsync();
        var accountId = await IdOfAsync(StudentEmail);

        // 1 · EL DIÁLOGO DE BAJA SIRVE LA ACCIÓN HABILITADA. Es lo que hace honesta la degradación:
        //     quien inhabilita es el guion, y sin guion la comparación la hace el servidor, que es
        //     como funcionaba antes de que el guion existiera.
        using var dialog = await GetAsync($"{AccountsRoute}?baja={accountId}", mark);
        var dialogHtml = Read(await dialog.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, dialog.StatusCode);
        var destructive = Regex.Match(dialogHtml, "<button[^>]*gf-btn--destructive[^>]*>").Value;
        _output.WriteLine($"acción destructiva servida: {destructive}");

        Assert.NotEqual(string.Empty, destructive);
        Assert.DoesNotContain("disabled", destructive, StringComparison.OrdinalIgnoreCase);

        // Y trae los dos atributos con los que el guion la acota, con el correo que el diálogo YA
        // muestra a la vista: no es un dato nuevo en el navegador.
        Assert.Contains("data-gf-match-input=\"accounts-deletion-confirmation\"", dialogHtml, StringComparison.Ordinal);
        Assert.Contains($"data-gf-match-value=\"{StudentEmail}\"", dialogHtml, StringComparison.Ordinal);

        // Y la salida que la tecla de escape va a activar es un control que YA existe: «Cancelar».
        Assert.Contains("data-gf-dialog-dismiss", dialogHtml, StringComparison.Ordinal);
        Assert.Contains(">Cancelar</a>", dialogHtml, StringComparison.Ordinal);

        // 2 · LA PROVISORIA LLEGA COMO TEXTO SELECCIONABLE, y el botón de copiado NO ESTÁ EN EL
        //     MARCADO: lo injerta el guion. Sin guion no queda un control muerto en la pantalla.
        using var enabled = await PostPanelAsync(
            dialog, dialogHtml, mark, AccountsRoute,
            "account-standing-" + accountId.ToString("N", System.Globalization.CultureInfo.InvariantCulture),
            new Dictionary<string, string>());
        var enabledHtml = Read(await enabled.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, enabled.StatusCode);
        Assert.Contains("id=\"accounts-provisional\"", enabledHtml, StringComparison.Ordinal);
        Assert.Contains("readonly", enabledHtml, StringComparison.Ordinal);
        Assert.Contains("data-gf-copy-source=\"accounts-provisional\"", enabledHtml, StringComparison.Ordinal);

        // El contenedor del copiado llega VACÍO: ningún botón, ningún indicador.
        Assert.Contains("data-gf-copy-unavailable=", enabledHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("gf-spinner", enabledHtml, StringComparison.Ordinal);

        // 3 · Y EL REGISTRO IGUAL: tres campos, cero de contraseña, y ningún indicador dibujado.
        using var registration = await _browser.GetAsync(RegistrationRoute);
        var registrationHtml = Read(await registration.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, registration.StatusCode);
        Assert.Contains("data-gf-pending=\"Enviando\"", registrationHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("gf-spinner", registrationHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("aria-busy", registrationHtml, StringComparison.Ordinal);

        var submit = Regex.Match(registrationHtml, "<button[^>]*type=\"submit\"[^>]*>").Value;
        Assert.DoesNotContain("disabled", submit, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Los textos de los cuatro estados en curso son los LITERALES DEL WIREFRAME, y los escribe el
    /// servidor en el marcado: el guion no lleva ni un texto de producto adentro.
    /// </summary>
    [Fact]
    public async Task TheInProgressTextsAreTheOnesTheWireframesWrote()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();

        var mark = await SignInAsAdministratorAsync();
        var accountId = await IdOfAsync(StudentEmail);

        using var panel = await GetAsync(AccountsRoute, mark);
        Assert.Contains(
            "data-gf-pending=\"Aplicando un cambio de situación\"",
            Read(await panel.Content.ReadAsStringAsync()), StringComparison.Ordinal);

        using var reset = await GetAsync($"{AccountsRoute}?reseteo={accountId}", mark);
        Assert.Contains(
            "data-gf-pending=\"Ejecutando el reseteo\"",
            Read(await reset.Content.ReadAsStringAsync()), StringComparison.Ordinal);

        using var deletion = await GetAsync($"{AccountsRoute}?baja={accountId}", mark);
        Assert.Contains(
            "data-gf-pending=\"Ejecutando la baja\"",
            Read(await deletion.Content.ReadAsStringAsync()), StringComparison.Ordinal);

        using var registration = await _browser.GetAsync(RegistrationRoute);
        Assert.Contains(
            "data-gf-pending=\"Enviando\"",
            Read(await registration.Content.ReadAsStringAsync()), StringComparison.Ordinal);

        // Y el guion, servido, NO TRAE NINGUNO DE LOS CUATRO.
        var script = await ScriptAsync();

        foreach (var text in new[]
                 {
                     "Aplicando un cambio de situación", "Ejecutando el reseteo",
                     "Ejecutando la baja", "Enviando", "Copiar la provisoria",
                 })
        {
            Assert.DoesNotContain(text, script, StringComparison.Ordinal);
        }
    }

    // ---- LA DEFENSA SIGUE DEL LADO DEL SERVIDOR ----

    /// <summary>
    /// LA BAJA SE RECHAZA CON EL CORREO QUE NO COINCIDE, FORZANDO LA SOLICITUD SIN PASAR POR LA
    /// PANTALLA. Que el botón espere a que coincida es comodidad de superficie y no la defensa, y
    /// esta prueba existe para que eso siga siendo verdad y no una intención escrita.
    /// </summary>
    [Fact]
    public async Task TheDeletionIsStillRefusedByTheServiceWhenTheRequestIsForcedPastTheScreen()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();

        var accountId = await IdOfAsync(StudentEmail);
        var accessToken = await AccessTokenAsync();

        using var data = _dataService.CreateClient();

        // 1 · Correo que NO coincide. Nunca hubo pantalla: la solicitud sale directo, con acceso
        //     de administrador de verdad y con todo lo demás en orden.
        using var forced = new HttpRequestMessage(HttpMethod.Delete, $"/cuentas/{accountId}")
        {
            Content = JsonContent.Create(
                new AccountDeletionRequest(accountId, "no-es-el-correo@frre.utn.edu.ar")),
        };
        forced.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        using var refused = await data.SendAsync(forced);
        _output.WriteLine($"baja forzada con el correo equivocado: {(int)refused.StatusCode}");

        Assert.NotEqual(HttpStatusCode.OK, refused.StatusCode);
        Assert.NotEqual(HttpStatusCode.NoContent, refused.StatusCode);

        // Y LA CUENTA SIGUE EXISTIENDO, leído del almacén y no de la respuesta.
        Assert.Equal("Pending", await StatusOfAsync(StudentEmail));

        // 2 · La confirmación VACÍA tampoco alcanza: el rechazo no es una comparación de cadenas
        //     que un valor ausente pudiera saltear.
        using var empty = new HttpRequestMessage(HttpMethod.Delete, $"/cuentas/{accountId}")
        {
            Content = JsonContent.Create(new AccountDeletionRequest(accountId, null)),
        };
        empty.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        using var refusedEmpty = await data.SendAsync(empty);
        _output.WriteLine($"baja forzada sin confirmación: {(int)refusedEmpty.StatusCode}");

        Assert.NotEqual(HttpStatusCode.OK, refusedEmpty.StatusCode);
        Assert.NotEqual(HttpStatusCode.NoContent, refusedEmpty.StatusCode);
        Assert.Equal("Pending", await StatusOfAsync(StudentEmail));

        // 3 · LA PRUEBA NO PASA POR IMPOTENCIA: con el correo correcto la misma solicitud forzada
        //     SÍ procede, y la cuenta deja de existir. Lo que rechaza es la confirmación, no el
        //     camino.
        using var correct = new HttpRequestMessage(HttpMethod.Delete, $"/cuentas/{accountId}")
        {
            Content = JsonContent.Create(new AccountDeletionRequest(accountId, StudentEmail)),
        };
        correct.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        using var applied = await data.SendAsync(correct);
        _output.WriteLine($"baja forzada con el correo correcto: {(int)applied.StatusCode}");

        Assert.True(applied.IsSuccessStatusCode);
        Assert.Null(await StatusOfAsync(StudentEmail));
    }

    // ---- EL GUION NO VE, NI PUEDE VER, EL TESTIGO DE SESIÓN ----

    /// <summary>
    /// El guion servido no lleva el testigo —comparado contra el literal—, no tiene con qué salir a
    /// la red y no toca el almacenamiento del navegador. Es el mismo cuadre que
    /// `scripts/verify-stage-c.sh` C-4 hace sobre el archivo, hecho acá sobre lo que se SIRVE.
    /// </summary>
    [Fact]
    public async Task TheAuthorizedScriptCarriesNoSessionTokenAndHasNoWayToReachTheDataService()
    {
        await ConfigureAdministratorAsync();

        var mark = await SignInAsAdministratorAsync();
        var token = SessionTokenOf(mark);
        var script = await ScriptAsync();

        _output.WriteLine($"guion servido: {script.Length} caracteres · testigo: {token.Length}");

        Assert.NotEqual(string.Empty, script);
        Assert.DoesNotContain(token, script, StringComparison.Ordinal);
        Assert.DoesNotMatch(SignedAccessShape, script);

        // NI UNA FORMA DE SALIR A LA RED, NI UNA DE TOCAR EL NAVEGADOR PERSISTENTE.
        foreach (var forbidden in new[]
                 {
                     "XMLHttpRequest", "WebSocket", "EventSource", "sendBeacon",
                     "localStorage", "sessionStorage", "document.cookie",
                     "Authorization", "Bearer", "accessToken", "ApiBaseUrl",
                 })
        {
            Assert.DoesNotContain(forbidden, script, StringComparison.Ordinal);
        }

        Assert.DoesNotMatch(new Regex(@"\bfetch\s*\(", RegexOptions.None, TimeSpan.FromSeconds(1)), script);
        Assert.DoesNotMatch(new Regex(@"\bimport\s*\(", RegexOptions.None, TimeSpan.FromSeconds(1)), script);

        // LA PRUEBA NO PASA POR AUSENCIA: el mismo instrumento SÍ reconoce lo que busca.
        Assert.Contains("navigator.clipboard", script, StringComparison.Ordinal);
        Assert.Contains(token, $"lo que el almacén guarda es {token}", StringComparison.Ordinal);
    }

    // ---- Andamiaje ----

    private async Task<string> ScriptAsync()
    {
        using var response = await _browser.GetAsync(ScriptRoute);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// El testigo que el almacén DEL SERVIDOR guarda para esta marca, resuelto igual que en
    /// <see cref="AccountLifecycleWebSurfaceTests"/>: la marca es opaca y no lo lleva adentro, que
    /// es justamente lo que `Web ADR-03` §2 exige.
    /// </summary>
    private string SessionTokenOf(string mark)
    {
        var value = mark[(SessionCookieDefaults.CookieName.Length + 1)..];

        var options = _publicPiece.Services
            .GetRequiredService<IOptionsMonitor<CookieAuthenticationOptions>>()
            .Get(SessionCookieDefaults.Scheme);

        var ticket = options.TicketDataFormat.Unprotect(value);
        Assert.NotNull(ticket);

        var sessionId = ticket!.Principal.FindFirst(SessionClaims.SessionId)?.Value;
        Assert.False(string.IsNullOrEmpty(sessionId));

        var token = _publicPiece.Tokens.Find(sessionId!);
        Assert.False(string.IsNullOrEmpty(token));

        return token!;
    }

    private async Task ConfigureAdministratorAsync()
    {
        using var data = _dataService.CreateClient();

        using var setup = await data.PostAsJsonAsync(
            "/cuentas/administrador",
            new AdministratorSetupRequest(AdministratorEmail, "Ana", "Rossi", AdministratorPassword));

        Assert.Equal(HttpStatusCode.Created, setup.StatusCode);
    }

    private async Task RegisterStudentAsync()
    {
        using var page = await _browser.GetAsync(RegistrationRoute);
        var html = Read(await page.Content.ReadAsStringAsync());

        using var request = new HttpRequestMessage(HttpMethod.Post, RegistrationRoute)
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "account-registration",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
                ["Input.Email"] = StudentEmail,
                ["Input.FirstName"] = "Ana",
                ["Input.LastName"] = "Diaz",
            }),
        };
        request.Headers.Add("Cookie", AntiforgeryMarkOf(page));

        using var registered = await _browser.SendAsync(request);
        Assert.Equal(HttpStatusCode.OK, registered.StatusCode);
        Assert.Equal("Pending", await StatusOfAsync(StudentEmail));
    }

    private async Task<string> SignInAsAdministratorAsync()
    {
        using var page = await _browser.GetAsync("/ingreso");
        var html = Read(await page.Content.ReadAsStringAsync());

        using var request = new HttpRequestMessage(HttpMethod.Post, "/ingreso")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "sign-in",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
                ["Input.Email"] = AdministratorEmail,
                ["Input.Password"] = AdministratorPassword,
            }),
        };
        request.Headers.Add("Cookie", AntiforgeryMarkOf(page));

        using var response = await _browser.SendAsync(request);
        Assert.Equal(HttpStatusCode.Found, response.StatusCode);

        var mark = response.Headers.TryGetValues("Set-Cookie", out var cookies)
            ? cookies.Select(cookie => cookie.Split(';')[0])
                .FirstOrDefault(cookie => cookie.StartsWith(SessionCookieDefaults.CookieName + "=", StringComparison.Ordinal))
                ?? string.Empty
            : string.Empty;

        Assert.NotEmpty(mark);

        return mark;
    }

    private async Task<string> AccessTokenAsync()
    {
        using var data = _dataService.CreateClient();
        using var exchange = await data.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(AdministratorEmail, AdministratorPassword));

        Assert.Equal(HttpStatusCode.OK, exchange.StatusCode);
        var session = await exchange.Content.ReadFromJsonAsync<SessionResponse>();

        return session!.AccessToken;
    }

    private async Task<HttpResponseMessage> PostPanelAsync(
        HttpResponseMessage page,
        string html,
        string mark,
        string route,
        string handler,
        IReadOnlyDictionary<string, string> fields)
    {
        var payload = new Dictionary<string, string>
        {
            ["_handler"] = handler,
            ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
        };

        foreach (var (name, value) in fields)
        {
            payload[name] = value;
        }

        using var request = new HttpRequestMessage(HttpMethod.Post, route)
        {
            Content = new FormUrlEncodedContent(payload),
        };
        request.Headers.Add("Cookie", $"{mark}; {AntiforgeryMarkOf(page)}");

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

    private async Task<string?> StatusOfAsync(string email)
    {
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "select Status from Account where NormalizedEmail = $email";
        command.Parameters.AddWithValue("$email", EmailIdentity.Normalize(email));

        return (await command.ExecuteScalarAsync())?.ToString();
    }

    private async Task<Guid> IdOfAsync(string email)
    {
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "select Id from Account where NormalizedEmail = $email";
        command.Parameters.AddWithValue("$email", EmailIdentity.Normalize(email));

        return Guid.Parse((await command.ExecuteScalarAsync())!.ToString()!);
    }

    private static string AntiforgeryTokenOf(string html) =>
        Regex.Match(html, "name=\"__RequestVerificationToken\" value=\"(?<token>[^\"]+)\"").Groups["token"].Value;

    private string AntiforgeryMarkOf(HttpResponseMessage response)
    {
        var emitted = response.Headers.TryGetValues("Set-Cookie", out var cookies)
            ? string.Join("; ", cookies.Select(cookie => cookie.Split(';')[0])
                .Where(cookie => cookie.Contains("Antiforgery", StringComparison.Ordinal)))
            : string.Empty;

        if (!string.IsNullOrEmpty(emitted))
        {
            _antiforgeryMark = emitted;
        }

        return _antiforgeryMark;
    }

    /// <summary>El texto tal como la persona lo lee, y no como el marcado lo transporta.</summary>
    private static string Read(string html) => System.Net.WebUtility.HtmlDecode(html);
}
