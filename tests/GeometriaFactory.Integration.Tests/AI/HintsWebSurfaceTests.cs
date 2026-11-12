using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.AI;
using GeometriaFactory.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// La pantalla de envío con y sin el flag de pistas (expediente 0002, tramo T6, D-FLAG y D-NIVEL3).
/// </summary>
public sealed class HintsWebSurfaceTests : IDisposable
{
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";
    private const string StudentEmail = "alumna@frre.utn.edu.ar";
    private const string ChosenPassword = "la-que-elijo-yo-ahora";
    private const string NewWorkRoute = "/trabajo-nuevo";

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private DataServiceHarness? _dataService;
    private PublicPieceHarness? _publicPiece;
    private HttpClient? _browser;
    private string _antiforgeryMark = string.Empty;

    public void Dispose()
    {
        _browser?.Dispose();
        _publicPiece?.Dispose();
        _dataService?.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }

    /// <summary>
    /// Con la configuración por omisión la pantalla de resultado no ofrece pistas, no trae el
    /// formulario y no llama a la ruta nueva: queda como antes de la capa de IA.
    /// </summary>
    [Fact]
    public async Task ByDefault_TheResultHasNoHintsButtonAndAsksNothing()
    {
        var calls = new CountingWriter();
        Start(hintsEnabled: null, services => services.AddScoped<IObservationHintWriter>(_ => calls));

        var (page, html) = await SubmitAsync(Scenarios.E1);
        using (page)
        {
            Assert.Contains("Tu trabajo quedó en estado", html, StringComparison.Ordinal);
            Assert.Contains("Advertencia", html, StringComparison.Ordinal);
            Assert.DoesNotContain("Ver pistas", html, StringComparison.Ordinal);
            Assert.DoesNotContain("gf-hints", html, StringComparison.Ordinal);
            Assert.DoesNotContain("work-hints", html, StringComparison.Ordinal);
            Assert.Equal(0, calls.Count);
        }
    }

    /// <summary>
    /// Con el flag encendido: un botón por advertencia; el nivel 1 al pedir, el 2 con «Otra pista» y
    /// el 3 sólo con «Ver la última pista». Lo que no se pidió no está en el marcado.
    /// </summary>
    [Fact]
    public async Task WithTheFlag_TheLevelsAppearOnlyWhenAskedFor()
    {
        Start(hintsEnabled: "true", services => services.AddScoped<IObservationHintWriter>(_ => new ScriptedWriter(
            new Hints("PRIMERA pista", "SEGUNDA pista", "TERCERA pista", HintOrigin.Model))));

        var (page, html) = await SubmitAsync(Scenarios.E1);
        using (page)
        {
            Assert.Equal(2, Regex.Matches(html, "Ver pistas").Count);

            using var level1 = await PostHintsAsync(page, html, Scenarios.E1, index: IndexOfFirstHintButton(html), level: 1);
            var first = Read(await level1.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, level1.StatusCode);
            Assert.Contains("PRIMERA pista", first, StringComparison.Ordinal);
            Assert.Contains("Pista redactada por IA: puede no ser exacta", first, StringComparison.Ordinal);
            // G4: la marca también en el texto accesible de cada nivel.
            Assert.Contains("Pista 1 de 3. Pista redactada por IA: puede no ser exacta.", first, StringComparison.Ordinal);
            // G3: la advertencia completa queda arriba de las pistas.
            Assert.Contains("Tu programa declara", first, StringComparison.Ordinal);
            Assert.Contains("La geometría da", first, StringComparison.Ordinal);
            Assert.True(
                first.IndexOf("La geometría da", StringComparison.Ordinal) < first.IndexOf("PRIMERA pista", StringComparison.Ordinal),
                "La advertencia tiene que ir arriba de las pistas.");
            // G5: el foco va a la pista nueva y la región viva está en el marcado.
            Assert.Matches("<li[^>]*autofocus[^>]*id=\"hints-level-1\"|<li[^>]*id=\"hints-level-1\"[^>]*autofocus", first);
            Assert.Contains("aria-live=\"polite\"", first, StringComparison.Ordinal);
            Assert.Contains("data-gf-pending=\"Buscando una pista…\"", first, StringComparison.Ordinal);
            // G6: la línea fija de acción.
            Assert.Contains("Usá la pista para revisar tu programa.", first, StringComparison.Ordinal);
            Assert.DoesNotContain("SEGUNDA pista", first, StringComparison.Ordinal);
            Assert.DoesNotContain("TERCERA pista", first, StringComparison.Ordinal);
            Assert.Contains("Otra pista", first, StringComparison.Ordinal);

            using var level2 = await PostHintsAsync(level1, first, Scenarios.E1, IndexOfFirstHintButton(html), level: 2);
            var second = Read(await level2.Content.ReadAsStringAsync());
            Assert.Contains("SEGUNDA pista", second, StringComparison.Ordinal);
            Assert.DoesNotContain("TERCERA pista", second, StringComparison.Ordinal);
            Assert.Contains("Ver la última pista", second, StringComparison.Ordinal);

            using var level3 = await PostHintsAsync(level2, second, Scenarios.E1, IndexOfFirstHintButton(html), level: 3);
            var third = Read(await level3.Content.ReadAsStringAsync());
            Assert.Contains("TERCERA pista", third, StringComparison.Ordinal);
            Assert.DoesNotContain("Ver la última pista", third, StringComparison.Ordinal);
        }
    }

