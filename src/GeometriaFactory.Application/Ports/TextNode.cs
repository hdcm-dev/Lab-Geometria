namespace GeometriaFactory.Application.Ports;

/// <summary>
/// El texto del alumno **tal como lo escribió**, en forma de árbol.
/// </summary>
/// <remarks>
/// POR QUÉ ESTO NO SE DERIVA DE LAS PIEZAS, que es la pregunta que corresponde hacer. El intake
/// §20 lo declara sin lugar a dudas: «el árbol del JSON **muestra las dos piezas, incluida la que
/// no se dibujó**. Se lee lo que el alumno escribió, no lo que la escena logró representar». Una
/// figura que no se pudo reconstruir **no produce pieza** —el adaptador emite su observación y
/// reserva la posición—, de modo que un árbol armado desde `Pieces` la haría desaparecer
/// justamente cuando el alumno más necesita verla: cuando está buscando qué escribió mal.
///
/// QUIÉN LO ARMA, Y POR QUÉ NADIE MÁS. El texto lo lee **un solo componente**, y es este puerto.
/// El árbol es una lectura del texto, así que armarlo en la pieza pública la volvería un segundo
/// intérprete: dos códigos leyendo el mismo texto con dos criterios que se separan el día que uno
/// de los dos cambia. Por eso viaja en <see cref="FigureInterpretation"/> y no se recalcula.
///
/// NO ES EL TEXTO. `FigureInterpretation` sigue sin devolver el texto original —quien lo tiene ya
/// lo tiene—: devuelve **su forma**, con los valores ya convertidos a la representación con la que
/// se muestran. No se puede reconstruir el texto original a partir de esto, y es deliberado.
///
/// LA POSICIÓN SÓLO LA LLEVAN LAS FIGURAS DEL CONJUNTO RAÍZ, y es lo que hace que este árbol
/// pueda sincronizarse con la escena por el mismo índice (`F-13`), sin traducir identidades.
/// </remarks>
public sealed class TextNode
{
    private TextNode(
        string? name,
        TextNodeKind kind,
        string? value,
        int? position,
        IReadOnlyList<TextNode> children)
    {
        Name = name;
        Kind = kind;
        Value = value;
        Position = position;
        Children = children;
    }

    /// <summary>La clave que el alumno escribió. **Nulo** en los elementos de una lista.</summary>
    public string? Name { get; }

    /// <summary>Qué clase de nodo es, del conjunto cerrado.</summary>
    public TextNodeKind Kind { get; }

    /// <summary>El valor, ya representado como texto. Nulo en objetos y listas.</summary>
    public string? Value { get; }

    /// <summary>
    /// Posición en el conjunto raíz, **sólo en las figuras del conjunto raíz**. Nulo en el resto.
    /// </summary>
    public int? Position { get; }

    /// <summary>Los hijos, en el orden en que el alumno los escribió. **No se reordenan.**</summary>
    public IReadOnlyList<TextNode> Children { get; }

    /// <summary>Nodo con hijos: un objeto o una lista.</summary>
    public static TextNode Branch(string? name, TextNodeKind kind, IReadOnlyList<TextNode> children, int? position = null) =>
        new(name, kind, null, position, children);

    /// <summary>Nodo sin hijos: un valor.</summary>
    public static TextNode Leaf(string? name, TextNodeKind kind, string? value) =>
        new(name, kind, value, null, []);
}

/// <summary>
/// Las clases de nodo, y son **seis**.
/// </summary>
/// <remarks>
/// SE DISTINGUE EL NÚMERO DEL TEXTO porque se muestran distinto —la maqueta les da color propio— y
/// porque para este producto la diferencia importa: un `"Largo": "3.00"` entre comillas **es un
/// texto**, no un número, y ver eso en el árbol es muchas veces la explicación de por qué la
/// figura no se reconstruyó.
/// </remarks>
public enum TextNodeKind
{
    /// <summary>Un objeto con claves.</summary>
    Object,

    /// <summary>Una lista de elementos.</summary>
    Array,

    /// <summary>Una cadena entre comillas.</summary>
    Text,

    /// <summary>Un número.</summary>
    Number,

    /// <summary>Verdadero o falso.</summary>
    Boolean,

    /// <summary>Nulo declarado.</summary>
    Empty,
}
