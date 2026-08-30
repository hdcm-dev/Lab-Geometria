using GeometriaFactory.Application.Ports;
using GeometriaFactory.Infrastructure.Time;

namespace GeometriaFactory.Samples.Infrastructure.Avanzado;

/// <summary>Acto 4 — `CU-06009`: el sello llega por un puerto, y por eso se puede fijar.</summary>
/// <remarks>
/// ESTA LÍNEA ES LA QUE HACE REPRODUCIBLE A TODO EL RESTO DEL PRODUCTO. Si esta capa leyera el
/// reloj del sistema directamente, ningún sample de ningún proyecto de código podría tener un
/// criterio de aceptación comparable: cada corrida traería otro instante y ninguna salida se
/// podría transcribir en un snapshot.
///
/// EL DOBLE DE ACÁ ES EL ÚNICO DEL SAMPLE, y no contradice que los demás actos corran contra los
/// componentes reales. `UtcSystemClock` se ejercita igual, en la primera mitad del acto; el doble
/// entra en la segunda para poder MOSTRAR lo que el puerto habilita, que es lo que no se puede
/// ver mirando la implementación real.
/// </remarks>
internal static class ActoReloj
{
    internal static ISellador Ejecutar(Action<string> escribir)
    {
        var delSistema = new UtcSystemClock();
        var sello = delSistema.UtcNow;

        var fijado = new SelladorFijo(new DateTimeOffset(2026, 8, 30, 12, 0, 0, TimeSpan.Zero));
        var primera = fijado.UtcNow;
        var segunda = fijado.UtcNow;

        escribir($"[4] Sello del reloj por el puerto: {(sello > DateTimeOffset.UnixEpoch ? "obtenido" : "SIN SELLO")} "
            + $"| dos corridas con el puerto fijado: "
            + $"{(primera == segunda ? "sello identico" : "SELLOS DISTINTOS")}");

        return fijado;
    }
}

/// <summary>El puerto del reloj, visto desde el sample.</summary>
internal interface ISellador
{
    DateTimeOffset UtcNow { get; }
}

/// <summary>Un reloj fijado, que es lo que el puerto existe para permitir.</summary>
internal sealed class SelladorFijo(DateTimeOffset instante) : ISellador, ISystemClock
{
    public DateTimeOffset UtcNow { get; } = instante;
}
