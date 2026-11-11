using System.Diagnostics;
using GeometriaFactory.Application.Concepts;
using GeometriaFactory.Application.Ports;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace GeometriaFactory.Infrastructure.AI;

/// <summary>
/// Redacción opcional de una ficha con el modelo (ADR-14013). Sólo viajan la ficha y la pregunta
/// (REQ-IA-15); sin herramientas (R1); un solo intento, con el cupo de la capa (REQ-IA-21).
/// </summary>
public sealed class ModelConceptAnswerWriter : IConceptAnswerWriter
{
    public const string SystemPrompt =
        "Sos la ayuda de la Fabrica de Geometria, un laboratorio donde alumnos de programacion escriben en C# "
        + "clases de figuras y un metodo Describir() que arma un texto JSON a mano. Explicas conceptos en lenguaje "
        + "llano, en voseo rioplatense, en dos o tres oraciones, usando SOLO la ficha que se te da y conservando sus "
        + "terminos exactos. Nunca des numeros, formulas, cantidades de caras, operaciones (sumar, multiplicar, "
        + "dividir), JSON, codigo ni rutas. Nunca digas que vas a hacer algo.";

    private const int MaxOutputTokens = 220;

    private readonly IChatClient? _chatClient;
    private readonly HintConcurrencyGate _gate;
    private readonly TimeSpan _timeout;
    private readonly ILogger<ModelConceptAnswerWriter> _logger;

    public ModelConceptAnswerWriter(
        IChatClient? chatClient,
        HintConcurrencyGate gate,
        AiOptions options,
        ILogger<ModelConceptAnswerWriter> logger)
    {
        ArgumentNullException.ThrowIfNull(gate);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(logger);
        _chatClient = chatClient;
        _gate = gate;
        _timeout = TimeSpan.FromSeconds(options.TimeoutSeconds > 0 ? options.TimeoutSeconds : AiOptions.DefaultTimeoutSeconds);
        _logger = logger;
    }

    public async Task<ConceptAnswerResult> WriteAsync(ConceptCard card, string question, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(card);

        if (_chatClient is null)
        {
            return Trace(ConceptAnswerResult.Degraded("sin-servidor"), 0);
        }

        if (string.IsNullOrWhiteSpace(question) || question.Length > ConceptRouter.MaxQuestionLength)
        {
            return Trace(ConceptAnswerResult.Degraded("sobre-el-tope"), 0);
        }

        if (!_gate.TryEnter())
        {
            return Trace(ConceptAnswerResult.Degraded("cupo-ocupado"), 0);
        }

        var started = Stopwatch.GetTimestamp();

        try
        {
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeout.CancelAfter(_timeout);

            List<ChatMessage> messages =
            [
                new(ChatRole.System, SystemPrompt),
                new(ChatRole.User, $"Ficha «{card.Title}»: {card.Text}\n\nPregunta: {question}"),
            ];

            ChatResponse response;

            try
            {
                response = await _chatClient.GetResponseAsync(
                    messages,
                    new ChatOptions { Temperature = 0.2f, Seed = 7, MaxOutputTokens = MaxOutputTokens, Tools = [] },
                    timeout.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                return Trace(ConceptAnswerResult.Degraded("fuera-de-tiempo"), started);
            }
            catch (Exception exception) when (exception is not OperationCanceledException)
            {
                _logger.LogWarning("Conceptos: el modelo falló ({Failure}).", exception.GetType().Name);
                return Trace(ConceptAnswerResult.Degraded("sin-respuesta"), started);
            }

            var text = response.Text?.Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                return Trace(ConceptAnswerResult.Degraded("respuesta-vacia"), started);
            }

            return ConceptAnswerFilter.Check(text) is { } reason
                ? Trace(ConceptAnswerResult.Rejected(reason), started)
                : Trace(ConceptAnswerResult.Ok(text), started);
        }
        finally
        {
            _gate.Release();
        }
    }

    // LA TRAZA LLEVA RESULTADO, MOTIVO Y DURACIÓN: nunca la pregunta, la respuesta, la dirección ni el modelo (R6).
    private ConceptAnswerResult Trace(ConceptAnswerResult result, long started)
    {
        var elapsed = started == 0 ? 0 : (long)Stopwatch.GetElapsedTime(started).TotalMilliseconds;
        _logger.LogInformation("Conceptos: {Kind} {Reason} en {Elapsed} ms.", result.Kind, result.Reason ?? "-", elapsed);
        return result;
    }
}
