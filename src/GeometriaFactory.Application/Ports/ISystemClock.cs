namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Reloj del sistema. Puerto DECLARADO por el intake §13, §14 y §17.2.P.11 punto 3.
/// </summary>
/// <remarks>
/// Su único adaptador es `UtcSystemClock` (`Plan-Etapa-A.md` §1.6), que llega en la
/// etapa `c` (`Infrastructure BT-12`). En la etapa `a` el puerto se declara y no se conecta.
/// El momento va siempre en tiempo universal coordinado (`Modelo-Datos-Logico.md` §2.1, `RC-06`),
/// y ése es el único miembro que la especificación declara hoy.
/// </remarks>
public interface ISystemClock
{
    /// <summary>Momento actual en tiempo universal coordinado.</summary>
    DateTimeOffset UtcNow { get; }
}
