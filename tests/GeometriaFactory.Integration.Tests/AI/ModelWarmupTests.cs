using System.Text.Json;
using GeometriaFactory.Api.Composition;
using GeometriaFactory.Infrastructure.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// REQ-IA-17 y REQ-IA-18 (`ai-layer/01` §4.5 y §7, `ADR-14012`): la precarga y lo que cada pedido lleva
/// al servidor. Sin modelo y sin red, con <see cref="FakeChatClient"/>.
/// </summary>
public sealed class ModelWarmupTests
{
    [Fact]
    public async Task Warmup_WithoutClient_DoesNothing()
    {
        var outcome = await ModelWarmup.RunAsync(null, TimeSpan.FromSeconds(1), NullLogger.Instance);

        Assert.Equal(WarmupOutcome.NoServer, outcome);
    }

    /// <summary>Un solo mensaje fijo, sin sistema, sin herramientas y con un token: no lleva nada del producto.</summary>
    [Fact]
    public async Task Warmup_SendsOneMinimalCall()
    {
        var client = FakeChatClient.Answering("o");

        var outcome = await ModelWarmup.RunAsync(client, TimeSpan.FromSeconds(1), NullLogger.Instance);

        Assert.Equal(WarmupOutcome.Loaded, outcome);
        var messages = Assert.Single(client.Calls);
        var message = Assert.Single(messages);
        Assert.Equal(ChatRole.User, message.Role);
        Assert.Equal(ModelWarmup.Probe, message.Text);
        var options = Assert.Single(client.Options);
        Assert.NotNull(options);
        Assert.Equal(1, options!.MaxOutputTokens);
        Assert.Empty(options.Tools ?? []);
    }

    [Fact]
    public async Task Warmup_ModelFailure_DoesNotThrow()
    {
        var outcome = await ModelWarmup.RunAsync(FakeChatClient.Throwing(), TimeSpan.FromSeconds(1), NullLogger.Instance);

        Assert.Equal(WarmupOutcome.Failed, outcome);
    }

    [Fact]
    public async Task Warmup_Timeout_DoesNotThrow()
    {
        var outcome = await ModelWarmup.RunAsync(FakeChatClient.Delaying(), TimeSpan.FromMilliseconds(50), NullLogger.Instance);

        Assert.Equal(WarmupOutcome.TimedOut, outcome);
    }

    /// <summary>Si el servicio se detiene durante la precarga, se propaga la cancelación: no es una falla.</summary>
    [Fact]
    public async Task Warmup_HostStopping_PropagatesCancellation()
    {
        using var stopping = new CancellationTokenSource();
        var run = ModelWarmup.RunAsync(FakeChatClient.Delaying(), TimeSpan.FromSeconds(30), NullLogger.Instance, stopping.Token);

        await stopping.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => run);
    }

    /// <summary>QG-06013: la traza dice resultado y latencia, nunca una dirección.</summary>
    [Fact]
    public async Task Warmup_TraceHasNoAddress()
    {
        var logger = new CapturingLogger<ModelWarmupTests>();

        await ModelWarmup.RunAsync(FakeChatClient.Throwing(), TimeSpan.FromSeconds(1), logger);

        Assert.NotEmpty(logger.Lines);
        // SE BUSCA LA FORMA DE UNA DIRECCIÓN, no la palabra «http»: el nombre del tipo de excepción
        // (HttpRequestException) sí puede aparecer y no revela nada.
        Assert.All(logger.Lines, line => Assert.DoesNotContain("://", line, StringComparison.Ordinal));
    }

    /// <summary>REQ-IA-18: todo pedido sale sin razonamiento y con la permanencia configurada.</summary>
    [Theory]
    [InlineData("30m", "30m")]
    [InlineData("8h", "8h")]
    [InlineData("-1", "-1")]
    [InlineData(" 45m ", "45m")]
    [InlineData("", AiOptions.DefaultKeepAlive)]
    [InlineData("mucho", AiOptions.DefaultKeepAlive)]
    [InlineData("30 m", AiOptions.DefaultKeepAlive)]
    [InlineData("-5m", AiOptions.DefaultKeepAlive)]
    public void EveryRequest_ThinkFalseAndKeepAlive(string configured, string expected)
    {
        var request = OllamaRequestDefaults.For(new AiOptions { KeepAlive = configured });

        // SE MIRA LO QUE VIAJA, no la propiedad: el contrato es con el servidor, y así la prueba no
        // depende de cómo OllamaSharp 5.5.0 represente `Think` por dentro.
        using var wire = JsonDocument.Parse(JsonSerializer.Serialize(request));
        Assert.Equal(JsonValueKind.False, wire.RootElement.GetProperty("think").ValueKind);
        Assert.Equal(expected, wire.RootElement.GetProperty("keep_alive").GetString());
    }

    [Fact]
    public void InvalidKeepAlive_IsDetectedForTheWarning()
    {
        Assert.True(OllamaRequestDefaults.HasInvalidKeepAlive(new AiOptions { KeepAlive = "mucho" }));
        Assert.False(OllamaRequestDefaults.HasInvalidKeepAlive(new AiOptions { KeepAlive = "30m" }));
        Assert.False(OllamaRequestDefaults.HasInvalidKeepAlive(new AiOptions { KeepAlive = "" }));
    }

    /// <summary>Los valores por omisión del código son los de la evaluación completa del 2026-11-03.</summary>
    [Fact]
    public void Defaults_MatchTheEvaluation()
    {
        var ai = new AiOptions();

        Assert.Equal(20, ai.TimeoutSeconds);
        Assert.Equal("30m", ai.KeepAlive);
        Assert.Equal(string.Empty, ai.Endpoint);
        Assert.Equal(string.Empty, ai.Model);
    }
}
