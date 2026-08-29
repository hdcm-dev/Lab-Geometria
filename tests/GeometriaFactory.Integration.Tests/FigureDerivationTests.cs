using GeometriaFactory.Application.Ports;
using GeometriaFactory.Infrastructure.Figures;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// Las **derivaciones geométricas** del validador: el área y el volumen que se calculan cuando el
/// texto del alumno no los trae, por cada tipo de figura y por cada camino de derivación.
/// </summary>
/// <remarks>
/// POR QUÉ ESTA BATERÍA. Cierra la escalada `ESC-001` de [`Mesa-2026-08-29.md`], que el Product
/// Owner resolvió con la opción **A**. `LocalFigureValidator` concentraba **202 de las 244 ramas**
/// de `GeometriaFactory-Infrastructure` y estaba en **73,3 %**: el hueco eran los caminos de
/// derivación por tipo —el área del círculo, el lado del cubo tomado de una cara, la altura del
/// cilindro tomada del lateral— que **la batería obligatoria de diez casos no recorre**, porque
/// sus escenarios traen los valores declarados.
///
/// LA DIFERENCIA CON LA BATERÍA OBLIGATORIA, y por eso ésta no la reemplaza: aquélla verifica
/// **los diez casos del intake §20** tal como el alumno los escribe; ésta verifica **qué pasa
/// cuando falta un dato** y el validador tiene que derivarlo o negarse. Son preguntas distintas
/// sobre el mismo código.
///
/// NINGÚN NÚMERO DE ACÁ SE INVENTA: todos salen de la geometría —área de un rectángulo, área de un
/// círculo, volumen de un cilindro— y la tolerancia es la que la propia clase declara.
/// </remarks>
public sealed class FigureDerivationTests
{
    private static FigureInterpretation Interpretar(string texto) =>
        new LocalFigureValidator().Interpret(texto);

    /// <summary>
    /// CA-01 — **Un rectángulo sin área declarada la deriva de largo por ancho**, y no produce
    /// observación: derivar no es advertir.
    /// </summary>
    [Theory]
    [InlineData("Rectangulo", 4.0, 2.5, 10.0)]
    [InlineData("Cuadrado", 3.0, 3.0, 9.0)]
    public void ARectangleWithoutDeclaredAreaDerivesItFromItsSides(
        string tipo, double largo, double ancho, double esperada)
    {
        var texto = $$"""{ "Tipo": "{{tipo}}", "Largo": {{largo}}, "Ancho": {{ancho}} }""";

        var interpretacion = Interpretar(texto);

        Assert.Single(interpretacion.Pieces);
        Assert.Equal(esperada, interpretacion.Pieces[0].DerivedArea!.Value,
            LocalFigureValidator.ComparisonTolerance);
    }

    /// <summary>
    /// CA-02 — **Un círculo deriva su área del radio**, con π y no con una aproximación escrita
    /// a mano.
    /// </summary>
    [Fact]
    public void ACircleDerivesItsAreaFromItsRadius()
    {
        var interpretacion = Interpretar("""{ "Tipo": "Circulo", "Radio": 2.0 }""");

        Assert.Equal(Math.PI * 4.0, interpretacion.Pieces[0].DerivedArea!.Value,
            LocalFigureValidator.ComparisonTolerance);
    }

    /// <summary>
    /// CA-03 — **Sin la dimensión que hace falta, el área NO se deriva y queda sin valor.** Es la
    /// rama que distingue «no lo trajo y lo calculo» de «no lo trajo y no puedo»: inventar un cero
    /// haría que una figura incompleta se viera como una de área nula.
    /// </summary>
    [Theory]
    [InlineData("""{ "Tipo": "Rectangulo", "Largo": 4.0 }""")]
    [InlineData("""{ "Tipo": "Rectangulo", "Ancho": 2.0 }""")]
    [InlineData("""{ "Tipo": "Circulo" }""")]
    public void WithoutTheNeededDimensionTheAreaIsNotDerived(string texto)
    {
        var interpretacion = Interpretar(texto);

        Assert.Single(interpretacion.Pieces);
        Assert.Null(interpretacion.Pieces[0].DerivedArea);
    }

    /// <summary>
    /// CA-04 — **El lado del cubo se toma de una cara cuando la figura no lo declara**, que es el
    /// camino que los escenarios `E-3` y `E-4` no ejercitan porque los dos traen el volumen.
    /// </summary>
    [Fact]
    public void ACubeTakesItsEdgeFromAFaceWhenItDoesNotDeclareIt()
    {
        var texto = """
            {
              "Tipo": "Cubo",
              "Caras": [
                { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00 },
                { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00 },
                { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00 },
                { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00 },
                { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00 },
                { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00 }
              ]
            }
            """;

        var interpretacion = Interpretar(texto);

        Assert.Single(interpretacion.Pieces);
        Assert.Equal(27.0, interpretacion.Pieces[0].DerivedVolume!.Value,
            LocalFigureValidator.ComparisonTolerance);
    }

