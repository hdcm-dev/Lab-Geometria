using GeometriaFactory.Application.Ports;
using GeometriaFactory.Infrastructure.Figures;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// La **lectura** del texto del alumno por el motor de interpretación, en los casos que ni la
/// batería obligatoria ni la de derivaciones recorren: lo que falta, lo que sobra y lo que viene
/// en una forma que el validador no lee.
/// </summary>
/// <remarks>
/// POR QUÉ ESTA BATERÍA (mesa del 2026-09-14, R-12). Cuando `QG-06` tuvo instrumento, el validador
/// dio **94,2 %** de líneas contra el piso de **95 %**. Las once líneas que faltaban son todas de
/// este tipo: una figura sin `Tipo`, un componente que no es objeto, sin `Tipo` o de un tipo que
/// no existe, un número escrito `null`, y los valores que el árbol del texto muestra y la pieza
/// no usa. **Ninguna prueba se escribió para mover el número**: cada una afirma qué hace el
/// producto con un texto que un alumno puede escribir.
///
/// LAS DOS LÍNEAS QUE QUEDAN SIN CUBRIR NO SE ALCANZAN desde `Interpret`: el área y el juego de
/// componentes de tipos que `TryReadPieceType` no devuelve nunca como pieza.
/// </remarks>
public sealed class FigureTextReadingTests
{
    private static FigureInterpretation Interpretar(string texto) =>
        new LocalFigureValidator().Interpret(texto);

    /// <summary>
    /// Una figura o un componente que no se puede tipar **no produce pieza** y queda observado en
    /// su posición sobre el campo `Tipo`, que es el que el alumno tiene que corregir (`RN-02009`).
    /// </summary>
    [Theory]
    [InlineData("""{ "Largo": 4.0, "Ancho": 2.0 }""")]
    [InlineData("""{ "Tipo": 7, "Largo": 4.0 }""")]
    [InlineData("""{ "Tipo": "Cubo", "Caras": [ 42 ] }""")]
    [InlineData("""{ "Tipo": "Cubo", "Caras": [ { "Largo": 2.0, "Ancho": 2.0 } ] }""")]
    [InlineData("""{ "Tipo": "Cubo", "Caras": [ { "Tipo": "Triangulo", "Area": 4.0 } ] }""")]
    public void AFigureOrComponentThatCannotBeTypedIsObservedOnItsTypeField(string texto)
    {
        var interpretacion = Interpretar(texto);

        Assert.Empty(interpretacion.Pieces);
        var observacion = Assert.Single(interpretacion.Observations);
        Assert.Equal(0, observacion.PiecePosition);
        Assert.Equal("Tipo", observacion.Field);
    }

    /// <summary>
    /// Un número escrito `null` se lee **como ausente**, no como ilegible: la pieza se reconstruye y
    /// el árbol muestra el campo vacío, tal como el alumno lo escribió.
    /// </summary>
    [Fact]
    public void ANumberWrittenAsNullIsReadAsAbsent()
    {
        var interpretacion = Interpretar("""{ "Tipo": "Circulo", "Radio": 1.0, "Area": null }""");

        Assert.Single(interpretacion.Pieces);
        Assert.DoesNotContain(interpretacion.Observations, o => o.Field == "Area" && o.DerivedValue is null && o.DeclaredValue is null);

        var area = Assert.Single(interpretacion.Tree!.Children, n => n.Name == "Area");
        Assert.Equal(TextNodeKind.Empty, area.Kind);
        Assert.Null(area.Value);
    }

    /// <summary>
    /// Un valor que la pieza no usa **no la invalida**, y el árbol del texto lo muestra con su
    /// forma: un booleano sigue siendo booleano.
    /// </summary>
    [Fact]
    public void AValueThePieceDoesNotUseStillShowsInTheTreeWithItsKind()
    {
        var interpretacion = Interpretar("""{ "Tipo": "Circulo", "Radio": 1.0, "Relleno": true }""");

        Assert.Single(interpretacion.Pieces);

        var relleno = Assert.Single(interpretacion.Tree!.Children, n => n.Name == "Relleno");
        Assert.Equal(TextNodeKind.Boolean, relleno.Kind);
        Assert.Equal("true", relleno.Value);
    }
}
