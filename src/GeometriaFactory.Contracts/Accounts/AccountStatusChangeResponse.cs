namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Resultado del cambio de situación del punto `A-07` (`Contracts CU-02` §4 paso 6).
/// </summary>
/// <remarks>
/// **CUANDO LA SITUACIÓN PRETENDIDA ES HABILITADA, TRAE LA CONTRASEÑA PROVISORIA EN CLARO**, una
/// sola vez, para que el administrador se la comunique al alumno por fuera del producto (RN-16,
/// `Contracts CU-02` CA-02). Cuando fue bloqueada, **no trae ninguna**: esa ausencia es la señal
/// de que no hay nada que comunicar (FA-06).
///
/// LO QUE `RT-01` PROHÍBE ES TRANSPORTAR LA CONTRASEÑA **ALMACENADA** —su forma derivada— y este
/// tipo no la lleva: lo que viaja es el valor en claro, una vez, exactamente como en el resultado
/// del reseteo. **Cero campos** con la forma derivada.
///
/// NO HAY MANERA DE VOLVER A PEDIR LA PROVISORIA. Si el administrador cierra la pantalla sin
/// comunicarla, el camino declarado es **volver a resetear**, que produce un valor nuevo: el
/// producto no la conserva en ninguna parte (`Api CU-05` §10).
/// </remarks>
/// <param name="ResultingStatus">Situación resultante de la cuenta, por su nombre.</param>
/// <param name="ProvisionalPassword">
/// El valor en claro de la provisoria, o nulo cuando la operación no produjo ninguna.
/// </param>
/// <param name="MustChangePassword">Si la cuenta quedó con cambio de contraseña pendiente (INV-09).</param>
public sealed record AccountStatusChangeResponse(
    string ResultingStatus,
    string? ProvisionalPassword,
    bool MustChangePassword);
