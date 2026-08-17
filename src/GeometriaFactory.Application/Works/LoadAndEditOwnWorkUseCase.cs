using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Guards;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Works;

/// <summary>
/// CU-04 — Orquesta la carga de un trabajo y su reedición mientras siga en `Draft`.
/// </summary>
/// <remarks>
/// EL DUEÑO ES SIEMPRE EL SOLICITANTE Y NO UN CAMPO DE LA SOLICITUD. No existe ningún camino por
/// el que el consumidor elija a nombre de quién se carga un trabajo: la identidad sale del acceso
/// firmado y entra acá como argumento. Es INV-02 hecho estructura.
///
/// LA UNIDAD DE TRABAJO SE ABRE RECIÉN EN EL ÚLTIMO PASO (`CU-04` §6): ninguna de las cinco
/// condiciones de este contrato deja escritura parcial, porque todas se resuelven antes de tocar
/// el repositorio.
///
/// EL SELLO LO APORTA EL PUERTO DE RELOJ Y NO EL DOMINIO (`Domain ADR-06`), y **no se confunde
/// con la fecha del alumno**: la carga toma el sello de alta y la reedición el de modificación,
/// mientras que la fecha declarada la escribe la persona y viaja como texto.
///
/// EL ESTADO NO SE RESUELVE ACÁ. El alumno tiene una sola acción de guardado —enviar— y es
/// `Application CU-05` quien interpreta el texto y decide entre `Draft` y `Submitted`. **`CU-05`
/// es de la etapa `f`**, y por eso en la etapa `e` toda carga y toda reedición terminan en
/// `Draft`, que es exactamente lo que el roadmap pide para la transición `e` → `f`.
/// </remarks>
public sealed class LoadAndEditOwnWorkUseCase
{
    private readonly IWorkRepository _works;
    private readonly ISystemClock _clock;
    private readonly IFigureValidator _validator;

    public LoadAndEditOwnWorkUseCase(IWorkRepository works, ISystemClock clock, IFigureValidator validator)
    {
        ArgumentNullException.ThrowIfNull(works);
        ArgumentNullException.ThrowIfNull(clock);
        ArgumentNullException.ThrowIfNull(validator);

        _works = works;
        _clock = clock;
        _validator = validator;
    }

    /// <summary>
    /// `Application CU-05` — Interpreta el texto, adopta el resultado y **deja que el dominio
    /// resuelva el estado**.
    /// </summary>
    /// <remarks>
    /// ETAPA `f`. Es el paso que la etapa `e` declaró pendiente en las dos operaciones: hasta hoy
    /// nadie podía declarar un resultado de interpretación, y por eso todo trabajo quedaba en
    /// `Draft`.
    ///
    /// ESTA CAPA NO DECIDE EL ESTADO Y NO CUENTA ERRORES POR SU CUENTA: le entrega al dominio si
    /// hubo observaciones de especie error de validación, y RN-05 hace el resto. Contar acá pondría
    /// la regla en dos lugares.
    ///
    /// UN TEXTO QUE NO VERIFICA NO ES UN RECHAZO DE LA OPERACIÓN (`Domain CU-08` FA-01): el
    /// resultado se aplica, el trabajo queda en `Draft` **con sus observaciones** y el alumno
    /// corrige y vuelve a enviar. Lo único que se rechaza acá es que la interpretación misma no se
    /// pueda adoptar, que es defecto del validador y no del alumno.
    /// </remarks>
    private DomainResult InterpretAndSubmit(Work work, DateTimeOffset now)
    {
        var interpretation = _validator.Interpret(work.OriginalJson);

        var adoption = work.AdoptInterpretation(
            interpretation.RootFigureCount,
            interpretation.Pieces,
            interpretation.Observations,
            now);

        if (!adoption.Succeeded)
        {
            return adoption;
        }

        return work.Submit(
            parseResultDeclared: true,
            validationErrorsDeclared: interpretation.Observations
                .Any(o => o.Kind == ObservationKind.ValidationError),
            now);
    }

