using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.AI;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// El filtro posterior (REQ-IA-05, REQ-IA-06) y las plantillas (decisión D-2 del expediente 0002).
/// </summary>
public sealed class HintFilterTests
{
    /// <summary>
    /// Los ocho casos de <c>autoprueba()</c> de <c>ai-layer/ensayos/pistas.py</c>, con los mismos
    /// motivos: es la paridad del port (8/8 en Python con <c>python3 -I pistas.py --autoprueba</c>).
    /// </summary>
    [Theory]
    [InlineData("Mira el area.", "Un cubo tiene seis caras.", "Conta las caras.", null)]
    [InlineData("El area deberia ser 54.", "x", "y", "valor-derivado")]
    [InlineData("Da 54,00.", "x", "y", "valor-derivado")]
    [InlineData("Da 54.004.", "x", "y", "valor-derivado")]
    [InlineData("Usa {\"Tipo\": \"Cubo\"}", "x", "y", "json-de-figura")]
    [InlineData("Escribi return 6 * l * l;", "x", "y", "codigo")]
    [InlineData("Declaraste 36.00, revisa.", "x", "y", null)]
    [InlineData("", "x", "y", "forma")]
    public void HintFilter_MatchesTheReferenceSelfTest(string level1, string level2, string level3, string? expected) =>
        Assert.Equal(expected, HintFilter.Check(level1, level2, level3, 54.0));

    [Theory]
    [InlineData("Te tiene que dar 54")]
    [InlineData("Te tiene que dar 54.0")]
    [InlineData("Te tiene que dar 54,00")]
    [InlineData("Te tiene que dar 54.004")]
    [InlineData("Te tiene que dar ５４")]
    [InlineData("Te tiene que dar ٥٤")]
    public void HintFilter_RejectsDerivedValue(string level1) =>
        Assert.Equal("valor-derivado", HintFilter.Check(level1, "x", "y", 54.0));

    [Fact]
    public void HintFilter_LetsTheDeclaredValueThrough() =>
        Assert.Null(HintFilter.Check("Tu programa declara 36.00.", "x", "y", 54.0));

    [Theory]
    [InlineData("Mira {esto}", "json-de-figura")]
    [InlineData("Usa \"Area\": algo", "json-de-figura")]
    [InlineData("Escribi class Cubo", "codigo")]
    [InlineData("y despues return", "codigo")]
    [InlineData("usa x => x", "codigo")]
    [InlineData("public double Area", "codigo")]
    [InlineData("usa Math.Pow", "codigo")]
    [InlineData("termina asi;\nsigue", "codigo")]
    public void HintFilter_RejectsFigureJsonAndCode(string level1, string expected) =>
        Assert.Equal(expected, HintFilter.Check(level1, "x", "y", 54.0));

    [Theory]
    [InlineData("<script>alert(1)</script>", "marcado")]
    [InlineData("mira https://ejemplo", "marcado")]
    [InlineData("un\u0007control", "control")]
    public void HintFilter_RejectsMarkupLinksAndControlCharacters(string level1, string expected) =>
        Assert.Equal(expected, HintFilter.Check(level1, "x", "y", 54.0));

    [Fact]
    public void HintFilter_RejectsALevelLongerThanTheCap() =>
        Assert.Equal("longitud", HintFilter.Check(new string('a', HintFilter.MaxLevelLength + 1), "x", "y", 54.0));

    [Fact]
    public void TheTemplatesCoverTheSixPieceTypesTimesTheTwoFields()
    {
        FigureType[] pieceTypes =
        [
            FigureType.Cylinder, FigureType.Cube, FigureType.Orthohedron,
            FigureType.Rectangle, FigureType.Square, FigureType.Circle,
        ];

        var expected = pieceTypes.SelectMany(type => new[] { (type, "Area"), (type, "Volumen") }).ToHashSet();

        Assert.Equal(12, TemplateHints.Pairs.Count());
        Assert.True(expected.SetEquals(TemplateHints.Pairs));
    }

    public static TheoryData<FigureType, string, double> EveryPairWithSomeDerivedValues()
    {
        var data = new TheoryData<FigureType, string, double>();

        foreach (var (type, field) in TemplateHints.Pairs)
        {
            foreach (var derived in new[] { 0.0, 1.0, 3.0, 6.0, 54.0, 28.27 })
            {
                data.Add(type, field, derived);
            }
        }

        return data;
    }

    [Theory]
    [MemberData(nameof(EveryPairWithSomeDerivedValues))]
    public void EveryTemplatePassesTheFilter(FigureType type, string field, double derived)
    {
        var hints = TemplateHints.For(type, field);

        Assert.Equal(Application.Ports.HintOrigin.Template, hints.Origin);
        Assert.Null(HintFilter.Check(hints.Level1, hints.Level2, hints.Level3, derived));
    }

    [Fact]
    public void APairOutsideTheTwelveStillGetsATemplate()
    {
        var hints = TemplateHints.For(FigureType.DevelopedRectangle, "Area");

        Assert.Null(HintFilter.Check(hints.Level1, hints.Level2, hints.Level3, 54.0));
    }
}
