using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// CU-09 — Las DOS traducciones que convierten un motivo interno en una respuesta de protocolo.
/// </summary>
/// <remarks>
/// El recorrido es el de `Definicion-Superficie-HTTP.md` §5, y son dos y no una:
/// <c>motivo de la capa de aplicación → código del contrato → código de respuesta</c>.
/// Confundirlas es el defecto característico de esta capa.
///
/// ESTA CAPA NO INVENTA CÓDIGOS (`Api ADR-04`): un motivo sin código propio en el conjunto
/// cerrado cae en el genérico, y el hueco se declara. Acá hay tres motivos en esa situación
/// —unicidad no verificada, estado inicial no negociable y credencial derivada ilegible— y los
/// tres son **defectos**, no situaciones de la persona: ninguno es alcanzable desde una petición
/// bien formada, porque esta superficie no tiene campo con el que pedirlos.
///
/// EL TEXTO CAMBIA CON EL MOTIVO AUNQUE EL CÓDIGO NO. `ACCOUNT_NOT_ENABLED` cubre la cuenta
/// `Pending` y la `Blocked` con un solo código, y el intake §17.5.P.5 exige `403` **con motivo**,
/// para que la persona sepa en qué situación está. El código es del conjunto cerrado; el texto
/// es de esta capa y no le agrega ningún código nuevo al contrato.
///
/// NINGÚN TEXTO LLEVA DIRECCIÓN DE SERVICIO INTERNO, RUTA DEL ALMACÉN NI TRAZA (RA-03).
/// </remarks>
public static class ContractTranslation
{
    /// <summary>La traducción completa de un motivo interno.</summary>
    /// <param name="Code">Código del conjunto cerrado del contrato.</param>
    /// <param name="StatusCode">Código de respuesta de los diez de la superficie.</param>
    /// <param name="Message">Texto neutro para la persona.</param>
    public sealed record Translation(string Code, int StatusCode, string Message);

    /// <summary>Traduce un motivo interno. Un motivo desconocido cae en el genérico y en `500`.</summary>
    public static Translation Translate(string? conditionCode) => conditionCode switch
    {
        ConditionCode.RequiredFieldMissing or ConditionCode.SetupWithoutCredential or ConditionCode.EmptyDerivedValue =>
            new Translation(ErrorCode.RequiredFieldMissing, StatusCodes.Status400BadRequest,
                "Faltan datos para completar la operación."),

        ConditionCode.AdministratorAlreadyConfigured =>
            new Translation(ErrorCode.AdministratorAlreadyConfigured, StatusCodes.Status409Conflict,
                "Este laboratorio ya tiene su cuenta de administrador. Entrá con ella."),

        ApplicationConditionCode.EmailAlreadyRegistered =>
            new Translation(ErrorCode.EmailAlreadyRegistered, StatusCodes.Status409Conflict,
                "Ese correo ya está registrado."),

        // El correo desconocido y la contraseña equivocada responden IGUAL, y es deliberado:
        // distinguirlos permitiría averiguar por tanteo qué correos están registrados.
        ApplicationConditionCode.AccountNotFound or ConditionCode.CurrentCredentialNotVerified =>
            new Translation(ErrorCode.InvalidCredentials, StatusCodes.Status401Unauthorized,
                "El correo o la contraseña no corresponden."),

        ConditionCode.AccountPending =>
            new Translation(ErrorCode.AccountNotEnabled, StatusCodes.Status403Forbidden,
                "Tu cuenta está a la espera de que el docente la habilite. Todavía no podés entrar."),

        ConditionCode.AccountBlocked or ConditionCode.AccountNotEnabledForCredential =>
            new Translation(ErrorCode.AccountNotEnabled, StatusCodes.Status403Forbidden,
                "Tu cuenta dejó de estar habilitada. Pedile al docente que la rehabilite."),

        ConditionCode.PasswordChangePending =>
            new Translation(ErrorCode.PasswordChangeRequired, StatusCodes.Status403Forbidden,
                "La contraseña con la que entraste es provisoria. Antes de seguir tenés que elegir una nueva."),

        // Los tres defectos que el conjunto cerrado no describe. No se inventa un código para
        // ellos: el genérico existe exactamente para que ningún fallo llegue sin representación.
        _ => new Translation(ErrorCode.UnclassifiedError, StatusCodes.Status500InternalServerError,
                "No pudimos completar la operación. Probá de nuevo en un rato."),
    };

    /// <summary>Arma la respuesta de error del contrato para un motivo interno.</summary>
    public static IResult Problem(string? conditionCode, DateTimeOffset occurredAt, params string[] fields)
    {
        var translation = Translate(conditionCode);
        var details = fields.Select(field => new ErrorDetail(field)).ToArray();

        return Results.Json(
            new ErrorResponse(translation.Code, translation.Message, details, occurredAt),
            statusCode: translation.StatusCode);
    }
}
