namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Solicitud de registro de una cuenta de alumno del punto `A-02` (`Contracts CU-02` §4 paso 1).
/// </summary>
/// <remarks>
/// TRES CAMPOS Y **CERO CAMPOS DE CONTRASEÑA** (`Contracts CU-02` CA-01). No es un detalle de
/// formulario: es lo que hace posible el flujo sin correo. La cuenta nace sin credencial y la
/// recibe **en el acto de habilitación**, con la provisoria que el sistema produce y que el
/// administrador le comunica en persona; recién entonces la persona elige la suya, cambiándola
/// por `A-05`.
///
/// ESTA SOLICITUD ES ANÓNIMA Y DEBE SEGUIRLO. **RN-16** suprimió la escritura anónima **de
/// credencial**, no toda escritura anónima: el registro de cuenta es anónimo por diseño, porque
/// es la puerta por la que el alumno entra al laboratorio (`PRODUCT-INTAKE` 1.15 §4.1). El tipo
/// que se retiró —la solicitud de establecimiento de contraseña— era otro: transportaba una
/// contraseña nueva sin credencial vigente, y **agregar uno así se rechaza aunque compile**.
/// </remarks>
public sealed record AccountRegistrationRequest(
    string? Email,
    string? FirstName,
    string? LastName);
