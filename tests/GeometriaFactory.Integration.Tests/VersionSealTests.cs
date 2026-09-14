using System.Net;
using GeometriaFactory.Web.Services;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// CMP-09 · R-14 — EL SELLO MUESTRA LA VERSIÓN QUE TRAE LA CONSTRUCCIÓN, y el marcador de origen
/// indeterminado sólo cuando no la trae (`Representacion-Sello-De-Version.md` §3 y §4).
/// </summary>
public sealed class VersionSealTests : IDisposable
{
    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly PublicPieceHarness _publicPiece;

    public VersionSealTests()
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

    [Theory]
    [InlineData("1.1.3-alpha.0.15+cc024c55364d63ad438e868317e436c12c6e10d5", "1.1.3-alpha.0.15", "cc024c55364d63ad438e868317e436c12c6e10d5", true)]
    [InlineData("1.2.0+a3f81c6", "1.2.0", "a3f81c6", false)]
    [InlineData("1.2.0", "1.2.0", null, false)]
    public void TheVersionArrivesFormedAndTheBuildIdStaysApart(string informational, string version, string? build, bool preliminary)
    {
        var identity = VersionIdentity.From(informational);

        Assert.Equal(version, identity.Version);
        Assert.Equal(build, identity.BuildId);
        Assert.Equal(preliminary, identity.IsPreliminary);
        Assert.False(identity.IsIndeterminate);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("+cc024c5")]
    [InlineData("0.0.0-alpha.0.3+cc024c5")]
    public void WithoutAnIdentityFromTheBuildTheSealIsIndeterminate(string? informational)
    {
        Assert.True(VersionIdentity.From(informational).IsIndeterminate);
    }

    /// <summary>La superficie de acceso, sin sesión: la ubicación que más se necesita (§3).</summary>
    [Fact]
    public async Task TheAccessShellShowsTheIdentityOfTheRunningBuild()
    {
        var expected = VersionIdentity.OfAssembly(typeof(VersionIdentity).Assembly);
        using var browser = _publicPiece.CreateClient();

        using var page = await browser.GetAsync("/aprovisionamiento-inicial");
        var html = WebUtility.HtmlDecode(await page.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, page.StatusCode);

        if (expected.IsIndeterminate)
        {
            Assert.Contains("Versión no identificada", html, StringComparison.Ordinal);
            return;
        }

        Assert.Contains($"Versión {expected.Version}", html, StringComparison.Ordinal);
        Assert.DoesNotContain("Versión no identificada", html, StringComparison.Ordinal);
        Assert.Equal(expected.IsPreliminary, html.Contains(">preliminar<", StringComparison.Ordinal));

        // Identidad, nunca topología (§4): ni la dirección del servicio de datos ni el identificador de construcción.
        Assert.DoesNotContain("el-servicio-de-datos", html, StringComparison.Ordinal);
        if (expected.BuildId is not null)
        {
            Assert.DoesNotContain(expected.BuildId, html, StringComparison.Ordinal);
        }
    }
}
