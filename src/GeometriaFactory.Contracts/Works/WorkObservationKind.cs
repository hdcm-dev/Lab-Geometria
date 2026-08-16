namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// Los dos nombres con los que la especie de una observación viaja por el contrato.
/// </summary>
/// <remarks>
/// EXISTE PORQUE LA PIEZA PÚBLICA NO CONOCE AL DOMINIO Y NO TIENE QUE CONOCERLO. El conjunto
/// cerrado vive en `GeometriaFactory-Domain` y el front sólo ve el contrato: sin estas dos
/// constantes, la superficie compararía contra cadenas sueltas escritas a mano, que es donde una
/// letra de diferencia deja de dibujar una advertencia **sin que nada falle**.
///
/// SON LOS NOMBRES DEL CONJUNTO CERRADO Y NO UNA TRADUCCIÓN SUYA: la especie viaja por su nombre y
/// nunca por su posición, y estas constantes son ese nombre. La etiqueta que lee la persona
/// —«Advertencia», «Error»— la redacta la superficie, que es la que habla su idioma.
/// </remarks>
public static class WorkObservationKind
{
    /// <summary>No impide que el trabajo pase a estado `Pendiente`.</summary>
    public const string Warning = "Warning";

    /// <summary>Lo impide, y deja el trabajo en `Borrador` (RN-02005).</summary>
    public const string ValidationError = "ValidationError";
}
