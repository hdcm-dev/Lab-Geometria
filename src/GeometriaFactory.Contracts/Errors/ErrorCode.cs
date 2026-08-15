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
/// ACÁ ESTÁN LOS OCHO QUE LA ETAPA `c` PUEDE PRODUCIR, de los diecisiete vivos del conjunto. Los
/// otros nueve describen condiciones sobre trabajos, sobre el gobierno de las cuentas y sobre el
/// reseteo, que son de etapas posteriores.
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

    /// <summary>
    /// `CONTRATO_ERROR_NO_CLASIFICADO` (`DXT-12`). Cierra el conjunto: ningún fallo llega a la
    /// persona sin representación, que es la definición de fallo silencioso que el producto
    /// viene a eliminar.
    /// </summary>
    public const string UnclassifiedError = "UNCLASSIFIED_ERROR";
}
