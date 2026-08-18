using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Works;

/// <summary>
/// CU-10 — Aplica el desenlace de la revisión: aprobar o rechazar un trabajo en estado `Pendiente`.
/// </summary>
/// <remarks>
/// ESTA CAPA NO DECIDE NADA DEL DESENLACE. Las cuatro guardas —que el trabajo no esté en un estado
/// terminal, que esté en `Pendiente`, que quien pide sea administrador y que el desenlace pertenezca
/// al conjunto cerrado— viven en `Work.ApplyOutcome`, y acá no se repiten: repetirlas crearía un
/// segundo lugar donde pueden decir otra cosa. Es el mismo criterio de <see cref="DeleteWorkUseCase"/>.
///
/// LO ÚNICO PROPIO DE ACÁ ES EL ORDEN: buscar, aplicar, guardar. Y una traducción de borde, la del
/// nombre del desenlace al valor del conjunto cerrado, que se hace **por nombre y nunca por
/// posición**, para que agregar un valor al conjunto no corra el significado de los que ya viajaron.
///
/// EL NO ENCONTRADO NO DEPENDE DEL PAPEL, a diferencia de la eliminación: acá **sólo el
/// administrador llega**, de modo que no hay recurso ajeno que ocultar. Un alumno que fuerce la
/// petición no cae en «no encontrado» sino en la guarda de papel del dominio, que es la que declara
/// la facultad exclusiva.
///
/// EL SELLO LO APORTA EL CONSUMIDOR y no se toma del reloj de esta capa: es el puerto
/// <see cref="ISystemClock"/>, con el mismo criterio que el resto del producto.
/// </remarks>
public sealed class ResolveWorkUseCase
{
    private readonly IWorkRepository _works;
    private readonly ISystemClock _clock;

    public ResolveWorkUseCase(IWorkRepository works, ISystemClock clock)
    {
        ArgumentNullException.ThrowIfNull(works);
        ArgumentNullException.ThrowIfNull(clock);
        _works = works;
        _clock = clock;
    }

    /// <summary>Resuelve un trabajo con el desenlace pedido (`Api CU-06` `A-15`).</summary>
    /// <param name="requesterRole">Papel de quien pide, tomado del acceso firmado.</param>
    /// <param name="workId">Identidad del trabajo.</param>
    /// <param name="outcomeName">Desenlace, por su nombre del contrato.</param>
    /// <param name="comment">Comentario escrito. Opcional en los dos desenlaces.</param>
    /// <returns>El estado que el trabajo alcanzó, y el momento en que lo alcanzó.</returns>
    public async Task<ApplicationResult<WorkResolution>> ExecuteAsync(
        Role requesterRole,
        Guid workId,
        string? outcomeName,
        string? comment,
        CancellationToken cancellationToken = default)
    {
        if (!Enum.IsDefined(requesterRole))
        {
            return ApplicationResult<WorkResolution>.Rejected(ApplicationConditionCode.UnrecognizedRole);
        }

        // LA TRADUCCIÓN DEL NOMBRE SE HACE ACÁ Y NO EN EL DOMINIO, porque es de borde: el dominio
        // recibe el valor del conjunto cerrado y no una cadena que pueda no pertenecer a él.
        if (!TryReadOutcome(outcomeName, out var outcome))
        {
            return ApplicationResult<WorkResolution>.Rejected(ConditionCode.UnknownOutcome);
        }

        var work = await _works.FindByIdAsync(workId, cancellationToken).ConfigureAwait(false);

        if (work is null)
        {
            return ApplicationResult<WorkResolution>.Rejected(ApplicationConditionCode.WorkNotFound);
        }

        var applied = work.ApplyOutcome(requesterRole, outcome, comment, _clock.UtcNow);

        if (!applied.Succeeded)
        {
            // El rechazo por estado transporta el estado actual, que es lo que la respuesta tiene
            // que declarar: quien pidió necesita saber en cuál quedó, no sólo que no se pudo.
            return applied.ConditionCode is ConditionCode.OutcomeOutsideSubmitted
                or ConditionCode.TransitionFromTerminalStatus
                ? ApplicationResult<WorkResolution>.Rejected(
                    applied.ConditionCode, new WorkResolution(work.Id, work.Status, work.UpdatedAt))
                : ApplicationResult<WorkResolution>.Rejected(applied.ConditionCode!);
        }

        await _works.UpdateAsync(work, cancellationToken).ConfigureAwait(false);

        return ApplicationResult<WorkResolution>.Applied(
            new WorkResolution(work.Id, work.Status, work.UpdatedAt));
    }

    /// <summary>Lee el desenlace por su nombre del contrato. Nunca por posición.</summary>
    private static bool TryReadOutcome(string? name, out WorkOutcome outcome)
    {
        switch (name)
        {
            case "Approve":
                outcome = WorkOutcome.Approve;
                return true;

            case "Reject":
                outcome = WorkOutcome.Reject;
                return true;

            default:
                outcome = default;
                return false;
        }
    }
}

/// <summary>Lo que el desenlace deja: el trabajo, el estado que alcanzó y cuándo.</summary>
/// <param name="WorkId">Identidad del trabajo resuelto.</param>
/// <param name="Status">Estado alcanzado.</param>
/// <param name="ResolvedAt">Momento del desenlace.</param>
public sealed record WorkResolution(Guid WorkId, WorkStatus Status, DateTimeOffset ResolvedAt);
