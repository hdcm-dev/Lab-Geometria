namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Solicitud de reseteo de contraseña del punto `A-09` (`Contracts CU-08` §4 paso 1).
/// </summary>
/// <remarks>
/// EXACTAMENTE UN CAMPO (`Contracts CU-08` CA-01), y las dos ausencias importan tanto como la
/// presencia:
///  · **cero campos de contraseña**, porque la provisoria no la escribe el administrador (RN-14);
///  · **cero campos que permitan conservar, descartar o referenciar los trabajos** de la cuenta:
///    el reseteo **no puede expresarse como una baja**, y ésa es la contracara exacta de
///    <see cref="AccountDeletionRequest"/>.
///
/// Y UNA TERCERA AUSENCIA, QUE ES ESTRUCTURAL: **cero parámetros de situación de cuenta**. El
/// reseteo procede sobre las tres situaciones sin cambiarlas, y no declarar el parámetro es la
/// forma en que `RN-15` se hace imposible de violar desde la superficie (`Api CU-05` CA-09).
///
/// NO DECLARA CONFIRMACIÓN ESCRITA, y es deliberado: pedirla sería trasladarle a una operación
/// conservadora la guarda de una destructiva (`Domain CU-13` §10).
/// </remarks>
/// <param name="AccountId">Identidad de la cuenta destino.</param>
public sealed record PasswordResetRequest(Guid AccountId);
