namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// Los códigos propios del catálogo de `GeometriaFactory-Application` que la etapa `c` ejerce.
/// </summary>
/// <remarks>
/// Los demás códigos que esta capa devuelve son los que PROPAGA del dominio, y para ésos la
/// fuente es `GeometriaFactory.Domain.Values.ConditionCode`: no se redeclaran acá, porque un
/// código con dos declaraciones es un código con dos verdades. Identificadores en inglés por
/// `Norma-De-Nomenclatura.md` §6.8.2.
/// </remarks>
public static class ApplicationConditionCode
{
    /// <summary>`CORREO_YA_REGISTRADO` — CU-01, CU-10, RN-02.</summary>
    public const string EmailAlreadyRegistered = "EMAIL_ALREADY_REGISTERED";

    /// <summary>`CUENTA_INEXISTENTE` — CU-02, CU-03, CU-11.</summary>
    public const string AccountNotFound = "ACCOUNT_NOT_FOUND";

    // ---- LOS TRES QUE AGREGA LA ETAPA `d` --------------------------------------------------

    /// <summary>
    /// `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` — CU-02, CU-11, RN-01. Es una negativa **por
    /// facultad y no por pertenencia**: acá la existencia de la cuenta destino no se oculta,
    /// porque quien pregunta no está pidiendo un recurso ajeno sino ejerciendo una facultad que
    /// no tiene (CU-02 §6).
    /// </summary>
    public const string AdministratorRoleRequired = "ADMINISTRATOR_ROLE_REQUIRED";

    /// <summary>`CONFIRMACION_DE_BAJA_NO_COINCIDE` — CU-02, RN-07. La unidad de trabajo no se abre.</summary>
    public const string DeletionConfirmationMismatch = "DELETION_CONFIRMATION_MISMATCH";

    /// <summary>
    /// `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` — CU-11 FA-02, RN-15, INV-08. El acotamiento es
    /// **de papel y no de situación de cuenta**, que es exactamente lo que lo hace compatible
    /// con el reseteo sobre `Pending` y sobre `Blocked`.
    /// </summary>
    public const string ResetLimitedToStudentAccounts = "RESET_LIMITED_TO_STUDENT_ACCOUNTS";
}
