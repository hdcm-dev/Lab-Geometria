using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.Figures;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// La batería obligatoria del producto: **diez casos**, con los escenarios `E-1` a `E-8` del intake
/// §20 como fixtures.
/// </summary>
/// <remarks>
/// ES LA MITIGACIÓN DECLARADA DEL RIESGO `RN-B3`, que el intake §11 califica de probabilidad alta e
/// impacto alto: «el defecto que más veces se repite es escribir el validador sin leer el
/// análisis», y su consecuencia es que «la aplicación no sirve para el dato que existe».
///
/// NINGÚN DATO DE PRUEBA SE INVENTÓ. Los ocho textos están transcriptos **carácter por carácter**
/// del `PRODUCT-INTAKE` §20, con sus comas finales, sus claves sinónimas y sus valores
/// incorrectos incluidos. Es la regla de delivery 5 de §15 del intake, y es lo que hace que esta
/// batería mida el dato real y no una idealización suya.
///
/// POR QUÉ VIVE EN ESTE PROYECTO DE PRUEBAS Y NO EN UNO PROPIO. `CU-06001` §3 exige poder ejercerla
/// **sin motor de persistencia**, y así se ejerce: no levanta ningún host, no abre ninguna base y
/// no toca la red —el adaptador no sabe hacerlo—. La solución tiene tres proyectos de prueba desde
/// la etapa `a` y ninguno es de `GeometriaFactory-Infrastructure`; agregar un cuarto es una
/// decisión de estructura que esta etapa no toma por su cuenta. **[decisión de la etapa `f`,
/// declarada y elevada al punto de control.]**
/// </remarks>
public sealed class FigureValidatorBatteryTests
{
    private readonly LocalFigureValidator _validator = new();

    // ── Caso 1 (T1, clave `Tapas`) y caso 2 (T2, comas finales), sobre E-2 ──────────────────────

    [Fact]
    public void E2IsReadDespiteTrailingCommasAndReadsBasesFromTapas()
    {
        var interpretation = _validator.Interpret(Scenarios.E2);

        // CA-01: se lee pese a las dos comas finales, y las bases salen de `Tapas`.
        Assert.Equal(1, interpretation.RootFigureCount);
        var piece = Assert.Single(interpretation.Pieces);
        Assert.Equal(FigureType.Orthohedron, piece.Type);
        Assert.Equal(2, piece.Components.Count(c => c.Role == ComponentRole.Base));
        Assert.Equal(4, piece.Components.Count(c => c.Role == ComponentRole.Lateral));
        Assert.DoesNotContain(interpretation.Observations, o => o.Kind == ObservationKind.ValidationError);
    }

    // ── Caso 3 (T3 por Ejemplo1) y caso 5 (área del cubo), sobre E-3 ────────────────────────────

    [Fact]
    public void E3ReadsSquareFacesAndWarnsOnlyAboutTheArea()
    {
        var interpretation = _validator.Interpret(Scenarios.E3);

        var piece = Assert.Single(interpretation.Pieces);
        Assert.Equal(6, piece.Components.Count);
        Assert.All(piece.Components, c => Assert.Equal(FigureType.Square, c.Type));

        // CA-04 de CU-06002: una advertencia de área, con LOS DOS VALORES, y ninguna de volumen.
        var warning = Assert.Single(interpretation.Observations);
        Assert.Equal(ObservationKind.Warning, warning.Kind);
        Assert.Equal("Area", warning.Field);
        Assert.Equal(0, warning.PiecePosition);
        Assert.Equal(36.00, warning.DeclaredValue);
        Assert.Equal(54.00, warning.DerivedValue!.Value, 2);
    }

    // ── Caso 4 (T3 por Ejemplo2, y el criterio negativo), sobre E-4 ─────────────────────────────

    [Fact]
    public void E4ReadsRectangleFacesAndProducesNoObservationAtAll()
    {
        var interpretation = _validator.Interpret(Scenarios.E4);

        var piece = Assert.Single(interpretation.Pieces);
        Assert.All(piece.Components, c => Assert.Equal(FigureType.Rectangle, c.Type));

        // Es el caso que un validador que advirtiera siempre haría fallar.
        Assert.Empty(interpretation.Observations);
    }

