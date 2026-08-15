using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Web.Services;
using Microsoft.Data.Sqlite;
using Xunit;
using Xunit.Abstractions;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// EL CICLO DE VIDA DE LA CUENTA VISTO DESDE LA INTERFAZ, SOBRE HTTP DE VERDAD y con las dos
/// piezas levantadas.
/// </summary>
/// <remarks>
/// POR QUÉ NO SE ARMA NINGÚN COMPONENTE EN MEMORIA. Es la misma razón que
/// <see cref="ForcedPasswordChangeSurfaceTests"/> declara en su cabecera y que le costó un defecto
/// al producto: una batería que arma el componente y le anota el estado a mano **por construcción
/// no puede ver** lo que pasa al cruzar una petición, y es exactamente ahí donde viven los
/// defectos de estas superficies. Acá se pide por HTTP, se envían formularios con la
/// antifalsificación puesta, se sigue la redirección a mano y la situación de la cuenta se lee
/// **del almacén**, nunca de una respuesta.
///
/// LO QUE ESTA BATERÍA MIDE, Y ES LO QUE LA TRANSICIÓN `d` → `e` EXIGE DEL LADO DE LA INTERFAZ:
/// el alumno se registra y su cuenta queda `Pending`; el alumno pendiente recibe el aviso
/// explícito al intentar entrar; el administrador habilita y **la provisoria aparece en la
/// respuesta que él ve**, y con esa provisoria el alumno entra y queda obligado a cambiarla; la
/// baja **no procede** con un correo que no coincide; el panel **no ofrece** ninguna operación
/// sobre la cuenta de administrador; y **ninguna respuesta servida al navegador contiene el
/// testigo de sesión**, comparado contra el testigo literal y no contra una forma.
/// </remarks>
public sealed class AccountLifecycleWebSurfaceTests : IDisposable
{
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";

    private const string StudentEmail = "alumna@frre.utn.edu.ar";
    private const string StudentFirstName = "Ana";
    private const string StudentLastName = "Diaz";
    private const string ChosenPassword = "la-que-elijo-yo-ahora";

    private const string AccountsRoute = "/cuentas";
    private const string RegistrationRoute = "/registro-de-cuenta";

    /// <summary>Forma de un acceso firmado: tres tramos separados por punto, el primero `eyJ`.</summary>
    private static readonly Regex SignedAccessShape =
        new(@"eyJ[A-Za-z0-9_-]{6,}\.[A-Za-z0-9_-]{6,}\.", RegexOptions.None, TimeSpan.FromSeconds(1));

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly PublicPieceHarness _publicPiece;
    private readonly HttpClient _browser;
    private readonly ITestOutputHelper _output;

    /// <summary>
    /// La marca de antifalsificación, que se conserva entre peticiones igual que en un navegador.
    /// </summary>
    /// <remarks>
    /// POR QUÉ SE GUARDA. El cliente de estas baterías no maneja cookies solo —cada petición
    /// declara qué lleva—, y la respuesta de un POST **no vuelve a emitir** la marca: la reemite
    /// el `GET` que dibujó el formulario. Sin conservarla, el segundo envío consecutivo llegaría
    /// sin marca y el producto lo rechazaría con `400`, que es lo que hace la antifalsificación y
    /// no lo que la prueba quiere medir.
    /// </remarks>
    private string _antiforgeryMark = string.Empty;

    public AccountLifecycleWebSurfaceTests(ITestOutputHelper output)
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

    // ---- EL ALUMNO SE REGISTRA, SIN ELEGIR CONTRASEÑA, Y QUEDA PENDIENTE ----

