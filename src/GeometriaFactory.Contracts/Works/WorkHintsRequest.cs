namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// Pedido de pistas de `POST /v1/interpretaciones/pistas` (expediente 0002, `ADR-14009`). **El
/// mismo cuerpo que A-18**: el texto tal como lo emitió el programa del alumno.
/// </summary>
public sealed record WorkHintsRequest(string? OriginalJson);
