using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Works;

/// <summary>
/// CU-06 — El alumno consulta **sus** trabajos: el listado y el detalle de uno.
/// </summary>
/// <remarks>
/// EL RECORTE SE TRASLADA A LA CONSULTA Y NO SE APLICA DESPUÉS (§4 paso 2). Se le piden al puerto
/// los trabajos **cuyo dueño es ese alumno**; no se trae un conjunto mayor para filtrarlo acá.
/// La diferencia no es de eficiencia: un filtro posterior es un lugar donde el recorte se puede
/// omitir sin que nada falle, y una consulta acotada no lo tiene.
///
/// EL ALUMNO VE SUS CUATRO ESTADOS, BORRADORES INCLUIDOS. La exclusión de borradores es del
/// alcance del administrador (RN-11) y no del propio: lo que un alumno todavía no entregó es
/// asunto suyo, y suyo quiere decir que lo ve él.
///
/// UN LISTADO VACÍO NO ES UN FALLO (FA-03): se devuelve una colección vacía, y quien la consume
/// distingue vacío de fallo **por el tipo recibido y no por el conteo**.
/// </remarks>
public sealed class ConsultOwnWorksUseCase
{
    private readonly IWorkRepository _works;
    private readonly IAccountRepository _accounts;

    public ConsultOwnWorksUseCase(IWorkRepository works, IAccountRepository accounts)
    {
        ArgumentNullException.ThrowIfNull(works);
        ArgumentNullException.ThrowIfNull(accounts);

        _works = works;
        _accounts = accounts;
    }

    /// <summary>Los trabajos del solicitante, en sus cuatro estados (`Api CU-07` `A-13`).</summary>
    public async Task<ApplicationResult<IReadOnlyList<WorkListEntry>>> ListAsync(
        Guid requesterId,
        CancellationToken cancellationToken = default)
    {
        if (requesterId == Guid.Empty)
        {
            // TERMINA SIN CONSULTAR EL REPOSITORIO: una consulta sin solicitante no se puede
            // acotar, y la que no se acota devuelve trabajos ajenos (§6).
            return ApplicationResult<IReadOnlyList<WorkListEntry>>.Rejected(
                ApplicationConditionCode.RequesterNotDeclared);
        }

        var entries = await _works.ListOwnedByAsync(requesterId, cancellationToken).ConfigureAwait(false);

        return ApplicationResult<IReadOnlyList<WorkListEntry>>.Applied(entries);
    }

    /// <summary>El detalle de un trabajo propio, en cualquiera de sus estados (`Api CU-07` `A-14`).</summary>
    /// <remarks>
    /// VER NO ESTÁ ACOTADO AL BORRADOR (`Domain CU-09` FA-02): lo que la acotación restringe es
    /// **operar** sobre el trabajo. El alumno ve el desenlace y el comentario de su propio
    /// trabajo, que es exactamente para lo que el desenlace existe.
    /// </remarks>
    public async Task<ApplicationResult<WorkDetail>> DetailAsync(
        Guid requesterId,
        Guid workId,
        CancellationToken cancellationToken = default)
    {
        if (requesterId == Guid.Empty)
        {
            return ApplicationResult<WorkDetail>.Rejected(ApplicationConditionCode.RequesterNotDeclared);
        }

        var work = await _works.FindByIdAsync(workId, cancellationToken).ConfigureAwait(false);

        if (work is null)
        {
            return ApplicationResult<WorkDetail>.Rejected(ConditionCode.WorkNotFoundForRequester);
        }

        var access = work.ResolveStudentAccess(requesterId, WorkOperation.View);
        if (!access.Succeeded)
        {
            return ApplicationResult<WorkDetail>.Rejected(access.ConditionCode!);
        }

        var owner = await _accounts.FindByIdAsync(work.OwnerId, cancellationToken).ConfigureAwait(false);

        if (owner is null)
        {
            // Un trabajo sin cuenta dueña no es alcanzable: la baja de la cuenta arrastra sus
            // trabajos en la misma unidad de trabajo (RN-07). Si igual ocurriera, se responde lo
            // mismo que ante un trabajo que no existe, y no una traza.
            return ApplicationResult<WorkDetail>.Rejected(ConditionCode.WorkNotFoundForRequester);
        }

        return ApplicationResult<WorkDetail>.Applied(WorkDetail.Of(work, owner));
    }
}
