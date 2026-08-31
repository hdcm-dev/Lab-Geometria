using System.Text;
using System.Text.Json;
using GeometriaFactory.Api.Composition;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// El último borde de la superficie: lo que responde cuando el fallo **no pasó** por la traducción
/// del contrato.
/// </summary>
/// <remarks>
/// POR QUÉ SE PRUEBA ACÁ Y NO GOLPEANDO EL SERVICIO. Los dos caminos que este manejador cubre son,
/// por definición, los que ninguna petición bien formada alcanza: un defecto no previsto y una
/// petición que no se puede leer. Se intentó provocarlos desde afuera —borrando el almacén por
/// debajo del servicio en marcha— y **no se pudo**: SQLite conserva el archivo abierto por
/// semántica POSIX y la conexión sigue sirviendo. Forzarlo habría exigido tocar el producto para
/// poder probarlo, que es lo contrario de lo que una prueba debe hacer.
///
/// LO QUE ESTAS PRUEBAS FIJAN ES UNA DECISIÓN, y por eso valen más que la cobertura que agregan:
/// que el `400` de petición ilegible **siga yendo sin cuerpo**. `Contratos-REST.md` §5.1 lo declara
/// deliberado, y sin una prueba que lo sostenga el próximo que lea el manejador va a pensar que
/// falta el cuerpo y se lo va a agregar.
/// </remarks>
public sealed class ContractErrorHandlerTests
{
    private sealed class RelojFijo : ISystemClock
    {
        public DateTimeOffset UtcNow { get; } = new(2026, 8, 30, 12, 0, 0, TimeSpan.Zero);
    }

    private static ContractErrorHandler Manejador() =>
        new(NullLogger<ContractErrorHandler>.Instance, new RelojFijo());

    private static HttpContext Contexto()
    {
        var contexto = new DefaultHttpContext();
        contexto.Request.Method = "POST";
        contexto.Request.Path = "/auth/token";
        contexto.Response.Body = new MemoryStream();
        return contexto;
    }

    private static string CuerpoDe(HttpContext contexto)
    {
        contexto.Response.Body.Position = 0;
        return new StreamReader(contexto.Response.Body, Encoding.UTF8).ReadToEnd();
    }

    [Fact]
    public async Task La_peticion_ilegible_responde_400_sin_cuerpo()
    {
        var contexto = Contexto();

        var atendido = await Manejador().TryHandleAsync(
            contexto, new BadHttpRequestException("no se pudo leer el cuerpo"), CancellationToken.None);

        Assert.True(atendido);
        Assert.Equal(StatusCodes.Status400BadRequest, contexto.Response.StatusCode);

        // SIN CUERPO, Y ES LA DECISIÓN QUE ESTA PRUEBA SOSTIENE. §5.1: ocurre antes de que la
        // petición llegue a ser el tipo del contrato, así que no hay contrato con el que hablar.
        Assert.Equal(string.Empty, CuerpoDe(contexto));
    }

    [Fact]
    public async Task El_defecto_no_previsto_responde_500_con_el_codigo_generico()
    {
        var contexto = Contexto();

        var atendido = await Manejador().TryHandleAsync(
            contexto, new InvalidOperationException("cualquier defecto"), CancellationToken.None);

        Assert.True(atendido);
        Assert.Equal(StatusCodes.Status500InternalServerError, contexto.Response.StatusCode);

        var respuesta = JsonSerializer.Deserialize<ErrorResponse>(
            CuerpoDe(contexto), new JsonSerializerOptions(JsonSerializerDefaults.Web));

        Assert.NotNull(respuesta);
        Assert.Equal(ErrorCode.UnclassifiedError, respuesta.Code);
        Assert.Empty(respuesta.Details);
    }

    [Fact]
    public async Task El_mensaje_del_defecto_no_previsto_no_dice_nada_de_la_implementacion()
    {
        var contexto = Contexto();

        // El mensaje de la excepción lleva a propósito las tres cosas que `RA-03` prohíbe.
        var interna = new InvalidOperationException(
            "SQLite error en /datos/geometriafactory.db al ejecutar GeometriaFactory.Infrastructure"
            + ".Persistence.EfCoreAccountRepository.ListAsync contra http://interno:5080");

        await Manejador().TryHandleAsync(contexto, interna, CancellationToken.None);

        var cuerpo = CuerpoDe(contexto);

        // LO QUE SE COMPRUEBA ES QUE NADA DE LA EXCEPCIÓN VIAJE, y no que el texto sea uno dado:
        // una prueba que compare el mensaje palabra por palabra se rompe cuando alguien lo mejora,
        // y no diría nada sobre la regla. Lo que la regla prohíbe son estas cuatro cosas.
        Assert.DoesNotContain("geometriafactory.db", cuerpo, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("EfCoreAccountRepository", cuerpo, StringComparison.Ordinal);
        Assert.DoesNotContain("http://", cuerpo, StringComparison.Ordinal);
        Assert.DoesNotContain("SQLite", cuerpo, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Los_dos_caminos_se_atienden_y_ninguno_se_deja_pasar()
    {
        // NINGÚN FALLO LLEGA SIN REPRESENTACIÓN: el manejador devuelve verdadero siempre, que es lo
        // que impide que la tubería caiga en la respuesta por omisión del servidor.
        foreach (var falla in new Exception[]
                 {
                     new BadHttpRequestException("ilegible"),
                     new InvalidOperationException("no previsto"),
                     new TimeoutException("tardó"),
                     new NotSupportedException("no admitido"),
                 })
        {
            Assert.True(await Manejador().TryHandleAsync(Contexto(), falla, CancellationToken.None));
        }
    }
}
