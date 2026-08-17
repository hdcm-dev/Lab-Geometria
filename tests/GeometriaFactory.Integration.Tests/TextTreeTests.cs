using GeometriaFactory.Application.Ports;
using GeometriaFactory.Infrastructure.Figures;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// El árbol del texto: lo que el alumno escribió, no lo que la escena logró representar.
/// </summary>
/// <remarks>
/// LA PRUEBA QUE JUSTIFICA QUE EL ÁRBOL EXISTA es la de la figura que falla. Todo lo demás
/// —claves, valores, orden— lo daría igual un árbol armado desde las piezas reconstruidas; lo que
/// **sólo** puede dar un árbol armado del texto es la figura que no produjo pieza. El intake §20 lo
/// declara: «muestra las dos piezas, incluida la que no se dibujó». Sin esa prueba, la próxima
/// persona que optimice el código armando el árbol desde `Pieces` no encontraría nada en rojo.
///
/// SE EJERCITA SOBRE EL VALIDADOR REAL y sobre los escenarios transcritos del intake, que es lo
/// mismo que hace la batería obligatoria de la etapa `f`.
/// </remarks>
public sealed class TextTreeTests
{
    private static TextNode Tree(string text)
    {
        var interpretation = new LocalFigureValidator().Interpret(text);
        Assert.NotNull(interpretation.Tree);
        return interpretation.Tree!;
    }

    // ------------------------------------------------------ la razón de ser del árbol --------

    /// <summary>
    /// `E-5` trae una figura de tipo no reconstruible: **no produce pieza y sí produce nodo**.
    /// </summary>
    [Fact]
    public void TheTreeShowsTheFigureThatProducedNoPiece()
    {
        var interpretation = new LocalFigureValidator().Interpret(Scenarios.E5);
        var tree = interpretation.Tree!;

        // El conjunto raíz del texto tiene más figuras que piezas reconstruidas: ese hueco es
        // justamente lo que el árbol tiene que seguir mostrando.
        Assert.True(interpretation.RootFigureCount > interpretation.Pieces.Count);

        var figures = tree.Kind == TextNodeKind.Array ? tree.Children : [tree];
        Assert.Equal(interpretation.RootFigureCount, figures.Count);

        // Y la posición reservada por la figura fallida está en el árbol, navegable.
        var drawn = interpretation.Pieces.Select(piece => piece.Position).ToHashSet();
        var missing = Enumerable.Range(0, interpretation.RootFigureCount).Where(p => !drawn.Contains(p)).ToList();

        Assert.NotEmpty(missing);
        Assert.All(missing, position => Assert.Contains(figures, node => node.Position == position));
    }

    // ------------------------------------------------------------------ la identidad ---------

    /// <summary>
    /// Las figuras del conjunto raíz llevan su posición, y **nadie más la lleva**: es lo que
    /// permite sincronizar con la escena por el mismo índice, sin traducir (`F-13`).
    /// </summary>
    [Fact]
    public void OnlyRootFiguresCarryThePosition()
    {
        var tree = Tree(Scenarios.E1);
        var figures = tree.Children;

        Assert.Equal(Enumerable.Range(0, figures.Count).ToList(), figures.Select(node => node.Position!.Value).ToList());

        // Ningún descendiente de una figura lleva posición: un componente no tiene representación
        // propia que resaltar, y darle una lo haría competir con su figura por el mismo índice.
        foreach (var figure in figures)
        {
            Assert.All(figure.Children, child => Assert.Null(child.Position));
        }
    }

    /// <summary>
    /// La figura suelta —`E-3`, sin envolver en lista— **es** la raíz y lleva la posición 0.
    /// </summary>
    [Fact]
    public void ALooseFigureIsTheRootAndCarriesPositionZero()
    {
        var tree = Tree(Scenarios.E3);

        Assert.Equal(TextNodeKind.Object, tree.Kind);
        Assert.Equal(0, tree.Position);
    }

    /// <summary>
    /// Dos figuras idénticas son un texto válido y **no se confunden**: lo que las distingue es su
    /// lugar. Es la prueba de que la posición no se deriva del contenido.
    /// </summary>
    [Fact]
    public void TwoIdenticalFiguresKeepTheirOwnPositions()
    {
        const string text = """
            [
              { "Tipo": "Circulo", "Radio": 2.50 },
              { "Tipo": "Circulo", "Radio": 2.50 }
            ]
            """;

        var figures = Tree(text).Children;

        Assert.Equal(2, figures.Count);
        Assert.Equal(0, figures[0].Position);
        Assert.Equal(1, figures[1].Position);
    }

    // ------------------------------------------------------------------ la fidelidad ---------

    /// <summary>
    /// EL NÚMERO SE CONSERVA COMO SE ESCRIBIÓ: `2.50` no se muestra `2.5`. Los ceros que el alumno
    /// escribió son información sobre lo que quiso decir.
    /// </summary>
    [Fact]
    public void NumbersKeepTheFormTheyWereWrittenIn()
    {
        var tree = Tree("""{ "Tipo": "Circulo", "Radio": 2.50 }""");

        var radius = tree.Children.Single(node => node.Name == "Radio");

        Assert.Equal(TextNodeKind.Number, radius.Kind);
        Assert.Equal("2.50", radius.Value);
    }

    /// <summary>
    /// UN NÚMERO ENTRE COMILLAS ES UN TEXTO, y el árbol lo dice. Es muchas veces la explicación de
    /// por qué una figura no se reconstruyó, y por eso las dos clases se distinguen.
    /// </summary>
    [Fact]
    public void AQuotedNumberIsReportedAsText()
    {
        var tree = Tree("""{ "Tipo": "Circulo", "Radio": "2.50" }""");

        var radius = tree.Children.Single(node => node.Name == "Radio");

        Assert.Equal(TextNodeKind.Text, radius.Kind);
        Assert.Equal("2.50", radius.Value);
    }

    /// <summary>Las claves conservan el orden en que fueron escritas. **No se reordenan.**</summary>
    [Fact]
    public void KeysKeepTheOrderTheyWereWrittenIn()
    {
        var tree = Tree("""{ "Volumen": 1, "Tipo": "Cubo", "Area": 2 }""");

        Assert.Equal(["Volumen", "Tipo", "Area"], tree.Children.Select(node => node.Name).ToList());
    }

    // ------------------------------------------------------------------ el texto ilegible ----

    /// <summary>
    /// UN TEXTO QUE NI SIQUIERA ES JSON DEVUELVE ÁRBOL NULO, y no un árbol vacío. La superficie
    /// necesita esa distinción para decir «no se pudo leer» en lugar de dibujar un árbol sin nodos,
    /// que se parece demasiado a un texto sin figuras.
    /// </summary>
    [Fact]
    public void AnUnreadableTextHasNoTreeAtAll()
    {
        var interpretation = new LocalFigureValidator().Interpret("esto no es json");

        Assert.Null(interpretation.Tree);
        Assert.NotEmpty(interpretation.Observations);
    }

    /// <summary>
    /// Y UN TEXTO LEGIBLE PERO VACÍO TAMPOCO TIENE ÁRBOL: el conjunto raíz sin figuras sale por la
    /// misma puerta que el ilegible, porque no hay estructura de figuras que recorrer.
    /// </summary>
    [Fact]
    public void AnEmptyRootSetHasNoTreeEither()
    {
        var interpretation = new LocalFigureValidator().Interpret("[]");

        Assert.Null(interpretation.Tree);
        Assert.NotEmpty(interpretation.Observations);
    }
}
