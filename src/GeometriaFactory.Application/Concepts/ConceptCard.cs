using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Concepts;

/// <summary>
/// Una ficha de concepto del asistente (`docs-as-code/ayuda/conceptos/`, ADR-14013): el texto que se
/// le muestra a la persona y los sinónimos con los que el enrutador la elige.
/// </summary>
/// <remarks>
/// LA FICHA ES LA RESPUESTA, NO UN PUNTERO. La sonda del banco midió que el modelo la parafrasea sin
/// superarla; por eso el respaldo determinista es la ficha misma y el modelo, si se enciende, sólo
/// redacta sobre ella.
/// </remarks>
public sealed record ConceptCard(
    string Id,
    string Title,
    string Text,
    IReadOnlyList<string> Synonyms,
    IReadOnlyList<Role> Audience,
    string Destination,
    string Source,
    IReadOnlyList<string>? Questions = null)
{
    /// <summary>
    /// Las preguntas reales del frontmatter (<c>preguntas:</c>, K6 §4): una pregunta igual a una de estas
    /// elige la ficha antes que cualquier sinónimo, y cada una es un caso de la medición de recall@3.
    /// </summary>
    public IReadOnlyList<string> Questions { get; init; } = Questions ?? [];
}
