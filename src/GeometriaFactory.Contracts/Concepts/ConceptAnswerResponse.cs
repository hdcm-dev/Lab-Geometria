namespace GeometriaFactory.Contracts.Concepts;

/// <summary>
/// La respuesta del asistente. <c>Form</c> es F1…F8 o <c>NotFound</c>; <c>Origin</c> es <c>ficha</c>,
/// <c>modelo</c>, <c>fijo</c> o <c>caso-de-uso</c>. El destino, si lo hay, sale del catálogo por id.
/// </summary>
public sealed record ConceptAnswerResponse(
    string Form,
    int Ring,
    string Intent,
    string Text,
    ConceptCardReference? Card,
    ConceptDestinationChip? Destination,
    string Origin,
    string? Note,
    IReadOnlyList<ConceptBreakdownEntry> Breakdown);

/// <summary>La ficha en juego: id, título, texto y fuente.</summary>
public sealed record ConceptCardReference(string Id, string Title, string Text, string Source);

/// <summary>El chip: id del catálogo, texto (verbo + objeto) y ruta ya revalidada.</summary>
public sealed record ConceptDestinationChip(string Id, string Label, string Path);

/// <summary>Una porción del desglose ofrecido (F2).</summary>
public sealed record ConceptBreakdownEntry(string Label, int Count);
