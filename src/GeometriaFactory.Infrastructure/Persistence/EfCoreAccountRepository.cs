using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Infrastructure.Persistence;

/// <summary>
/// CU-05 — Único adaptador del puerto de repositorio de cuentas (`Infrastructure BT-09`).
/// </summary>
/// <remarks>
/// UNA UNIDAD DE TRABAJO POR OPERACIÓN (`Infrastructure ADR-02`): cada escritura confirma la
/// suya. El contexto es de alcance de petición y no se comparte.
///
/// NO HAY REPOSITORIO GENÉRICO (`ADR-01`): los miembros son los que el caso de uso necesita y
/// las consultas están escritas, que es lo que permite verlas y verificarlas.
/// </remarks>
public sealed class EfCoreAccountRepository : IAccountRepository
{
    private readonly GeometriaFactoryDbContext _dbContext;

    public EfCoreAccountRepository(GeometriaFactoryDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        _dbContext = dbContext;
    }

    public Task<Account?> FindByNormalizedEmailAsync(
        string normalizedEmail,
        CancellationToken cancellationToken = default) =>
        _dbContext.Accounts
            .FirstOrDefaultAsync(account => account.NormalizedEmail == normalizedEmail, cancellationToken);

    public Task<Account?> FindByIdAsync(Guid accountId, CancellationToken cancellationToken = default) =>
        _dbContext.Accounts
            .FirstOrDefaultAsync(account => account.Id == accountId, cancellationToken);

    public Task<bool> AdministratorExistsAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Accounts.AnyAsync(account => account.Role == Role.Administrator, cancellationToken);

    public Task<bool> EmailIsRegisteredAsync(
        string normalizedEmail,
        CancellationToken cancellationToken = default) =>
        _dbContext.Accounts.AnyAsync(account => account.NormalizedEmail == normalizedEmail, cancellationToken);

    public async Task AddAsync(Account account, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(account);

        await _dbContext.Accounts.AddAsync(account, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(account);

        _dbContext.Accounts.Update(account);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