    /// <summary>
    /// TRES CAMPOS Y CERO CAMPOS DE CONTRASEÑA, contados sobre el marcado servido, y la cuenta
    /// queda `Pending` leída del almacén.
    /// </summary>
    [Fact]
    public async Task TheStudentRegistersWithoutChoosingAPasswordAndTheAccountIsLeftPending()
    {
        await ConfigureAdministratorAsync();

        using var page = await _browser.GetAsync(RegistrationRoute);
        var html = Read(await page.Content.ReadAsStringAsync());

        Trace("1 · GET del registro", page);
        Assert.Equal(HttpStatusCode.OK, page.StatusCode);

        // El recuento que el wireframe declara criterio de aceptación.
        Assert.Equal(3, CountOf(html, "class=\"gf-input\""));
        Assert.Equal(0, CountOf(html, "type=\"password\""));
        Assert.Contains("registration-email", html, StringComparison.Ordinal);
        Assert.Contains("registration-first-name", html, StringComparison.Ordinal);
        Assert.Contains("registration-last-name", html, StringComparison.Ordinal);

        // El subtítulo de expectativa, ANTES del intento.
        Assert.Contains(
            "Tu cuenta queda a la espera de que el docente la habilite. El laboratorio no envía correos.",
            html, StringComparison.Ordinal);

        using var registered = await PostRegistrationAsync(page, html, StudentEmail, StudentFirstName, StudentLastName);
        var registeredHtml = Read(await registered.Content.ReadAsStringAsync());

        Trace("2 · POST del registro", registered);
        Assert.Equal(HttpStatusCode.OK, registered.StatusCode);

        // El bloque de éxito REEMPLAZA el formulario, y declara las dos cosas que importan.
        Assert.Contains("Tu cuenta quedó registrada", registeredHtml, StringComparison.Ordinal);
        Assert.Contains(
            "Todavía no podés ingresar: el docente tiene que habilitarla. No vas a recibir ningún correo.",
            registeredHtml, StringComparison.Ordinal);
        Assert.Equal(0, CountOf(registeredHtml, "class=\"gf-input\""));

        // Y la situación se lee DEL ALMACÉN, no de la respuesta.
        Assert.Equal("Pending", await StatusOfAsync(StudentEmail));
    }

    /// <summary>El correo repetido no procede y NO revela ningún dato de la cuenta existente.</summary>
    [Fact]
    public async Task TheSecondRegistrationWithTheSameEmailIsRefusedWithoutRevealingTheAccount()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();

        using var page = await _browser.GetAsync(RegistrationRoute);
        var html = Read(await page.Content.ReadAsStringAsync());

        using var repeated = await PostRegistrationAsync(page, html, StudentEmail, "Otra", "Persona");
        var repeatedHtml = Read(await repeated.Content.ReadAsStringAsync());

        Trace("registro repetido", repeated);
        Assert.Equal(HttpStatusCode.OK, repeated.StatusCode);
        Assert.Contains(
            "Ese correo ya pertenece a una cuenta del laboratorio. Usá otro, o entrá con el que ya tenés.",
            repeatedHtml, StringComparison.Ordinal);

        // No revela NADA de la cuenta existente: ni su nombre, ni su situación.
        Assert.DoesNotContain(StudentFirstName + " " + StudentLastName, repeatedHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("Pendiente", repeatedHtml, StringComparison.Ordinal);
    }

    // ---- LA CUENTA PENDIENTE RECIBE EL AVISO EXPLÍCITO ----

    /// <summary>
    /// El alumno que todavía no fue habilitado intenta entrar y lee POR QUÉ no puede, sin que el
    /// mensaje diga nada de ninguna otra cuenta y sin que se le otorgue ninguna sesión.
    /// </summary>
    [Fact]
    public async Task ThePendingStudentIsToldExplicitlyThatTheAccountIsNotEnabledYet()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();
        Assert.Equal("Pending", await StatusOfAsync(StudentEmail));

