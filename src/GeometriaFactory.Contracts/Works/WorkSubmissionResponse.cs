namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// El resultado del guardado: identificador propio, estado y fecha de registro.
/// </summary>
/// <remarks>
/// EL ESTADO QUE VIENE ACÁ ES **EL QUE LA INTERPRETACIÓN DECIDIÓ**, y quien lo recibe no lo
/// reinterpreta. Que sea `Borrador` **no es un fallo**: la petición se cumplió y el trabajo quedó
/// guardado con su texto. Confundir las dos cosas es el defecto más caro de esta frontera, porque
/// le diría a la persona que su petición estaba mal cuando lo que pasó es que su programa emitió
/// algo que no se puede interpretar —y el trabajo, mientras tanto, está guardado—.
///
/// EL ESTADO VIAJA POR SU NOMBRE Y NUNCA POR SU POSICIÓN (`Contratos-REST.md` §2): un valor
/// insertado en el medio del conjunto cerrado cambiaría el significado de todo lo ya emitido.
///
/// LA COLECCIÓN DE OBSERVACIONES ENTRA EN LA ETAPA `f`, y entra como el cambio **compatible** que
/// la etapa `e` anticipó al declarar su ausencia (`Contratos-Abstractions.md` §6): la etapa `e` no
/// la declaró porque habría tenido que inventar la forma de la observación, que es lo que decide
/// la etapa que construye el validador.
///
/// VIENE VACÍA CUANDO EL TEXTO VERIFICÓ SIN DISCREPANCIAS, y eso **no es lo mismo que no haberlo
/// interpretado**: quien quiera distinguir los dos casos mira el estado, que en el primero es
/// `Pendiente`.
/// </remarks>
/// <param name="WorkId">Identidad propia del trabajo, asignada por el producto.</param>
/// <param name="Status">Estado resultante, por su nombre, del conjunto cerrado de cuatro valores.</param>
/// <param name="RegisteredAt">Momento del registro, en tiempo universal coordinado.</param>
/// <param name="Observations">Lo que la interpretación emitió, de las dos especies.</param>
public sealed record WorkSubmissionResponse(
    Guid WorkId,
    string Status,
    DateTimeOffset RegisteredAt,
    IReadOnlyList<WorkObservation> Observations);