    // ── Caso 6 (volumen del ortoedro), sobre E-2 ────────────────────────────────────────────────

    [Fact]
    public void E2WarnsAboutTheVolumeAndNotAboutTheArea()
    {
        var interpretation = _validator.Interpret(Scenarios.E2);

        var warning = Assert.Single(interpretation.Observations);
        Assert.Equal(ObservationKind.Warning, warning.Kind);
        Assert.Equal("Volumen", warning.Field);
        Assert.Equal(343.00, warning.DeclaredValue);
        Assert.Equal(1029.00, warning.DerivedValue!.Value, 2);
    }

    // ── Caso 7 (dimensión en 0), sobre E-6 ──────────────────────────────────────────────────────

    [Fact]
    public void E6InterpretsTheFigureWithALengthOfZeroAndDoesNotDiscardIt()
    {
        var interpretation = _validator.Interpret(Scenarios.E6);

        // Existencia contra veracidad: el `0.00` está, y la figura no se descarta.
        Assert.Equal(1, interpretation.RootFigureCount);
        var piece = Assert.Single(interpretation.Pieces);
        Assert.Equal(FigureType.Rectangle, piece.Type);
        // El área derivada se calcula CON el cero, en lugar de descartar la figura por tenerlo.
        Assert.Equal(0.00, piece.DerivedArea);
        Assert.Equal(0.00, piece.DeclaredArea);
        Assert.DoesNotContain(interpretation.Observations, o => o.Kind == ObservationKind.ValidationError);
    }

    // ── Caso 8 (tipo desconocido), sobre E-5 ────────────────────────────────────────────────────

    [Fact]
    public void E5ReportsTheUnknownTypeAtPositionOneAndKeepsTheValidPiece()
    {
        var interpretation = _validator.Interpret(Scenarios.E5);

        // CA-04: el índice reportado es 1 y NO 0, que es lo que verifica que se calcula.
        Assert.Equal(2, interpretation.RootFigureCount);
        var piece = Assert.Single(interpretation.Pieces);
        Assert.Equal(0, piece.Position);

        var error = Assert.Single(interpretation.Observations, o => o.Kind == ObservationKind.ValidationError);
        Assert.Equal(1, error.PiecePosition);
        Assert.Equal("Tipo", error.Field);
    }

    // ── Caso 9 (JSON semilla completo), sobre E-1 ───────────────────────────────────────────────

    [Fact]
    public void E1ProducesThreePiecesAndExactlyTwoWarnings()
    {
        var interpretation = _validator.Interpret(Scenarios.E1);

        Assert.Equal(3, interpretation.RootFigureCount);
        Assert.Equal(3, interpretation.Pieces.Count);
        Assert.Equal([0, 1, 2], interpretation.Pieces.Select(p => p.Position));

        // El cilindro se reconstruye con 2 tapas `Circulo` y 1 `Lado` `RectanguloDesarrollado`.
        var cylinder = interpretation.Pieces[0];
        Assert.Equal(FigureType.Cylinder, cylinder.Type);
        Assert.Equal(2, cylinder.Components.Count(c => c.Role == ComponentRole.Cap && c.Type == FigureType.Circle));
        Assert.Single(cylinder.Components, c => c.Role == ComponentRole.Side && c.Type == FigureType.DevelopedRectangle);

        // El resultado canónico del producto: 3 piezas y 2 advertencias. NI UNA MÁS.
        Assert.Equal(2, interpretation.Observations.Count);
        Assert.All(interpretation.Observations, o => Assert.Equal(ObservationKind.Warning, o.Kind));

        var cubeArea = interpretation.Observations.Single(o => o.PiecePosition == 1);
        Assert.Equal("Area", cubeArea.Field);
        Assert.Equal(36.00, cubeArea.DeclaredValue);
        Assert.Equal(54.00, cubeArea.DerivedValue!.Value, 2);

        var orthohedronVolume = interpretation.Observations.Single(o => o.PiecePosition == 2);
        Assert.Equal("Volumen", orthohedronVolume.Field);
        Assert.Equal(343.00, orthohedronVolume.DeclaredValue);
        Assert.Equal(1029.00, orthohedronVolume.DerivedValue!.Value, 2);
    }

