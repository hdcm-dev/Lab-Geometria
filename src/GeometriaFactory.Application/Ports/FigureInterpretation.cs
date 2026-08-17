using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Lo que el validador de figuras devuelve: **cuatro cosas**.
/// </summary>
/// <remarks>
/// LA TERCERA ES LA QUE SE OLVIDA (`Definicion-Contrato-Del-Validador-De-Figuras.md` §3).
/// <see cref="RootFigureCount"/> **no es derivable de <see cref="Pieces"/>**, porque el conjunto
/// admite huecos: es el rango de posiciones válidas, y sin él el dominio no tiene contra qué
/// comprobar que la posición de una observación existe (RN-02009).
///
/// Y UNA COSA QUE NO DEVUELVE: **el estado del trabajo**. El contrato entrega el conjunto de
/// observaciones y **el dominio resuelve el estado** con <see cref="Domain.Entities.Work.Submit"/>.
/// Un validador que decidiera el estado tendría dentro una regla de negocio que no le pertenece.
///
/// TAMPOCO DEVUELVE EL TEXTO: quien lo tiene ya lo tiene, y devolverlo abriría la puerta a
/// devolverlo distinto. G-1 se cumple por construcción —este contrato no recibe nada que pueda
/// escribir— y `CU-06001` CA-09 lo verifica igual sobre el texto del llamador.
///
/// LA CUARTA ES <see cref="Tree"/>, agregada en la etapa `g`, y **no es lo mismo que las piezas**:
/// es la forma del texto tal como se escribió, con las figuras que no se pudieron reconstruir
/// incluidas. Viaja acá porque este es el único componente que lee el texto: armarla afuera
/// crearía un segundo intérprete. Ver <see cref="TextNode"/>.
/// </remarks>
public sealed class FigureInterpretation
{
    private FigureInterpretation(
        int rootFigureCount,
        IReadOnlyList<Piece> pieces,
        IReadOnlyList<Observation> observations,
        TextNode? tree)
    {
        RootFigureCount = rootFigureCount;
        Pieces = pieces;
        Observations = observations;
        Tree = tree;
    }

    /// <summary>
    /// Cuántas figuras trae el conjunto raíz, **incluidas las que no se pudieron reconstruir**.
    /// </summary>
    public int RootFigureCount { get; }

    /// <summary>Las piezas reconstruidas, cada una con su posición en el conjunto raíz.</summary>
    public IReadOnlyList<Piece> Pieces { get; }

    /// <summary>Las observaciones emitidas, de las dos especies.</summary>
    public IReadOnlyList<Observation> Observations { get; }

    /// <summary>
    /// La forma del texto, para mostrarlo como árbol. **Nula cuando el texto no se pudo leer.**
    /// </summary>
    /// <remarks>
    /// NULA NO ES VACÍA. Un texto que ni siquiera es JSON no tiene forma que mostrar, y esa
    /// diferencia es la que permite a la superficie decir «no se pudo leer» en vez de dibujar un
    /// árbol vacío, que se parece demasiado a un texto sin figuras.
    /// </remarks>
    public TextNode? Tree { get; }

    /// <summary>Compone un resultado de interpretación.</summary>
    public static FigureInterpretation From(
        int rootFigureCount,
        IReadOnlyList<Piece> pieces,
        IReadOnlyList<Observation> observations,
        TextNode? tree = null) =>
        new(rootFigureCount, pieces, observations, tree);
}
