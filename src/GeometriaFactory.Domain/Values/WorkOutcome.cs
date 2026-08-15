namespace GeometriaFactory.Domain.Values;

/// <summary>
/// Desenlace de la revisión. Conjunto cerrado de dos valores (`Contratos-Abstractions.md` §4.2).
/// </summary>
/// <remarks>
/// APROBAR LLEVA A `Approved` Y RECHAZAR A `Rejected`, y los dos destinos son terminales
/// (`Domain CU-10` §4 paso 5, INV-07, RN-10).
///
/// ETAPA `e`: el desenlace se modela **en el dominio** porque `Domain BT-12` —la máquina de
/// estados del trabajo— es de esta etapa y su criterio de aceptación nombra el desenlace y la
/// terminalidad. **NO se expone en la superficie HTTP**: el punto `A-15` es de la etapa `h`, que
/// es donde el roadmap pone el criterio «el administrador aprueba un trabajo en estado
/// `Pendiente`». Lo que la etapa `e` construye es la transición, no la capacidad.
/// </remarks>
public enum WorkOutcome
{
    /// <summary>Aprobar. Lleva el trabajo a `Approved`, etiqueta «Finalizado».</summary>
    Approve = 1,

    /// <summary>Rechazar. Lleva el trabajo a `Rejected`, etiqueta «Rechazado».</summary>
    Reject = 2
}
