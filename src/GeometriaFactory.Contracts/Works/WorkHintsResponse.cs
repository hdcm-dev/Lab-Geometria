namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// Respuesta de las pistas: las observaciones **idénticas a las de A-18** para el mismo texto
/// (REQ-IA-16) y, al lado, las pistas de cada advertencia de valor.
/// </summary>
public sealed record WorkHintsResponse(
    IReadOnlyList<WorkObservation> Observations,
    IReadOnlyList<WorkObservationHints> Hints);

/// <summary>
/// Las tres pistas de una advertencia. <c>Origin</c> es <c>Model</c> o <c>Template</c>, y la Web lo
/// muestra: el alumno sabe si la pista es automática o del docente.
/// </summary>
public sealed record WorkObservationHints(
    int ObservationIndex,
    string Level1,
    string Level2,
    string Level3,
    string Origin);