    /// <summary>Una pista con marcado se dibuja codificada, nunca como marcado.</summary>
    [Fact]
    public async Task AHintWithMarkupIsShownEscaped()
    {
        Start(hintsEnabled: "true", services => services.AddScoped<IObservationHintWriter>(_ => new ScriptedWriter(
            new Hints("<script>alert(1)</script>", "b", "c", HintOrigin.Template))));

        var (page, html) = await SubmitAsync(Scenarios.E1);
        using (page)
        {
            using var hints = await PostHintsAsync(page, html, Scenarios.E1, IndexOfFirstHintButton(html), level: 1);
            var raw = await hints.Content.ReadAsStringAsync();

            Assert.DoesNotContain("<script>alert(1)</script>", raw, StringComparison.Ordinal);
            Assert.Contains("&lt;script&gt;", raw, StringComparison.Ordinal);
            Assert.Contains("Pista general del laboratorio", raw, StringComparison.Ordinal);
        }
    }

    /// <summary>
    /// G1 · Un modelo que no responde nunca: el servicio de datos corta en su tope, antes que la Web,
    /// y la alumna recibe la pista general del laboratorio en lugar de «No pudimos traer pistas».
    /// </summary>
    [Fact]
    public async Task ASlowModel_StillGivesTheLabHint()
    {
        Start(hintsEnabled: "true", services => services.AddScoped<IObservationHintWriter>(provider => new ModelObservationHintWriter(
            FakeChatClient.Delaying(), new HintConcurrencyGate(), new AiOptions { TimeoutSeconds = 1 },
            new CapturingLogger<ModelObservationHintWriter>(), includeContentInTelemetry: false)));

        var (page, html) = await SubmitAsync(Scenarios.E1);
        using (page)
        {
            using var hints = await PostHintsAsync(page, html, Scenarios.E1, IndexOfFirstHintButton(html), level: 1);
            var shown = Read(await hints.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, hints.StatusCode);
            Assert.Contains("Pista general del laboratorio", shown, StringComparison.Ordinal);
            Assert.DoesNotContain("No pudimos traer pistas", shown, StringComparison.Ordinal);
        }
    }

