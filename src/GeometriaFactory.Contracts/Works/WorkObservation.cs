namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// Una observación tal como cruza la frontera: **datos que ubican y cuantifican, no una frase**.
/// </summary>
/// <remarks>
/// EL TEXTO NO VIAJA, Y ES LA DECISIÓN DE FONDO DE ESTE TIPO. La observación lleva la especie, la
/// posición, el campo y los dos valores, y **quien la redacta para la persona es la pieza pública**.
/// Mandar la frase armada ataría el idioma del producto a la capa que menos tiene que ver con la
/// persona, y haría que cambiar una palabra fuera un cambio de contrato.
///
/// LA ESPECIE VIAJA POR SU NOMBRE Y NUNCA POR SU POSICIÓN (`Contratos-REST.md` §2), con el mismo
/// criterio que el estado del trabajo.
///
/// LA POSICIÓN ES NULABLE PORQUE HAY DOS OBSERVACIONES QUE NO SON DE NINGUNA FIGURA: el conjunto
/// raíz vacío y el texto que no se pudo leer. Las dos son lo único que el alumno tiene para
/// entender qué pasó, de modo que **omitirlas sería peor que declararlas sin posición**.
///
/// EL CAMPO CONSERVA LA CLAVE DEL TEXTO DEL ALUMNO —`Tipo`, `Largo`, `Area`, `Volumen`— y **no se
/// traduce**: la persona lo va a buscar en su propio programa.
/// </remarks>
/// <param name="Kind">`Advertencia` o `ErrorDeValidacion`, por su nombre.</param>
/// <param name="PiecePosition">Posición de la figura en el texto. Nula si no es de ninguna.</param>
/// <param name="Field">La clave del texto del alumno donde está el defecto.</param>
/// <param name="DeclaredValue">El valor que trae el texto. Presente en las advertencias.</param>
/// <param name="DerivedValue">El valor recalculado. Presente en las advertencias.</param>
public sealed record WorkObservation(
    string Kind,
    int? PiecePosition,
    string Field,
    double? DeclaredValue,
    double? DerivedValue);
