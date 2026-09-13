using System.Net;
using System.Text.RegularExpressions;
using GeometriaFactory.Api.Endpoints;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// La inspección de la superficie publicada contra la lista del contrato, **en las dos
/// direcciones**, con el prefijo de versión `/v1/` de `ADR-00010` (`BT-00032`, criterio 4).
/// </summary>
/// <remarks>
/// LA LISTA DE ABAJO SE TRANSCRIBE Y NO SE DERIVA. Es la tabla de `Contratos-REST.md` §3 —los
/// diecisiete puntos, con la ruta de `Definicion-Superficie-HTTP.md` §3— escrita a mano, verbo y
/// ruta, y ésa es la gracia: si alguien agrega un punto al código sin agregarlo al contrato, la
/// primera dirección falla; si alguien lo agrega al contrato sin mapearlo, falla la segunda. Una
/// lista calculada desde las constantes del servicio pasaría siempre, que es lo mismo que no
/// tener prueba (`Contratos-REST.md` §6: «la prueba de inspección de la guardia falla si no
/// está»).
///
/// LO QUE SE LEE ES LO QUE EL SERVICIO PUBLICA DE VERDAD, no lo que sus constantes dicen: la
/// fuente de datos de puntos del propio host, con el patrón de ruta ya compuesto por el grupo de
/// `Program.cs`. Es la única forma de que la prueba vea el prefijo puesto —o faltante— y no la
/// constante que debería haberse puesto.
///
/// TRES RUTAS ESTÁN EXENTAS Y SE ENUMERAN, no se toleran por forma: `/salud`, `/openapi/v1.json`
/// y `/documentacion`. Los motivos están en <see cref="ContractRoutePrefix"/>. Una cuarta ruta sin
/// prefijo hace fallar la prueba aunque tenga buen motivo, y la salida dice qué agregar.
///
/// SIN PREFIJO NO HAY CONTRATO, y se mide con peticiones y no leyendo el enrutador: la ruta que
/// valió hasta el 2026-09-12 responde `404` sin redirección, que es lo que `Contratos-REST.md` §3
/// declara desde `BT-00032`.
/// </remarks>
public sealed class ContractRoutePrefixTests : IDisposable
{
    /// <summary>
    /// Los diecisiete puntos de `Contratos-REST.md` §3 con su ruta **pública**: los dieciséis del
    /// contrato bajo `/v1/` y `A-16`, exento, tal cual se publica.
    /// </summary>
    private static readonly (string Punto, string Verbo, string Ruta)[] Contrato =
    [
        ("A-01", "POST", "/v1/auth/token"),
        ("A-02", "POST", "/v1/cuentas"),
        ("A-03", "POST", "/v1/cuentas/administrador"),
        ("A-05", "POST", "/v1/cuenta/contrasena"),
        ("A-06", "GET", "/v1/cuentas"),
        ("A-07", "POST", "/v1/cuentas/{id}/situacion"),
        ("A-08", "DELETE", "/v1/cuentas/{id}"),
        ("A-09", "POST", "/v1/cuentas/{id}/reseteo-de-contrasena"),
        ("A-10", "POST", "/v1/trabajos"),
        ("A-11", "POST", "/v1/trabajos/{id}"),
        ("A-12", "DELETE", "/v1/trabajos/{id}"),
        ("A-13", "GET", "/v1/trabajos"),
        ("A-14", "GET", "/v1/trabajos/{id}"),
        ("A-15", "POST", "/v1/trabajos/{id}/desenlace"),
        ("A-16", "GET", "/salud"),
        ("A-17", "GET", "/v1/aprovisionamiento"),
        ("A-18", "POST", "/v1/interpretaciones"),
    ];

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;

    public ContractRoutePrefixTests()
    {
        _dataService = new DataServiceHarness(_storePath);
    }

    // ------------------------------------------------------- las dos direcciones ------------

    /// <summary>
    /// Dirección 1 — **toda ruta publicada está en la lista, o es una de las tres exentas.** Un
    /// punto nuevo que no entre a `Contratos-REST.md` §3 en la misma intervención rompe acá.
    /// </summary>
    [Fact]
    public void EveryPublishedRouteIsInTheContractOrDeclaredExempt()
    {
        var publicadas = PublishedRoutes();
        var contrato = Contrato.Select(p => (p.Verbo, p.Ruta)).ToHashSet();

        var deMas = publicadas
            .Where(r => !contrato.Contains(r) && !IsExempt(r.Ruta))
            .ToArray();

        Assert.True(deMas.Length == 0,
            "Estas rutas se publican y no están en la tabla de `Contratos-REST.md` §3 ni entre las "
            + "tres exentas de `ContractRoutePrefix`. Agregalas al contrato en esta misma "
            + "intervención, o declaralas exentas con su motivo: "
            + string.Join(", ", deMas.Select(r => $"{r.Verbo} {r.Ruta}")));
    }

    /// <summary>
    /// Dirección 2 — **todo punto de la lista está publicado**, con su verbo y su ruta pública.
    /// Un punto que el contrato declare y el código no mapee —o mapee sin el prefijo— rompe acá.
    /// </summary>
    [Fact]
    public void EveryContractPointIsPublished()
    {
        var publicadas = PublishedRoutes().ToHashSet();

        var faltan = Contrato
            .Where(p => !publicadas.Contains((p.Verbo, p.Ruta)))
            .ToArray();

        Assert.True(faltan.Length == 0,
            "Estos puntos del contrato no se publican con esa ruta. Rutas publicadas: "
            + string.Join(", ", publicadas.Select(r => $"{r.Verbo} {r.Ruta}")) + ". Faltan: "
            + string.Join(", ", faltan.Select(p => $"{p.Punto} {p.Verbo} {p.Ruta}")));
    }

