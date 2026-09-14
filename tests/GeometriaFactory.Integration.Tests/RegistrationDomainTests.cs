using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Domain.Values;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// R-07 · `Api ADR-00012` — EL AUTORREGISTRO ADMITE SÓLO LOS DOMINIOS QUE EL DESPLIEGUE DECLARA,
/// sobre HTTP de verdad, contra el almacén de verdad y a través del front.
/// </summary>
/// <remarks>
/// LA RESTRICCIÓN LLEGA POR CONFIGURACIÓN, por la misma llave que usa la composición del
/// despliegue, y no sustituyendo ningún servicio. Que sin la llave el registro admite cualquier
/// correo lo sostiene el resto de la batería, que registra con dominios cualesquiera.
/// </remarks>
public sealed class RegistrationDomainTests : IDisposable
{
    private const string AdmittedDomain = "frre.utn.edu.ar";
    private const string RegistrationRoute = "/registro-de-cuenta";

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly PublicPieceHarness _publicPiece;
    private readonly HttpClient _client;
    private readonly HttpClient _browser;

    private string _antiforgeryMark = string.Empty;

    public RegistrationDomainTests()
    {
        _dataService = new DataServiceHarness(_storePath, new Dictionary<string, string?>
        {
            ["Registration:AdmittedEmailDomains:0"] = AdmittedDomain,
        });
        _client = _dataService.CreateClient();
        _publicPiece = new PublicPieceHarness(_dataService.Server.CreateHandler());
        _browser = _publicPiece.CreateClient(new WebApplicationFactoryClientOptions
        {
            HandleCookies = false,
            AllowAutoRedirect = false,
        });
    }

    public void Dispose()
    {
        _browser.Dispose();
        _publicPiece.Dispose();
        _client.Dispose();
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }

    [Fact]
    public async Task AnEmailOutsideTheAdmittedDomainsIsRefusedAndNothingIsStored()
    {
        using var response = await _client.PostAsJsonAsync(
            "/v1/cuentas", new AccountRegistrationRequest("intruso@example.com", "Eva", "Luna"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains(ErrorCode.EmailDomainNotAdmitted, await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
        Assert.Equal(0, await StoredAccountsAsync("intruso@example.com"));
    }

    [Fact]
    public async Task AnEmailOfTheAdmittedDomainRegisters()
    {
        using var response = await _client.PostAsJsonAsync(
            "/v1/cuentas", new AccountRegistrationRequest("alumna@FRRE.utn.edu.ar", "Ana", "Diaz"));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(1, await StoredAccountsAsync("alumna@frre.utn.edu.ar"));
    }

    [Fact]
    public async Task TheRegistrationScreenTellsThePersonTheDomainIsNotAdmitted()
    {
        // SIN ADMINISTRADOR, EL FRONT DESVÍA A LA CONFIGURACIÓN INICIAL y no dibuja el registro.
        // El administrador se configura por su propio camino, que no pasa por la política.
        using (var setup = await _client.PostAsJsonAsync(
            "/v1/cuentas/administrador",
            new AdministratorSetupRequest("docente@frre.utn.edu.ar", "Ana", "Rossi", "la-que-eligio-el-docente")))
        {
            Assert.Equal(HttpStatusCode.Created, setup.StatusCode);
        }

        using var page = await _browser.GetAsync(RegistrationRoute);
        var html = Read(await page.Content.ReadAsStringAsync());

        using var request = new HttpRequestMessage(HttpMethod.Post, RegistrationRoute)
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "account-registration",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
                ["Input.Email"] = "intruso@example.com",
                ["Input.FirstName"] = "Eva",
                ["Input.LastName"] = "Luna",
            }),
        };
        request.Headers.Add("Cookie", AntiforgeryMarkOf(page));

        using var response = await _browser.SendAsync(request);
        var answer = Read(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Ese correo no es de un dominio que el laboratorio admita.", answer, StringComparison.Ordinal);
        Assert.DoesNotContain("Tu cuenta quedó registrada", answer, StringComparison.Ordinal);
        Assert.Equal(0, await StoredAccountsAsync("intruso@example.com"));
    }

    private async Task<long> StoredAccountsAsync(string email)
    {
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "select count(*) from Account where NormalizedEmail = $email";
        command.Parameters.AddWithValue("$email", EmailIdentity.Normalize(email));

        return (long)(await command.ExecuteScalarAsync())!;
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
