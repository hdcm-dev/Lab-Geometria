using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
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

    public LoadAndEditOwnWorkUseCase(IWorkRepository works, ISystemClock clock)
    {
        ArgumentNullException.ThrowIfNull(works);
        ArgumentNullException.ThrowIfNull(clock);

        _works = works;
        _clock = clock;
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

        await _works.AddAsync(work, cancellationToken).ConfigureAwait(false);

        return ApplicationResult<WorkOutcomeSnapshot>.Applied(
            new WorkOutcomeSnapshot(work.Id, work.Status, work.CreatedAt));
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

        await _works.UpdateAsync(work, cancellationToken).ConfigureAwait(false);

        return ApplicationResult<WorkOutcomeSnapshot>.Applied(
            new WorkOutcomeSnapshot(work.Id, work.Status, work.UpdatedAt));
    }
}
