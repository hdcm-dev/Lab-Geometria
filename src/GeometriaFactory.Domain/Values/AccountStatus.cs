namespace GeometriaFactory.Domain.Values;

/// <summary>
/// Situación de una cuenta. Conjunto cerrado de tres valores.
/// </summary>
/// <remarks>
/// Se serializa por nombre. Etiquetas en castellano por `Norma-De-Nomenclatura.md` §6.7.
/// `Pending` es deliberadamente distinto de `WorkStatus.Submitted`: el castellano
/// colapsa los dos conceptos en «Pendiente» y el inglés los separa (§6.7).
/// </remarks>
public enum AccountStatus
{
    /// <summary>Etiqueta: «Pendiente». Espera habilitación.</summary>
    Pending = 1,

    /// <summary>Etiqueta: «Habilitado».</summary>
    Enabled = 2,

    /// <summary>Etiqueta: «Bloqueado».</summary>
    Blocked = 3
}