    /// <summary>
    /// CA-05 — **El cilindro deriva su volumen del radio de la tapa y del largo del lateral.** Es
    /// la derivación con dos componentes distintos, y la nota del código lo declara: la altura es
    /// el `Largo` del rectángulo desarrollado, porque su `Ancho` es `2πr`.
    /// </summary>
    [Fact]
    public void ACylinderTakesItsRadiusFromTheCapAndItsHeightFromTheSide()
    {
        // Las claves son `Tapas` y `Lado`, que es lo que el propio validador declara en su
        // tabla de papeles: `Caras` es del cubo. Escribirlo mal fue el primer intento de esta
        // prueba y el validador NO derivó — se deja anotado porque es la clase de error que
        // una prueba que sólo mira el camino feliz no distingue de un defecto del código.
        var texto = """
            {
              "Tipo": "Cilindro",
              "Tapas": [
                { "Tipo": "Circulo", "Radio": 2.0 },
                { "Tipo": "Circulo", "Radio": 2.0 }
              ],
              "Lado": [
                { "Tipo": "RectanguloDesarrollado", "Largo": 5.0, "Ancho": 12.566 }
              ]
            }
            """;

        var interpretacion = Interpretar(texto);

        Assert.Single(interpretacion.Pieces);
        Assert.Equal(Math.PI * 4.0 * 5.0, interpretacion.Pieces[0].DerivedVolume!.Value, 0.05);
    }

    /// <summary>
    /// CA-06 — **Un texto que no es ni objeto ni arreglo no trae ninguna figura**, y el validador
    /// lo dice en lugar de fallar. Es el camino que `G-7` exige: todo camino termina en un
    /// resultado y ninguno en una negativa.
    /// </summary>
    [Theory]
    [InlineData("42")]
    [InlineData("\"un texto\"")]
    [InlineData("true")]
    [InlineData("null")]
    public void ATextThatIsNeitherObjectNorArrayYieldsNoFigures(string texto)
    {
        var interpretacion = Interpretar(texto);

        Assert.Empty(interpretacion.Pieces);
        Assert.Equal(0, interpretacion.RootFigureCount);
    }

    /// <summary>
    /// CA-07 — **Un elemento del arreglo que no es objeto se rechaza en su posición**, sin arrastrar
    /// a los que sí lo son. Es `RN-02009`: la posición queda reservada y la anterior no se renumera.
    /// </summary>
    [Fact]
    public void ANonObjectInsideTheArrayIsRejectedAtItsPosition()
    {
        var interpretacion = Interpretar("""[ { "Tipo": "Circulo", "Radio": 1.0 }, 42 ]""");

        Assert.Equal(2, interpretacion.RootFigureCount);
        Assert.Single(interpretacion.Pieces);
        Assert.NotEmpty(interpretacion.Observations);
    }

    /// <summary>
    /// CA-08 — **El ortoedro deriva su volumen de la base y de la altura del lateral**, y la
    /// altura es **la dimensión del lateral que NO es un lado de la base**. Es la derivación más
    /// sutil de las cuatro: un lateral de 4×7 sobre una base de 4×5 aporta **7** y no 4, porque el
    /// 4 es el lado que comparte con la base.
    /// </summary>
    [Fact]
    public void AnOrthohedronTakesItsHeightFromTheLateralSideThatIsNotABaseEdge()
    {
        var texto = """
            {
              "Tipo": "Ortoedro",
              "Bases": [ { "Tipo": "Rectangulo", "Largo": 4.0, "Ancho": 5.0 } ],
              "Laterales": [ { "Tipo": "Rectangulo", "Largo": 4.0, "Ancho": 7.0 } ]
            }
            """;

        var interpretacion = Interpretar(texto);

        Assert.Single(interpretacion.Pieces);
        Assert.Equal(4.0 * 5.0 * 7.0, interpretacion.Pieces[0].DerivedVolume!.Value,
            LocalFigureValidator.ComparisonTolerance);
    }

    /// <summary>
    /// CA-09 — **Sin base no hay volumen de ortoedro**, y sin lateral tampoco hay altura que
    /// derivar. Las dos negativas son ramas distintas y las dos se ejercitan.
    /// </summary>
    [Theory]
    [InlineData("""{ "Tipo": "Ortoedro", "Laterales": [ { "Tipo": "Rectangulo", "Largo": 4.0, "Ancho": 7.0 } ] }""")]
    [InlineData("""{ "Tipo": "Ortoedro", "Bases": [ { "Tipo": "Rectangulo", "Largo": 4.0, "Ancho": 5.0 } ] }""")]
    public void AnOrthohedronWithoutItsPartsDerivesNoVolume(string texto)
    {
        Assert.Null(Interpretar(texto).Pieces.SingleOrDefault()?.DerivedVolume);
    }
}
