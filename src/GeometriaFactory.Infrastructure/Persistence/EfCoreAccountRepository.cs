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

    /// <summary>
    /// Todas las cuentas de la comisión, ordenadas por correo normalizado.
    /// </summary>
    /// <remarks>
    /// EL ORDEN LO FIJA EL ADAPTADOR Y NO EL CASO DE USO, porque es lo que el motor puede
    /// resolver con el índice que ya existe. **Ninguna fuente declara un orden para el listado**:
    /// se toma el del correo normalizado, que es estable, no depende de la cultura y es la forma
    /// que ya decide la identidad. **[decisión de la etapa `d`, declarada]**
    /// </remarks>
    public async Task<IReadOnlyList<Account>> ListAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Accounts
            .OrderBy(account => account.NormalizedEmail)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Retira una cuenta y todos sus trabajos, en una única unidad de trabajo (RN-07).
    /// </summary>
    /// <remarks>
    /// HOY RETIRA LA CUENTA SOLA, Y NO ES UN ARRASTRE INCOMPLETO: **los trabajos todavía no
    /// existen** —son de la etapa `e`— y por lo tanto no hay ninguno que dejar huérfano. Cuando
    /// la tabla exista, el arrastre entra en ESTA operación y no en el caso de uso, para que
    /// siga siendo una sola unidad de trabajo y no dos escrituras que puedan quedar a medias
    /// (`Infrastructure ADR-02`; `RETIRO_PARCIAL_NO_ADMITIDO`).
    /// </remarks>
    public async Task RemoveAsync(Account account, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(account);

        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
