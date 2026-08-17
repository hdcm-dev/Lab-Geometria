using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Domain.Entities;

/// <summary>
/// Observación emitida sobre un trabajo: advertencia o error de validación.
/// </summary>
/// <remarks>
/// ETAPA `f` (`Domain BT-06`): la entidad se modela con los atributos de
/// `Definicion-Modelo-De-Dominio.md` §2.5.
///
/// LA ENTIDAD ES UNA Y SU ESPECIE ES UN ATRIBUTO (§2.5). No hay dos clases: hay dos valores de
/// <see cref="ObservationKind"/> con efecto distinto sobre el envío, y **sólo el error de
/// validación impide el paso a `Submitted`** (RN-02005). Modelarlas como dos tipos haría que el
/// dominio tuviera que unirlas para contarlas, que es la operación que RN-02005 hace.
///
/// NINGÚN MENSAJE ES GENÉRICO, y por eso los cuatro atributos que ubican y cuantifican viajan como
/// datos y no dentro de un texto: la **posición** y el **campo** localizan (RN-02009, G-4), y el
/// **valor declarado** y el **derivado** son obligatorios en las advertencias de discrepancia
/// (§2.5), porque el mensaje tiene que expresar **los dos** (`CU-06002` CA-04).
///
/// EL TEXTO DEL MENSAJE NO ES UN ATRIBUTO. `Definicion-Modelo-De-Dominio.md` §2.5 declara cuatro
/// atributos y ninguno es una cadena redactada: quien la arma para la persona es la pieza pública,
/// con estos datos. Guardar acá una frase la ataría al idioma y a la redacción del día en que se
/// escribió. **[derivación de la etapa `f`, declarada]**
///
/// LA POSICIÓN ES LA DEL TEXTO Y NO LA DE LA PIEZA ADOPTADA (§2.5), de modo que una figura que no
/// se pudo reconstruir sigue siendo ubicable. Es nula sólo cuando la observación **no es atribuible
/// a ninguna figura**: el conjunto raíz vacío de `CU-06001` FA-03 y el texto ilegible de FA-04.
/// </remarks>
public sealed class Observation
{
    private Observation()
    {
        Field = string.Empty;
    }

    /// <summary>Advertencia o error de validación. Sólo la segunda impide el paso a `Submitted`.</summary>
    public ObservationKind Kind { get; private set; }

    /// <summary>
    /// Figura del conjunto raíz sobre la que se emite, **contada en el texto**. Nula cuando la
    /// observación no es atribuible a ninguna figura.
    /// </summary>
    public int? PiecePosition { get; private set; }

    /// <summary>
    /// Campo del dato del alumno sobre el que se emite, **con la clave que su texto usa**:
    /// `Tipo`, `Largo`, `Ancho`, `Radio`, `Area`, `Volumen`. No se traduce.
    /// </summary>
    public string Field { get; private set; }

    /// <summary>El valor que trae el texto. Obligatorio en las advertencias de discrepancia.</summary>
    public double? DeclaredValue { get; private set; }

    /// <summary>El valor recalculado. Obligatorio en las advertencias de discrepancia.</summary>
    public double? DerivedValue { get; private set; }

    /// <summary>
    /// Error de validación en una figura, con su posición y su campo (RN-02009).
    /// </summary>
    /// <param name="piecePosition">
    /// Posición de la figura en el texto. **Nula sólo** en las dos observaciones que no son de una
    /// figura: conjunto raíz vacío y texto ilegible.
    /// </param>
    /// <param name="field">La clave del texto del alumno donde está el defecto.</param>
    public static Observation ValidationErrorAt(int? piecePosition, string field) => new()
    {
        Kind = ObservationKind.ValidationError,
        PiecePosition = piecePosition,
        Field = field,
        DeclaredValue = null,
        DerivedValue = null,
    };

    /// <summary>
    /// Advertencia de discrepancia entre el valor declarado y el derivado, **con los dos valores**.
    /// </summary>
    public static Observation ValueDiscrepancyAt(
        int piecePosition,
        string field,
        double declaredValue,
        double derivedValue) => new()
        {
            Kind = ObservationKind.Warning,
            PiecePosition = piecePosition,
            Field = field,
            DeclaredValue = declaredValue,
            DerivedValue = derivedValue,
        };
}
