using GeometriaFactory.Application.Concepts;

namespace GeometriaFactory.Application.Ports;

/// <summary>
/// El corpus de fichas de concepto (ARQ-4). El adaptador las lee de `docs-as-code/ayuda/conceptos/`,
/// embebidas al compilar: lo que se publica y lo que contesta el asistente son el mismo archivo.
/// </summary>
public interface IConceptCorpus
{
    /// <summary>Versión del corpus: va a la huella para saber con qué fichas se contestó.</summary>
    string Version { get; }

    /// <summary>Todas las fichas publicadas.</summary>
    IReadOnlyList<ConceptCard> Cards { get; }
}
