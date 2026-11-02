using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace GeometriaFactory.Infrastructure.AI;

/// <summary>
/// Adaptador de <see cref="IObservationHintWriter"/> sobre <see cref="IChatClient"/>
/// (`ai-layer/01` §3.3 y §5, `ADR-14006`, `ADR-14007`).
/// </summary>
/// <remarks>
/// <para>
/// NIVEL A0: una llamada, sin herramientas, sin historial. El mensaje se arma **sólo** con el tipo de
/// figura, el campo, el valor declarado y el sentido (REQ-IA-04); el valor derivado se queda acá para
/// el filtro.
/// </para>
/// <para>
/// NUNCA LANZA POR EL MODELO. Sin cliente, sin respuesta, fuera de tiempo, con el cupo ocupado o con
/// una respuesta que el filtro rechaza, devuelve la plantilla del par (REQ-IA-07).
/// </para>
/// <para>
/// LA TRAZA LLEVA CATEGORÍA Y LATENCIA, Y NUNCA LA DIRECCIÓN DEL SERVIDOR. El contenido de los
/// mensajes sólo se registra si la composición lo habilita, y la composición lo habilita sólo en
/// desarrollo (REQ-IA-11).
/// </para>
/// </remarks>
public sealed class ModelObservationHintWriter : IObservationHintWriter
{
    /// <summary>
    /// El prompt de sistema de `ai-layer/01` §5.2, **literal** y en la misma escritura sin tildes que
    /// <c>PROMPT_SISTEMA</c> de <c>ai-layer/ensayos/pistas.py</c>, que es la que se ensayó (0/30 fugas).
    /// No se ajusta sin volver a correr el banco (decisión D-NIVEL3).
    /// </summary>
    public const string SystemPrompt =
        "Sos un asistente de una catedra de programacion. Un validador ya encontro una diferencia entre el "
        + "valor que declaro el programa del alumno y el que corresponde a las dimensiones de la figura. Escribi "
        + "tres pistas graduadas para que el alumno encuentre por su cuenta el error en su programa. Reglas: "
        + "1) nunca escribas el valor correcto ni lo calcules; 2) nunca escribas JSON, codigo ni nombres de clases; "
        + "3) no corrijas el texto del alumno; 4) nivel1 dice donde mirar; nivel2 dice que concepto geometrico "
        + "interviene; nivel3 dice como comprobarlo paso a paso, sin numeros. Cada pista en una o dos oraciones.";

    /// <summary>Tope de salida: tres pistas de una o dos oraciones.</summary>
    private const int MaxOutputTokens = 400;

    private static readonly JsonElement ResponseSchema = JsonDocument.Parse(
        """
        {"type":"object","properties":{"nivel1":{"type":"string"},"nivel2":{"type":"string"},"nivel3":{"type":"string"}},"required":["nivel1","nivel2","nivel3"]}
        """).RootElement.Clone();

    private readonly IChatClient? _chatClient;
    private readonly HintConcurrencyGate _gate;
    private readonly TimeSpan _timeout;
    private readonly ILogger<ModelObservationHintWriter> _logger;
    private readonly bool _includeContentInTelemetry;

    public ModelObservationHintWriter(
        IChatClient? chatClient,
        HintConcurrencyGate gate,
        AiOptions options,
        ILogger<ModelObservationHintWriter> logger,
        bool includeContentInTelemetry)
    {
        ArgumentNullException.ThrowIfNull(gate);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(logger);

        _chatClient = chatClient;
        _gate = gate;
        _timeout = TimeSpan.FromSeconds(options.TimeoutSeconds > 0 ? options.TimeoutSeconds : 20);
        _logger = logger;
        _includeContentInTelemetry = includeContentInTelemetry;
    }

    public Hints TemplateFor(ObservationHintRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return TemplateHints.For(request.FigureType, request.Field);
    }