    [Fact]
    public void E1DoesNotWarnAboutTheCylinderBecauseTheDifferenceIsExactlyTheTolerance()
    {
        var interpretation = _validator.Interpret(Scenarios.E1);

        // CA-02 de CU-06002: 113.10 declarada contra 113.09 derivada. Con «mayor o igual» este
        // escenario daría 3 advertencias y el caso de prueba canónico fallaría.
        Assert.DoesNotContain(interpretation.Observations, o => o.PiecePosition == 0);
    }

    // ── Caso 10 (dimensión no legible), sobre E-8 ───────────────────────────────────────────────

    [Fact]
    public void E8ReportsTheUnreadableDimensionAtPositionOneAndKeepsTheOrthohedron()
    {
        var interpretation = _validator.Interpret(Scenarios.E8);

        // CA-12: el texto ES JSON válido; lo que falla es la lectura de un valor.
        Assert.Equal(2, interpretation.RootFigureCount);
        var piece = Assert.Single(interpretation.Pieces);
        Assert.Equal(0, piece.Position);
        Assert.Equal(FigureType.Orthohedron, piece.Type);

        var error = Assert.Single(interpretation.Observations, o => o.Kind == ObservationKind.ValidationError);
        Assert.Equal(1, error.PiecePosition);
        Assert.Equal("Largo", error.Field);
    }

    // ── Cobertura adicional: E-7, los seis tipos ────────────────────────────────────────────────

    [Fact]
    public void E7ReconstructsTheSixDrawableTypesWithThePlainOnesAsRootPieces()
    {
        var interpretation = _validator.Interpret(Scenarios.E7);

        Assert.Equal(6, interpretation.RootFigureCount);
        Assert.Equal(6, interpretation.Pieces.Count);
        Assert.Equal(
            [
                FigureType.Cylinder, FigureType.Cube, FigureType.Orthohedron,
                FigureType.Rectangle, FigureType.Square, FigureType.Circle
            ],
            interpretation.Pieces.Select(p => p.Type));
        Assert.DoesNotContain(interpretation.Observations, o => o.Kind == ObservationKind.ValidationError);
    }

    // ── Las garantías del contrato ──────────────────────────────────────────────────────────────

    [Fact]
    public void AnUnreadableTextIsAResultAndNotABreakdown()
    {
        // CA-10, y la garantía G-7, que es la que más veces se rompe al implementar.
        var interpretation = _validator.Interpret("esto no es json { ni con tolerancia");

        Assert.Equal(0, interpretation.RootFigureCount);
        Assert.Empty(interpretation.Pieces);
        var error = Assert.Single(interpretation.Observations);
        Assert.Equal(ObservationKind.ValidationError, error.Kind);
        Assert.Null(error.PiecePosition);
    }

    [Fact]
    public void AnEmptyRootSetProducesAnObservationWithoutPiecePosition()
    {
        // FA-03: el conjunto vacío NO se confunde con la ausencia de texto.
        var interpretation = _validator.Interpret("[]");

        Assert.Equal(0, interpretation.RootFigureCount);
        Assert.Empty(interpretation.Pieces);
        Assert.Null(Assert.Single(interpretation.Observations).PiecePosition);
    }

    [Fact]
    public void TheOriginalTextIsNeverModified()
    {
        // CA-09 de CU-06001, la garantía G-1: idénticos carácter por carácter.
        var original = Scenarios.E2;

        _validator.Interpret(original);

        Assert.Equal(Scenarios.E2, original);
    }

    [Theory]
    [InlineData(6.01, false)]
    [InlineData(6.011, true)]
    public void TheStrictOperatorAnchorsTheToleranceInATestAndNotOnlyInProse(
        double declaredArea,
        bool expectsWarning)
    {
        // CA-09 de CU-06002: 0.010 NO advierte y 0.011 SÍ. Un rectángulo de 2 × 3 deriva 6.00.
        var text = $$"""[ { "Tipo": "Rectangulo", "Largo": 2.00, "Ancho": 3.00, "Area": {{declaredArea.ToString(System.Globalization.CultureInfo.InvariantCulture)}} } ]""";

        var interpretation = _validator.Interpret(text);

        Assert.Equal(expectsWarning, interpretation.Observations.Any(o => o.Field == "Area"));
    }
}
