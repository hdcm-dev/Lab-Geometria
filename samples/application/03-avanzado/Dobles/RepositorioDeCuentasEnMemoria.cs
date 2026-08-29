using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Samples.Application.Avanzado.Dobles;

/// <summary>
/// El doble del puerto de cuentas, **con una sola cuenta**: la alumna dueña de los ocho trabajos.
/// </summary>
/// <remarks>
/// **DESVÍO DECLARADO DE §5.** Su árbol enumera tres dobles —validador, repositorio de trabajos y
/// reloj— y esta carpeta trae **cuatro**. El motivo es que `ConsultOwnWorksUseCase` declara el
/// puerto de cuentas en su constructor, para poder poner el correo y el nombre de la persona dueña
/// en el detalle. Sin el doble, los actos `[E-7]` y `[Consulta]` no se pueden recorrer.
///
/// **Se agrega un archivo y no se renombra ni se quita ninguno de los que §5 declara.**
/// </remarks>
internal sealed class RepositorioDeCuentasEnMemoria : IAccountRepository
{
    private readonly List<Account> _cuentas = [];

    internal void Agregar(Account cuenta) => _cuentas.Add(cuenta);

    public Task<Account?> FindByNormalizedEmailAsync(string e, CancellationToken ct = default) =>
        Task.FromResult(_cuentas.FirstOrDefault(c => c.NormalizedEmail == e));

    public Task<Account?> FindByIdAsync(Guid id, CancellationToken ct = default) =>
        Task.FromResult(_cuentas.FirstOrDefault(c => c.Id == id));

    public Task<bool> AdministratorExistsAsync(CancellationToken ct = default) => Task.FromResult(false);

    public Task<bool> EmailIsRegisteredAsync(string e, CancellationToken ct = default) =>
        Task.FromResult(_cuentas.Any(c => c.NormalizedEmail == e));

    public Task AddAsync(Account c, CancellationToken ct = default) { _cuentas.Add(c); return Task.CompletedTask; }

    public Task UpdateAsync(Account c, CancellationToken ct = default) => Task.CompletedTask;

    public Task<IReadOnlyList<Account>> ListAsync(CancellationToken ct = default) =>
        Task.FromResult<IReadOnlyList<Account>>(_cuentas);

    public Task RemoveAsync(Account c, CancellationToken ct = default) { _cuentas.Remove(c); return Task.CompletedTask; }
}