    /// <summary>
    /// G2 · Las pistas se piden desde el trabajo guardado, sin reenviarlo: el detalle ofrece «Ver
    /// pistas» por advertencia, las dibuja adentro de esa advertencia con sus valores a la vista, y el
    /// almacén no gana ningún trabajo.
    /// </summary>
    [Fact]
    public async Task FromTheSavedWork_HintsComeWithoutResubmitting()
    {
        Start(hintsEnabled: "true", services => services.AddScoped<IObservationHintWriter>(_ => new ScriptedWriter(
            new Hints("PRIMERA pista", "SEGUNDA pista", "TERCERA pista", HintOrigin.Template))));

        var (page, html) = await SubmitAsync(Scenarios.E1);
        page.Dispose();
        var detailRoute = Regex.Match(html, "href=\"(?<r>/trabajos/[0-9a-f-]{36})\"").Groups["r"].Value;
        Assert.NotEmpty(detailRoute);

        using var before = await GetAsync("/mis-trabajos", _mark);
        var listedBefore = Regex.Matches(Read(await before.Content.ReadAsStringAsync()), "Trabajo con advertencias").Count;

        using var detail = await GetAsync(detailRoute, _mark);
        var detailHtml = Read(await detail.Content.ReadAsStringAsync());
        Assert.Equal(2, Regex.Matches(detailHtml, "Ver pistas").Count);
        Assert.DoesNotContain("OriginalJson", detailHtml, StringComparison.Ordinal);
        var step = Regex.Match(detailHtml, "name=\"HintsInput.Step\" value=\"(?<s>\\d+:1)\"").Groups["s"].Value;
        Assert.NotEmpty(step);

        using var asked = await PostAsync(detail, detailHtml, detailRoute, "work-hints", new Dictionary<string, string>
        {
            ["HintsInput.Step"] = step,
        });
        var shown = Read(await asked.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, asked.StatusCode);
        Assert.Contains("PRIMERA pista", shown, StringComparison.Ordinal);
        Assert.Contains("Pista general del laboratorio", shown, StringComparison.Ordinal);
        Assert.DoesNotContain("SEGUNDA pista", shown, StringComparison.Ordinal);
        Assert.Contains("Otra pista", shown, StringComparison.Ordinal);
        Assert.True(
            shown.IndexOf("La geometría da", StringComparison.Ordinal) < shown.IndexOf("PRIMERA pista", StringComparison.Ordinal),
            "La advertencia tiene que seguir arriba de las pistas.");
        // La otra advertencia sigue ofreciendo sus pistas.
        Assert.Single(Regex.Matches(shown, "Ver pistas"));

        // 0 REENVÍOS Y 0 DUPLICADOS: la alumna sigue teniendo un solo trabajo.
        using var mine = await GetAsync("/mis-trabajos", _mark);
        var list = Read(await mine.Content.ReadAsStringAsync());
        Assert.Equal(listedBefore, Regex.Matches(list, "Trabajo con advertencias").Count);
    }

