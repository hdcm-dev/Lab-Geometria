using System.Security.Claims;
using GeometriaFactory.Application;
using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Contracts.Works;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.Security;

namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// Realiza los puntos `A-10`, `A-11` y `A-12` (`Api CU-06`) y `A-13` y `A-14` (`Api CU-07`).
/// </summary>
/// <remarks>
/// LOS CINCO EXIGEN ACCESO FIRMADO Y LOS CINCO PASAN POR LA GUARDIA de
/// <see cref="PendingPasswordChangeGuard"/>, que es un intermediario y no un filtro por punto:
/// agregar estos cinco **no exigió acordarse de nada**, que es exactamente la propiedad por la
/// que se eligió esa forma en la etapa `d`.
///
/// EL TEXTO DEL ALUMNO CRUZA POR ACÁ Y ÉSTE ES EL PRIMER LUGAR DONDE SE PUEDE PERDER. Las tres
/// formas de romperlo sin que nada falle son **normalizar la codificación**, **recortarlo por un
/// límite de tamaño** y **reserializarlo**. Este contrato no hace ninguna: el campo llega como
/// cadena, se pasa como cadena y se guarda como cadena. Ninguna capa de adentro puede repararlo,
/// porque cuando lo recibe ya está alterado (`Api CU-06` §10, RN-08).
///
/// LA CONFUSIÓN MÁS CARA DE ESTA CAPA, Y ACÁ NO OCURRE: **un guardado cuyo texto no verifica es
/// una respuesta exitosa**. Que el resultado traiga `Draft` no cambia el código de respuesta. En
/// la etapa `e` ese es el único resultado posible, porque el texto todavía no se interpreta.
///
/// EL `404` DE `A-11`, `A-12` Y `A-14` ES DE SEGURIDAD Y NO DE CORTESÍA (RN-03). El trabajo
/// inexistente, el ajeno y el que está fuera de lo que el solicitante ve responden **el mismo
/// código, el mismo texto y el mismo cuerpo**: `403` confirmaría que ese trabajo existe, y ésa es
/// la información que RN-03 existe para no dar. Se rompe eligiendo mal un número.
///
/// EL PAPEL SE EXIGE ACÁ SÓLO DONDE LA SUPERFICIE LO DECLARA COMO EXCLUYENTE —`A-10` y `A-11`,
/// papel `Alumno`—, y **el resto de la autorización es de la capa de aplicación**, sobre el dato
/// recuperado. El intake §17.5.P.5 lo dice en una línea: el papel no alcanza.
///
/// **[APARTAMIENTO DECLARADO DE LA ETAPA `e`]** El rechazo de `A-10` y `A-11` a un acceso que no
/// es de papel `Alumno` responde `403` con el código **genérico**. El conjunto cerrado del
/// contrato tiene código propio para la negativa de facultad **del administrador**
/// —`OPERATION_ADMIN_ONLY`— y **ninguno para la simétrica**: no existe un «esta operación es del
/// alumno». `Api ADR-04` manda que un motivo sin código propio caiga en el genérico y que el
/// hueco se declare en lugar de inventarse un código, y eso es lo que se hace acá. El `403` es el
/// número que `Definicion-Superficie-HTTP.md` §3 ya le da a los dos puntos. **Queda elevado al
/// Product Owner.**
///
/// `A-13` NO TIENE NINGÚN PARÁMETRO CON EL QUE PEDIR BORRADORES AJENOS (`Api CU-07` CA-03), y es
/// una propiedad que se verifica **por inspección** y no forzando nada: el único parámetro es el
/// filtro por alumno, que **acota** lo que el alcance del papel ya dejó pasar. Para un acceso de
/// papel `Alumno` el filtro **se ignora** (`Contracts CU-04` FA-01): su alcance es su propia
/// identidad y no hay nada que filtrar.
/// </remarks>
public static class WorkEndpoints
{
    /// <summary>Ruta de `A-10` y de `A-13`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string WorksRoute = "/trabajos";

    /// <summary>Ruta de `A-11`, `A-12` y `A-14`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string WorkRoute = "/trabajos/{id:guid}";

