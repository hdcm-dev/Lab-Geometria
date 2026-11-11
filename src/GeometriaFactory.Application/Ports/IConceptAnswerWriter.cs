using GeometriaFactory.Application.Concepts;

namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Redactor opcional sobre una ficha (ADR-14013). Sólo recibe la ficha y la pregunta: nunca el trabajo,
/// la figura ni sus valores (REQ-IA-15). Nunca lanza por una falla del modelo.
/// </summary>
public interface IConceptAnswerWriter
{
    /// <summary>Redacta, o devuelve el motivo por el que no; el caso de uso responde con la ficha.</summary>
    Task<ConceptAnswerResult> WriteAsync(ConceptCard card, string question, CancellationToken cancellationToken = default);
}

/// <summary>Resultado del redactor. El motivo es una categoría, nunca el texto rechazado.</summary>
public sealed record ConceptAnswerResult(HintResultKind Kind, string? Text, string? Reason)
{
    public static ConceptAnswerResult Ok(string text) => new(HintResultKind.Ok, text, null);

    public static ConceptAnswerResult Degraded(string reason) => new(HintResultKind.Degraded, null, reason);

    public static ConceptAnswerResult Rejected(string reason) => new(HintResultKind.Rejected, null, reason);
}
