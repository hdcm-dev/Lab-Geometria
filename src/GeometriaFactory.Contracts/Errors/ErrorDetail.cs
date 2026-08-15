namespace GeometriaFactory.Contracts.Errors;

/// <summary>
/// Ubicación de un defecto dentro de la solicitud o dentro del texto del alumno.
/// </summary>
/// <remarks>
/// El índice de figura es nulo mientras el fallo no provenga de la interpretación del texto
/// (`Contracts CU-06` §4 paso 3). En la etapa `c` es siempre nulo: no hay texto que interpretar.
/// </remarks>
/// <param name="Field">Nombre del campo señalado.</param>
/// <param name="FigureIndex">Índice de la figura, cuando el fallo viene de la interpretación (RN-09).</param>
public sealed record ErrorDetail(string Field, int? FigureIndex = null);
