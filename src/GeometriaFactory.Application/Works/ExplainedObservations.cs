using GeometriaFactory.Application.Ports;

namespace GeometriaFactory.Application.Works;

/// <summary>
/// Lo que devuelve <see cref="ExplainObservationsUseCase"/>: la interpretación del validador,
/// **sin tocar**, y las pistas de cada advertencia de valor.
/// </summary>
public sealed record ExplainedObservations(
    FigureInterpretation Interpretation,
    IReadOnlyList<ObservationHints> Hints);

/// <summary>Las pistas de una advertencia, con su posición en la lista de observaciones.</summary>
public sealed record ObservationHints(int ObservationIndex, Hints Hints);
