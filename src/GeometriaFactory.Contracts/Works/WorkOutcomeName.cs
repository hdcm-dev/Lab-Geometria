namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// Los dos nombres con los que el desenlace de una revisión viaja por el contrato
/// (`Contracts CU-08007` §3, conjunto cerrado de dos).
/// </summary>
/// <remarks>
/// EXISTE POR EL MISMO MOTIVO QUE <see cref="WorkObservationKind"/>: la pieza pública no conoce al
/// dominio y no tiene que conocerlo. El conjunto cerrado vive en `GeometriaFactory-Domain`, y sin
/// estas dos constantes la superficie enviaría cadenas escritas a mano —donde una letra de
/// diferencia se traduce en un desenlace que no se aplica **sin que nada falle**—.
///
/// SON DOS Y NO TRES. No hay «pendiente» acá: pendiente es el estado del que se sale, no un
/// desenlace al que se llega. Que el tipo no lo declare es lo que vuelve imposible pedirlo.
///
/// EL NOMBRE DEL DESENLACE NO ES EL DEL ESTADO QUE PRODUCE, y es deliberado: se pide **aprobar** y
/// el trabajo queda **`Finalizado`**. Quien decide qué estado produce cada desenlace es el dominio,
/// y confundirlos acá pondría esa decisión en el borde.
/// </remarks>
public static class WorkOutcomeName
{
    /// <summary>Aprobar. El trabajo queda en `Finalizado`, que es terminal (RN-02010).</summary>
    public const string Approve = "Approve";

    /// <summary>Rechazar. El trabajo queda en `Rechazado`, que también es terminal.</summary>
    public const string Reject = "Reject";
}
