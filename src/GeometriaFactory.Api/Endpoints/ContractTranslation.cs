using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Contracts.Works;
using GeometriaFactory.Domain.Entities;
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
/// cerrado cae en el genérico, y el hueco se declara.
///
/// LOS HUECOS DEL CONJUNTO CERRADO, CONTADOS, Y SON DE DOS CLASES DISTINTAS:
///
///  · **Defectos que ninguna petición bien formada alcanza**: unicidad no verificada, estado
///    inicial no negociable, credencial derivada ilegible, credencial no admitida en el alta,
///    papel de administrador fuera del auto-registro, baja sin arrastre y reseteo con arrastre.
///    Esta superficie **no tiene campo con el que pedirlos**. Caen en el genérico y en `500`.
///
///  · **Dos situaciones que la persona sí produce y que el conjunto cerrado no nombra**, y son
///    las dos que la etapa `d` deja elevadas al Product Owner: la **transición de cuenta no
///    admitida** —bloquear una cuenta `Pending`, por ejemplo— y **cualquiera de las cuatro
///    operaciones de `CU-02` pedida sobre la cuenta de administrador** (INV-08). `Api CU-04`
///    FA-03 y FA-04 declaran las dos y remiten a su §10, que **no nombra ningún código**.
///    **[APARTAMIENTO DECLARADO DE LA ETAPA `d`]** Caen en el genérico —que es lo que `Api ADR-04`
///    manda— pero con código de respuesta **`409`** y no `500`, porque `Definicion-Superficie-HTTP.md`
///    §4 define `409` como «la operación es legítima y el estado no la admite», que es
///    literalmente lo que pasa en las dos. Lo que el apartamiento contradice es §6 del mismo
///    documento, que le da al genérico sólo `500` y `503`, y las listas de códigos de `A-07` y
///    `A-08` de su §3, que no incluyen `409`. Se elige así porque un `500` le diría a la persona
///    que el producto falló cuando lo que pasa es que la operación no procede sobre esa cuenta.
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

        // ---- LOS MOTIVOS QUE AGREGA LA ETAPA `d` -------------------------------------------

        // Negativa por facultad, y NO tiene nada que ocultar: el recurso no es ajeno, lo que no
        // alcanza es el papel. El código entró al conjunto cerrado por `PRODUCT-INTAKE` 1.29,
        // que es lo que cerró el punto abierto 1 de `Definicion-Superficie-HTTP.md` §9.
        ApplicationConditionCode.AdministratorRoleRequired =>
            new Translation(ErrorCode.OperationAdminOnly, StatusCodes.Status403Forbidden,
                "Esta operación es del docente a cargo del laboratorio."),

        // Es un campo de la petición que no cumple lo que el contrato le pide, y no un estado que
        // impida la operación: por eso `400` y no `409`. LA RESPUESTA NO DEVUELVE EL CORREO
        // ESPERADO, que es justamente lo que la confirmación escrita existe para exigir.
        ApplicationConditionCode.DeletionConfirmationMismatch =>
            new Translation(ErrorCode.ConfirmationMismatch, StatusCodes.Status400BadRequest,
                "El correo que escribiste no es el de la cuenta que estás dando de baja."),

        // `409` y NO `403`: quien pide TIENE la facultad, y lo que no procede es la operación
        // sobre esa cuenta (`Api CU-05` §10). El camino que sí existe es `A-05`.
        ApplicationConditionCode.ResetLimitedToStudentAccounts =>
            new Translation(ErrorCode.ResetNotApplicableToAdministratorAccount, StatusCodes.Status409Conflict,
                "La cuenta del docente no se resetea. Su contraseña la cambia él mismo."),

        // Sin fuente de material impredecible NO SE PRODUCE NINGUNA PROVISORIA, y el reseteo no
        // se completa. `503` porque no depende de lo que se pidió y puede resolverse solo: el
        // camino declarado es volver a pedirlo (`Api CU-05` §6, `Infrastructure ADR-05` §6).
        InfrastructureConditionCode.RandomnessSourceUnavailable or ConditionCode.EnableWithoutTemporaryCredential =>
            new Translation(ErrorCode.UnclassifiedError, StatusCodes.Status503ServiceUnavailable,
                "No pudimos completar la operación. Probá de nuevo en un rato."),

        // Los dos huecos declarados del conjunto cerrado. Ver el apartamiento en la cabecera.
        ConditionCode.AccountTransitionNotAllowed =>
            new Translation(ErrorCode.UnclassifiedError, StatusCodes.Status409Conflict,
                "La cuenta no admite ese cambio de situación desde la que tiene ahora."),

        ConditionCode.OperationNotApplicableToAdministratorAccount =>
            new Translation(ErrorCode.UnclassifiedError, StatusCodes.Status409Conflict,
                "Esa operación no se aplica sobre la cuenta del docente a cargo del laboratorio."),

        // ---- LOS MOTIVOS QUE AGREGA LA ETAPA `e` -------------------------------------------

        // LOS TRES SALEN POR LA MISMA PUERTA, Y ES EL PUNTO ENTERO DE RN-03: el trabajo que no
        // existe, el que existe y es de otro, y el borrador que el administrador no ve responden
        // el MISMO código, el MISMO texto y el MISMO cuerpo. `403` confirmaría que ese trabajo
        // existe, y ninguna capa de adentro podría repararlo: la regla se rompe eligiendo mal un
        // número. El texto es neutro a propósito y no dice «no es tuyo».
        ConditionCode.WorkNotFoundForRequester
            or ConditionCode.WorkOutsideAdministratorScope
            or ApplicationConditionCode.WorkNotFound =>
            new Translation(ErrorCode.WorkNotFound, StatusCodes.Status404NotFound,
                "No encontramos ese trabajo."),

        // Defectos de la capa que consume, no de la persona: esta superficie no tiene ningún
        // campo con el que pedirlos. El solicitante siempre viaja en el acceso firmado.
        ConditionCode.WorkWithoutOwner
            or ApplicationConditionCode.RequesterNotDeclared
            or ApplicationConditionCode.UnrecognizedRole
            or ConditionCode.OriginalJsonAltered =>
            new Translation(ErrorCode.UnclassifiedError, StatusCodes.Status500InternalServerError,
                "No pudimos completar la operación. Probá de nuevo en un rato."),

        // Los defectos que el conjunto cerrado no describe y que ninguna petición bien formada
        // alcanza. No se inventa un código para ellos: el genérico existe exactamente para que
        // ningún fallo llegue sin representación.
        _ => new Translation(ErrorCode.UnclassifiedError, StatusCodes.Status500InternalServerError,
                "No pudimos completar la operación. Probá de nuevo en un rato."),
    };

    /// <summary>
    /// El alumno pide eliminar un trabajo suyo que ya no está en `Borrador` (`Api CU-06` §6).
    /// </summary>
    /// <remarks>
    /// NO SALE DE <see cref="Translate"/>, Y POR EL MISMO MOTIVO QUE <see cref="AccountNotFound"/>:
    /// el motivo interno `OPERATION_OUTSIDE_DRAFT` tiene **dos destinos según la operación que lo
    /// produzca** —`CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` cuando lo produce la eliminación y
    /// `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` cuando lo produce la reedición—, y resolverlo en la
    /// tabla habría obligado a elegir uno de los dos y a romper el otro.
    ///
    /// LA RESPUESTA DECLARA EL ESTADO ACTUAL, que es lo que la superficie exige, y **no sugiere
    /// ninguna forma de volver a `Borrador`**, porque no existe.
    ///
    /// ESTE CÓDIGO NO SE PRODUCE NUNCA EN EL CAMINO DEL ADMINISTRADOR: a él no lo acota el estado.
    /// </remarks>
    public static IResult WorkStateForbidsDelete(DateTimeOffset occurredAt, WorkStatus currentStatus) =>
        Results.Json(
            new ErrorResponse(
                ErrorCode.StateForbidsDelete,
                $"Este trabajo está en «{LabelOf(currentStatus)}» y ya no lo podés eliminar.",
                [],
                occurredAt),
            statusCode: StatusCodes.Status409Conflict);

    /// <summary>
    /// El alumno pide reeditar un trabajo suyo que ya no está en `Borrador` (`Api CU-06` §10).
    /// </summary>
    /// <remarks>
    /// El otro destino del mismo motivo interno. Entró al conjunto cerrado por `PRODUCT-INTAKE`
    /// **1.29** §17.4 P.3, y **cerró el punto abierto 2** que `Definicion-Superficie-HTTP.md` §9
    /// declaraba: hasta esa decisión este camino caía en el código genérico.
    /// </remarks>
    public static IResult WorkStateForbidsUpdate(DateTimeOffset occurredAt, WorkStatus currentStatus) =>
        Results.Json(
            new ErrorResponse(
                ErrorCode.StateForbidsUpdate,
                $"Este trabajo está en «{LabelOf(currentStatus)}» y ya no se puede modificar.",
                [],
                occurredAt),
            statusCode: StatusCodes.Status409Conflict);

    /// <summary>
    /// Un acceso que no es de papel `Alumno` pide cargar o reeditar un trabajo.
    /// </summary>
    /// <remarks>
    /// **[APARTAMIENTO DECLARADO DE LA ETAPA `e`, ELEVADO AL PRODUCT OWNER]** El conjunto cerrado
    /// tiene código propio para la negativa de facultad **del administrador**
    /// —`OPERATION_ADMIN_ONLY`— y **ninguno para la simétrica**. `Api ADR-04` manda que un motivo
    /// sin código propio caiga en el genérico y que el hueco se declare en lugar de inventarse un
    /// código, y eso es lo que se hace: código genérico con respuesta `403`, que es el número que
    /// `Definicion-Superficie-HTTP.md` §3 ya le da a `A-10` y a `A-11`. Lo que el apartamiento
    /// contradice es §6 del mismo documento, que le da al genérico sólo `500` y `503`; se elige
    /// así porque un `500` diría que el producto falló cuando lo que pasa es que quien pide no es
    /// un alumno. Es el mismo criterio con el que la etapa `d` resolvió sus dos `409`.
    /// </remarks>
    public static IResult WorkWritingLimitedToStudents(DateTimeOffset occurredAt) =>
        Results.Json(
            new ErrorResponse(
                ErrorCode.UnclassifiedError,
                "Cargar y reeditar trabajos es del alumno.",
                [],
                occurredAt),
            statusCode: StatusCodes.Status403Forbidden);

    /// <summary>
    /// La etiqueta castellana de un estado del trabajo (`Norma-De-Nomenclatura.md` §6.7).
    /// </summary>
    /// <remarks>
    /// EL IDENTIFICADOR SE PERSISTE Y SE SERIALIZA; LA ETIQUETA ES LO QUE VE LA PERSONA, y son
    /// dos cosas distintas por decisión `F-02`. `Submitted` se etiqueta «Pendiente» y `Approved`
    /// se etiqueta «Finalizado»: traducir por criterio propio acá metería el identificador inglés
    /// en un texto que lee un alumno, que es lo que el control `V-3` de la norma detecta.
    /// </remarks>
    private static string LabelOf(WorkStatus status) => status switch
    {
        WorkStatus.Draft => "Borrador",
        WorkStatus.Submitted => "Pendiente",
        WorkStatus.Approved => "Finalizado",
        _ => "Rechazado",
    };

    /// <summary>
    /// La cuenta que un punto de administración referencia y no existe (`Api CU-04` CA-06).
    /// </summary>
    /// <remarks>
    /// NO SALE DE <see cref="Translate"/>, Y ES DELIBERADO. El motivo `ACCOUNT_NOT_FOUND` tiene
    /// **dos destinos según el punto que lo produzca**: en el canje y en el cambio de la propia
    /// contraseña se responde igual que ante una contraseña equivocada —`401` genérico, para que
    /// no se pueda averiguar por tanteo qué correos están registrados—, y en los puntos de
    /// administración se responde `404`, porque ahí no hay nada que ocultar: quien pregunta ya
    /// demostró la facultad de administrador. Resolverlo en la tabla habría obligado a elegir uno
    /// de los dos y a romper el otro.
    ///
    /// EL CÓDIGO ES UNA **ADOPCIÓN DE CAUSA DECLARADA** por `Api CU-04` §10: `STUDENT_NOT_FOUND`
    /// nombraba el filtro por alumno de un listado de trabajos, y describe exactamente la misma
    /// situación desde otro punto de acceso. **No es un código nuevo.**
    /// </remarks>
    public static IResult AccountNotFound(DateTimeOffset occurredAt) =>
        Results.Json(
            new ErrorResponse(
                ErrorCode.StudentNotFound,
                "No encontramos esa cuenta.",
                [],
                occurredAt),
            statusCode: StatusCodes.Status404NotFound);

    /// <summary>Arma la respuesta de error del contrato para un motivo interno.</summary>
    public static IResult Problem(string? conditionCode, DateTimeOffset occurredAt, params string[] fields)
    {
        var translation = Translate(conditionCode);
        var details = fields.Select(field => new ErrorDetail(field)).ToArray();

        return Results.Json(
            new ErrorResponse(translation.Code, translation.Message, details, occurredAt),
            statusCode: translation.StatusCode);
    }
    /// <summary>
    /// Traduce las observaciones del dominio a las del contrato, **sin redactar ninguna frase**.
    /// </summary>
    /// <remarks>
    /// ES UNA TRANSPOSICIÓN Y NO UNA REDACCIÓN: cambia el tipo y no el contenido. La especie viaja
    /// por su nombre, la posición y el campo van tal como el dominio los emitió, y el campo
    /// **conserva la clave del texto del alumno** —`Tipo`, `Largo`, `Area`— porque la persona la va
    /// a buscar en su propio programa.
    ///
    /// EL ORDEN ES EL DE EMISIÓN, que es el del recorrido del conjunto raíz: la observación de la
    /// figura 0 antes que la de la 1. Reordenar por especie pondría los errores primero y las
    /// advertencias después, que se lee bien y **deja de decir dónde estaba cada cosa**.
    /// </remarks>
    public static IReadOnlyList<WorkObservation> Observations(WorkOutcomeSnapshot outcome)
    {
        ArgumentNullException.ThrowIfNull(outcome);

        return outcome.Observations is null ? [] : Observations(outcome.Observations);
    }
    /// <summary>
    /// Traduce las piezas reconstruidas del dominio a las del contrato.
    /// </summary>
    /// <remarks>
    /// TRANSPOSICIÓN Y NO REDACCIÓN, igual que las observaciones: cambia el tipo y no el contenido.
    /// Los conjuntos cerrados —el tipo de figura y el papel del componente— viajan **por su nombre**
    /// y nunca por su posición.
    ///
    /// EL ORDEN ES EL DEL CONJUNTO RAÍZ, y los componentes el de su pieza. Reordenar por tipo se
    /// leería mejor y **rompería la única identidad que la pieza tiene**, que es su posición.
    /// </remarks>
    public static IReadOnlyList<WorkPiece> Pieces(IEnumerable<Piece> pieces)
    {
        ArgumentNullException.ThrowIfNull(pieces);

        return
        [
            .. pieces.Select(piece => new WorkPiece(
                piece.Position,
                piece.Type.ToString(),
                piece.DeclaredArea,
                piece.DerivedArea,
                piece.DeclaredVolume,
                piece.DerivedVolume,
                [
                    .. piece.Components.Select(component => new WorkPieceComponent(
                        component.Position,
                        component.Role.ToString(),
                        component.Type.ToString(),
                        component.DeclaredLength,
                        component.DeclaredWidth,
                        component.DeclaredRadius,
                        component.DeclaredArea))
                ]))
        ];
    }

    /// <summary>Traduce las observaciones de una interpretación que **no se guardó**.</summary>
    public static IReadOnlyList<WorkObservation> Observations(IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        return
        [
            .. observations.Select(o => new WorkObservation(
                o.Kind.ToString(),
                o.PiecePosition,
                o.Field,
                o.DeclaredValue,
                o.DerivedValue))
        ];
    }
}
