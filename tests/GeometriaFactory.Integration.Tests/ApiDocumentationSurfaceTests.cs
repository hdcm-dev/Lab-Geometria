using System.Net;
using GeometriaFactory.Api.Composition;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// La documentación navegable de la superficie HTTP, y sobre todo **cuándo no se publica**.
/// </summary>
/// <remarks>
/// LA MITAD QUE IMPORTA ES LA SEGUNDA. Que el explorador se vea en desarrollo lo nota cualquiera
/// la primera vez que lo abre; que **deje de verse al desplegar** no lo nota nadie hasta que ya
/// está publicado, y para entonces cualquiera que tenga la dirección enumera todos los puntos del
/// servicio con sus formas y sus verbos. El servicio de datos de este producto se expone a
/// Internet, así que esa mitad es la que se ejerce acá.
///
/// EL DOCUMENTO Y EL EXPLORADOR SE MIDEN POR SEPARADO, porque son dos cosas: el documento es el
/// contrato en JSON y el explorador es la página que lo lee. Publicar uno sin el otro sería un
/// resultado a medias que una sola comprobación no distinguiría.
///
/// NO SE VERIFICA EL CONTENIDO DEL DOCUMENTO PUNTO POR PUNTO, y es deliberado: el documento se
/// GENERA desde los puntos declarados, de modo que repetir acá la lista de rutas crearía la
/// segunda fuente que la generación existe para no tener. Lo que sí se comprueba es que describa
/// **la superficie de este producto** y no un documento vacío.
/// </remarks>
public sealed class ApiDocumentationSurfaceTests : IDisposable
{
    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;

    public ApiDocumentationSurfaceTests()
    {
        _dataService = new DataServiceHarness(_storePath);
    }

    // ------------------------------------------------------------------ en desarrollo -------

    [Fact]
    public async Task InDevelopmentTheDocumentAndTheExplorerAreBothServed()
    {
        using var client = _dataService.CreateClient();

        using var document = await client.GetAsync(ApiDocumentation.DocumentRoute);
        Assert.Equal(HttpStatusCode.OK, document.StatusCode);

        // Que describa ESTA superficie: `/salud` y `/trabajos` son puntos del producto, y un
        // documento vacío —que también daría 200— no los tendría.
        var contract = await document.Content.ReadAsStringAsync();
        Assert.Contains("\"/salud\"", contract, StringComparison.Ordinal);
        Assert.Contains("\"/trabajos\"", contract, StringComparison.Ordinal);

        // El explorador vive en una carpeta, así que la ruta sin barra final redirige a ella.
        using var explorer = await client.GetAsync(ApiDocumentation.ExplorerRoute + "/");
        Assert.Equal(HttpStatusCode.OK, explorer.StatusCode);
    }

    // ------------------------------------------------------- fuera de desarrollo ------------

    [Fact]
    public async Task OutsideDevelopmentNeitherIsServedUnlessItIsSaidExplicitly()
    {
        using var deployed = _dataService.WithWebHostBuilder(builder => builder.UseEnvironment("Production"));
        using var client = deployed.CreateClient();

        using var document = await client.GetAsync(ApiDocumentation.DocumentRoute);
        Assert.Equal(HttpStatusCode.NotFound, document.StatusCode);

        using var explorer = await client.GetAsync(ApiDocumentation.ExplorerRoute + "/");
        Assert.Equal(HttpStatusCode.NotFound, explorer.StatusCode);
    }

    [Fact]
    public async Task OutsideDevelopmentTheSettingIsWhatPublishesThem()
    {
        using var deployed = _dataService.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Production");
            builder.UseSetting(ApiDocumentation.PublishedSetting, "true");
        });

        using var client = deployed.CreateClient();

        using var document = await client.GetAsync(ApiDocumentation.DocumentRoute);
        Assert.Equal(HttpStatusCode.OK, document.StatusCode);

        using var explorer = await client.GetAsync(ApiDocumentation.ExplorerRoute + "/");
        Assert.Equal(HttpStatusCode.OK, explorer.StatusCode);
    }

    // ------------------------------------------------------------------ sin acceso ----------

    /// <summary>
    /// EL EXPLORADOR NO PIDE ACCESO, y conviene que quede dicho: describir la forma de la
    /// superficie no revela ningún dato de ninguna cuenta ni de ningún trabajo. Lo que decide si
    /// se ve o no es la llave de configuración —una decisión de despliegue— y no la sesión.
    /// </summary>
    [Fact]
    public async Task TheDocumentIsServedWithoutAnyAccessToken()
    {
        using var client = _dataService.CreateClient();

        using var response = await client.GetAsync(ApiDocumentation.DocumentRoute);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Null(response.Headers.WwwAuthenticate.FirstOrDefault());
    }

    public void Dispose()
    {
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }
}
