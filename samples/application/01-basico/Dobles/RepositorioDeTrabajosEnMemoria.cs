using GeometriaFactory.Application.Ports;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Samples.Application.Basico.Dobles;

/// <summary>
/// El doble del puerto de repositorio de trabajos. **Vacío**: el acto `[4]` no consulta trabajos,
/// consulta si la cuenta puede pedirlos.
/// </summary>
/// <remarks>
/// **DESVÍO DECLARADO DE §5 DEL DOCUMENTO QUE GOBIERNA ESTA CARPETA.** Su árbol enumera dos dobles
/// —el de cuentas y el de reloj— y este sample trae **tres**. El motivo es que el acto `[4]` ejerce
/// la puerta de `ADR-04004` **a través de una petición de listado**, y esa petición entra por
/// `ConsultOwnWorksUseCase`, que declara el puerto de trabajos en su constructor. Sin el doble, el
/// acto no se puede recorrer.
///
/// **Se agrega un archivo y no se renombra ni se quita ninguno de los que §5 declara**, que es el
/// único ajuste que la estructura admite sin cambiar el documento. Queda anotado acá y en el
/// informe del incremento, en lugar de aparecer sin explicación en el árbol.
///
/// **Y el doble prueba algo por estar vacío**: si la puerta de la marca de cambio pendiente
/// cortara *después* de consultar el repositorio, el listado devolvería la lista vacía en vez del
/// rechazo, y la línea del snapshot no coincidiría.
/// </remarks>
internal sealed class RepositorioDeTrabajosEnMemoria : IWorkRepository
{
    private readonly List<Work> _trabajos = [];

    public Task<Work?> FindByIdAsync(Guid workId, CancellationToken ct = default) =>
        Task.FromResult(_trabajos.FirstOrDefault(w => w.Id == workId));

    public Task AddAsync(Work work, CancellationToken ct = default)
    {
        _trabajos.Add(work);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Work work, CancellationToken ct = default) => Task.CompletedTask;

    public Task RemoveAsync(Work work, CancellationToken ct = default)
    {
        _trabajos.Remove(work);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<WorkListEntry>> ListOwnedByAsync(Guid ownerId, CancellationToken ct = default) =>
        Task.FromResult<IReadOnlyList<WorkListEntry>>([]);

    public Task<IReadOnlyList<WorkListEntry>> ListInAdministratorScopeAsync(
        Guid? ownerFilter, CancellationToken ct = default) =>
        Task.FromResult<IReadOnlyList<WorkListEntry>>([]);
}
