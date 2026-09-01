using System.Net;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// El punto de salud contra el ALMACÉN REAL, sin ayudarlo.
/// </summary>
/// <remarks>
/// POR QUÉ ESTA PRUEBA EXISTE, Y POR QUÉ LAS DE <c>StoreHealthTests</c> NO ALCANZABAN. Aquéllas
/// ejercitan la clase y llaman <c>SqliteConnection.ClearAllPools()</c> antes de romper el almacén —
/// <b>algo que el servicio corriendo jamás hace</b>. Pasaban, y el defecto seguía vivo: con el
/// binario real y el archivo borrado, <c>/salud</c> contestaba <c>200 {"ready":true}</c>
/// indefinidamente, porque la conexión agrupada conservaba el inodo desenlazado.
///
/// <b>Es la forma más cara de verde: la que se consigue poniendo en la prueba la condición que le
/// falta al producto.</b> Lo levantó la mesa del 2026-09-01 como <c>R-2</c>, y la corrección fue
/// <c>Pooling=False</c> en <c>CompositionRoot</c>.
///
/// Esta prueba entra por el <b>punto de acceso</b> y no por la clase, y <b>no llama a
/// <c>ClearAllPools</c></b>. Si alguien devuelve el agrupamiento, esto se pone en rojo.
/// </remarks>
public sealed class HealthEndpointStoreTests
{
    [Fact]
    public async Task ConElAlmacenBorradoLaSaludDejaDeDecirQueEstaListo()
    {
        var rutaDelAlmacen = DataServiceHarness.ReserveStorePath();
        await using var harness = new DataServiceHarness(rutaDelAlmacen);
        var cliente = harness.CreateClient();

        var arranque = await cliente.GetAsync(new Uri("/salud", UriKind.Relative));
        Assert.Equal(HttpStatusCode.OK, arranque.StatusCode);

        // SE BORRA EL ARCHIVO Y NADA MÁS. Ni `ClearAllPools`, ni reiniciar el host, ni tocar el
        // contexto: exactamente lo que pasa cuando alguien borra el almacén con el servicio arriba.
        foreach (var sufijo in new[] { "", "-wal", "-shm" })
        {
            var ruta = rutaDelAlmacen + sufijo;
            if (File.Exists(ruta)) File.Delete(ruta);
        }

        var despues = await cliente.GetAsync(new Uri("/salud", UriKind.Relative));

        Assert.Equal(HttpStatusCode.ServiceUnavailable, despues.StatusCode);
        Assert.Contains("\"ready\":false", await despues.Content.ReadAsStringAsync(), StringComparison.Ordinal);
    }
}
