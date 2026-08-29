using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Avanzado.Inspeccion;

/// <summary>
/// Dos corridas consecutivas **sin fijar el reloj**, comparadas. Es `ADR-02006` verificado:
/// **el dominio no lee el reloj** y su resultado no depende del momento en que se lo invoca.
/// </summary>
/// <remarks>
/// El momento se le pasa DISTINTO a cada corrida a propósito. Si el dominio lo usara para algo más
/// que registrarlo, los dos resultados diferirían — y esa diferencia es lo que esta inspección
/// busca. Se comparan el estado, el recuento de piezas y el de observaciones, que es todo lo que
/// una decisión del dominio puede mover.
/// </remarks>
internal static class SinRelojNiConjunto
{
    private static (WorkStatus Estado, int Piezas, int Observaciones) UnaCorrida(DateTimeOffset momento)
    {
        var w = Work.Create(Guid.NewGuid(), "Trabajo de la inspección", "2026-08-29", null, "[]",
            true, momento).Value!;
        w.AdoptInterpretation(1, [Piece.Reconstruct(0, FigureType.Circle, 3.14, 3.14, null, null)],
            [], momento);
        w.Submit(parseResultDeclared: true, validationErrorsDeclared: false, updatedAt: momento);
        return (w.Status, w.Pieces.Count, w.Observations.Count);
    }

    internal static bool ResultadoIdentico()
    {
        var primera = UnaCorrida(new DateTimeOffset(2026, 1, 1, 3, 0, 0, TimeSpan.Zero));
        var segunda = UnaCorrida(new DateTimeOffset(2026, 12, 31, 23, 59, 0, TimeSpan.Zero));
        return primera == segunda;
    }
}
