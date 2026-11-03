using System.Net;
using GeometriaFactory.Api.Composition;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// Prueba negativa PAREADA de la llave `Documentacion:Publicada` (expediente 0003, tramo T-8).
/// </summary>
/// <remarks>
/// CADA CASO NEGATIVO TIENE SU PAR POSITIVO EN EL MISMO AMBIENTE. Un 404 solo no prueba que la
/// llave gobierne la publicación: también lo daría una ruta mal escrita o un explorador que dejó
/// de registrarse. Por eso, para cada ambiente fuera de desarrollo, la misma fábrica da 404 con
/// la llave apagada (ausente o `false` dicho explícitamente) y 200 con la llave en `true`. La
/// única variable entre los dos lados del par es la llave.
///
/// COMPLEMENTA, NO REPITE, a `ApiDocumentationSurfaceTests`: allá se mide Production sin la llave
/// y con ella; acá se suma el `false` explícito y un segundo ambiente (`Staging`), para que la
/// regla dependa de «no es desarrollo» y no del nombre `Production`.
/// </remarks>
public sealed class ApiDocumentationPublishedSettingPairTests : IDisposable
{
    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;

    public ApiDocumentationPublishedSettingPairTests()
    {
        _dataService = new DataServiceHarness(_storePath);
    }

    [Theory]
    [InlineData("Production", null)]
    [InlineData("Production", "false")]
    [InlineData("Staging", null)]
    [InlineData("Staging", "false")]
    public async Task WithTheSettingOffNeitherRouteIsServedAndWithItOnBothAre(string environment, string? off)
    {
        // Lado negativo: la llave ausente o apagada.
        using (var hidden = _dataService.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment(environment);
            if (off is not null)
            {
                builder.UseSetting(ApiDocumentation.PublishedSetting, off);
            }
        }))
        {
            Assert.Equal(
                (HttpStatusCode.NotFound, HttpStatusCode.NotFound),
                await ProbeAsync(hidden));
        }

        // Su par positivo: mismo ambiente, sólo cambia la llave.
        using var shown = _dataService.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment(environment);
            builder.UseSetting(ApiDocumentation.PublishedSetting, "true");
        });

        Assert.Equal(
            (HttpStatusCode.OK, HttpStatusCode.OK),
            await ProbeAsync(shown));
    }

    private static async Task<(HttpStatusCode Document, HttpStatusCode Explorer)> ProbeAsync(
        WebApplicationFactory<TwoPhaseStartup> factory)
    {
        using var client = factory.CreateClient();
        using var document = await client.GetAsync(ApiDocumentation.DocumentRoute);
        using var explorer = await client.GetAsync(ApiDocumentation.ExplorerRoute + "/");
        return (document.StatusCode, explorer.StatusCode);
    }

    public void Dispose()
    {
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }
}
