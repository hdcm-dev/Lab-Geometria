namespace GeometriaFactory.Contracts.Errors;

/// <summary>
/// El conjunto CERRADO de códigos con los que un fallo cruza la frontera entre las dos piezas
/// desplegables (`Contracts CU-06`).
/// </summary>
/// <remarks>
/// LA CAPA QUE EXPONE NO INVENTA CÓDIGOS: lo que este conjunto no declara, no viaja como código.
/// Un motivo interno sin código propio cae en <see cref="UnclassifiedError"/>, y el hueco se
/// declara en lugar de taparse con un código nuevo.
///
/// IDENTIFICADORES EN INGLÉS Y SIN EL PREFIJO `CONTRATO_`, por la decisión `F-03` de
/// `Norma-De-Nomenclatura.md` §5.3, con la correspondencia uno a uno en su §6.8.6. La identidad
/// del código la da este conjunto, no un prefijo dentro del nombre.
///
/// ACÁ ESTÁN LOS DOCE QUE LAS ETAPAS `c` Y `d` PUEDEN PRODUCIR, de los diecisiete vivos del
/// conjunto. Ocho los escribió la etapa `c`; los cuatro últimos los agrega la `d` con el gobierno
/// de las cuentas y el reseteo. Los cinco que faltan describen condiciones **sobre trabajos**,
/// que son de las etapas `e` y siguientes.
/// </remarks>
public static class ErrorCode
{
    /// <summary>`CONTRATO_CAMPO_REQUERIDO_AUSENTE` (`DXT-01`). La solicitud llega incompleta.</summary>
    public const string RequiredFieldMissing = "REQUIRED_FIELD_MISSING";

    /// <summary>
    /// `CONTRATO_CREDENCIAL_INVALIDA` (`DXT-02`). El par no corresponde a ninguna cuenta.
    /// GENÉRICO: el texto no declara cuál de los dos campos falló (intake §17.5.P.5).
    /// </summary>
    public const string InvalidCredentials = "INVALID_CREDENTIALS";

    /// <summary>`CONTRATO_CUENTA_NO_HABILITADA` (`DXT-03`). La cuenta está `Pending` o `Blocked` (RN-06).</summary>
    public const string AccountNotEnabled = "ACCOUNT_NOT_ENABLED";

    /// <summary>`CONTRATO_CORREO_YA_REGISTRADO` (`DXT-04`). RN-02.</summary>
    public const string EmailAlreadyRegistered = "EMAIL_ALREADY_REGISTERED";

    /// <summary>`CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` (`DXT-06`). RN-01, INV-05.</summary>
    public const string AdministratorAlreadyConfigured = "ADMINISTRATOR_ALREADY_CONFIGURED";

    /// <summary>
    /// `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` (`DXT-16`). Un solo código para todas las
    /// operaciones bloqueadas y para los dos orígenes de la marca (RN-13, RN-16, INV-09).
    /// </summary>
    public const string PasswordChangeRequired = "PASSWORD_CHANGE_REQUIRED";

    /// <summary>`CONTRATO_SERVICIO_NO_DISPONIBLE` (`DXT-11`). Sin dirección del servicio que falló.</summary>
    public const string ServiceUnavailable = "SERVICE_UNAVAILABLE";

    // ---- LOS CUATRO QUE AGREGA LA ETAPA `d` ------------------------------------------------

    /// <summary>
    /// `CONTRATO_CONFIRMACION_NO_COINCIDE` (`DXT-05`). El correo escrito como confirmación de la
    /// baja no es el de la cuenta (RN-07). **La respuesta no devuelve el correo esperado.**
    /// </summary>
    public const string ConfirmationMismatch = "CONFIRMATION_MISMATCH";

    /// <summary>
    /// `CONTRATO_ALUMNO_NO_ENCONTRADO` (`DXT-10`). **Adopción de causa declarada por `Api CU-04`
    /// §10**: además del filtro por alumno de un listado de trabajos, cubre la cuenta que un punto
    /// de administración referencia y no existe. Es la misma situación desde otro punto de acceso.
    /// </summary>
    public const string StudentNotFound = "STUDENT_NOT_FOUND";

    /// <summary>
    /// `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` (`DXT-19`). Negativa **de facultad fuera
    /// del desenlace** —gobierno de cuentas, listado de la comisión y reseteo— y, como la del
    /// desenlace, **no tiene nada que ocultar**: el recurso no es ajeno, lo que no alcanza es el
    /// papel. Entra al conjunto cerrado por `PRODUCT-INTAKE` **1.29** §17.4 P.3.
    /// </summary>
    public const string OperationAdminOnly = "OPERATION_ADMIN_ONLY";

    /// <summary>
    /// `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` (`DXT-17`). RN-15, INV-08.
    /// **No es una negativa de facultad**: quien pide la tiene, y lo que no procede es la
    /// operación sobre esa cuenta. El camino que sí existe es el cambio de la propia contraseña.
    /// </summary>
    public const string ResetNotApplicableToAdministratorAccount =
        "RESET_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT";

    /// <summary>
    /// `CONTRATO_ERROR_NO_CLASIFICADO` (`DXT-12`). Cierra el conjunto: ningún fallo llega a la
    /// persona sin representación, que es la definición de fallo silencioso que el producto
    /// viene a eliminar.
    /// </summary>
    public const string UnclassifiedError = "UNCLASSIFIED_ERROR";
}
