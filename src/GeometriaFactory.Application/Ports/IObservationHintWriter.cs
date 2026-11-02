namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Redactor de pistas sobre una observación de valor (`ai-layer/01-Especificacion-V1-Pistas.md`
/// §3.3, `ADR-14006`). Es el único puerto asincrónico que habla con un modelo, y vive aparte de
/// <see cref="IFigureValidator"/> para que el validador siga sincrónico y sin red (G-6).
/// </summary>
/// <remarks>
/// EL VALIDADOR DECIDE Y ESTE PUERTO SÓLO REDACTA. Nunca recibe el texto del alumno, nunca cambia
/// una observación y nunca persiste nada (REQ-IA-04, REQ-IA-09, REQ-IA-16).
/// </remarks>
public interface IObservationHintWriter
{
    /// <summary>
    /// Pide las tres pistas al modelo. Nunca lanza por una falla del modelo: sin servidor, sin
    /// respuesta, fuera de tiempo, con el cupo ocupado o con la respuesta rechazada por el filtro,
    /// devuelve <see cref="HintResultKind.Degraded"/> o <see cref="HintResultKind.Rejected"/> con la
    /// plantilla del par (figura, campo).
    /// </summary>
    Task<HintResult> WriteAsync(ObservationHintRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// La plantilla del par (figura, campo), sin pasar por el modelo. La usa el caso de uso para las
    /// observaciones que exceden el tope por pedido (REQ-IA-10).
    /// </summary>
    Hints TemplateFor(ObservationHintRequest request);
}
