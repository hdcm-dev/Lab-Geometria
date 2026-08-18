namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// La solicitud con la que el administrador resuelve un trabajo en estado `Pendiente`
/// (`Contracts CU-08007` §4, flujo principal, paso 1).
/// </summary>
/// <remarks>
/// TRES CAMPOS, Y EL TERCERO ES OPCIONAL EN LOS DOS DESENLACES (`CU-08007` FA-01 y FA-02). El
/// contrato **no impone comentario ni siquiera al rechazar**, y esa ausencia es una consecuencia
/// aceptada por escrito aguas arriba: el estado le informa al alumno que no fue aceptado.
///
/// EL DESENLACE VIAJA POR SU NOMBRE Y NUNCA POR SU POSICIÓN, con las constantes de
/// <see cref="WorkOutcomeName"/>. Un entero acá dejaría que agregar un valor al conjunto del
/// dominio corriera el significado de los que ya viajaron.
///
/// EL IDENTIFICADOR DE LA RUTA GOBIERNA SOBRE EL DEL CUERPO, con el mismo criterio que
/// <see cref="WorkSubmissionRequest"/>: el contrato lo declara porque es el mismo tipo para los dos
/// extremos, y el punto de acceso usa el de la ruta, para que no haya un lugar donde los dos puedan
/// no coincidir.
///
/// NO LLEVA EL ESTADO PRETENDIDO. Se pide un desenlace y el dominio decide a qué estado lleva: un
/// campo de estado acá permitiría pedir `Finalizado` sin aprobar, que es la misma clase de defecto
/// que <see cref="WorkSubmissionRequest"/> evita al no declarar estado.
/// </remarks>
/// <param name="WorkId">Identidad del trabajo a resolver. El de la ruta gobierna.</param>
/// <param name="Outcome">Aprobar o rechazar, por el nombre de <see cref="WorkOutcomeName"/>.</param>
/// <param name="Comment">
/// Comentario escrito por el administrador. **Opcional en los dos desenlaces**: viaja sin valor
/// cuando no escribió nada, y el contrato no lo rellena.
/// </param>
public sealed record WorkOutcomeRequest(
    Guid? WorkId,
    string Outcome,
    string? Comment);