    /// <summary>Con el flag apagado, el detalle no ofrece pistas.</summary>
    [Fact]
    public async Task ByDefault_TheSavedWorkHasNoHintsButton()
    {
        var calls = new CountingWriter();
        Start(hintsEnabled: null, services => services.AddScoped<IObservationHintWriter>(_ => calls));

        var (page, html) = await SubmitAsync(Scenarios.E1);
        page.Dispose();
        var detailRoute = Regex.Match(html, "href=\"(?<r>/trabajos/[0-9a-f-]{36})\"").Groups["r"].Value;

        using var detail = await GetAsync(detailRoute, _mark);
        var detailHtml = Read(await detail.Content.ReadAsStringAsync());

        Assert.Contains("Advertencia", detailHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("Ver pistas", detailHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("gf-hints", detailHtml, StringComparison.Ordinal);
        Assert.Equal(0, calls.Count);
    }

    // ------------------------------------------------- EXP-0005: el chip abre la acción ----------------

    /// <summary>
    /// D-3 · <c>?pistas=1</c> abre el nivel 1 de la PRIMERA advertencia dentro de esa advertencia, con el foco en la
    /// pista y sin reenviar el trabajo; la otra advertencia sigue ofreciendo su «Ver pistas».
    /// </summary>
    [Fact]
    public async Task WorkView_ConPistas1_AbrePanelYEnfoca()
    {
        var calls = new CountingScriptedWriter(new Hints("PRIMERA pista", "SEGUNDA pista", "TERCERA pista", HintOrigin.Template));
        Start(hintsEnabled: "true", services => services.AddScoped<IObservationHintWriter>(_ => calls));
        var detailRoute = await SavedWorkRouteAsync(Scenarios.E1);

        using var detail = await GetAsync(detailRoute + "?pistas=1", _mark);
        var html = Read(await detail.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, detail.StatusCode);
        Assert.Contains("PRIMERA pista", html, StringComparison.Ordinal);
        Assert.DoesNotContain("SEGUNDA pista", html, StringComparison.Ordinal);
        Assert.Contains("Otra pista", html, StringComparison.Ordinal);
        Assert.Matches("<li[^>]*autofocus[^>]*id=\"hints-\\d+-level-1\"|<li[^>]*id=\"hints-\\d+-level-1\"[^>]*autofocus", html);
        Assert.True(
            html.IndexOf("La geometría da", StringComparison.Ordinal) < html.IndexOf("PRIMERA pista", StringComparison.Ordinal),
            "La advertencia tiene que seguir arriba de las pistas.");
        Assert.Single(Regex.Matches(html, "Ver pistas"));
        Assert.Contains("<title>Pistas para tu advertencia</title>", html, StringComparison.Ordinal);
    }

    /// <summary>D-3 · Sobre un trabajo sin advertencias, <c>?pistas=1</c> deja la página igual que sin el parámetro.</summary>
    [Fact]
    public async Task WorkView_ConPistas1_SinAdvertencias_IgualQueSinParametro()
    {
        var calls = new CountingWriter();
        Start(hintsEnabled: "true", services => services.AddScoped<IObservationHintWriter>(_ => calls));
        var detailRoute = await SavedWorkRouteAsync(Scenarios.E4);

        Assert.Equal(await NormalizedAsync(detailRoute), await NormalizedAsync(detailRoute + "?pistas=1"));
        Assert.Equal(0, calls.Count);
    }

    /// <summary>
    /// D-3 · Con las pistas apagadas (configuración por omisión), <c>?pistas=1</c> sobre un trabajo CON advertencias
    /// tampoco cambia nada y no pide nada: el enlace no abre lo que la pantalla no ofrece.
    /// </summary>
    [Fact]
    public async Task WorkView_ConPistas1_PistasApagadas_IgualQueSinParametro()
    {
        var calls = new CountingWriter();
        Start(hintsEnabled: null, services => services.AddScoped<IObservationHintWriter>(_ => calls));
        var detailRoute = await SavedWorkRouteAsync(Scenarios.E1);

        var plain = await NormalizedAsync(detailRoute);
        Assert.Equal(plain, await NormalizedAsync(detailRoute + "?pistas=1"));
        Assert.DoesNotContain("Ver pistas", plain, StringComparison.Ordinal);
        Assert.Equal(0, calls.Count);
    }

    /// <summary>D-3 · Un valor distinto de 1 no abre nada.</summary>
    [Fact]
    public async Task WorkView_ConPistasDeOtroValor_IgualQueSinParametro()
    {
        var calls = new CountingWriter();
        Start(hintsEnabled: "true", services => services.AddScoped<IObservationHintWriter>(_ => calls));
        var detailRoute = await SavedWorkRouteAsync(Scenarios.E1);

        Assert.Equal(await NormalizedAsync(detailRoute), await NormalizedAsync(detailRoute + "?pistas=si"));
        Assert.Equal(0, calls.Count);
    }

    /// <summary>D-2 · <c>?ejemplo=1</c> despliega «Ver un ejemplo» y le da el foco; sin el parámetro, colapsado.</summary>
    [Fact]
    public async Task WorkSubmission_ConEjemplo1_DespliegaYEnfoca()
    {
        Start(hintsEnabled: null, _ => { });
        _mark = await SignInAsStudentAsync();

        using var opened = await GetAsync(NewWorkRoute + "?ejemplo=1", _mark);
        var html = Read(await opened.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, opened.StatusCode);
        Assert.Matches("<details[^>]*id=\"ejemplo-formato\"[^>]*\\sopen", html);
        Assert.Matches("<summary[^>]*autofocus[^>]*>Ver un ejemplo</summary>", html);
    }

    /// <summary>D-2 · Sin el parámetro, o con otro valor, la pantalla de carga queda igual que antes.</summary>
    [Fact]
    public async Task WorkSubmission_SinEjemplo1_IgualQueSinParametro()
    {
        Start(hintsEnabled: null, _ => { });
        _mark = await SignInAsStudentAsync();

        var plain = await NormalizedAsync(NewWorkRoute);
        Assert.DoesNotMatch("<details[^>]*\\sopen", plain);
        Assert.DoesNotContain("autofocus", plain, StringComparison.Ordinal);
        Assert.Equal(plain, await NormalizedAsync(NewWorkRoute + "?ejemplo=0"));
    }

    private async Task<string> SavedWorkRouteAsync(string text)
    {
        var (page, html) = await SubmitAsync(text);
        page.Dispose();
        var route = Regex.Match(html, "href=\"(?<r>/trabajos/[0-9a-f-]{36})\"").Groups["r"].Value;
        Assert.NotEmpty(route);
        return route;
    }

    /// <summary>
    /// El marcado de una página sin lo que cambia en cada pedido: las marcas protegidas y el eco de la propia
    /// dirección (el <c>action</c> de los formularios repite la consulta tal como llegó).
    /// </summary>
    private async Task<string> NormalizedAsync(string route)
    {
        using var response = await GetAsync(route, _mark);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var bare = route.Split('?')[0];
        var html = Read(await response.Content.ReadAsStringAsync())
            .Replace(route, bare, StringComparison.Ordinal)
            .Replace(Uri.EscapeDataString(route), Uri.EscapeDataString(bare), StringComparison.Ordinal);
        // Toda marca protegida por la Web (antifalsificación y estado del formulario) empieza con el prefijo de
        // Data Protection y cambia en cada pedido.
        return Regex.Replace(html, "CfDJ8[A-Za-z0-9_\\-+/=]+", "MARCA");
    }

    private void Start(string? hintsEnabled, Action<IServiceCollection> services)
    {
        _dataService = new AiDataServiceHarness(_storePath, services);
        var settings = new Dictionary<string, string?>();
        if (hintsEnabled is not null)
        {
            settings["AI:HintsEnabled"] = hintsEnabled;
        }

        _publicPiece = new PublicPieceHarness(_dataService.Server.CreateHandler(), settings);
        _browser = _publicPiece.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            HandleCookies = false,
            AllowAutoRedirect = false,
        });
    }

