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
    /// ETAPA `e`: EL ARRASTRE ENTRÓ ACÁ ADENTRO, que es donde la etapa `c` anunció que iba a
    /// entrar. Los trabajos de la cuenta se retiran **en la misma unidad de trabajo** que la
    /// cuenta: una sola confirmación, y por lo tanto **no existe una baja a medias** que deje
    /// trabajos sin dueño (`Infrastructure CU-04`; `RETIRO_PARCIAL_NO_ADMITIDO`).
    ///
    /// SE RETIRAN LOS CUATRO ESTADOS, TERMINALES INCLUIDOS (`Infrastructure CU-04` FA-03):
    /// `Approved` y `Rejected` son terminales **para las transiciones, no para el retiro**.
    ///
    /// Y SE RETIRAN EXPLÍCITAMENTE, aunque la clave foránea de `RE-06` ya declare el arrastre en
    /// el esquema. No es redundancia defensiva: el arrastre del esquema depende de que el motor
    /// tenga la comprobación de claves foráneas encendida, que es un ajuste de conexión y no una
    /// propiedad del producto. Escribirlo acá hace que **la unidad de trabajo sea la misma en los
    /// dos casos** y que la prueba de ausencia mida el comportamiento del producto y no el de un
    /// ajuste.
    ///
    /// UNA CUENTA SIN TRABAJOS SE DA DE BAJA IGUAL: un arrastre de cero es válido (FA-02).
    /// </remarks>
    public async Task RemoveAsync(Account account, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(account);

        var works = await _dbContext.Works
            .Where(work => work.OwnerId == account.Id)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        _dbContext.Works.RemoveRange(works);
        _dbContext.Accounts.Remove(account);

        // UNA SOLA CONFIRMACIÓN PARA LAS DOS COSAS: todo o nada.
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
