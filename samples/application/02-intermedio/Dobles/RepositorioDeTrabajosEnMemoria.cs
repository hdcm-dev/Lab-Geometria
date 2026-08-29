using GeometriaFactory.Application.Ports;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Samples.Application.Intermedio.Dobles;

/// <summary>El doble del puerto de repositorio de trabajos: una lista en memoria, sin reglas.</summary>
/// <remarks>
/// **No valida nada y es a propósito.** Un doble que empezara a comprobar pertenencia o estado
/// estaría moviendo reglas de negocio a la infraestructura, que es lo que la capa existe para
/// impedir.
/// </remarks>
internal sealed class RepositorioDeTrabajosEnMemoria : IWorkRepository
{
    private readonly List<Work> _trabajos = [];

    internal IReadOnlyList<Work> Todos => _trabajos;

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
        Task.FromResult<IReadOnlyList<WorkListEntry>>(
            _trabajos.Where(w => w.OwnerId == ownerId).Select(Entrada).ToList());

    public Task<IReadOnlyList<WorkListEntry>> ListInAdministratorScopeAsync(
        Guid? ownerFilter, CancellationToken ct = default) =>
        Task.FromResult<IReadOnlyList<WorkListEntry>>(
            _trabajos.Where(w => w.Status != Work.StatusOutsideAdministratorScope)
                     .Where(w => ownerFilter is null || w.OwnerId == ownerFilter)
                     .Select(Entrada).ToList());

    // LOS DATOS DE LA PERSONA DUEÑA LOS APORTA EL ADAPTADOR REAL CRUZANDO CON CUENTAS, y este
    // doble los declara fijos: el recorrido no los mira y fabricarlos con un repositorio de
    // cuentas de mentira agregaría una pieza que el sample no enseña.
    private static WorkListEntry Entrada(Work w) => new(
        w.Id, w.Name, w.DeclaredDate, w.Status, w.OwnerId,
        "alumna@frre.utn.edu.ar", "Alumna", "Ejemplo");
}
