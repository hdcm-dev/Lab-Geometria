using GeometriaFactory.Application.Ports;

namespace GeometriaFactory.Samples.Application.Avanzado.Dobles;

/// <summary>El doble del puerto de reloj: un momento fijo, que es lo que hace comparable la salida.</summary>
internal sealed class RelojFijo : ISystemClock
{
    internal static readonly DateTimeOffset Momento = new(2026, 8, 29, 0, 0, 0, TimeSpan.Zero);

    public DateTimeOffset UtcNow => Momento;
}
