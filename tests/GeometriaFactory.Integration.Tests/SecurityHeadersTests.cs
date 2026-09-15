using System.Net;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// `R-06` (mesa del 2026-09-14) resuelto en el origen (mesa del 2026-09-15, D-3): las dos piezas
/// responden con cabeceras de seguridad, y HSTS sólo por `https`.
/// </summary>
public sealed class SecurityHeadersTests : IDisposable
{
    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly PublicPieceHarness _publicPiece;

    public SecurityHeadersTests()
    {
        _dataService = new DataServiceHarness(_storePath);
        _publicPiece = new PublicPieceHarness(_dataService.Server.CreateHandler());
    }

    public void Dispose()
    {
        _publicPiece.Dispose();
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }

    [Fact]
    public async Task TheFrontAnswersWithSecurityHeadersAndNoHstsOverHttp()
    {
        using var browser = _publicPiece.CreateClient();
        using var page = await browser.GetAsync("/ingreso");

        Assert.Equal(HttpStatusCode.OK, page.StatusCode);
        Assert.Equal("nosniff", Header(page, "X-Content-Type-Options"));
        Assert.Equal("DENY", Header(page, "X-Frame-Options"));
        Assert.Contains("frame-ancestors 'none'", Header(page, "Content-Security-Policy"), StringComparison.Ordinal);
        Assert.Contains("form-action 'self'", Header(page, "Content-Security-Policy"), StringComparison.Ordinal);
        Assert.DoesNotContain("script-src", Header(page, "Content-Security-Policy"), StringComparison.Ordinal);
        Assert.False(page.Headers.Contains("Strict-Transport-Security"));
    }

    [Fact]
    public async Task TheDataServiceAnswersWithSecurityHeaders()
    {
        using var client = _dataService.CreateClient();
        using var health = await client.GetAsync("/salud");

        Assert.Equal("nosniff", Header(health, "X-Content-Type-Options"));
        Assert.Equal("DENY", Header(health, "X-Frame-Options"));
        Assert.Equal("no-referrer", Header(health, "Referrer-Policy"));
        Assert.Equal("frame-ancestors 'none'", Header(health, "Content-Security-Policy"));
        Assert.False(health.Headers.Contains("Strict-Transport-Security"));
    }

    [Fact]
    public async Task OverHttpsTheFrontEmitsAShortHstsWithoutPreload()
    {
        using var browser = _publicPiece.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
        });
        using var page = await browser.GetAsync("/ingreso");

        var hsts = Header(page, "Strict-Transport-Security");
        Assert.StartsWith("max-age=", hsts, StringComparison.Ordinal);
        Assert.DoesNotContain("preload", hsts, StringComparison.Ordinal);
        Assert.DoesNotContain("includeSubDomains", hsts, StringComparison.Ordinal);
    }

    private static string Header(HttpResponseMessage response, string name) =>
        response.Headers.TryGetValues(name, out var values) ? string.Join(", ", values) : string.Empty;
}
