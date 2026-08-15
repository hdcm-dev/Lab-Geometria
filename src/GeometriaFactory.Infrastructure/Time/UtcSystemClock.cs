using GeometriaFactory.Application.Ports;

namespace GeometriaFactory.Infrastructure.Time;

/// <summary>
/// CU-09 — Único adaptador del puerto de reloj (`Infrastructure BT-12`).
/// </summary>
/// <remarks>
/// El momento va SIEMPRE en tiempo universal coordinado (`Modelo-Datos-Logico.md` `RC-06`). El
/// reloj es un puerto justamente para que las fechas de alta sean verificables en prueba
/// (intake §17.2.P.11 punto 3): el doble de prueba reemplaza a este adaptador y a nadie más.
/// </remarks>
public sealed class UtcSystemClock : ISystemClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