    private async Task<(HttpResponseMessage Page, string Html)> SubmitAsync(string text)
    {
        var mark = await SignInAsStudentAsync();
        _mark = mark;

        using var form = await GetAsync(NewWorkRoute, mark);
        var formHtml = Read(await form.Content.ReadAsStringAsync());

        var sent = await PostAsync(form, formHtml, NewWorkRoute, "work-submission", new Dictionary<string, string>
        {
            ["Input.Name"] = "Trabajo con advertencias",
            ["Input.DeclaredDate"] = "2026-08-09",
            ["Input.Description"] = string.Empty,
            ["Input.OriginalJson"] = text,
        });
        Assert.Equal(HttpStatusCode.OK, sent.StatusCode);

        return (sent, Read(await sent.Content.ReadAsStringAsync()));
    }

    private string _mark = string.Empty;

    private Task<HttpResponseMessage> PostHintsAsync(HttpResponseMessage page, string html, string text, int index, int level) =>
        PostAsync(page, html, NewWorkRoute, "work-hints", new Dictionary<string, string>
        {
            ["HintsInput.OriginalJson"] = text,
            ["HintsInput.Index"] = index.ToString(System.Globalization.CultureInfo.InvariantCulture),
            ["HintsInput.Level"] = level.ToString(System.Globalization.CultureInfo.InvariantCulture),
        });

    private static int IndexOfFirstHintButton(string html) =>
        int.Parse(
            Regex.Match(html, "name=\"HintsInput.Index\" value=\"(?<i>\\d+)\"").Groups["i"].Value,
            System.Globalization.CultureInfo.InvariantCulture);

