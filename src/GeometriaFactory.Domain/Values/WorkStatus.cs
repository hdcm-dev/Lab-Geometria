namespace GeometriaFactory.Domain.Values;

/// <summary>
/// Estado de un trabajo. Conjunto cerrado de cuatro valores.
/// </summary>
/// <remarks>
/// Se serializa por nombre. Etiquetas en castellano por `Norma-De-Nomenclatura.md` §6.7.
/// La máquina de estados que gobierna las transiciones es de la etapa `e` (`Domain BT-12`).
/// </remarks>
public enum WorkStatus
{
    /// <summary>Etiqueta: «Borrador».</summary>
    Draft = 1,

    /// <summary>Etiqueta: «Pendiente». Espera revisión del administrador.</summary>
    Submitted = 2,

    /// <summary>Etiqueta: «Finalizado». Es el desenlace de aprobación.</summary>
    Approved = 3,

    /// <summary>Etiqueta: «Rechazado».</summary>
    Rejected = 4
}
