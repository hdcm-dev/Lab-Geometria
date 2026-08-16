namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// Lo que `A-18` devuelve: las piezas para dibujar y las observaciones para leer.
/// </summary>
/// <remarks>
/// **NO DEVUELVE ESTADO DE TRABAJO, Y ES LA PROPIEDAD QUE MÁS CUIDA ESTE TIPO.** No hay trabajo:
/// resolver el estado es del dominio sobre un trabajo que existe, y devolver un estado acá
/// afirmaría una entrega que no ocurrió. Quien quiera saber si su trabajo va a quedar entregado
/// tiene una sola forma de averiguarlo, que es enviarlo —y enviar no cuesta nada, porque el trabajo
/// queda guardado y se reenvía cuantas veces haga falta—.
///
/// LA CANTIDAD DE FIGURAS NO ES DERIVABLE DE LAS PIEZAS, porque el conjunto admite huecos: una
/// figura que no se pudo reconstruir cuenta acá y no está en la lista. Es lo que permite decir «de
/// tres figuras se dibujaron dos» sin inventar el tercer número.
///
/// LAS OBSERVACIONES VIAJAN AUNQUE NADIE VAYA A GUARDARLAS. Previsualizar es el momento en que el
/// alumno mira su trabajo antes de entregarlo, y mostrarle el dibujo sin decirle qué no se pudo
/// interpretar sería **el fallo silencioso** que este producto existe para eliminar.
/// </remarks>
/// <param name="RootFigureCount">Cuántas figuras trae el texto, **incluidas las que fallaron**.</param>
/// <param name="Pieces">Las piezas reconstruidas, en su posición del conjunto raíz.</param>
/// <param name="Observations">Lo que la interpretación emitió, de las dos especies.</param>
public sealed record WorkInterpretationResponse(
    int RootFigureCount,
    IReadOnlyList<WorkPiece> Pieces,
    IReadOnlyList<WorkObservation> Observations);
