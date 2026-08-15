namespace GeometriaFactory.Domain.Values;

/// <summary>
/// Los códigos de condición del dominio que la etapa `c` ejerce.
/// </summary>
/// <remarks>
/// El conjunto es CERRADO y su fuente única es el catálogo de `GeometriaFactory-Domain/03`
/// (`Domain ADR-02` §2): esta clase no acuña ninguno, los transcribe. El identificador va en
/// inglés por `Norma-De-Nomenclatura.md` §6.8.1, que da la correspondencia una a una con el
/// nombre castellano con el que los declara el catálogo.
///
/// ACÁ ESTÁN LOS DIECINUEVE QUE LAS ETAPAS `c` Y `d` USAN, y no los 42 del catálogo: escribir
/// los otros veintitrés sería declarar condiciones que ninguna operación de estas etapas puede
/// producir. Los doce primeros los escribió la etapa `c`; los siete últimos los agrega la `d`
/// con el ciclo de vida de la cuenta (CU-01, CU-02 y CU-13).
/// </remarks>
public static class ConditionCode
{
    /// <summary>`DATO_OBLIGATORIO_AUSENTE` — CU-01, CU-05, CU-12.</summary>
    public const string RequiredFieldMissing = "REQUIRED_FIELD_MISSING";

    /// <summary>`ADMINISTRADOR_YA_CONFIGURADO` — CU-12, RN-01, INV-05.</summary>
    public const string AdministratorAlreadyConfigured = "ADMINISTRATOR_ALREADY_CONFIGURED";

    /// <summary>`UNICIDAD_DE_CORREO_NO_VERIFICADA` — CU-01, CU-12, RN-02, INV-01.</summary>
    public const string EmailUniquenessNotVerified = "EMAIL_UNIQUENESS_NOT_VERIFIED";

    /// <summary>`CONFIGURACION_SIN_CREDENCIAL` — CU-12.</summary>
    public const string SetupWithoutCredential = "SETUP_WITHOUT_CREDENTIAL";

    /// <summary>`ESTADO_INICIAL_NO_NEGOCIABLE` — CU-01, CU-12.</summary>
    public const string InitialStatusNotNegotiable = "INITIAL_STATUS_NOT_NEGOTIABLE";

    /// <summary>`CUENTA_NO_HABILITADA_PARA_CREDENCIAL` — CU-03, RN-06.</summary>
    public const string AccountNotEnabledForCredential = "ACCOUNT_NOT_ENABLED_FOR_CREDENTIAL";

    /// <summary>`CREDENCIAL_YA_FIJADA` — CU-03.</summary>
    public const string CredentialAlreadySet = "CREDENTIAL_ALREADY_SET";

    /// <summary>`CREDENCIAL_VIGENTE_NO_VERIFICADA` — CU-03.</summary>
    public const string CurrentCredentialNotVerified = "CURRENT_CREDENTIAL_NOT_VERIFIED";

    /// <summary>`VALOR_DERIVADO_VACIO` — CU-03, CU-13.</summary>
    public const string EmptyDerivedValue = "EMPTY_DERIVED_VALUE";

    /// <summary>`CUENTA_PENDIENTE` — CU-04, RN-06, INV-06. Motivo de no admisión.</summary>
    public const string AccountPending = "ACCOUNT_PENDING";

    /// <summary>`CUENTA_BLOQUEADA` — CU-04, RN-06, INV-06. Motivo de no admisión.</summary>
    public const string AccountBlocked = "ACCOUNT_BLOCKED";

    /// <summary>`CAMBIO_DE_CONTRASENA_PENDIENTE` — CU-04, RN-13, RN-16, INV-09. Motivo de no admisión.</summary>
    public const string PasswordChangePending = "PASSWORD_CHANGE_PENDING";

    // ---- LOS SIETE QUE AGREGA LA ETAPA `d` -------------------------------------------------
    // Los siete ya estaban en el catálogo y en `Norma-De-Nomenclatura.md` §6.8.1 con su nombre
    // inglés fijado por `F-03`: acá se transcriben, no se acuñan.

    /// <summary>`CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` — CU-01. El auto-registro no lleva contraseña.</summary>
    public const string CredentialNotAllowedOnRegistration = "CREDENTIAL_NOT_ALLOWED_ON_REGISTRATION";

    /// <summary>`PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` — CU-01, RN-01. Remite a CU-12.</summary>
    public const string AdministratorRoleOutsideThisPath = "ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH";

    /// <summary>`TRANSICION_DE_CUENTA_NO_ADMITIDA` — CU-02. El par estado y transición no figura en la tabla.</summary>
    public const string AccountTransitionNotAllowed = "ACCOUNT_TRANSITION_NOT_ALLOWED";

    /// <summary>`HABILITACION_SIN_CREDENCIAL_PROVISORIA` — CU-02, RN-16, RN-14.</summary>
    public const string EnableWithoutTemporaryCredential = "ENABLE_WITHOUT_TEMPORARY_CREDENTIAL";

    /// <summary>`BAJA_SIN_ARRASTRE_DE_TRABAJOS` — CU-02, RN-07.</summary>
    public const string DeletionWithoutWorkCascade = "DELETION_WITHOUT_WORK_CASCADE";

    /// <summary>
    /// `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` — CU-02, CU-13, RN-01, INV-08.
    /// UN SOLO CÓDIGO PARA LAS CINCO OPERACIONES: las cuatro del ciclo de vida y el reseteo.
    /// `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` quedó retirado en CU-02 1.2 porque cubría una
    /// sola de las cuatro, y §6.9 de la norma unifica los dos nombres castellanos en éste.
    /// </summary>
    public const string OperationNotApplicableToAdministratorAccount =
        "OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT";

    /// <summary>`RESETEO_CON_ARRASTRE_DE_TRABAJOS` — CU-13, RN-12. Resetear no es dar de baja.</summary>
    public const string ResetWithWorkCascade = "RESET_WITH_WORK_CASCADE";
}
