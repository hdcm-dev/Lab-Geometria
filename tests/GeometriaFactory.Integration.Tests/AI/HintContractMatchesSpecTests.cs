using System.Text.RegularExpressions;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.AI;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// Prueba «prompt del código = ficha» (mesa 0004, K-1 y K-15), bloqueante. El prompt de sistema y las doce
/// plantillas son un solo contrato versionado (`ai-layer/01` §5.2): lo que publica la especificación entre las
/// marcas <c>contrato-pistas</c> tiene que ser exactamente lo que corre el código. Si alguien cambia uno sin el
/// otro, esta prueba falla.
/// </summary>
public sealed class HintContractMatchesSpecTests
{
    private static readonly Lazy<string> Spec = new(() => File.ReadAllText(Path.Combine(RepoRoot(), "ai-layer", "01-Especificacion-V1-Pistas.md")));

    private static string RepoRoot()
    {
        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
        {
            if (File.Exists(Path.Combine(dir.FullName, "ai-layer", "01-Especificacion-V1-Pistas.md")))
                return dir.FullName;
        }

        throw new InvalidOperationException("No se encontró ai-layer/01-Especificacion-V1-Pistas.md subiendo desde el directorio de la prueba.");
    }

    private static string Block(string name)
    {
        var start = $"<!-- contrato-pistas:{name}:inicio -->";
        var end = $"<!-- contrato-pistas:{name}:fin -->";
        var text = Spec.Value;
        var i = text.IndexOf(start, StringComparison.Ordinal);
        var j = text.IndexOf(end, StringComparison.Ordinal);
        Assert.True(i >= 0 && j > i, $"Faltan las marcas del bloque «{name}» en ai-layer/01.");
        return text[(i + start.Length)..j];
    }

    [Fact]
    public void ContractVersion_IsTheOnePublished()
    {
        var match = Regex.Match(Spec.Value, @"<!-- contrato-pistas:version (\S+) -->");
        Assert.True(match.Success, "Falta la marca de versión del contrato en ai-layer/01.");
        Assert.Equal(TemplateHints.ContractVersion, match.Groups[1].Value);
    }

    [Fact]
    public void SystemPrompt_IsTheOnePublished()
    {
        var block = Block("prompt").Trim();
        Assert.StartsWith("```text", block, StringComparison.Ordinal);
        Assert.EndsWith("```", block, StringComparison.Ordinal);
        var published = block["```text".Length..^3].Trim();
        Assert.Equal(ModelObservationHintWriter.SystemPrompt, published);
    }

    [Fact]
    public void SystemPrompt_IsTheOneTheBenchHarnessSends()
    {
        var py = File.ReadAllText(Path.Combine(RepoRoot(), "ai-layer", "ensayos", "pistas.py"));
        var assignment = Regex.Match(py, @"PROMPT_SISTEMA = \((.*?)\n\)", RegexOptions.Singleline);
        Assert.True(assignment.Success, "No se encontró PROMPT_SISTEMA en ai-layer/ensayos/pistas.py.");
        var text = string.Concat(Regex.Matches(assignment.Groups[1].Value, "\"([^\"]*)\"").Select(m => m.Groups[1].Value));
        Assert.Equal(ModelObservationHintWriter.SystemPrompt, text);
    }

    [Fact]
    public void Templates_AreTheOnesPublished()
    {
        var labels = new Dictionary<string, FigureType>
        {
            ["Cubo"] = FigureType.Cube,
            ["Ortoedro"] = FigureType.Orthohedron,
            ["Cilindro"] = FigureType.Cylinder,
            ["Rectángulo"] = FigureType.Rectangle,
            ["Cuadrado"] = FigureType.Square,
            ["Círculo"] = FigureType.Circle,
        };

        var published = new Dictionary<(FigureType, string), (string, string, string)>();
        foreach (var line in Block("plantillas").Split('\n').Select(l => l.Trim()).Where(l => l.StartsWith('|')))
        {
            var cells = line.Trim('|').Split('|').Select(c => c.Trim()).ToArray();
            if (cells.Length != 4 || !cells[0].Contains(" · ", StringComparison.Ordinal))
                continue;
            var parts = cells[0].Split(" · ");
            if (!labels.TryGetValue(parts[0], out var figure))
                continue;
            published.Add((figure, parts[1]), (cells[1], cells[2], cells[3]));
        }

        var code = TemplateHints.Pairs.ToHashSet();
        Assert.Equal(12, published.Count);
        Assert.True(code.SetEquals(published.Keys), "Los pares (figura, campo) de la ficha no son los del código.");
        foreach (var ((figure, field), (l1, l2, l3)) in published)
        {
            var hints = TemplateHints.For(figure, field);
            Assert.Equal(l1, hints.Level1);
            Assert.Equal(l2, hints.Level2);
            Assert.Equal(l3, hints.Level3);
        }
    }
}
