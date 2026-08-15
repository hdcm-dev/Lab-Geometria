using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Works;

/// <summary>
/// CU-07 — El administrador revisa los trabajos de la comisión: el listado y el detalle.
/// </summary>
/// <remarks>
/// ACÁ NO RIGE LA PERTENENCIA SINO LA FACULTAD, y las dos operaciones empiezan comprobándola:
/// un solicitante sin papel `Administrator` **no llega a consultar nada** (FA-01, CA-03).
///
/// Y RIGE ADEMÁS UN RECORTE QUE NO ES DE PERTENENCIA: **el administrador no ve los borradores**
/// (RN-11). Lo que un alumno todavía no entregó no es asunto de nadie. El recorte se traslada a
/// la consulta con el predicado que declara el dominio, y **el filtro por alumno acota lo que ese
/// recorte ya dejó pasar, no lo amplía**: no hay ningún valor del parámetro con el que pedir un
/// borrador ajeno, y por eso no hay nada que forzar (`Api CU-07` §10, CA-03).
///
/// LOS DOS MOTIVOS DE «NO SE PUEDE VER» NO SON EL MISMO Y NO SE MEZCLAN.
/// `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` dice que el trabajo está fuera de su flujo de trabajo y
/// `WORK_NOT_FOUND` que no existe; los dos terminan en la misma respuesta de protocolo —«no
/// encontrado»—, que es lo que hace indistinguible el borrador ajeno del inexistente
/// (`Api CU-07` CA-08). Se separan acá adentro porque el registro del servidor tiene que poder
/// decir cuál fue.
/// </remarks>
public sealed class ReviewCommissionWorksUseCase
{
    private readonly IWorkRepository _works;
    private readonly IAccountRepository _accounts;

    public ReviewCommissionWorksUseCase(IWorkRepository works, IAccountRepository accounts)
    {
        ArgumentNullException.ThrowIfNull(works);
        ArgumentNullException.ThrowIfNull(accounts);

        _works = works;
        _accounts = accounts;
    }

    /// <summary>
    /// El listado de la comisión, **sin borradores**, con el dato de dueño y con filtro opcional
    /// por alumno (`Api CU-07` `A-13`).
    /// </summary>
    /// <param name="requesterRole">Papel de quien pide, tomado del acceso firmado.</param>
    /// <param name="ownerFilter">Alumno por el que se filtra, o nulo para toda la comisión.</param>
    public async Task<ApplicationResult<IReadOnlyList<WorkListEntry>>> ListAsync(
        Role requesterRole,
        Guid? ownerFilter,
        CancellationToken cancellationToken = default)
    {
        if (requesterRole != Role.Administrator)
        {
            // FA-01: **sin consultar el repositorio**. CA-03 lo mide sobre el repositorio y no
            // sobre la respuesta.
            return ApplicationResult<IReadOnlyList<WorkListEntry>>.Rejected(
                ApplicationConditionCode.AdministratorRoleRequired);
        }

        if (ownerFilter is { } filter)
        {
            // El filtro referencia un alumno que tiene que existir: sin esta comprobación, un
            // identificador inventado devolvería una colección vacía indistinguible de la de un
            // alumno sin entregas (`Api CU-07` §6, `CONTRATO_ALUMNO_NO_ENCONTRADO`).
            var student = await _accounts.FindByIdAsync(filter, cancellationToken).ConfigureAwait(false);

            if (student is null)
            {
                return ApplicationResult<IReadOnlyList<WorkListEntry>>.Rejected(
                    ApplicationConditionCode.AccountNotFound);
            }
        }

        var entries = await _works
            .ListInAdministratorScopeAsync(ownerFilter, cancellationToken)
            .ConfigureAwait(false);

        return ApplicationResult<IReadOnlyList<WorkListEntry>>.Applied(entries);
    }

    /// <summary>
    /// El detalle de un trabajo que entra en su alcance. **Es el mismo que ve el alumno**
    /// (`Api CU-07` `A-14`).
    /// </summary>
    public async Task<ApplicationResult<WorkDetail>> DetailAsync(
        Role requesterRole,
        Guid workId,
        CancellationToken cancellationToken = default)
    {
        if (requesterRole != Role.Administrator)
        {
            return ApplicationResult<WorkDetail>.Rejected(ApplicationConditionCode.AdministratorRoleRequired);
        }

        var work = await _works.FindByIdAsync(workId, cancellationToken).ConfigureAwait(false);

        if (work is null)
        {
            return ApplicationResult<WorkDetail>.Rejected(ApplicationConditionCode.WorkNotFound);
        }

        var scope = work.ResolveAdministratorScope(requesterRole, WorkOperation.View);
        if (!scope.Succeeded)
        {
            return ApplicationResult<WorkDetail>.Rejected(scope.ConditionCode!);
        }

        var owner = await _accounts.FindByIdAsync(work.OwnerId, cancellationToken).ConfigureAwait(false);

        if (owner is null)
        {
            return ApplicationResult<WorkDetail>.Rejected(ApplicationConditionCode.WorkNotFound);
        }

        return ApplicationResult<WorkDetail>.Applied(WorkDetail.Of(work, owner));
    }
}
