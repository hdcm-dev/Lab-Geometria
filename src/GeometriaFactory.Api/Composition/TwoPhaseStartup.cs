using GeometriaFactory.Infrastructure.Persistence;

namespace GeometriaFactory.Api.Composition;

/// <summary>
/// Arranque en dos fases: primero se prepara el almacén, después se atiende (`Api ADR-07`).
/// </summary>
/// <remarks>
/// `QG-11`: CERO peticiones atendidas con la preparación incompleta. `US-27` y `US-28`.
/// Si la preparación falla, la excepción sube y el proceso NO abre la escucha: detener el
/// arranque es preferible a atender sobre un almacén dudoso.
/// </remarks>
public sealed class TwoPhaseStartup
{
    private readonly IServiceProvider _services;

    public TwoPhaseStartup(IServiceProvider services)
    {
        _services = services;
    }

    /// <summary>
    /// Si la fase 1 terminó. Es el dato que el punto de salud publica sobre el almacén.
    /// </summary>
    public bool StoreIsPrepared { get; private set; }

    public async Task PrepareStoreAsync(CancellationToken cancellationToken = default)
    {
        using var scope = _services.CreateScope();
        var preparation = scope.ServiceProvider.GetRequiredService<StorePreparation>();

        await preparation.PrepareAsync(cancellationToken).ConfigureAwait(false);

        StoreIsPrepared = preparation.IsPrepared;
    }
}
