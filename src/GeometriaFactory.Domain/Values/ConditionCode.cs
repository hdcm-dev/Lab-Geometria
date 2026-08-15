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
/// ACÁ ESTÁN LOS TREINTA Y TRES QUE LAS ETAPAS `c`, `d` Y `e` USAN, y no los 42 del catálogo:
/// escribir los NUEVE restantes sería declarar condiciones que ninguna operación de estas etapas
/// puede producir. Ocho de esos nueve describen la **interpretación del texto**, la reconstrucción
/// de las piezas y el registro de las observaciones —`WARNING_MISSING_BOTH_VALUES`,
/// `ERROR_WITHOUT_LOCATION`, `UNKNOWN_OBSERVATION_KIND`, `DECLARED_FAMILY_CONTRADICTS_TYPE`,
/// `OBSERVATION_ON_MISSING_PIECE`, `INVALID_PIECE_POSITION`, `REBUILD_ON_TERMINAL_WORK` y
/// `UNKNOWN_PIECE_TYPE`—, que son de la etapa `f`. El noveno,
/// `OUTCOME_NOT_ALLOWED_BY_CONTRACT`, es **inalcanzable por construcción**: el desenlace tiene
/// operación propia —<c>ApplyOutcome</c>— y no hay forma de pedirlo por la vía del envío.
///
/// Los doce primeros los escribió la etapa `c`; los siete siguientes los agrega la `d` con el
/// ciclo de vida de la cuenta (CU-01, CU-02 y CU-13); los CATORCE últimos los agrega la `e` con
/// el trabajo (CU-05, CU-08, CU-09, CU-10 y CU-11).
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

    // ---- LOS CATORCE QUE AGREGA LA ETAPA `e` -----------------------------------------------
    // Los catorce ya estaban en el catálogo y en `Norma-De-Nomenclatura.md` §6.8.1 con su nombre
    // inglés fijado por `F-03`: acá se transcriben, no se acuñan.

    /// <summary>`TRABAJO_SIN_DUENO` — CU-05, RN-03, INV-02. Un trabajo sin dueño no es un trabajo.</summary>
    public const string WorkWithoutOwner = "WORK_WITHOUT_OWNER";

    /// <summary>
    /// `TEXTO_ORIGINAL_ALTERADO` — CU-05, RN-08. El consumidor aporta como original una versión
    /// corregida por el producto. El producto **no edita el dato del alumno**.
    /// </summary>
    public const string OriginalJsonAltered = "ORIGINAL_JSON_ALTERED";

    /// <summary>`REEDICION_FUERA_DE_BORRADOR` — CU-05, RN-04. La reedición está acotada al borrador.</summary>
    public const string EditOutsideDraft = "EDIT_OUTSIDE_DRAFT";

    /// <summary>
    /// `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` — CU-09, RN-03, INV-02. **Deliberadamente
    /// indistinguible de la inexistencia**: el consumidor lo traduce a «no encontrado» y NUNCA a
    /// «no autorizado», porque «no autorizado» confirmaría que el trabajo ajeno existe.
    /// </summary>
    public const string WorkNotFoundForRequester = "WORK_NOT_FOUND_FOR_REQUESTER";

    /// <summary>
    /// `OPERACION_FUERA_DE_BORRADOR` — CU-09, RN-04, INV-03. Es un motivo **distinto** del
    /// anterior porque acá la existencia del trabajo ya está admitida para su dueño.
    /// </summary>
    public const string OperationOutsideDraft = "OPERATION_OUTSIDE_DRAFT";

    /// <summary>`OPERACION_DESCONOCIDA` — CU-09, CU-11. La operación no pertenece al conjunto declarado.</summary>
    public const string UnknownOperation = "UNKNOWN_OPERATION";

    /// <summary>
    /// `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` — CU-11, RN-11, RN-04. El trabajo está en
    /// `Draft`. **No oculta la existencia**: expresa que está fuera de su flujo de trabajo.
    /// </summary>
    public const string WorkOutsideAdministratorScope = "WORK_OUTSIDE_ADMINISTRATOR_SCOPE";

    /// <summary>`ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR` — CU-11, RN-01, RN-11.</summary>
    public const string ScopeRequiresAdministratorRole = "SCOPE_REQUIRES_ADMINISTRATOR_ROLE";

    /// <summary>`ENVIO_FUERA_DE_BORRADOR` — CU-08, RN-05. Acotado a `Submitted`: los terminales llevan el suyo.</summary>
    public const string SubmissionOutsideDraft = "SUBMISSION_OUTSIDE_DRAFT";

    /// <summary>
    /// `ENVIO_SIN_INTERPRETACION` — CU-08, RN-05. El envío decide sobre el resultado de la
    /// interpretación, y sin ese resultado no hay nada que decidir. **En la etapa `e` es el
    /// rechazo que recibe todo envío**, porque el intérprete del texto es de la etapa `f`.
    /// </summary>
    public const string SubmissionWithoutParseResult = "SUBMISSION_WITHOUT_PARSE_RESULT";

    /// <summary>
    /// `TRANSICION_DESDE_ESTADO_TERMINAL` — CU-08, CU-10, RN-10, INV-07. **Un solo motivo para los
    /// dos terminales**, que no los distingue.
    /// </summary>
    public const string TransitionFromTerminalStatus = "TRANSITION_FROM_TERMINAL_STATUS";

    /// <summary>`DESENLACE_FUERA_DE_PENDIENTE` — CU-10, RN-10, RN-11.</summary>
    public const string OutcomeOutsideSubmitted = "OUTCOME_OUTSIDE_SUBMITTED";

    /// <summary>`DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR` — CU-10, RN-10, RN-01. Facultad exclusiva y no delegable.</summary>
    public const string OutcomeRequiresAdministratorRole = "OUTCOME_REQUIRES_ADMINISTRATOR_ROLE";

    /// <summary>`DESENLACE_DESCONOCIDO` — CU-10, RN-10. El desenlace no es aprobar ni rechazar.</summary>
    public const string UnknownOutcome = "UNKNOWN_OUTCOME";
}
