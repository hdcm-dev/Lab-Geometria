using System.Diagnostics;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace GeometriaFactory.Infrastructure.AI;

/// <summary>Cómo terminó la precarga. Sólo va a la traza; nadie decide nada con esto.</summary>
public enum WarmupOutcome
{
    /// <summary>No hay cliente de modelo: la capa está apagada y no hay nada que precargar.</summary>
    NoServer,

    /// <summary>El servidor respondió: el modelo quedó en memoria.</summary>
    Loaded,

    /// <summary>El servidor no respondió dentro del tope de la precarga.</summary>
    TimedOut,

    /// <summary>El servidor respondió con un error o no se pudo hablar con él.</summary>
    Failed,
}

/// <summary>
/// La precarga del modelo al arrancar el servicio (REQ-IA-17, `ADR-14012`).
/// </summary>
/// <remarks>
/// <para>
/// POR QUÉ EXISTE. El primer pedido después de que el servidor descargó el modelo paga la carga
/// («frío»). Con el modelo elegido el frío medido es 13,3 s y entra en el tope de 20 s, pero con un
/// modelo más grande el frío llega a 30–40 s y rompería ese tope: el alumno recibiría la plantilla sin
/// que nada fallara de verdad. La precarga hace que el frío lo pague el arranque y no un alumno.
/// </para>
/// <para>
/// QUÉ MANDA. Un único mensaje de usuario, fijo, sin prompt de sistema, sin herramientas y con un token
/// de salida. No lleva ningún dato del producto ni del alumno (REQ-IA-04 se cumple por construcción).
/// La permanencia del modelo (<see cref="AiOptions.KeepAlive"/>) viaja en el mismo pedido porque la
/// pone el cliente compuesto, igual que en las pistas.
/// </para>
/// <para>
/// NUNCA LANZA Y NUNCA USA EL CUPO. Una precarga fallida no impide arrancar: el producto cumple su
/// función entera con plantillas. No toma <see cref="HintConcurrencyGate"/> para que un alumno que
/// pide pistas durante la precarga no reciba «cupo ocupado» por culpa de ella.
/// </para>
/// <para>
/// LA TRAZA LLEVA RESULTADO Y LATENCIA, NUNCA LA DIRECCIÓN (QG-06013).
/// </para>
/// </remarks>
public static class ModelWarmup
{
    /// <summary>
    /// Tope de la precarga. Más largo que el de una pista porque acá se paga la carga a propósito: el
    /// frío más largo medido entre los candidatos fue 42,5 s; 120 s lo cubre con margen.
    /// </summary>
    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(120);

    /// <summary>El único texto que se manda. Fijo: no se arma con nada que venga de afuera.</summary>
    public const string Probe = "ok";

    public static async Task<WarmupOutcome> RunAsync(
        IChatClient? chatClient,
        TimeSpan timeout,
        ILogger logger,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(logger);

        if (chatClient is null)
        {
            return Trace(logger, WarmupOutcome.NoServer, 0);
        }

        var started = Stopwatch.GetTimestamp();

        using var limit = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        limit.CancelAfter(timeout);

        try
        {
            await chatClient.GetResponseAsync(
                [new ChatMessage(ChatRole.User, Probe)],
                new ChatOptions { MaxOutputTokens = 1, Temperature = 0f, Tools = [] },
                limit.Token).ConfigureAwait(false);

            return Trace(logger, WarmupOutcome.Loaded, started);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return Trace(logger, WarmupOutcome.TimedOut, started);
        }
        catch (OperationCanceledException)
        {
            // EL SERVICIO SE ESTÁ DETENIENDO: no es una falla de la precarga.
            throw;
        }
        catch (Exception exception)
        {
            logger.LogWarning("Precarga del modelo: falló ({Failure}).", exception.GetType().Name);
            return Trace(logger, WarmupOutcome.Failed, started);
        }
    }

    private static WarmupOutcome Trace(ILogger logger, WarmupOutcome outcome, long started)
    {
        var elapsed = started == 0 ? 0 : (long)Stopwatch.GetElapsedTime(started).TotalMilliseconds;
        logger.LogInformation("Precarga del modelo: {Outcome} en {ElapsedMilliseconds} ms.", outcome, elapsed);
        return outcome;
    }
}
