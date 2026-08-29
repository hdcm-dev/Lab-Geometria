using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Application.Intermedio.Actos;

/// <summary>
/// `CU-04005` — Lee el desenlace del envío que la carga ya produjo y lo declara con el vocabulario
/// del snapshot. **`RN-04005`: un error de validación retiene el trabajo en `Borrador`.**
/// </summary>
internal static class ActoEnviar
{
    internal static int Errores(WorkOutcomeSnapshot s) =>
        s.Observations?.Count(o => o.Kind == ObservationKind.ValidationError) ?? 0;

    internal static int Advertencias(WorkOutcomeSnapshot s) =>
        s.Observations?.Count(o => o.Kind == ObservationKind.Warning) ?? 0;

    internal static string Error(WorkOutcomeSnapshot s)
    {
        var e = s.Observations!.First(o => o.Kind == ObservationKind.ValidationError);
        return $"observacion {Vocabulario.De(e.Kind)} indice-figura={e.PiecePosition} campo={e.Field}";
    }

    internal static string Advertencia(WorkOutcomeSnapshot s)
    {
        var a = s.Observations!.First(o => o.Kind == ObservationKind.Warning);
        return $"advertencia de area declarado={a.DeclaredValue:F2} derivado={a.DerivedValue:F2}";
    }
}
