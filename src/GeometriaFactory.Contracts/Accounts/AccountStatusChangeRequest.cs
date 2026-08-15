namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Solicitud de cambio de situación de una cuenta del punto `A-07` (`Contracts CU-02` §4 paso 5).
/// </summary>
/// <remarks>
/// EXACTAMENTE DOS CAMPOS Y **CERO CAMPOS DE CONTRASEÑA** (`Contracts CU-02` CA-06), porque **la
/// provisoria no la escribe el administrador** (RN-14). El panel del administrador no tiene campo
/// de contraseña, y el motivo está registrado aguas arriba: si la escribe el docente, termina
/// siendo la misma clave para toda la comisión.
///
/// NO HAY CAMPO QUE DISTINGA UNA HABILITACIÓN DE UNA REHABILITACIÓN, y no hace falta: las dos
/// llevan a la misma situación y las dos traen provisoria nueva (`Contracts CU-02` FA-05).
/// </remarks>
/// <param name="AccountId">Identidad de la cuenta destino.</param>
/// <param name="IntendedStatus">Situación pretendida, por su nombre, del conjunto cerrado de tres.</param>
public sealed record AccountStatusChangeRequest(Guid AccountId, string? IntendedStatus);