    /// <summary>
    /// Nombre del parámetro de filtro por alumno de `A-13`. **[derivado de la etapa `e`]**
    /// </summary>
    /// <remarks>
    /// VA EN CASTELLANO PORQUE ES SUPERFICIE Y NO IDENTIFICADOR, con el mismo criterio con el que
    /// las rutas de los dieciséis puntos van en castellano: `Norma-De-Nomenclatura.md` §3 rige el
    /// identificador de código —la constante se llama <c>StudentFilterParameter</c>— y §4 rige el
    /// texto, que es lo que la cadena contiene.
    /// </remarks>
    public const string StudentFilterParameter = "alumno";

    /// <summary>Ruta de `A-18`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    /// <remarks>
    /// **NO CUELGA DE `/trabajos`, Y ES DELIBERADO.** El producto tiene **una sola acción de
    /// guardado** y es enviar; colgar de ahí un punto que no guarda insinuaría que hay un segundo
    /// camino por el que un trabajo entra al almacén. Lo que este punto produce es una
    /// interpretación, no un trabajo, y la ruta lo dice.
    /// </remarks>
    public const string InterpretationsRoute = "/interpretaciones";

    public static IEndpointRouteBuilder MapWorkEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // ---- A-10 · el alta de un trabajo ----------------------------------------------------
        endpoints.MapPost(WorksRoute, async (
            WorkSubmissionRequest request,
            HttpContext context,
            LoadAndEditOwnWorkUseCase loadWork,
            ISystemClock clock,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var log = loggerFactory.CreateLogger(typeof(WorkEndpoints));

            if (RoleOf(context.User) != Role.Student)
            {
                return StudentOnly(now);
            }

            if (MissingField(request) is { } missing)
            {
                return ContractTranslation.Problem(ConditionCode.RequiredFieldMissing, now, missing);
            }

            var result = await loadWork
                .LoadAsync(
                    AccountIdOf(context.User),
                    request.Name,
                    request.DeclaredDate,
                    request.Description,
                    // EL TEXTO SE PASA TAL CUAL. Ni `Trim`, ni normalización de saltos de línea,
                    // ni reserialización: la cadena que llegó es la cadena que sigue.
                    request.OriginalJson,
                    cancellationToken)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                // EL REGISTRO ANOTA EL MOTIVO Y NUNCA EL TEXTO DEL ALUMNO, ni entero ni en parte
                // (`Api CU-06` §9, exigencia propia sobre RA-03).
                log.LogInformation("Alta de trabajo rechazada con el motivo {Condition}.", result.ConditionCode);

                return ContractTranslation.Problem(result.ConditionCode, now);
            }

            var outcome = result.Value!;
            log.LogInformation("Trabajo {WorkId} constituido en {Status}.", outcome.WorkId, outcome.Status);

            // `201`: se constituyó algo que antes no existía.
            return Results.Created(
                $"{WorksRoute}/{outcome.WorkId}",
                new WorkSubmissionResponse(outcome.WorkId, outcome.Status.ToString(), outcome.RegisteredAt, ContractTranslation.Observations(outcome)));
        })
        .WithName("SubmitWork")
        .RequireAuthorization();

        // ---- A-18 · la interpretación que NO guarda nada -------------------------------------
        // Lo exige `ADR-08006`: el visor recibe las piezas reconstruidas y no el texto, de modo que
        // previsualizar necesita quien las reconstruya. **Este punto no escribe una sola fila**: no
        // toca el repositorio, no constituye ningún trabajo y no resuelve ningún estado.
        //
        // NO DEVUELVE ESTADO DE TRABAJO PORQUE NO HAY TRABAJO. Resolver el estado es del dominio
        // sobre un trabajo que existe, y devolver uno acá afirmaría una entrega que no ocurrió.
        //
        // EXIGE ACCESO FIRMADO Y PAPEL `Alumno`, con el mismo criterio que `A-10`: interpretar
        // consume el motor del laboratorio, y un punto anónimo que lo consume es una superficie de
        // abuso gratuita sobre la única pieza cara del producto.
        endpoints.MapPost(InterpretationsRoute, (
            WorkInterpretationRequest request,
            HttpContext context,
            IFigureValidator validator,
            ISystemClock clock) =>
        {
            var now = clock.UtcNow;

            if (RoleOf(context.User) != Role.Student)
            {
                return StudentOnly(now);
            }

            if (string.IsNullOrWhiteSpace(request?.OriginalJson))
            {
                return ContractTranslation.Problem(ConditionCode.RequiredFieldMissing, now, "OriginalJson");
            }

            // EL TEXTO SE PASA TAL CUAL, como en `A-10`: ni recorte, ni normalización de saltos de
            // línea, ni reserialización. Que este punto no guarde no lo autoriza a tocarlo.
            var interpretation = validator.Interpret(request.OriginalJson);

            // NO SE REGISTRA NADA DEL TEXTO NI DE SU RESULTADO. `A-10` anota el motivo de un rechazo
            // porque hay un trabajo del que hablar; acá no hay ninguno, y un registro de cada
            // previsualización sería una traza del trabajo en curso de la persona.
            return Results.Ok(new WorkInterpretationResponse(
                interpretation.RootFigureCount,
                ContractTranslation.Pieces(interpretation.Pieces),
                ContractTranslation.Observations(interpretation.Observations),
                ContractTranslation.Tree(interpretation.Tree)));
        })
        .WithName("InterpretWork")
        .RequireAuthorization();

        // ---- A-11 · la reedición de un trabajo en `Draft` ------------------------------------
        endpoints.MapPost(WorkRoute, async (
            Guid id,
            WorkSubmissionRequest request,
            HttpContext context,
            LoadAndEditOwnWorkUseCase loadWork,
            ISystemClock clock,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var log = loggerFactory.CreateLogger(typeof(WorkEndpoints));

            if (RoleOf(context.User) != Role.Student)
            {
                return StudentOnly(now);
            }

            if (MissingField(request) is { } missing)
            {
                return ContractTranslation.Problem(ConditionCode.RequiredFieldMissing, now, missing);
            }

            var result = await loadWork
                .EditAsync(
                    AccountIdOf(context.User),
                    // EL IDENTIFICADOR DE LA RUTA GOBIERNA SOBRE EL DEL CUERPO, con el criterio
                    // que la etapa `d` fijó para las tres solicitudes de cuenta: aceptar dos
                    // fuentes para el mismo dato crea un lugar donde pueden no coincidir.
                    id,
                    request.Name,
                    request.DeclaredDate,
                    request.Description,
                    request.OriginalJson,
                    cancellationToken)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                log.LogInformation(
                    "Reedición del trabajo {WorkId} rechazada con el motivo {Condition}.", id, result.ConditionCode);

                // UN REENVÍO RECHAZADO NO REEMPLAZA EL TEXTO GUARDADO (`Api CU-06` §7): la capa
                // de aplicación no abrió ninguna unidad de trabajo.
                return result.ConditionCode == ConditionCode.OperationOutsideDraft
                    ? ContractTranslation.WorkStateForbidsUpdate(now, result.Value!.Status)
                    : ContractTranslation.Problem(result.ConditionCode, now);
            }

            var outcome = result.Value!;
            log.LogInformation("Trabajo {WorkId} reeditado y resuelto en {Status}.", id, outcome.Status);

            return Results.Ok(
                new WorkSubmissionResponse(outcome.WorkId, outcome.Status.ToString(), outcome.RegisteredAt, ContractTranslation.Observations(outcome)));
        })
        .WithName("ResubmitWork")
        .RequireAuthorization();

        // ---- A-12 · la eliminación, con los dos alcances opuestos ----------------------------
        endpoints.MapDelete(WorkRoute, async (
            Guid id,
            HttpContext context,
            DeleteWorkUseCase deleteWork,
            ISystemClock clock,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var log = loggerFactory.CreateLogger(typeof(WorkEndpoints));

            // UN SOLO PUNTO PARA LOS DOS ALCANCES, Y NO DOS. Lo que cambia no es el tipo sino la
            // regla que lo acota, y esa regla vive adentro. Dos puntos habrían puesto el papel en
            // la ruta, que es información sobre el solicitante donde no hace falta.
            var result = await deleteWork
                .ExecuteAsync(AccountIdOf(context.User), RoleOf(context.User), id, cancellationToken)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                log.LogInformation(
                    "Eliminación del trabajo {WorkId} rechazada con el motivo {Condition}.", id, result.ConditionCode);

                return result.ConditionCode == ConditionCode.OperationOutsideDraft
                    ? ContractTranslation.WorkStateForbidsDelete(now, result.Value)
                    : ContractTranslation.Problem(result.ConditionCode, now);
            }

            log.LogInformation("Trabajo {WorkId} retirado.", id);

            // `204`: se retiró algo y no hay cuerpo que devolver.
            return Results.NoContent();
        })
        .WithName("DeleteWork")
        .RequireAuthorization();

        // ---- A-13 · el listado, con el alcance que el papel determina ------------------------
        endpoints.MapGet(WorksRoute, async (
            HttpContext context,
            ConsultOwnWorksUseCase consultOwnWorks,
            ReviewCommissionWorksUseCase reviewWorks,
            IFigureValidator validator,
            ISystemClock clock,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var role = RoleOf(context.User);

            ApplicationResult<IReadOnlyList<WorkListEntry>> result;

            if (role == Role.Administrator)
            {
                Guid? filter = Guid.TryParse(
                    context.Request.Query[StudentFilterParameter], out var studentId) ? studentId : null;

                result = await reviewWorks
                    .ListAsync(role, filter, cancellationToken)
                    .ConfigureAwait(false);
            }
            else
            {
                // EL FILTRO SE IGNORA PARA EL ALUMNO (`Contracts CU-04` FA-01): su alcance es su
                // propia identidad, y no hay ningún valor del parámetro que lo amplíe.
                result = await consultOwnWorks
                    .ListAsync(AccountIdOf(context.User), cancellationToken)
                    .ConfigureAwait(false);
            }

            if (!result.Succeeded)
            {
                return result.ConditionCode == ApplicationConditionCode.AccountNotFound
                    ? ContractTranslation.AccountNotFound(now)
                    : ContractTranslation.Problem(result.ConditionCode, now);
            }

            // UN LISTADO VACÍO NO ES UN FALLO: `200` con una colección vacía, y quien la consume
            // distingue vacío de fallo por el tipo recibido y no por el conteo.
            return Results.Ok(result.Value!.Select(entry => new WorkListItem(
                entry.WorkId,
                entry.Name,
                entry.DeclaredDate,
                entry.Status.ToString(),
                entry.OwnerId,
                entry.OwnerEmail,
                entry.OwnerFirstName,
                entry.OwnerLastName)).ToArray());
        })
        .WithName("ListWorks")
        .RequireAuthorization();

        // ---- A-14 · el detalle de un trabajo -------------------------------------------------
        endpoints.MapGet(WorkRoute, async (
            Guid id,
            HttpContext context,
            ConsultOwnWorksUseCase consultOwnWorks,
            ReviewCommissionWorksUseCase reviewWorks,
            IFigureValidator validator,
            ISystemClock clock,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var role = RoleOf(context.User);

            var result = role == Role.Administrator
                ? await reviewWorks.DetailAsync(role, id, cancellationToken).ConfigureAwait(false)
                : await consultOwnWorks
                    .DetailAsync(AccountIdOf(context.User), id, cancellationToken)
                    .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                // LOS TRES MOTIVOS QUE LLEGAN ACÁ SALEN POR LA MISMA PUERTA: el inexistente, el
                // ajeno y el borrador que el administrador no ve. La traducción los junta a
                // propósito, y es lo que hace indistinguible el trabajo que no existe del que
                // existe y no es de quien pregunta.
                return ContractTranslation.Problem(result.ConditionCode, now);
            }

            var detail = result.Value!;

            return Results.Ok(new WorkDetailResponse(
                detail.WorkId,
                detail.Name,
                detail.DeclaredDate,
                detail.Description,
                detail.OriginalJson,
                detail.Status.ToString(),
                detail.AdministratorComment,
                detail.OwnerId,
                detail.OwnerEmail,
                detail.OwnerFirstName,
                detail.OwnerLastName,
                detail.CreatedAt,
                detail.UpdatedAt,
                detail.RootFigureCount,
                ContractTranslation.Pieces(detail.Pieces),
                ContractTranslation.Observations(detail.Observations),

                // EL ÁRBOL SE DERIVA ACÁ Y LAS PIEZAS NO, y la asimetría es deliberada. Las piezas
                // guardadas son el resultado de la evaluación: reinterpretarlas dejaría que la
                // vista muestre algo distinto de lo que el producto decidió. El árbol no evalúa
                // nada —es la forma del texto, que está guardado literal e inmutable—, y guardarlo
                // crearía una segunda copia del texto capaz de decir otra cosa.
                //
                // LO ARMA EL MISMO COMPONENTE QUE LEE EL TEXTO EN EL ENVÍO, así que el árbol de la
                // previsualización y el de la vista **no pueden diferir**: es el mismo código.
                ContractTranslation.Tree(validator.Interpret(detail.OriginalJson).Tree)));
        })
        .WithName("GetWork")
        .RequireAuthorization();

        return endpoints;
    }

    /// <summary>
    /// El primer campo obligatorio que falta, o nulo si están los tres (`Api CU-06` §6).
    /// </summary>
    /// <remarks>
    /// LA DESCRIPCIÓN NO ESTÁ EN LA LISTA y no es un olvido: admite vacío y admite ausencia.
    ///
    /// EL TEXTO SE COMPRUEBA POR PRESENCIA Y NO POR FORMA: se rechaza que **no venga**, no que no
    /// verifique. Un texto que no verifica es el caso normal del producto y termina en `Draft`.
    /// </remarks>
    private static string? MissingField(WorkSubmissionRequest? request) =>
        request switch
        {
            null => nameof(WorkSubmissionRequest.OriginalJson),
            _ when string.IsNullOrWhiteSpace(request.Name) => nameof(WorkSubmissionRequest.Name),
            _ when string.IsNullOrWhiteSpace(request.DeclaredDate) => nameof(WorkSubmissionRequest.DeclaredDate),
            _ when request.OriginalJson is null => nameof(WorkSubmissionRequest.OriginalJson),
            _ => null,
        };

    /// <summary>
    /// La negativa de `A-10` y `A-11` a un acceso que no es de papel `Alumno`.
    /// </summary>
    /// <remarks>
    /// Ver el apartamiento declarado en la cabecera de este tipo: el conjunto cerrado no tiene
    /// código para esta negativa y esta capa **no inventa códigos** (`Api ADR-04`).
    /// </remarks>
    private static IResult StudentOnly(DateTimeOffset occurredAt) =>
        ContractTranslation.WorkWritingLimitedToStudents(occurredAt);

    /// <summary>
    /// El papel que el acceso firmado declara. Un acceso sin papel reconocible se trata como
    /// `Student`, que es el papel de menor alcance sobre esta superficie.
    /// </summary>
    private static Role RoleOf(ClaimsPrincipal principal)
    {
        var claim = principal.FindFirstValue(AccessTokenIssuer.RoleClaim);

        return Enum.TryParse<Role>(claim, ignoreCase: false, out var role) && Enum.IsDefined(role)
            ? role
            : Role.Student;
    }

    /// <summary>
    /// La identidad que el acceso firmado declara, o vacía si no trae ninguna reconocible.
    /// </summary>
    /// <remarks>
    /// LA IDENTIDAD VACÍA NO ABRE NINGUNA PUERTA: la capa de aplicación la rechaza antes de
    /// consultar nada, y ningún trabajo tiene ese dueño.
    /// </remarks>
    private static Guid AccountIdOf(ClaimsPrincipal principal) =>
        AuthenticationEndpoints.AccountIdOf(principal) ?? Guid.Empty;
}
