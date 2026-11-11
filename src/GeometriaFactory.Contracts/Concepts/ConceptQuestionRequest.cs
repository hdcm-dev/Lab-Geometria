namespace GeometriaFactory.Contracts.Concepts;

/// <summary>
/// Una pregunta al asistente de conceptos. <c>WorkId</c> es el trabajo de la pantalla y nunca se toma del
/// texto (ARQ-2): el servicio lo revalida con la misma regla que la vista.
/// </summary>
public sealed record ConceptQuestionRequest(string Question, Guid? WorkId);
