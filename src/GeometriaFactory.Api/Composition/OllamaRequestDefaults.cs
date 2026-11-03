using System.Text.RegularExpressions;
using GeometriaFactory.Infrastructure.AI;
using OllamaSharp.Models.Chat;

namespace GeometriaFactory.Api.Composition;

/// <summary>
/// Lo que cada pedido al servidor de modelos lleva además del mensaje (`ADR-14012`): sin razonamiento y
/// con la permanencia del modelo en memoria.
/// </summary>
/// <remarks>
/// <para>
/// VIVE EN LA COMPOSICIÓN porque nombra un tipo de OllamaSharp, y el adaptador concreto sólo se ve acá
/// (`ADR-14007`, REQ-IA-02). Se extrajo de <c>AddAiLayer</c> para poder probarlo sin servidor.
/// </para>
/// <para>
/// <c>Think = false</c>: con razonamiento y salida con esquema, el modelo elegido gasta todo el tope de
/// tokens pensando y devuelve el contenido vacío (0 de 10 en la evaluación del 2026-11-03, unos 210 s
/// por caso). Sin razonamiento pasa 10 de 10 en 12,6 s de p95. En las pistas el razonamiento no aporta.
/// </para>
/// </remarks>
public static partial class OllamaRequestDefaults
{
    public static ChatRequest For(AiOptions ai)
    {
        ArgumentNullException.ThrowIfNull(ai);
        return new ChatRequest { Think = false, KeepAlive = KeepAliveOf(ai) };
    }

    /// <summary>
    /// La permanencia configurada si tiene forma de duración del servidor (<c>-1</c>, o un entero con
    /// <c>ms</c>, <c>s</c>, <c>m</c> o <c>h</c>); si no, <see cref="AiOptions.DefaultKeepAlive"/>.
    /// </summary>
    public static string KeepAliveOf(AiOptions ai)
    {
        ArgumentNullException.ThrowIfNull(ai);
        var value = ai.KeepAlive?.Trim() ?? string.Empty;
        return Duration().IsMatch(value) ? value : AiOptions.DefaultKeepAlive;
    }

    /// <summary>¿La configuración trae una permanencia que no se puede usar? Sirve para avisar.</summary>
    public static bool HasInvalidKeepAlive(AiOptions ai) =>
        !string.IsNullOrWhiteSpace(ai.KeepAlive) && !Duration().IsMatch(ai.KeepAlive.Trim());

    [GeneratedRegex(@"^(?:-1|\d{1,6}(?:ms|s|m|h))$", RegexOptions.CultureInvariant)]
    private static partial Regex Duration();
}
