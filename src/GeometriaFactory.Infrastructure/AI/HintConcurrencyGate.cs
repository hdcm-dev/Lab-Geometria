namespace GeometriaFactory.Infrastructure.AI;

/// <summary>
/// El cupo de una llamada al modelo por instancia (REQ-IA-10, decisión D-CUPO).
/// </summary>
/// <remarks>
/// SE REGISTRA COMO SINGLETON, Y ES LO QUE LO HACE UN CUPO: uno de ámbito por petición daría un
/// cupo por pedido, que no limita nada. **Sin cola**: si está ocupado, el pedido no espera y recibe
/// la plantilla. Un servidor sólo CPU con cola es un pedido que tarda minutos.
/// </remarks>
public sealed class HintConcurrencyGate : IDisposable
{
    private readonly SemaphoreSlim _slot = new(1, 1);

    /// <summary>Toma el cupo si está libre, sin esperar.</summary>
    public bool TryEnter() => _slot.Wait(0);

    /// <summary>Suelta el cupo tomado con <see cref="TryEnter"/>.</summary>
    public void Release() => _slot.Release();

    public void Dispose() => _slot.Dispose();
}
