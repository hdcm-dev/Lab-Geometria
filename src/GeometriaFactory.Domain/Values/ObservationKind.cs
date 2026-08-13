namespace GeometriaFactory.Domain.Values;

/// <summary>
/// Especie de una observación. Conjunto cerrado de dos valores.
/// </summary>
/// <remarks>
/// Se serializa por nombre. Etiquetas en castellano por `Norma-De-Nomenclatura.md` §6.7.
/// </remarks>
public enum ObservationKind
{
    /// <summary>Etiqueta: «Advertencia».</summary>
    Warning = 1,

    /// <summary>Etiqueta: «Error de validación».</summary>
    ValidationError = 2
}
