using GeometriaFactory.Application.Concepts;

namespace GeometriaFactory.Application.Ports;

/// <summary>
/// La huella de un turno del asistente (REQ-IA-30, K2 R7): qué anillo, qué intención, qué forma y con
/// qué resultado. SIN TEXTO Y SIN PERSONA: ni la pregunta, ni la respuesta, ni la cuenta, ni el trabajo,
/// y el día en lugar del instante.
/// </summary>
public sealed record ConceptTrace(
    ConceptRing Ring,
    string Intent,
    ConceptForm Form,
    string Outcome,
    string CorpusVersion,
    DateOnly Day);

/// <summary>Dónde queda la huella. Su lector es el docente (D-1, D-5).</summary>
public interface IConceptTraceStore
{
    /// <summary>Guarda una huella. Una falla al guardar no rompe la respuesta.</summary>
    Task RecordAsync(ConceptTrace trace, CancellationToken cancellationToken = default);
}
