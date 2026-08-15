using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Works;

/// <summary>
/// CU-09 — Elimina un trabajo, con los DOS ALCANCES OPUESTOS en un solo contrato.
/// </summary>
/// <remarks>
/// LA ÚNICA DECISIÓN PROPIA DE ESTA CAPA ES ELEGIR LA RESOLUCIÓN POR EL PAPEL. Las dos reglas
/// —la del alumno, acotada a lo propio y a `Draft` (RN-03, RN-04, INV-02, INV-03), y la del
/// administrador, que alcanza los tres estados que ve y ninguno más (RN-04, RN-11)— viven en el
/// dominio, y acá no se repiten: repetirlas crearía un segundo lugar donde pueden decir otra cosa.
///
/// SON OPUESTOS Y NO COMPLEMENTARIOS, y conviene verlo escrito: el alumno elimina **sólo**
/// `Draft`, el administrador **todo menos** `Draft`. No hay ningún trabajo que los dos puedan
/// eliminar, y no hay ninguno que ninguno de los dos pueda.
///
/// LA VERIFICACIÓN ES DEL LADO DEL SERVIDOR Y NO DE LA PANTALLA. Que la interfaz no ofrezca el
/// botón no prueba nada: la petición se puede forzar contra la superficie, y es exactamente así
/// como el intake §17.5.P.6 exige verificar RN-04.
///
/// EL RETIRO ES DEFINITIVO: no hay estado de eliminado y no hay recuperación (§10).
/// </remarks>
public sealed class DeleteWorkUseCase
{
    private readonly IWorkRepository _works;

    public DeleteWorkUseCase(IWorkRepository works)
    {
        ArgumentNullException.ThrowIfNull(works);
        _works = works;
    }

    /// <summary>Elimina un trabajo, con el alcance que el papel determina (`Api CU-06` `A-12`).</summary>
    /// <param name="requesterId">Identidad de quien pide, tomada del acceso firmado.</param>
    /// <param name="requesterRole">Papel de quien pide, tomado del acceso firmado.</param>
    /// <param name="workId">Identidad del trabajo.</param>
    /// <returns>
    /// El estado que el trabajo tenía. En el éxito es el que tenía al retirarse; en el rechazo
    /// por estado es **el estado actual que la respuesta tiene que declarar**.
    /// </returns>
    public async Task<ApplicationResult<WorkStatus>> ExecuteAsync(
        Guid requesterId,
        Role requesterRole,
        Guid workId,
        CancellationToken cancellationToken = default)
    {
        if (!Enum.IsDefined(requesterRole))
        {
            // TERMINA SIN EVALUAR NINGUNA DE LAS DOS RESOLUCIONES: sin papel no se sabe cuál de
            // los dos alcances opuestos aplicar (§6).
            return ApplicationResult<WorkStatus>.Rejected(ApplicationConditionCode.UnrecognizedRole);
        }

        var work = await _works.FindByIdAsync(workId, cancellationToken).ConfigureAwait(false);

        if (work is null)
        {
            // EL MOTIVO DEPENDE DEL PAPEL, y no es una asimetría gratuita: al alumno hay que
            // ocultarle si el identificador corresponde o no a un trabajo de otro (RN-03), y al
            // administrador no hay nada que ocultarle porque no hay recurso ajeno.
            return ApplicationResult<WorkStatus>.Rejected(requesterRole == Role.Administrator
                ? ApplicationConditionCode.WorkNotFound
                : ConditionCode.WorkNotFoundForRequester);
        }

        var resolution = requesterRole == Role.Administrator
            ? work.ResolveAdministratorScope(requesterRole, WorkOperation.Delete)
            : work.ResolveStudentAccess(requesterId, WorkOperation.Delete);

        if (!resolution.Succeeded)
        {
            // Sólo el rechazo por estado transporta el estado: el de pertenencia y el de alcance
            // no pueden decir nada del trabajo que el solicitante no ve.
            return resolution.ConditionCode == ConditionCode.OperationOutsideDraft
                ? ApplicationResult<WorkStatus>.Rejected(resolution.ConditionCode, work.Status)
                : ApplicationResult<WorkStatus>.Rejected(resolution.ConditionCode!);
        }

        var retired = work.Status;

        await _works.RemoveAsync(work, cancellationToken).ConfigureAwait(false);

        return ApplicationResult<WorkStatus>.Applied(retired);
    }
}
