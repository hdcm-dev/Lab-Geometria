namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Validación de figuras. Puerto DECLARADO por el intake §17.2.P.1.
/// </summary>
/// <remarks>
/// ETAPA `f`: el puerto se escribe. Su adaptador es `LocalFigureValidator`, motor propio y **sin
/// red** (`Infrastructure BT-16`).
///
/// UN SOLO MIEMBRO PARA LAS DOS MITADES DEL CONTRATO. `CU-06001` interpreta y reconstruye y
/// `CU-06002` verifica los valores, pero **el consumidor no elige entre las dos**: pide una
/// interpretación y recibe las observaciones de las dos especies juntas, porque lo que el dominio
/// necesita para resolver el estado es el conjunto completo. Partir el puerto en dos obligaría al
/// llamador a orquestar un orden que es del adaptador. **[decisión de la etapa `f`, declarada]**
///
/// NO ES ASINCRÓNICO, y es lo mismo que decir que no sale de la máquina: este contrato no hace red,
/// no lee configuración propia y no toca la base (G-6, `CU-06001` §3). Un `Task` acá insinuaría lo
/// contrario en la firma que todo el mundo lee primero.
///
/// UN TEXTO QUE EL ALUMNO ESCRIBIÓ MAL ES UN RESULTADO Y NO UNA AVERÍA (G-7): la interpretación de
/// un texto ilegible **devuelve observaciones**, no una condición de error. Es la garantía que más
/// veces se rompe al implementar, y por eso la firma no ofrece ningún camino para romperla.
/// </remarks>
public interface IFigureValidator
{
    /// <summary>
    /// Interpreta el texto original del trabajo y devuelve las figuras y las observaciones.
    /// </summary>
    /// <param name="originalJson">El texto del alumno, tal como lo pegó. **No se modifica.**</param>
    FigureInterpretation Interpret(string originalJson);
}
