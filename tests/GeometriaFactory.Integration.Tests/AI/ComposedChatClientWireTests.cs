using System.Net;
using System.Text;
using System.Text.Json;
using GeometriaFactory.Api.Composition;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.AI;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// REQ-IA-18 «todo pedido» (`ADR-14012`): el cliente que compone <c>AddAiLayer</c> manda <c>think=false</c>
/// y <c>keep_alive</c> tanto en las pistas como en la precarga. Se mira el JSON que sale por HTTP, con un
/// handler falso: sin servidor y sin red.
/// </summary>
public sealed class ComposedChatClientWireTests : IDisposable
{
    private static readonly Uri Endpoint = new("http://modelo.invalid/");
    private readonly HintConcurrencyGate _gate = new();

    public void Dispose() => _gate.Dispose();

    [Fact]
    public async Task HintsAndWarmup_BothCarryThinkFalseAndKeepAlive()
    {
        var ai = new AiOptions { Model = "modelo-de-prueba", KeepAlive = "45m" };
        var handler = new CapturingHandler(FakeChatClient.ValidAnswer);
        using var client = OllamaRequestDefaults.CreateChatClient(ai, Endpoint, handler);

        var writer = new ModelObservationHintWriter(client, _gate, ai, NullLogger<ModelObservationHintWriter>.Instance, false);
        await writer.WriteAsync(new ObservationHintRequest(FigureType.Cube, "Area", 36, DiscrepancyDirection.Below, 54));
        var outcome = await ModelWarmup.RunAsync(client, TimeSpan.FromSeconds(5), NullLogger.Instance);

        Assert.Equal(WarmupOutcome.Loaded, outcome);
        Assert.Equal(2, handler.Bodies.Count);
        foreach (var body in handler.Bodies)
        {
            using var wire = JsonDocument.Parse(body);
            Assert.Equal(JsonValueKind.False, wire.RootElement.GetProperty("think").ValueKind);
            Assert.Equal("45m", wire.RootElement.GetProperty("keep_alive").GetString());
            Assert.Equal("modelo-de-prueba", wire.RootElement.GetProperty("model").GetString());
        }
    }

    private sealed class CapturingHandler(string content) : HttpMessageHandler
    {
        public List<string> Bodies { get; } = [];

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Bodies.Add(request.Content is null ? "{}" : await request.Content.ReadAsStringAsync(cancellationToken));
            var answer = JsonSerializer.Serialize(new
            {
                model = "modelo-de-prueba",
                created_at = "2026-11-03T00:00:00Z",
                message = new { role = "assistant", content },
                done = true,
                done_reason = "stop",
            });
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(answer + "\n", Encoding.UTF8, "application/x-ndjson"),
            };
        }
    }
}
