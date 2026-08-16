using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Works;

/// <summary>
/// Lo que la carga y la reedición devuelven: el identificador, el estado resuelto y el sello.
/// </summary>
/// <remarks>
/// EL ESTADO LLEGA DECIDIDO Y QUIEN EXPONE NO LO INTERPRETA (`Api CU-06` §4). Que el resultado
/// traiga `Draft` **no cambia el código de respuesta**: la petición se cumplió y el trabajo se
/// guardó. En la etapa `e` el estado es siempre `Draft`, porque el texto todavía no se interpreta.
///
/// NO LLEVA EL TEXTO ORIGINAL DE VUELTA: el consumidor ya lo tiene, y devolverlo abriría un
/// segundo lugar donde podría venir alterado.
/// </remarks>
/// <param name="WorkId">Identidad propia del trabajo.</param>
/// <param name="Status">Estado con el que quedó.</param>
/// <param name="RegisteredAt">Momento del registro, en tiempo universal coordinado.</param>
/// <param name="Observations">
/// Lo que la interpretación emitió. **Vacío en los rechazos**, donde no hubo interpretación que
/// adoptar, y vacío también cuando el texto verificó sin discrepancias.
/// </param>
public sealed record WorkOutcomeSnapshot(
    Guid WorkId,
    WorkStatus Status,
    DateTimeOffset RegisteredAt,
    IReadOnlyList<Observation>? Observations = null);
