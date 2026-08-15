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
/// ACÁ ESTÁN LOS DOCE QUE LA ETAPA `c` USA, y no los 42 del catálogo: escribir los otros treinta
/// sería declarar condiciones que ninguna operación de esta etapa puede producir.
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
}
