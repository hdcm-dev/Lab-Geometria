using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Errors;
using Microsoft.AspNetCore.Diagnostics;

namespace GeometriaFactory.Api.Composition;

/// <summary>
/// El último borde: convierte cualquier fallo que no pasó por la traducción del contrato en una
/// respuesta que **sí** cumple el contrato, y deja el detalle del lado del servidor.
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE, Y QUÉ NO HACE. Hasta el 2026-08-30 no había ninguno, y eso dejaba dos huecos que
/// se midieron golpeando la superficie en los dos entornos:
///
///   · **Un defecto no previsto respondía `500` con el cuerpo vacío.** `Contratos-REST.md` §4 le
///     pide a ese código «nunca lleva detalle de implementación» —que se cumplía— y §5.4 pide, para
///     todo lo que no se puede decir, «**el código genérico, con su código de respuesta**». Vacío no
///     es el código genérico: `UNCLASSIFIED_ERROR` existe, y por esta vía no llegaba nunca.
///
///   · **La petición ilegible respondía distinto según el entorno.** En `Development` el cuerpo
///     traía `BadHttpRequestException` y el nombre del tipo del contrato que no se pudo leer; en
///     `Production`, nada. §5.4 prohíbe «nombres de tipos internos» **y no admite excepción por
///     entorno**, de modo que la garantía quedaba colgando de `ASPNETCORE_ENVIRONMENT`.
///
/// LO QUE ESTE MANEJADOR NO HACE ES INVENTAR CÓDIGOS, y es la mitad que importa. `Contratos-REST.md`
/// §5.1 declara **dos respuestas sin código del contrato** —el `401` de la guardia y el `400` de
/// petición ilegible— y las declara «para que su ausencia de código no se lea como un olvido». Este
/// manejador **respeta las dos**: al `401` no lo toca, y al `400` lo deja sin cuerpo en TODOS los
/// entornos, que es lo que la declaración dice y lo que `Production` ya hacía.
///
/// TAMPOCO TOCA EL `404`, EL `405` NI EL `415`. Son del protocolo y no del producto: darles un
/// código del conjunto cerrado haría crecer un conjunto cerrado por motivos que no son del producto.
///
/// LA CONTRACARA ES OBLIGATORIA Y ESTÁ ACÁ. §5.4 la enuncia: «registro estructurado del lado del
/// servidor de cada error». Sin ese registro, la prohibición de exponer se convierte en
/// imposibilidad de diagnosticar. El detalle no se pierde: se muda al lugar donde se lo busca.
/// </remarks>
public sealed class ContractErrorHandler : IExceptionHandler
{
    private readonly ILogger<ContractErrorHandler> _registro;
    private readonly ISystemClock _reloj;

    public ContractErrorHandler(ILogger<ContractErrorHandler> registro, ISystemClock reloj)
    {
        ArgumentNullException.ThrowIfNull(registro);
        ArgumentNullException.ThrowIfNull(reloj);
        _registro = registro;
        _reloj = reloj;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(exception);

        // EL PEDIDO SE REGISTRA POR SU MÉTODO Y SU RUTA, y no por su cuerpo: el cuerpo de una
        // petición de esta superficie puede traer una contraseña en claro, y el registro es
        // exactamente donde `RA-03` no quiere que termine.
        _registro.LogError(
            exception,
            "Fallo no traducido en {Metodo} {Ruta}.",
            context.Request.Method,
            context.Request.Path);

        if (exception is BadHttpRequestException)
        {
            // LA PETICIÓN ILEGIBLE VA SIN CUERPO, y es lo que §5.1 declara: ocurre **antes** de que
            // la petición llegue a ser el tipo del contrato, así que no hay contrato con el que
            // hablar todavía. El número lo dice entero.
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return true;
        }

        // EL RESTO ES UN DEFECTO QUE EL PRODUCTO NO PREVIÓ, y ése sí tiene código: el genérico
        // existe para que ningún fallo llegue sin representación. El texto es el mismo que la
        // traducción del contrato ya usa para sus `500`, y no se redacta uno nuevo.
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(
            new ErrorResponse(
                ErrorCode.UnclassifiedError,
                "No pudimos completar la operación. Probá de nuevo en un rato.",
                [],
                _reloj.UtcNow),
            cancellationToken).ConfigureAwait(false);

        return true;
    }
}
