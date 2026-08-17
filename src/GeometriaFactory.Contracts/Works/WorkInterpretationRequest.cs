namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// Lo que `A-18` recibe: el texto del alumno y nada más.
/// </summary>
/// <remarks>
/// NO LLEVA IDENTIFICADOR DE TRABAJO, y es deliberado: interpretar **no toca ningún trabajo**. Si
/// llevara uno, la siguiente pregunta razonable sería por qué no guarda el resultado, y la
/// respuesta —porque enviar es la única acción de guardado— quedaría dependiendo de que nadie lo
/// pregunte. Sin identificador, no hay nada que guardar.
///
/// NO LLEVA NOMBRE, FECHA NI DESCRIPCIÓN: no se está constituyendo un trabajo. Es la diferencia con
/// `WorkSubmissionRequest`, que sí los lleva porque el envío sí lo constituye.
/// </remarks>
/// <param name="OriginalJson">El texto del alumno, **tal como lo pegó**. No se recorta ni se normaliza.</param>
public sealed record WorkInterpretationRequest(string? OriginalJson);