    public async Task<HintResult> WriteAsync(ObservationHintRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var template = TemplateFor(request);

        if (_chatClient is null)
        {
            return Trace(HintResult.Degraded(template, "sin-servidor"), 0);
        }

        if (!_gate.TryEnter())
        {
            return Trace(HintResult.Degraded(template, "cupo-ocupado"), 0);
        }

        var started = Stopwatch.GetTimestamp();

        try
        {
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeout.CancelAfter(_timeout);

            var messages = MessagesFor(request);

            if (_includeContentInTelemetry)
            {
                _logger.LogDebug("Pistas: mensaje al modelo: {Content}", messages[1].Text);
            }

            ChatResponse response;

            try
            {
                response = await _chatClient.GetResponseAsync(messages, NewOptions(), timeout.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                return Trace(HintResult.Degraded(template, "fuera-de-tiempo"), started);
            }
            // CUALQUIER FALLA DEL MODELO CAE A LA PLANTILLA: es el contrato del puerto (REQ-IA-07).
            catch (Exception exception) when (exception is not OperationCanceledException)
            {
                _logger.LogWarning("Pistas: el modelo falló ({Failure}).", exception.GetType().Name);
                return Trace(HintResult.Degraded(template, "sin-respuesta"), started);
            }

            var text = response.Text;

            if (_includeContentInTelemetry)
            {
                _logger.LogDebug("Pistas: respuesta del modelo: {Content}", text);
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return Trace(HintResult.Degraded(template, "respuesta-vacia"), started);
            }

            if (Parse(text) is not { } levels)
            {
                return Trace(HintResult.Rejected(template, "forma"), started);
            }

            if (HintFilter.Check(levels.Level1, levels.Level2, levels.Level3, request.DerivedValue) is { } reason)
            {
                return Trace(HintResult.Rejected(template, reason), started);
            }

            return Trace(HintResult.Ok(levels), started);
        }
        finally
        {
            _gate.Release();
        }
    }

    /// <summary>
    /// Los dos mensajes. El de usuario es el de la variante A del ensayo (`pistas.py`, <c>mensaje</c>):
    /// cuatro líneas armadas desde constantes y un número con formato invariante.
    /// </summary>
    private static List<ChatMessage> MessagesFor(ObservationHintRequest request)
    {
        var direction = request.Direction == DiscrepancyDirection.Above ? "mayor" : "menor";
        var user = string.Join(
            '\n',
            $"Figura: {LabelOf(request.FigureType)}",
            $"Campo: {request.Field}",
            $"Valor declarado por el programa: {request.DeclaredValue.ToString("0.00", CultureInfo.InvariantCulture)}",
            $"El valor declarado es {direction} que el que corresponde.");

        return [new ChatMessage(ChatRole.System, SystemPrompt), new ChatMessage(ChatRole.User, user)];
    }

    /// <summary>Opciones nuevas en cada llamada: sin herramientas, salida acotada y con esquema.</summary>
    private static ChatOptions NewOptions() => new()
    {
        Temperature = 0.2f,
        Seed = 7,
        MaxOutputTokens = MaxOutputTokens,
        ResponseFormat = ChatResponseFormat.ForJsonSchema(ResponseSchema, "pistas"),
        Tools = [],
    };

    /// <summary>La etiqueta del emisor para cada tipo (`FigureType`), la misma que escribe el alumno.</summary>
    private static string LabelOf(FigureType figureType) => figureType switch
    {
        FigureType.Cylinder => "Cilindro",
        FigureType.Cube => "Cubo",
        FigureType.Orthohedron => "Ortoedro",
        FigureType.Rectangle => "Rectangulo",
        FigureType.Square => "Cuadrado",
        FigureType.Circle => "Circulo",
        _ => "Figura",
    };

    private static Hints? Parse(string text)
    {
        try
        {
            using var document = JsonDocument.Parse(text.Trim());

            if (document.RootElement.ValueKind != JsonValueKind.Object)
            {
                return null;
            }

            string? Read(string name) =>
                document.RootElement.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String
                    ? value.GetString()
                    : null;

            var level1 = Read("nivel1");
            var level2 = Read("nivel2");
            var level3 = Read("nivel3");

            return level1 is null || level2 is null || level3 is null
                ? null
                : new Hints(level1.Trim(), level2.Trim(), level3.Trim(), HintOrigin.Model);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private HintResult Trace(HintResult result, long started)
    {
        var elapsed = started == 0 ? 0 : (long)Stopwatch.GetElapsedTime(started).TotalMilliseconds;

        _logger.LogInformation(
            "Pistas: {Kind} ({Reason}) en {ElapsedMilliseconds} ms.",
            result.Kind,
            result.Reason ?? "ok",
            elapsed);

        return result;
    }
}
