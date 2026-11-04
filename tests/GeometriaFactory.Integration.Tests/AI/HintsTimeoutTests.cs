using System.Diagnostics;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Infrastructure.AI;
using GeometriaFactory.Infrastructure.Figures;
using GeometriaFactory.Web.Integration;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// G1 de la mesa UX del 2026-11-03: el servicio de datos corta ANTES que la Web, de modo que la
/// alumna siempre recibe al menos la plantilla (REQ-IA-07, REQ-IA-10).
/// </summary>
public sealed class HintsTimeoutTests : IDisposable
{
    private readonly HintConcurrencyGate _gate = new();

    public void Dispose() => _gate.Dispose();

    /// <summary>
    /// El tope de pistas por omisión queda por debajo de lo que la Web espera al servicio de datos.
    /// Si alguien sube uno sin el otro, la Web vuelve a cortar primero y la plantilla no llega.
    /// </summary>
    [Fact]
    public void TheDataServiceGivesUpBeforeTheWeb()
    {
        Assert.Equal(8, AiOptions.DefaultTimeoutSeconds);
        Assert.True(new AiOptions().TimeoutSeconds < DataServiceClient.RequestTimeout.TotalSeconds);
        Assert.True(new AiOptions().TimeoutSeconds < DataServiceClient.HintsRequestTimeout.TotalSeconds);
    }

    /// <summary>
    /// Un modelo que no termina nunca, con dos advertencias en el texto: el tope es del PEDIDO, no de
    /// cada llamada. Todas las pistas salen de plantilla y el pedido entero termina dentro del tope
    /// (con margen), no en «tope × advertencias».
    /// </summary>
    [Fact]
    public async Task ASlowModel_GivesTheTemplateForEveryWarning_WithinOneBudget()
    {
        const double budget = 1.0;
        var writer = new ModelObservationHintWriter(
            FakeChatClient.Delaying(), _gate, new AiOptions { TimeoutSeconds = budget },
            new CapturingLogger<ModelObservationHintWriter>(), includeContentInTelemetry: false);
        var useCase = new ExplainObservationsUseCase(new LocalFigureValidator(), writer, maxObservations: 5);

        var watch = Stopwatch.StartNew();
        var explained = await useCase.ExplainAsync(Scenarios.E1);
        watch.Stop();

        Assert.True(explained.Hints.Count >= 2, $"E1 trae dos advertencias; llegaron {explained.Hints.Count}.");
        Assert.All(explained.Hints, h => Assert.Equal(HintOrigin.Template, h.Hints.Origin));
        Assert.True(
            watch.Elapsed < TimeSpan.FromSeconds(budget * 1.8),
            $"El pedido tardó {watch.Elapsed.TotalSeconds:0.00} s con un tope de {budget} s.");
    }

    /// <summary>El tope agotado no vuelve a tocar el modelo: la llamada siguiente es plantilla sin espera.</summary>
    [Fact]
    public async Task OnceTheBudgetIsSpent_TheNextCallDoesNotReachTheModel()
    {
        var client = FakeChatClient.Delaying();
        var writer = new ModelObservationHintWriter(
            client, _gate, new AiOptions { TimeoutSeconds = 0.2 },
            new CapturingLogger<ModelObservationHintWriter>(), includeContentInTelemetry: false);
        var request = new ObservationHintRequest(
            Domain.Values.FigureType.Cube, "Area", 36, DiscrepancyDirection.Below, 54);

        var first = await writer.WriteAsync(request);
        var second = await writer.WriteAsync(request);

        Assert.Equal("fuera-de-tiempo", first.Reason);
        Assert.Equal("fuera-de-tiempo", second.Reason);
        Assert.Single(client.Calls);
    }
}