    private async Task<string> SignInAsStudentAsync()
    {
        using var data = _dataService!.CreateClient();
        using (var setup = await data.PostAsJsonAsync("/v1/cuentas/administrador",
            new AdministratorSetupRequest(AdministratorEmail, "Ana", "Rossi", AdministratorPassword)))
        {
            Assert.Equal(HttpStatusCode.Created, setup.StatusCode);
        }

        using var registration = await data.PostAsJsonAsync("/v1/cuentas", new AccountRegistrationRequest(StudentEmail, "Ana", "Diaz"));
        var accountId = (await registration.Content.ReadFromJsonAsync<AccountRegistrationResponse>())!.AccountId;

        using var exchange = await data.PostAsJsonAsync("/v1/auth/token", new CredentialExchangeRequest(AdministratorEmail, AdministratorPassword));
        var administrator = (await exchange.Content.ReadFromJsonAsync<SessionResponse>())!.AccessToken;

        using var enabling = new HttpRequestMessage(HttpMethod.Post, $"/v1/cuentas/{accountId}/situacion")
        {
            Content = JsonContent.Create(new AccountStatusChangeRequest(accountId, nameof(AccountStatus.Enabled))),
        };
        enabling.Headers.Authorization = new AuthenticationHeaderValue("Bearer", administrator);
        using var enabled = await data.SendAsync(enabling);
        var provisional = (await enabled.Content.ReadFromJsonAsync<AccountStatusChangeResponse>())!.ProvisionalPassword!;

        using (var change = await data.PostAsJsonAsync("/v1/cuenta/contrasena", new OwnPasswordChangeRequest(provisional, ChosenPassword, StudentEmail)))
        {
            Assert.Equal(HttpStatusCode.OK, change.StatusCode);
        }

        using var page = await _browser!.GetAsync("/ingreso");
        var html = Read(await page.Content.ReadAsStringAsync());
        using var request = new HttpRequestMessage(HttpMethod.Post, "/ingreso")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "sign-in",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
                ["Input.Email"] = StudentEmail,
                ["Input.Password"] = ChosenPassword,
            }),
        };
        request.Headers.Add("Cookie", AntiforgeryMarkOf(page));

        using var response = await _browser.SendAsync(request);
        Assert.Equal(HttpStatusCode.Found, response.StatusCode);

        return response.Headers.GetValues("Set-Cookie")
            .Select(cookie => cookie.Split(';')[0])
            .First(cookie => cookie.StartsWith(SessionCookieDefaults.CookieName + "=", StringComparison.Ordinal));
    }

    private async Task<HttpResponseMessage> GetAsync(string route, string mark)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, route);
        request.Headers.Add("Cookie", mark);
        return await _browser!.SendAsync(request);
    }

    private async Task<HttpResponseMessage> PostAsync(
        HttpResponseMessage page, string html, string route, string handler, IReadOnlyDictionary<string, string> fields)
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

        using var request = new HttpRequestMessage(HttpMethod.Post, route) { Content = new FormUrlEncodedContent(payload) };
        request.Headers.Add("Cookie", $"{_mark}; {AntiforgeryMarkOf(page)}");

        return await _browser!.SendAsync(request);
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

    private static string Read(string html) => WebUtility.HtmlDecode(html);

    private sealed class ScriptedWriter(Hints hints) : IObservationHintWriter
    {
        public Task<HintResult> WriteAsync(ObservationHintRequest request, CancellationToken cancellationToken = default) =>
            Task.FromResult(hints.Origin == HintOrigin.Model ? HintResult.Ok(hints) : HintResult.Degraded(hints, "guion"));

        public Hints TemplateFor(ObservationHintRequest request) => hints;
    }

    private sealed class CountingScriptedWriter(Hints hints) : IObservationHintWriter
    {
        public int Count { get; private set; }

        public Task<HintResult> WriteAsync(ObservationHintRequest request, CancellationToken cancellationToken = default)
        {
            Count++;
            return Task.FromResult(HintResult.Degraded(hints, "guion"));
        }

        public Hints TemplateFor(ObservationHintRequest request) => hints;
    }

    private sealed class CountingWriter : IObservationHintWriter
    {
        public int Count { get; private set; }

        public Task<HintResult> WriteAsync(ObservationHintRequest request, CancellationToken cancellationToken = default)
        {
            Count++;
            return Task.FromResult(HintResult.Degraded(TemplateFor(request), "conteo"));
        }

        public Hints TemplateFor(ObservationHintRequest request)
        {
            Count++;
            return new Hints("a", "b", "c", HintOrigin.Template);
        }
    }
}
