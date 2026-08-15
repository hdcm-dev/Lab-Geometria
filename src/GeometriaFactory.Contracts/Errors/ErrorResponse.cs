namespace GeometriaFactory.Contracts.Errors;

/// <summary>
/// El ÚNICO tipo con el que un fallo cruza la frontera entre las dos piezas desplegables.
/// </summary>
/// <remarks>
/// CUATRO CAMPOS Y NINGUNO MÁS (`Contracts CU-06` CA-01): código, texto neutro, detalles y
/// momento. Ninguno puede transportar una dirección de servicio interno, una ruta del almacén,
/// un valor de secreto ni una traza (RA-03). Que el tipo no los declare es lo que lo vuelve
/// imposible, en lugar de dejarlo librado a que nadie los escriba.
/// </remarks>
/// <param name="Code">Código del conjunto cerrado de <see cref="ErrorCode"/>.</param>
/// <param name="Message">Texto neutro, apto para mostrarle a una persona.</param>
/// <param name="Details">Ubicación del defecto cuando la hay; vacío cuando no.</param>
/// <param name="OccurredAt">Momento en que la condición se produjo, en tiempo universal coordinado.</param>
public sealed record ErrorResponse(
    string Code,
    string Message,
    IReadOnlyList<ErrorDetail> Details,
    DateTimeOffset OccurredAt);
