using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Application.Basico.Dobles;

/// <summary>
/// El doble del puerto de repositorio de cuentas: una lista en memoria.
/// </summary>
/// <remarks>
/// LO QUE ESTE DOBLE ENSEÑA ES LA INVERSIÓN DE DEPENDENCIAS, y por eso el sample lo trae en vez
/// de usar la infraestructura real: la capa de aplicación **declara el puerto** y no sabe quién lo
/// implementa. Cambiar este doble por el adaptador de base de datos no cambia una línea de los
/// cuatro actos.
///
/// **No valida nada y es a propósito.** Un doble que empezara a comprobar unicidad o formato
/// estaría moviendo reglas de negocio a la infraestructura, que es exactamente lo que la capa
/// existe para impedir. Las comprobaciones viven en el caso de uso y en el dominio.
/// </remarks>
internal sealed class RepositorioDeCuentasEnMemoria : IAccountRepository
{
    private readonly List<Account> _cuentas = [];

    internal int Cantidad => _cuentas.Count;

    public Task<Account?> FindByNormalizedEmailAsync(string normalizedEmail, CancellationToken ct = default) =>
        Task.FromResult(_cuentas.FirstOrDefault(
            c => string.Equals(c.NormalizedEmail, normalizedEmail, StringComparison.Ordinal)));

    public Task<Account?> FindByIdAsync(Guid accountId, CancellationToken ct = default) =>
        Task.FromResult(_cuentas.FirstOrDefault(c => c.Id == accountId));

    public Task<bool> AdministratorExistsAsync(CancellationToken ct = default) =>
        Task.FromResult(_cuentas.Any(c => c.Role == Role.Administrator));

    public Task<bool> EmailIsRegisteredAsync(string normalizedEmail, CancellationToken ct = default) =>
        Task.FromResult(_cuentas.Any(
            c => string.Equals(c.NormalizedEmail, normalizedEmail, StringComparison.Ordinal)));

    public Task AddAsync(Account account, CancellationToken ct = default)
    {
        _cuentas.Add(account);
        return Task.CompletedTask;
    }

    // La cuenta es la MISMA instancia que la lista ya tiene: el doble no copia estado, y por eso
    // una operación del dominio sobre la entidad se ve acá sin que nadie la vuelva a guardar.
    public Task UpdateAsync(Account account, CancellationToken ct = default) => Task.CompletedTask;

    public Task<IReadOnlyList<Account>> ListAsync(CancellationToken ct = default) =>
        Task.FromResult<IReadOnlyList<Account>>(_cuentas);

    public Task RemoveAsync(Account account, CancellationToken ct = default)
    {
        _cuentas.Remove(account);
        return Task.CompletedTask;
    }
}
