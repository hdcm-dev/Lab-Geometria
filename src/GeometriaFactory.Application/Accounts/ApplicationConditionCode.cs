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
}
