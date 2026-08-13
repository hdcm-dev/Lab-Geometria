using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Infrastructure.Persistence;

/// <summary>
/// Prepara el almacén al arrancar: aplica las transformaciones de esquema pendientes y
/// DETIENE EL ARRANQUE ante un esquema que no corresponde.
/// </summary>
/// <remarks>
/// `Infrastructure ADR-07`, `BT-06`, `US-24` y `US-25`. La regla que hace cumplir es la de
/// `QG-11`: CERO peticiones atendidas con la preparación incompleta. Quien la invoca antes de
/// abrir la escucha es `TwoPhaseStartup` de `GeometriaFactory.Api.Composition`.
/// </remarks>
public sealed class StorePreparation
{
    private readonly GeometriaFactoryDbContext _dbContext;

    public StorePreparation(GeometriaFactoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Si la preparación terminó. Es el único dato de estado que el punto de salud publica
    /// sobre el almacén: la respuesta NO lleva su ruta (`US-29` §3, tercer criterio).
    /// </summary>
    public bool IsPrepared { get; private set; }

    /// <summary>
    /// Aplica las transformaciones pendientes. Si al terminar queda alguna sin aplicar, lanza:
    /// el arranque se detiene en lugar de atender sobre un almacén dudoso (`US-28`).
    /// </summary>
    public async Task PrepareAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);

        var pending = await _dbContext.Database
            .GetPendingMigrationsAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pending.Any())
        {
            throw new InvalidOperationException(
                "El almacén quedó con transformaciones de esquema sin aplicar. " +
                "El arranque se detiene en lugar de atender sobre un esquema que no corresponde.");
        }

        IsPrepared = true;
    }
}
