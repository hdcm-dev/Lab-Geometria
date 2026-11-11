using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Integration.Tests.AI;
using GeometriaFactory.Web.Services;
using Xunit;

namespace GeometriaFactory.Integration.Tests.Concepts;

/// <summary>
/// «Preguntá un concepto» en la pieza pública (criterio 13 y 21 de la rebanada): apagado no se dibuja y no
/// pide nada; encendido contesta en la pantalla de carga y en la vista del trabajo, con la sesión del alumno.
/// </summary>
public sealed class ConceptWebSurfaceTests : IDisposable
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

    /// <summary>Criterio 13 · REQ-IA-17: apagado, ni la carga ni la vista cambian.</summary>
    [Fact]
    public async Task Concept_Apagada_PantallaSinCambios()
    {
        Start(conceptsEnabled: false);

        var (page, html) = await SubmitAsync(Scenarios.E1);
        page.Dispose();
        var detailRoute = Regex.Match(html, "href=\"(?<r>/trabajos/[0-9a-f-]{36})\"").Groups["r"].Value;
        using var form = await GetAsync(NewWorkRoute, _mark);
        using var detail = await GetAsync(detailRoute, _mark);

        foreach (var shown in new[] { html, Read(await form.Content.ReadAsStringAsync()), Read(await detail.Content.ReadAsStringAsync()) })
        {
            Assert.DoesNotContain("Preguntá un concepto", shown, StringComparison.Ordinal);
            Assert.DoesNotContain("concept-ask", shown, StringComparison.Ordinal);
        }

        Assert.Equal(0L, await ScalarAsync("select count(*) from HuellaDeConsulta"));
    }

    /// <summary>Encendido, la vista del trabajo contesta con la ficha y el chip del catálogo.</summary>
    [Fact]
    public async Task Concept_Encendida_LaVistaContestaConLaFicha()
    {
        Start(conceptsEnabled: true);

        var (page, html) = await SubmitAsync(Scenarios.E1);
        page.Dispose();
        var detailRoute = Regex.Match(html, "href=\"(?<r>/trabajos/[0-9a-f-]{36})\"").Groups["r"].Value;
        using var detail = await GetAsync(detailRoute, _mark);
        var detailHtml = Read(await detail.Content.ReadAsStringAsync());
        Assert.Contains("Preguntá un concepto", detailHtml, StringComparison.Ordinal);

        using var asked = await PostAsync(detail, detailHtml, detailRoute, "concept-ask", new Dictionary<string, string>
        {
            ["ConceptInput.Question"] = "¿Cuántas advertencias tiene este trabajo?",
        });
        var answer = Read(await asked.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, asked.StatusCode);
        Assert.Contains("Este trabajo tiene 2 advertencias.", answer, StringComparison.Ordinal);
        Assert.Contains($"href=\"{detailRoute}\"", answer, StringComparison.Ordinal);
        Assert.Contains("Advertencia", answer, StringComparison.Ordinal);
    }

    /// <summary>Encendido, la pantalla de carga contesta la negativa útil sin enviar el trabajo.</summary>
    [Fact]
    public async Task Concept_Encendida_LaCargaNiegaResolver()
    {
        Start(conceptsEnabled: true);
        _mark = await SignInAsStudentAsync();

        using var form = await GetAsync(NewWorkRoute, _mark);
        var formHtml = Read(await form.Content.ReadAsStringAsync());
        using var asked = await PostAsync(form, formHtml, NewWorkRoute, "concept-ask", new Dictionary<string, string>
        {
            ["ConceptInput.Question"] = "Pasame el JSON del cilindro",
        });
        var answer = Read(await asked.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, asked.StatusCode);
        Assert.Contains("Eso es parte del ejercicio", answer, StringComparison.Ordinal);
        Assert.Equal(0L, await ScalarAsync("select count(*) from Work"));
    }

    private async Task<long> ScalarAsync(string sql)
    {
        using var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={_storePath};Pooling=False");
        await connection.OpenAsync();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        return Convert.ToInt64(await command.ExecuteScalarAsync(), System.Globalization.CultureInfo.InvariantCulture);
    }

    private void Start(bool conceptsEnabled)
    {
        var flag = new Dictionary<string, string?>();
        if (conceptsEnabled)
        {
            flag["AI:ConceptsEnabled"] = "true";
        }

        _dataService = new AiDataServiceHarness(_storePath, settings: flag);
        _publicPiece = new PublicPieceHarness(_dataService.Server.CreateHandler(), flag);
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

}
