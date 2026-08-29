using GeometriaFactory.Application.Ports;

namespace GeometriaFactory.Samples.Application.Basico.Dobles;

/// <summary>
/// El doble del puerto de reloj: un momento **fijo y declarado**.
/// </summary>
/// <remarks>
/// ES LO QUE HACE COMPARABLE LA SALIDA, y no una comodidad. El dominio no lee el reloj
/// (`Domain ADR-02006`): lo recibe por parámetro, y **esta capa es quien se lo aporta por el
/// puerto**. Un sample que leyera el reloj de la máquina produciría una salida distinta en cada
/// corrida y su criterio de aceptación dejaría de poder compararse contra nada.
/// </remarks>
internal sealed class RelojFijo : ISystemClock
{
    internal static readonly DateTimeOffset Momento = new(2026, 8, 29, 0, 0, 0, TimeSpan.Zero);

    public DateTimeOffset UtcNow => Momento;
}
