using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Domain.Entities;

/// <summary>
/// Pieza de un trabajo: cada figura del conjunto raíz. **Su identidad es su posición.**
/// </summary>
/// <remarks>
/// ETAPA `f` (`Domain BT-06`): la entidad se modela con los atributos de
/// `Definicion-Modelo-De-Dominio.md` §2.3.
///
/// LA POSICIÓN NO SE RECALCULA (§2.3): una pieza conserva la posición de su figura **en el texto
/// del alumno**, aunque otras figuras del mismo conjunto no se hayan podido reconstruir. Es lo que
/// hace que la posición reservada de una figura fallida siga siendo ubicable (`CU-06001` §4 paso 6)
/// y que el conjunto de piezas admita huecos.
///
/// EL DECLARADO Y EL DERIVADO SE GUARDAN POR SEPARADO, y es decisión tomada aguas arriba
/// (§17.1.P.11 · GeometriaFactory-Domain punto 3): es lo que hace verificable la comparación sin
/// recalcularla en cada consulta, y lo que permite mostrarle al alumno **los dos valores** sobre su
/// propio trabajo.
///
/// EL VOLUMEN NO APLICA A LAS FIGURAS PLANAS (§2.3) y por eso los cuatro valores son anulables:
/// `null` significa **que el texto no lo trae o que el tipo no lo tiene**, y no cero.
///
/// ESTA ENTIDAD NO COMPARA NADA. Quién discrepa de quién lo resuelve `CU-06002` y lo reporta como
/// observación; acá los dos valores conviven sin juzgarse.
/// </remarks>
public sealed class Piece
{
    private readonly List<Component> _components = [];

    private Piece()
    {
    }

    /// <summary>Lugar que la figura ocupa en el conjunto raíz. **Es su identidad**, y es estable.</summary>
    public int Position { get; private set; }

    /// <summary>Discriminante que el texto del alumno declara.</summary>
    public FigureType Type { get; private set; }

    /// <summary>El área que trae el texto. **Se guarda tal cual, sin corregir.**</summary>
    public double? DeclaredArea { get; private set; }

    /// <summary>El área recalculada desde las dimensiones que el propio texto declara.</summary>
    public double? DerivedArea { get; private set; }

    /// <summary>El volumen que trae el texto. **Se guarda tal cual.** No aplica a las planas.</summary>
    public double? DeclaredVolume { get; private set; }

    /// <summary>El volumen recalculado desde las dimensiones. No aplica a las planas.</summary>
    public double? DerivedVolume { get; private set; }

    /// <summary>Figuras planas que forman la pieza. **Vacío admisible** en las piezas planas.</summary>
    public IReadOnlyList<Component> Components => _components;

    /// <summary>Valor de la clave `Largo` **de la propia figura**. Nulo si no la trae.</summary>
    /// <remarks>
    /// LAS PLANAS DEL CONJUNTO RAÍZ LLEVAN SU MEDIDA EN SÍ MISMAS, y por eso estos tres atributos
    /// existen: `§20.E-7` transcribe `{ "Tipo": "Circulo", "Radio": 2.50 }` como figura del conjunto
    /// raíz **sin componentes**. Sin ellos esa medida se perdía al guardar y la figura no se podía
    /// dibujar, aunque el conjunto raíz la contara.
    ///
    /// NULAS EN LAS VOLUMÉTRICAS: el cubo lleva su arista en sus caras y el ortoedro sus lados en
    /// sus bases. Duplicarlas acá crearía el segundo lugar donde el mismo dato puede decir otra cosa.
    /// </remarks>
    public double? DeclaredLength { get; private set; }

    /// <summary>Valor de la clave `Ancho` de la propia figura. Nulo si no la trae.</summary>
    public double? DeclaredWidth { get; private set; }

    /// <summary>Valor de la clave `Radio` de la propia figura. Nulo si no la trae.</summary>
    public double? DeclaredRadius { get; private set; }

    /// <summary>
    /// Reconstruye una pieza leída del texto del alumno, en su posición del conjunto raíz.
    /// </summary>
    /// <remarks>
    /// NO VALIDA Y NO PUEDE FALLAR, por el mismo motivo que <see cref="Component.Declare"/>: lo que
    /// no se pudo reconstruir **no llega hasta acá**, porque el adaptador emitió su observación y
    /// reservó la posición sin construir ninguna pieza.
    /// </remarks>
    public static Piece Reconstruct(
        int position,
        FigureType type,
        double? declaredArea,
        double? derivedArea,
        double? declaredVolume,
        double? derivedVolume,
        IEnumerable<Component>? components = null,
        double? declaredLength = null,
        double? declaredWidth = null,
        double? declaredRadius = null)
    {
        var piece = new Piece
        {
            Position = position,
            Type = type,
            DeclaredArea = declaredArea,
            DerivedArea = derivedArea,
            DeclaredVolume = declaredVolume,
            DerivedVolume = derivedVolume,
            DeclaredLength = declaredLength,
            DeclaredWidth = declaredWidth,
            DeclaredRadius = declaredRadius,
        };

        if (components is not null)
        {
            piece._components.AddRange(components);
        }

        return piece;
    }
}
