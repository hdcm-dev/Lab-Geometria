using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Intermedio.Recorrido;

/// <summary>
/// `OP-07` — Lee las observaciones que el trabajo adoptó y las declara. No las produce: el
/// dominio ya las tiene, y este acto sólo las muestra con el vocabulario del snapshot.
/// </summary>
internal static class ActoAdoptarObservaciones
{
    internal static int ErroresDeValidacion(Work trabajo) =>
        trabajo.Observations.Count(o => o.Kind == ObservationKind.ValidationError);

    internal static void DeclararAdvertencia(Bitacora bitacora, Work trabajo, string escenario)
    {
        var advertencia = trabajo.Observations.First(o => o.Kind == ObservationKind.Warning);
        bitacora.Escribir(
            $"[{escenario}] Observacion adoptada: especie={Vocabulario.De(advertencia.Kind)} "
            + $"campo={advertencia.Field} declarado={advertencia.DeclaredValue:F2} "
            + $"derivado={advertencia.DerivedValue:F2}");
    }

    internal static void DeclararError(Bitacora bitacora, Work trabajo, string escenario, string rotulo)
    {
        var error = trabajo.Observations.First(o => o.Kind == ObservationKind.ValidationError);
        bitacora.Escribir(
            $"[{escenario}] {rotulo}: indice-figura={error.PiecePosition} campo={error.Field}");
    }
}
