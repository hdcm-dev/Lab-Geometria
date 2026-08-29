using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Samples.Domain.Intermedio.Escenarios;

namespace GeometriaFactory.Samples.Domain.Intermedio.Recorrido;

/// <summary>
/// `OP-06` y `OP-07` — Adopta el resultado de interpretación: las piezas reconstruidas y las
/// observaciones, en una sola operación, porque el dominio los recibe juntos.
/// </summary>
internal static class ActoAdoptarPiezas
{
    internal static void Ejecutar(Bitacora bitacora, Work trabajo, Interpretacion interpretacion,
        DateTimeOffset momento)
    {
        bitacora.Invocar(() => trabajo.AdoptInterpretation(
            rootFigureCount: interpretacion.FigurasRaiz,
            pieces: interpretacion.Piezas,
            observations: interpretacion.Observaciones,
            updatedAt: momento));
    }
}