    /// <summary>
    /// Carga un trabajo nuevo a nombre del solicitante (`Api CU-06` `A-10`).
    /// </summary>
    /// <param name="requesterId">Identidad del alumno que carga, tomada del acceso firmado.</param>
    /// <param name="name">Título del trabajo.</param>
    /// <param name="declaredDate">Fecha que declara el alumno.</param>
    /// <param name="description">Descripción, opcional.</param>
    /// <param name="originalJson">El texto del alumno, TAL COMO LLEGÓ.</param>
    public async Task<ApplicationResult<WorkOutcomeSnapshot>> LoadAsync(
        Guid requesterId,
        string? name,
        string? declaredDate,
        string? description,
        string? originalJson,
        CancellationToken cancellationToken = default)
    {
        // Paso 2 — el sello, antes de constituir: el dominio no lee el reloj.
        var now = _clock.UtcNow;

        var constitution = Work.Create(
            requesterId,
            name,
            declaredDate,
            description,
            originalJson,
            // ESTA CAPA NUNCA CORRIGE EL TEXTO DEL ALUMNO, y la declaración lo dice en el sitio
            // donde se podría dejar de cumplir (RN-08).
            originalJsonPreservedDeclared: true,
            now);

        if (!constitution.Succeeded)
        {
            // Paso 5 no ocurre: el repositorio no recibe ninguna escritura (`CU-04` CA-05).
            return ApplicationResult<WorkOutcomeSnapshot>.Rejected(constitution.ConditionCode!);
        }

        var work = constitution.Value!;

        // Paso 4 — ENVIAR ES LA ÚNICA ACCIÓN DE GUARDADO: el texto se interpreta acá mismo y el
        // estado con el que el trabajo nace ya no es siempre `Draft`.
        var submission = InterpretAndSubmit(work, now);

        if (!submission.Succeeded)
        {
            // El repositorio no recibe ninguna escritura: nada quedó a medio constituir.
            return ApplicationResult<WorkOutcomeSnapshot>.Rejected(submission.ConditionCode!);
        }

        await _works.AddAsync(work, cancellationToken).ConfigureAwait(false);

        return ApplicationResult<WorkOutcomeSnapshot>.Applied(
            new WorkOutcomeSnapshot(work.Id, work.Status, work.CreatedAt, work.Observations));
    }

    /// <summary>
    /// FA-01 — Reedita un trabajo propio que sigue en `Draft` (`Api CU-06` `A-11`).
    /// </summary>
    /// <remarks>
    /// LA PERTENENCIA SE COMPRUEBA CONTRA EL DATO RECUPERADO Y NO CONTRA EL ACCESO. Un trabajo
    /// que no existe y uno que existe y es de otro devuelven **el mismo motivo**, que el
    /// consumidor traduce a «no encontrado»: distinguirlos confirmaría la existencia del ajeno
    /// (RN-03).
    /// </remarks>
    public async Task<ApplicationResult<WorkOutcomeSnapshot>> EditAsync(
        Guid requesterId,
        Guid workId,
        string? name,
        string? declaredDate,
        string? description,
        string? originalJson,
        CancellationToken cancellationToken = default)
    {
        if (requesterId == Guid.Empty)
        {
            return ApplicationResult<WorkOutcomeSnapshot>.Rejected(ConditionCode.WorkWithoutOwner);
        }

        var work = await _works.FindByIdAsync(workId, cancellationToken).ConfigureAwait(false);

        if (work is null)
        {
            // MISMO MOTIVO QUE EL TRABAJO AJENO, y por eso se devuelve el del dominio en lugar de
            // uno propio de esta capa: los dos caminos tienen que ser indistinguibles.
            return ApplicationResult<WorkOutcomeSnapshot>.Rejected(ConditionCode.WorkNotFoundForRequester);
        }

        var access = work.ResolveStudentAccess(requesterId, WorkOperation.Edit);
        if (!access.Succeeded)
        {
            // EL RECHAZO POR ESTADO TRANSPORTA EL ESTADO ACTUAL, porque es lo que la respuesta
            // tiene que declarar. El de pertenencia NO transporta nada: transportarlo confirmaría
            // que el trabajo ajeno existe, que es exactamente lo que RN-03 impide.
            return access.ConditionCode == ConditionCode.OperationOutsideDraft
                ? ApplicationResult<WorkOutcomeSnapshot>.Rejected(
                    access.ConditionCode,
                    new WorkOutcomeSnapshot(work.Id, work.Status, work.UpdatedAt))
                : ApplicationResult<WorkOutcomeSnapshot>.Rejected(access.ConditionCode!);
        }

        var now = _clock.UtcNow;

        var edition = work.Edit(
            name,
            declaredDate,
            description,
            originalJson,
            originalJsonPreservedDeclared: true,
            now);

        if (!edition.Succeeded)
        {
            // NO SE MATERIALIZA NADA: un reenvío rechazado no reemplaza el texto guardado
            // (`Api CU-06` §7).
            return ApplicationResult<WorkOutcomeSnapshot>.Rejected(edition.ConditionCode!);
        }

        var resubmission = InterpretAndSubmit(work, now);

        if (!resubmission.Succeeded)
        {
            return ApplicationResult<WorkOutcomeSnapshot>.Rejected(resubmission.ConditionCode!);
        }

        await _works.UpdateAsync(work, cancellationToken).ConfigureAwait(false);

        return ApplicationResult<WorkOutcomeSnapshot>.Applied(
            new WorkOutcomeSnapshot(work.Id, work.Status, work.UpdatedAt, work.Observations));
    }
}
