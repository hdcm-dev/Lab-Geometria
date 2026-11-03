using GeometriaFactory.Infrastructure.AI;
using Microsoft.Extensions.AI;

namespace GeometriaFactory.Api.Composition;

/// <summary>
/// Corre <see cref="ModelWarmup"/> una vez, en segundo plano, al arrancar el servicio de datos
/// (REQ-IA-17, `ADR-14012`).
/// </summary>
/// <remarks>
/// EN SEGUNDO PLANO A PROPÓSITO: el servicio queda atendiendo desde el primer instante. Si un alumno pide
/// pistas antes de que termine la precarga, su pedido espera la misma carga (el servidor carga el modelo
/// una sola vez) o, si no entra en el tope, recibe la plantilla. Ninguno de los dos casos es un error.
/// Sólo se registra cuando hay cliente de modelo: con la capa apagada no existe.
/// </remarks>
public sealed class ModelWarmupHostedService(IChatClient chatClient, ILogger<ModelWarmupHostedService> logger)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
        ModelWarmup.RunAsync(chatClient, ModelWarmup.DefaultTimeout, logger, stoppingToken);
}
