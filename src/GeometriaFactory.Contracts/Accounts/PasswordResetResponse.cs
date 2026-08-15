namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Resultado del reseteo de contraseña del punto `A-09` (`Contracts CU-08` §4 paso 2).
/// </summary>
/// <remarks>
/// TRES CAMPOS: la situación de la cuenta —**la misma que tenía**—, la declaración de que quedó
/// con cambio de contraseña pendiente y **la contraseña provisoria en claro**, que es lo único
/// que el administrador tiene que comunicar (`Contracts CU-08` CA-02).
///
/// **CERO CAMPOS CON LA CONTRASEÑA ALMACENADA** —su forma derivada—, con una dirección de
/// servicio interno o con **cualquier referencia a los trabajos de la cuenta**. La última
/// ausencia es la que hace observable la promesa de `RN-12`: por este resultado no hay forma de
/// que un trabajo se pierda.
///
/// LA SITUACIÓN VUELVE SIN CAMBIO, y por eso el campo se llama situación de la cuenta y no
/// situación resultante: el reseteo no es una transición de la máquina de estados (RN-15).
/// </remarks>
/// <param name="Status">Situación de la cuenta, por su nombre. Es la misma que antes del reseteo.</param>
/// <param name="ProvisionalPassword">El valor en claro de la provisoria, devuelto una sola vez.</param>
/// <param name="MustChangePassword">Si la cuenta quedó con cambio de contraseña pendiente (INV-09).</param>
public sealed record PasswordResetResponse(
    string Status,
    string ProvisionalPassword,
    bool MustChangePassword);
