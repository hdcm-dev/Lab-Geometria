using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Samples.Domain.Intermedio.Recorrido;

/// <summary>
/// `OP-08` — Envía el trabajo. **Es la operación que `RN-02005` gobierna**: con un error de
/// validación el trabajo **se retiene en `Borrador`**, y con advertencias solas pasa a `Pendiente`.
/// </summary>
/// <remarks>
/// LAS ADVERTENCIAS NO IMPIDEN EL ENVÍO Y LOS ERRORES SÍ, y la diferencia no la decide este acto:
/// la decide el dominio a partir de lo que el consumidor le declara. Acá se le declara lo que el
/// resultado de interpretación de cada escenario efectivamente trae.
/// </remarks>
internal static class ActoEnviar
{
    internal static void Ejecutar(Bitacora bitacora, Work trabajo, DateTimeOffset momento)
    {
        var hayErrores = ActoAdoptarObservaciones.ErroresDeValidacion(trabajo) > 0;

        bitacora.Invocar(() => trabajo.Submit(
            parseResultDeclared: true,
            validationErrorsDeclared: hayErrores,
            updatedAt: momento));

        bitacora.ContarDesenlace(trabajo.Status);
    }
}