        var (response, mark) = await SignInAsync(StudentEmail, "cualquier-cosa-que-escriba");
        using (response)
        {
            var html = Read(await response.Content.ReadAsStringAsync());
            Trace("ingreso de la cuenta pendiente", response);

            // No hay redirección y no hay marca de sesión: la banda se dibuja en el lugar.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(string.Empty, mark);

            // EL AVISO ES EXPLÍCITO y nombra la situación, que es lo que RN-06 y el intake
            // §17.5.P.5 exigen: `403` con motivo, no una negativa muda.
            Assert.Contains(
                "Tu cuenta está a la espera de que el docente la habilite. Todavía no podés entrar.",
                html, StringComparison.Ordinal);

            // Y no dice nada de ninguna otra cuenta.
            Assert.DoesNotContain(AdministratorEmail, html, StringComparison.Ordinal);
        }
    }

    // ---- HABILITAR MUESTRA LA PROVISORIA, Y CON ELLA EL ALUMNO QUEDA OBLIGADO A CAMBIARLA ----

    /// <summary>
    /// El administrador habilita desde el panel, **la provisoria aparece en la respuesta que él
    /// ve** —y el panel no tiene ningún campo de contraseña—, y con esa provisoria el alumno entra
    /// y termina en el cambio forzado, sin sesión de trabajo.
    /// </summary>
    [Fact]
    public async Task EnablingShowsTheProvisionalToTheAdministratorAndTheStudentIsForcedToChangeIt()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();

        var mark = await SignInAsAdministratorAsync();
        var accountId = await IdOfAsync(StudentEmail);

        // 1 · El panel dibuja la cuenta pendiente con la ÚNICA transición que su situación admite.
        using var panel = await GetAsync(AccountsRoute, mark);
        var panelHtml = Read(await panel.Content.ReadAsStringAsync());

        Trace("1 · GET del panel", panel);
        Assert.Equal(HttpStatusCode.OK, panel.StatusCode);
        Assert.Contains(StudentEmail, panelHtml, StringComparison.Ordinal);
        Assert.Contains("Habilitar la cuenta de Ana Diaz", panelHtml, StringComparison.Ordinal);
        Assert.Contains("Pendiente", panelHtml, StringComparison.Ordinal);

        // EL PANEL NO TIENE CAMPO DE CONTRASEÑA. NUNCA.
        Assert.Equal(0, CountOf(panelHtml, "type=\"password\""));

        // 2 · Habilitar. La provisoria viene EN LA RESPUESTA que el administrador ve.
        using var enabled = await PostStandingAsync(panel, panelHtml, accountId, mark);
        var enabledHtml = Read(await enabled.Content.ReadAsStringAsync());

        Trace("2 · POST de la habilitación", enabled);
        Assert.Equal(HttpStatusCode.OK, enabled.StatusCode);
        Assert.Contains("Contraseña provisoria de Ana Diaz", enabledHtml, StringComparison.Ordinal);
        Assert.Contains("No se vuelve a mostrar.", enabledHtml, StringComparison.Ordinal);
        Assert.Equal(0, CountOf(enabledHtml, "type=\"password\""));

        var provisional = ProvisionalOf(enabledHtml);
        _output.WriteLine($"provisoria servida: {provisional.Length} caracteres");
        Assert.NotEqual(string.Empty, provisional);

        // La cuenta quedó habilitada y CON LA MARCA de cambio pendiente, leídas del almacén.
        Assert.Equal("Enabled", await StatusOfAsync(StudentEmail));
        Assert.True(await MarkOfAsync(StudentEmail));

        // 3 · SE PIERDE AL RECARGAR, y es la decisión declarada en la cabecera del componente.
        using var reloaded = await GetAsync(AccountsRoute, mark);
        var reloadedHtml = Read(await reloaded.Content.ReadAsStringAsync());

        Trace("3 · GET del panel después de habilitar", reloaded);
        Assert.DoesNotContain("Contraseña provisoria de", reloadedHtml, StringComparison.Ordinal);
        Assert.DoesNotContain(provisional, reloadedHtml, StringComparison.Ordinal);

        // Y la fila se repinta con la situación que devolvió el servicio.
        Assert.Contains("Bloquear la cuenta de Ana Diaz", reloadedHtml, StringComparison.Ordinal);

        // 4 · El alumno entra con esa provisoria y NO obtiene sesión de trabajo: va al cambio.
        var (diverted, divertedMark) = await SignInAsync(StudentEmail, provisional);
        using (diverted)
        {
            Trace("4 · ingreso del alumno con la provisoria", diverted);
            Assert.Equal(HttpStatusCode.Found, diverted.StatusCode);
            Assert.Equal("/credencial-propia/cambio-obligado", diverted.Headers.Location?.AbsolutePath);
            Assert.Equal(string.Empty, divertedMark);
        }

        // 5 · Cambia la contraseña y RECIÉN AHÍ opera con normalidad.
        using var forcedPage = await _browser.GetAsync("/credencial-propia/cambio-obligado");
        var forcedHtml = Read(await forcedPage.Content.ReadAsStringAsync());

        using var changed = await PostForcedChangeAsync(forcedPage, forcedHtml, provisional);
        Trace("5 · POST del cambio forzado", changed);
        Assert.Equal(HttpStatusCode.Found, changed.StatusCode);
        Assert.False(await MarkOfAsync(StudentEmail));

        var (session, studentMark) = await SignInAsync(StudentEmail, ChosenPassword);
        using (session)
        {
            Trace("6 · ingreso con la contraseña elegida", session);
            Assert.Equal(HttpStatusCode.Found, session.StatusCode);
            Assert.Equal("/mis-trabajos", session.Headers.Location?.AbsolutePath);
            Assert.NotEmpty(studentMark);
        }
    }

    // ---- LA BAJA NO PROCEDE SI EL CORREO ESCRITO NO COINCIDE ----

    /// <summary>
    /// El diálogo declara el arrastre ANTES de que la persona escriba, y con un correo que no es
    /// el de la cuenta **la baja no procede**: la cuenta sigue estando.
    /// </summary>
    [Fact]
    public async Task TheDeletionDoesNotProceedWhenTheWrittenEmailDoesNotMatch()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();

        var mark = await SignInAsAdministratorAsync();
        var accountId = await IdOfAsync(StudentEmail);

        // 1 · El diálogo, con el aviso de arrastre en el MISMO lugar donde se pide la confirmación
        //     y asociado por descripción accesible al campo.
        using var dialog = await GetAsync($"{AccountsRoute}?baja={accountId}", mark);
        var dialogHtml = Read(await dialog.Content.ReadAsStringAsync());

        Trace("1 · GET del diálogo de baja", dialog);
        Assert.Equal(HttpStatusCode.OK, dialog.StatusCode);
        Assert.Contains("Dar de baja la cuenta de Ana Diaz", dialogHtml, StringComparison.Ordinal);
        Assert.Contains(
            "Esta baja elimina la cuenta y también TODOS sus trabajos. No se puede deshacer",
            dialogHtml, StringComparison.Ordinal);
        Assert.Contains("aria-describedby=\"accounts-deletion-warning\"", dialogHtml, StringComparison.Ordinal);

        // 2 · Con un correo que no es el de la cuenta, LA BAJA NO PROCEDE.
        using var refused = await PostDeletionAsync(dialog, dialogHtml, accountId, "no-es-el-correo@frre.utn.edu.ar", mark);
        var refusedHtml = Read(await refused.Content.ReadAsStringAsync());

        Trace("2 · POST de la baja con el correo equivocado", refused);
        Assert.Equal(HttpStatusCode.OK, refused.StatusCode);
        Assert.Contains(
            "El correo que escribiste no es el de la cuenta. La baja no procedió.",
            refusedHtml, StringComparison.Ordinal);

        // La cuenta SIGUE EXISTIENDO, leído del almacén, y el diálogo sigue abierto para reintentar.
        Assert.Equal("Pending", await StatusOfAsync(StudentEmail));
        Assert.Contains("Dar de baja la cuenta de Ana Diaz", refusedHtml, StringComparison.Ordinal);

        // 3 · Con el correo correcto sí procede, y la cuenta deja de existir.
        using var deleted = await PostDeletionAsync(refused, refusedHtml, accountId, StudentEmail, mark);
        var deletedHtml = Read(await deleted.Content.ReadAsStringAsync());

        Trace("3 · POST de la baja con el correo correcto", deleted);
        Assert.Equal(HttpStatusCode.OK, deleted.StatusCode);
        Assert.Null(await StatusOfAsync(StudentEmail));
        Assert.DoesNotContain(StudentEmail, deletedHtml, StringComparison.Ordinal);
    }

    // ---- EL RESETEO, DESDE EL MISMO PANEL Y SIN NINGÚN CAMPO DE CONTRASEÑA ----

    /// <summary>
    /// El reseteo procede sobre una cuenta `Pendiente` **sin cambiarle la situación**, la
    /// provisoria la produce el servicio y el panel no tiene dónde escribir una; y dos reseteos
    /// consecutivos sobre la misma cuenta producen provisorias **distintas**.
    /// </summary>
    [Fact]
    public async Task TheAdministratorResetsFromTheSamePanelAndTheProvisionalsDiffer()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();

        var mark = await SignInAsAdministratorAsync();
        var accountId = await IdOfAsync(StudentEmail);

        using var dialog = await GetAsync($"{AccountsRoute}?reseteo={accountId}", mark);
        var dialogHtml = Read(await dialog.Content.ReadAsStringAsync());

        Trace("1 · GET del diálogo de reseteo", dialog);
        Assert.Equal(HttpStatusCode.OK, dialog.StatusCode);
        Assert.Contains("Resetear la contraseña de Ana Diaz", dialogHtml, StringComparison.Ordinal);

        // Confirmación SIMPLE: sin transcripción, y con el aviso de conservación a la vista.
        Assert.Contains(
            "conserva su cuenta y TODOS sus trabajos", dialogHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("escribí el correo de la cuenta", dialogHtml, StringComparison.Ordinal);
        Assert.Equal(0, CountOf(dialogHtml, "type=\"password\""));

        using var first = await PostResetAsync(dialog, dialogHtml, accountId, mark);
        var firstHtml = Read(await first.Content.ReadAsStringAsync());

        Trace("2 · POST del primer reseteo", first);
        Assert.Equal(HttpStatusCode.OK, first.StatusCode);
        var firstProvisional = ProvisionalOf(firstHtml);
        Assert.NotEqual(string.Empty, firstProvisional);

        // LA SITUACIÓN NO CAMBIA: la cuenta sigue `Pending`, leído del almacén, y la marca queda
        // puesta.
        Assert.Equal("Pending", await StatusOfAsync(StudentEmail));
        Assert.True(await MarkOfAsync(StudentEmail));

        using var second = await GetAsync($"{AccountsRoute}?reseteo={accountId}", mark);
        var secondDialogHtml = Read(await second.Content.ReadAsStringAsync());

        using var repeated = await PostResetAsync(second, secondDialogHtml, accountId, mark);
        var repeatedHtml = Read(await repeated.Content.ReadAsStringAsync());

        Trace("3 · POST del segundo reseteo", repeated);
        var secondProvisional = ProvisionalOf(repeatedHtml);
        _output.WriteLine($"dos provisorias, {firstProvisional.Length} y {secondProvisional.Length} caracteres");

        Assert.NotEqual(string.Empty, secondProvisional);
        Assert.NotEqual(firstProvisional, secondProvisional);
        Assert.Equal("Pending", await StatusOfAsync(StudentEmail));
    }

    // ---- INV-08 · EL PANEL NO OFRECE NINGUNA OPERACIÓN SOBRE LA CUENTA DE ADMINISTRADOR ----

    /// <summary>
    /// Se acota OCULTANDO y no mostrando deshabilitado (`Web ADR-03` §6.2): la fila de la cuenta
    /// de administrador no está, y por lo tanto ninguna de sus cinco operaciones tampoco.
    /// </summary>
    [Fact]
    public async Task ThePanelOffersNoOperationOnTheAdministratorAccount()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();

        var mark = await SignInAsAdministratorAsync();
        var administratorId = await IdOfAsync(AdministratorEmail);

        using var panel = await GetAsync(AccountsRoute, mark);
        var html = Read(await panel.Content.ReadAsStringAsync());

        Trace("panel con las dos cuentas en el almacén", panel);
        Assert.Equal(HttpStatusCode.OK, panel.StatusCode);

        // La cuenta de alumno SÍ está, con sus tres operaciones. Es lo que evita que esta prueba
        // pase por una lista vacía.
        Assert.Contains(StudentEmail, html, StringComparison.Ordinal);
        Assert.Contains($"/cuentas?baja={await IdOfAsync(StudentEmail)}", html, StringComparison.Ordinal);

        // Y LA DE ADMINISTRADOR NO SE DIBUJA: ni su correo, ni su identificador en ninguna acción.
        Assert.DoesNotContain(AdministratorEmail + "</td>", html, StringComparison.Ordinal);
        Assert.DoesNotContain(administratorId.ToString(), html, StringComparison.Ordinal);
        Assert.DoesNotContain(administratorId.ToString("N", CultureInfo.InvariantCulture), html, StringComparison.Ordinal);
        Assert.DoesNotContain("la cuenta de Ana Rossi", html, StringComparison.Ordinal);

        // Y el servicio igual la rechaza: esta pieza ACOTA, no hace cumplir.
        using var data = _dataService.CreateClient();
        using var forced = new HttpRequestMessage(
            HttpMethod.Post, $"/cuentas/{administratorId}/reseteo-de-contrasena")
        {
            Content = JsonContent.Create(new PasswordResetRequest(administratorId)),
        };
        forced.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Bearer", await AccessTokenAsync());

        using var rejection = await data.SendAsync(forced);
        _output.WriteLine($"reseteo forzado contra el servicio de datos: {(int)rejection.StatusCode}");
        Assert.Equal(HttpStatusCode.Conflict, rejection.StatusCode);
    }

    // ---- NINGUNA RESPUESTA SERVIDA AL NAVEGADOR CONTIENE EL TESTIGO DE SESIÓN ----

    /// <summary>
    /// Comparado contra el testigo LITERAL que el servicio de datos emitió, y no sólo contra una
    /// forma: la comparación por forma no distingue «no está» de «está escrito de otro modo».
    /// </summary>
    [Fact]
    public async Task NoResponseServedToTheBrowserCarriesTheSessionToken()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();

        var mark = await SignInAsAdministratorAsync();
        var accountId = await IdOfAsync(StudentEmail);

        // EL TESTIGO LITERAL, el que el almacén del servidor guarda para esta sesión.
        var token = SessionTokenOf(mark);
        _output.WriteLine($"testigo guardado del lado del servidor: {token.Length} caracteres");

        var seen = new List<(string Step, string Payload)>();

        void Record(string step, HttpResponseMessage response, string body)
        {
            seen.Add((step, body));
            seen.Add((step + " · cabeceras", HeadersOf(response)));
        }

        using (var panel = await GetAsync(AccountsRoute, mark))
        {
            Record("panel de cuentas", panel, Read(await panel.Content.ReadAsStringAsync()));

            var panelHtml = seen[0].Payload;

            using var enabled = await PostStandingAsync(panel, panelHtml, accountId, mark);
            Record("habilitación con provisoria a la vista", enabled, Read(await enabled.Content.ReadAsStringAsync()));
        }

        using (var dialog = await GetAsync($"{AccountsRoute}?baja={accountId}", mark))
        {
            Record("diálogo de baja", dialog, Read(await dialog.Content.ReadAsStringAsync()));
        }

        using (var reset = await GetAsync($"{AccountsRoute}?reseteo={accountId}", mark))
        {
            var resetHtml = Read(await reset.Content.ReadAsStringAsync());
            Record("diálogo de reseteo", reset, resetHtml);

            using var applied = await PostResetAsync(reset, resetHtml, accountId, mark);
            Record("reseteo con provisoria a la vista", applied, Read(await applied.Content.ReadAsStringAsync()));
        }

        using (var registration = await _browser.GetAsync(RegistrationRoute))
        {
            Record("registro de cuenta", registration, Read(await registration.Content.ReadAsStringAsync()));
        }

        foreach (var (step, payload) in seen)
        {
            _output.WriteLine($"{step,-42} {payload.Length} caracteres");
            Assert.DoesNotContain(token, payload, StringComparison.Ordinal);
            Assert.DoesNotMatch(SignedAccessShape, payload);
            Assert.DoesNotContain("accessToken", payload, StringComparison.OrdinalIgnoreCase);
        }

        // LA PRUEBA NO PASA POR AUSENCIA: el mismo instrumento SÍ reconoce el testigo cuando está.
        Assert.Contains(token, $"lo que el almacén guarda es {token}", StringComparison.Ordinal);
    }

    // ---- Andamiaje ----

    private void Trace(string step, HttpResponseMessage response) =>
        _output.WriteLine($"{step,-46} {(int)response.StatusCode} {response.Headers.Location?.OriginalString}");

    private static string HeadersOf(HttpResponseMessage response) =>
        string.Join("\n", response.Headers.Select(header => $"{header.Key}: {string.Join(", ", header.Value)}"));

    private static int CountOf(string html, string needle)
    {
        var count = 0;
        var index = html.IndexOf(needle, StringComparison.Ordinal);

        while (index >= 0)
        {
            count++;
            index = html.IndexOf(needle, index + needle.Length, StringComparison.Ordinal);
        }

        return count;
    }

    /// <summary>La provisoria, leída del campo de sólo lectura que el bloque de comunicación dibuja.</summary>
    private static string ProvisionalOf(string html) =>
        Regex.Match(html, "id=\"accounts-provisional\"[^>]*value=\"(?<value>[^\"]+)\"").Groups["value"].Value;

    private async Task ConfigureAdministratorAsync()
    {
        using var data = _dataService.CreateClient();

        using var setup = await data.PostAsJsonAsync(
            "/cuentas/administrador",
            new AdministratorSetupRequest(AdministratorEmail, "Ana", "Rossi", AdministratorPassword));

        Assert.Equal(HttpStatusCode.Created, setup.StatusCode);
    }

    /// <summary>Registra al alumno POR LA SUPERFICIE PÚBLICA, que es lo que se está verificando.</summary>
    private async Task RegisterStudentAsync()
    {
        using var page = await _browser.GetAsync(RegistrationRoute);
        var html = Read(await page.Content.ReadAsStringAsync());

        using var registered = await PostRegistrationAsync(page, html, StudentEmail, StudentFirstName, StudentLastName);
        Assert.Equal(HttpStatusCode.OK, registered.StatusCode);
        Assert.Equal("Pending", await StatusOfAsync(StudentEmail));
    }

    private async Task<string> SignInAsAdministratorAsync()
    {
        var (response, mark) = await SignInAsync(AdministratorEmail, AdministratorPassword);
        using (response)
        {
            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
            Assert.NotEmpty(mark);
        }

        return mark;
    }

    /// <summary>Un acceso firmado del administrador, para forzar la solicitud contra el servicio.</summary>
    private async Task<string> AccessTokenAsync()
    {
        using var data = _dataService.CreateClient();
        using var exchange = await data.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(AdministratorEmail, AdministratorPassword));

        Assert.Equal(HttpStatusCode.OK, exchange.StatusCode);
        var session = await exchange.Content.ReadFromJsonAsync<SessionResponse>();
        return session!.AccessToken;
    }

    private async Task<HttpResponseMessage> PostRegistrationAsync(
        HttpResponseMessage page, string html, string email, string firstName, string lastName)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, RegistrationRoute)
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "account-registration",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
                ["Input.Email"] = email,
                ["Input.FirstName"] = firstName,
                ["Input.LastName"] = lastName,
            }),
        };
        request.Headers.Add("Cookie", AntiforgeryMarkOf(page));

        return await _browser.SendAsync(request);
    }

    private Task<HttpResponseMessage> PostStandingAsync(
        HttpResponseMessage page, string html, Guid accountId, string mark) =>
        PostPanelAsync(
            page, html, mark,
            AccountsRoute,
            "account-standing-" + accountId.ToString("N", CultureInfo.InvariantCulture),
            new Dictionary<string, string>());

    private Task<HttpResponseMessage> PostResetAsync(
        HttpResponseMessage page, string html, Guid accountId, string mark) =>
        PostPanelAsync(
            page, html, mark, $"{AccountsRoute}?reseteo={accountId}", "account-reset",
            new Dictionary<string, string>());

    private Task<HttpResponseMessage> PostDeletionAsync(
        HttpResponseMessage page, string html, Guid accountId, string confirmationEmail, string mark) =>
        PostPanelAsync(
            page, html, mark, $"{AccountsRoute}?baja={accountId}", "account-deletion",
            new Dictionary<string, string> { ["Deletion.ConfirmationEmail"] = confirmationEmail });

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

        // Las dos marcas van juntas: la de sesión y la de antifalsificación.
        request.Headers.Add("Cookie", $"{mark}; {AntiforgeryMarkOf(page)}");

        return await _browser.SendAsync(request);
    }

    private async Task<HttpResponseMessage> PostForcedChangeAsync(
        HttpResponseMessage page, string html, string provisional)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "/credencial-propia/cambio-obligado")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "forced-password-change",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
                ["Input.Email"] = StudentEmail,
                ["Input.ProvisionalPassword"] = provisional,
                ["Input.NewPassword"] = ChosenPassword,
                ["Input.NewPasswordRepeat"] = ChosenPassword,
            }),
        };
        request.Headers.Add("Cookie", AntiforgeryMarkOf(page));

        return await _browser.SendAsync(request);
    }

    private async Task<(HttpResponseMessage Response, string Mark)> SignInAsync(string email, string password)
    {
        using var page = await _browser.GetAsync("/ingreso");
        var html = Read(await page.Content.ReadAsStringAsync());

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

    private async Task<HttpResponseMessage> GetAsync(string route, string? mark)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, route);

        if (!string.IsNullOrEmpty(mark))
        {
            request.Headers.Add("Cookie", mark);
        }

        return await _browser.SendAsync(request);
    }

    // ---- Lo que se lee DEL ALMACÉN, y nunca de una respuesta ----

    private async Task<string?> StatusOfAsync(string email) =>
        (await ScalarOfAsync("select Status from Account where NormalizedEmail = $email", email))?.ToString();

    private async Task<bool> MarkOfAsync(string email) =>
        (await ScalarOfAsync("select MustChangePassword from Account where NormalizedEmail = $email", email))
            ?.ToString() == "1";

    private async Task<Guid> IdOfAsync(string email) =>
        Guid.Parse((await ScalarOfAsync("select Id from Account where NormalizedEmail = $email", email))!.ToString()!);

    private async Task<object?> ScalarOfAsync(string sql, string email)
    {
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("$email", EmailIdentity.Normalize(email));

        return await command.ExecuteScalarAsync();
    }

    private static string AntiforgeryTokenOf(string html) =>
        Regex.Match(html, "name=\"__RequestVerificationToken\" value=\"(?<token>[^\"]+)\"").Groups["token"].Value;

    /// <summary>La marca de antifalsificación vigente, actualizada cuando la respuesta trae una.</summary>
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

    /// <summary>
    /// El texto tal como la persona lo lee, y no como el marcado lo transporta.
    /// </summary>
    /// <remarks>
    /// HACE FALTA, Y NO ES UNA COMODIDAD. El render estático del marco escribe las expresiones
    /// dinámicas con las no-ASCII **escapadas** —`est&#xE1;`— y el marcado literal sin escapar. Un
    /// mismo texto castellano, por lo tanto, se compara distinto según de dónde salga, y comparar
    /// contra el marcado crudo haría que estas pruebas midieran la codificación en lugar del
    /// texto. Se decodifica y se compara contra lo que la persona ve.
    /// </remarks>
    private static string Read(string html) => WebUtility.HtmlDecode(html);

    /// <summary>
    /// El testigo firmado que el almacén del servidor guarda para esta sesión, obtenido tal como
    /// el producto lo obtiene: descifrando la marca y leyendo su identificador opaco.
    /// </summary>
    /// <remarks>
    /// NO SE LO PIDE PRESTADO A NINGÚN ATAJO. Se toma el formato de la marca que la propia pieza
    /// configuró, se la descifra con él, se lee el identificador opaco —que es lo único que el
    /// navegador conserva— y con ese identificador se le pregunta al almacén. Es el único camino
    /// por el que el testigo se puede leer del lado del servidor, y por eso sirve para afirmar
    /// que **ese valor** no aparece en ninguna respuesta.
    /// </remarks>
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
}
