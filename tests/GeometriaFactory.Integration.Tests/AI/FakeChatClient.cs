using Microsoft.Extensions.AI;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// Doble guionado de <see cref="IChatClient"/> (`ai-layer/01` §5.1, «Doble»): sin red, sin
/// librerías de mocking. Captura los mensajes y responde lo que el guion diga.
/// </summary>
internal sealed class FakeChatClient : IChatClient
{
    private readonly Func<IReadOnlyList<ChatMessage>, CancellationToken, Task<ChatResponse>> _script;

    public FakeChatClient(Func<IReadOnlyList<ChatMessage>, CancellationToken, Task<ChatResponse>> script) =>
        _script = script;

    public List<IReadOnlyList<ChatMessage>> Calls { get; } = [];

    public List<ChatOptions?> Options { get; } = [];

    public static FakeChatClient Answering(string text) =>
        new((_, _) => Task.FromResult(new ChatResponse(new ChatMessage(ChatRole.Assistant, text))));

    public static FakeChatClient Throwing() =>
        new((_, _) => throw new HttpRequestException("el servidor no respondió"));

    public static FakeChatClient Delaying() =>
        new(async (_, token) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, token);
            return new ChatResponse();
        });

    public const string ValidAnswer =
        """{"nivel1":"Mira como calcula el area tu programa.","nivel2":"El area total suma todas las caras.","nivel3":"Conta las caras y verifica que las sume todas."}""";

    public async Task<ChatResponse> GetResponseAsync(
        IEnumerable<ChatMessage> messages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var captured = messages.ToList();

        lock (Calls)
        {
            Calls.Add(captured);
            Options.Add(options);
        }

        return await _script(captured, cancellationToken);
    }

    public IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(
        IEnumerable<ChatMessage> messages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default) =>
        throw new NotSupportedException("La capa de pistas no usa respuestas en flujo.");

    public object? GetService(Type serviceType, object? serviceKey = null) => null;

    public void Dispose()
    {
    }
}
