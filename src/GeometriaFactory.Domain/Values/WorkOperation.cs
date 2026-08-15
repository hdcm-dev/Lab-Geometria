namespace GeometriaFactory.Domain.Values;

/// <summary>
/// Operación que se consulta sobre un trabajo. Conjunto cerrado de tres valores.
/// </summary>
/// <remarks>
/// LOS DOS ALCANCES NO ADMITEN EL MISMO SUBCONJUNTO, y por eso el conjunto es uno solo y cada
/// resolución declara cuál acepta: `Domain CU-09` §3 declara **ver, reeditar y eliminar** para el
/// alumno, y `Domain CU-11` §3 declara **ver y eliminar** para el administrador. Una operación
/// fuera del subconjunto que la resolución admite devuelve `UNKNOWN_OPERATION` (`OPERACION_DESCONOCIDA`).
///
/// No se serializa ni se persiste: es un argumento de consulta y no un atributo de ninguna
/// entidad, de modo que no le aplica la regla de etiqueta castellana de `Norma-De-Nomenclatura.md`
/// §6.7, que rige sobre los conjuntos cerrados que la persona ve.
/// </remarks>
public enum WorkOperation
{
    /// <summary>Ver el trabajo. La admiten las dos resoluciones.</summary>
    View = 1,

    /// <summary>Reeditar el trabajo. **Sólo** la admite la resolución del alumno (`Domain CU-09`).</summary>
    Edit = 2,

    /// <summary>Eliminar el trabajo. La admiten las dos resoluciones, con alcances opuestos.</summary>
    Delete = 3
}
