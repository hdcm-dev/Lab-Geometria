using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Domain.Values;
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
            Assert.Contains("Pista automática", first, StringComparison.Ordinal);
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
            Assert.Contains("Pista de plantilla", raw, StringComparison.Ordinal);
        }
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
