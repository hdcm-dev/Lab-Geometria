namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Elemento del listado de cuentas de la comisión del punto `A-06` (`Contracts CU-02` §4 paso 4).
/// </summary>
/// <remarks>
/// **CERO CAMPOS CON LA CONTRASEÑA ALMACENADA**, o sea su forma derivada, y cero con cualquier
/// dirección de servicio interno (`Contracts CU-02` CA-05; `Api CU-04` CA-01). Que el tipo no los
/// declare es lo que lo vuelve imposible.
///
/// LA MARCA SÍ VIAJA Y NO ES UNA CONTRASEÑA. Es lo que le permite al administrador ver que una
/// cuenta que él **habilitó o reseteó** todavía no cambió su provisoria, **sin conocer ningún
/// valor** (`Api CU-04` §4 paso 3 y §10).
///
/// EL IDENTIFICADOR VIAJA PORQUE ES CON LO QUE SE NOMBRA LA CUENTA en `A-07`, `A-08` y `A-09`.
/// `Contracts CU-02` CA-05 enumera los cinco datos que el elemento **trae** y no declara un
/// recuento cerrado de campos, a diferencia de CA-01 y CA-06, que sí lo hacen sobre las
/// solicitudes. **[lectura de la etapa `d`, declarada]**
/// </remarks>
/// <param name="AccountId">Identidad propia de la cuenta.</param>
/// <param name="Email">Correo escrito.</param>
/// <param name="FirstName">Nombre.</param>
/// <param name="LastName">Apellido.</param>
/// <param name="Status">Situación de la cuenta, por su nombre.</param>
/// <param name="RegisteredAt">Fecha de registro, en tiempo universal coordinado.</param>
/// <param name="MustChangePassword">Marca de cambio de contraseña pendiente (INV-09).</param>
public sealed record AccountListItem(
    Guid AccountId,
    string Email,
    string FirstName,
    string LastName,
    string Status,
    DateTimeOffset RegisteredAt,
    bool MustChangePassword);