    /// <summary>
    /// **Toda ruta publicada lleva el prefijo `/v1/`, salvo las tres exentas.** Es la regla de
    /// `ADR-00010` §2.1 punto 1 medida sobre lo que el host enruta, y falla si aparece una cuarta
    /// ruta sin prefijo aunque tenga buen motivo: el motivo se escribe en `ContractRoutePrefix`.
    /// </summary>
    [Fact]
    public void EveryPublishedRouteCarriesTheVersionPrefixExceptTheThreeExempt()
    {
        var sinPrefijo = PublishedRoutes()
            .Where(r => !r.Ruta.StartsWith(ContractRoutePrefix.Value + "/", StringComparison.Ordinal))
            .Select(r => r.Ruta)
            .Distinct()
            .ToArray();

        Assert.All(sinPrefijo, ruta => Assert.True(IsExempt(ruta),
            $"`{ruta}` se publica sin el prefijo `{ContractRoutePrefix.Value}/` y no está entre las "
            + "exentas declaradas en `ContractRoutePrefix.ExemptRoutes`."));

        // Y las exentas son exactamente tres, para que la lista no crezca por comodidad.
        Assert.Equal(3, ContractRoutePrefix.ExemptRoutes.Count);
    }

    /// <summary>
    /// El recuento que `Contratos-REST.md` §3 declara: diecisiete puntos, dieciséis bajo el
    /// prefijo y uno exento. Se cuenta sobre la lista y sobre lo publicado, para que las dos
    /// direcciones no cierren por casualidad con listas de distinto tamaño.
    /// </summary>
    [Fact]
    public void SeventeenPointsSixteenUnderThePrefixAndOneExempt()
    {
        Assert.Equal(17, Contrato.Length);
        Assert.Equal(16, Contrato.Count(p => p.Ruta.StartsWith("/v1/", StringComparison.Ordinal)));
        Assert.Single(Contrato, p => IsExempt(p.Ruta));

        var publicadasDelContrato = PublishedRoutes()
            .Where(r => r.Ruta.StartsWith("/v1/", StringComparison.Ordinal))
            .Distinct()
            .Count();
        Assert.Equal(16, publicadasDelContrato);
    }

    // ------------------------------------------------------------ sin prefijo: 404 ----------

    /// <summary>
    /// **La ruta sin prefijo no existe: `404`, sin redirección y sin cuerpo del contrato.** Se
    /// ejercen un punto anónimo, uno bajo la guardia y uno con parámetro: los tres responden lo
    /// mismo que cualquier ruta inexistente, y no `401` —lo que diría que la guardia sí los
    /// reconoce— ni `3xx` —lo que mantendría viva la forma sin versión bajo otro nombre—.
    /// </summary>
    [Theory]
    [InlineData("GET", "/aprovisionamiento")]
    [InlineData("POST", "/auth/token")]
    [InlineData("GET", "/trabajos")]
    [InlineData("POST", "/cuentas/administrador")]
    [InlineData("GET", "/cuentas/00000000-0000-0000-0000-000000000001")]
    public async Task ARequestWithoutTheVersionPrefixIsNotARequestToTheContract(string verbo, string ruta)
    {
        using var client = _dataService.CreateClient();
        using var request = new HttpRequestMessage(new HttpMethod(verbo), ruta);
        if (verbo == "POST")
        {
            request.Content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");
        }

        using var response = await client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Null(response.Headers.Location);
        Assert.Empty(await response.Content.ReadAsStringAsync());
    }

    /// <summary>
    /// La exención es en una sola dirección: `/salud` responde donde siempre y **no** responde
    /// bajo el prefijo. Si un día se decide versionarlo, esta prueba es la que hay que cambiar,
    /// junto con los dos `healthcheck` y la página de estado del front.
    /// </summary>
    [Fact]
    public async Task TheHealthPointAnswersOutsideThePrefixAndNotInsideIt()
    {
        using var client = _dataService.CreateClient();

        using var afuera = await client.GetAsync(HealthEndpoint.Route);
        Assert.Equal(HttpStatusCode.OK, afuera.StatusCode);

        using var adentro = await client.GetAsync(ContractRoutePrefix.Value + HealthEndpoint.Route);
        Assert.Equal(HttpStatusCode.NotFound, adentro.StatusCode);
    }

    // ---------------------------------------------------------------- andamiaje -------------

    /// <summary>
    /// Lo que el host enruta de verdad: verbo y patrón de cada punto, con las restricciones de
    /// parámetro quitadas (`{id:guid}` → `{id}`) para poder compararlo con la tabla del contrato,
    /// que las declara sin restricción.
    /// </summary>
    private IEnumerable<(string Verbo, string Ruta)> PublishedRoutes()
    {
        var puntos = _dataService.Services.GetRequiredService<EndpointDataSource>().Endpoints;

        foreach (var punto in puntos.OfType<RouteEndpoint>())
        {
            var ruta = "/" + Regex.Replace(punto.RoutePattern.RawText ?? string.Empty, @"\{(\w+)[^}]*\}", "{$1}")
                .TrimStart('/');
            var verbos = punto.Metadata.GetMetadata<IHttpMethodMetadata>()?.HttpMethods ?? ["*"];

            foreach (var verbo in verbos)
            {
                yield return (verbo, ruta);
            }
        }
    }

    private static bool IsExempt(string ruta) =>
        ContractRoutePrefix.ExemptRoutes.Any(exenta =>
            ruta.Equals(exenta, StringComparison.Ordinal)
            || ruta.StartsWith(exenta + "/", StringComparison.Ordinal));

    public void Dispose()
    {
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }
}
