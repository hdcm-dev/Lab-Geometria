using System.Text.Json;
using GeometriaFactory.Application.Concepts;
using GeometriaFactory.Domain.Values;
using Xunit;
using Xunit.Abstractions;

namespace GeometriaFactory.Integration.Tests.Concepts;

/// <summary>
/// Criterio 27 · K6 §7 y K7 R12.5: recall por rol con el ENRUTADOR REAL y sin modelo, sobre las nueve fichas tal
/// como se servirían el día de la firma y el conjunto de <c>docs-as-code/controles/medicion-conceptos.jsonl</c>.
/// </summary>
/// <remarks>
/// El producto sirve UNA ficha (la primera del enrutador): acá recall@3 y hit@1 coinciden. El control
/// <c>recall_conceptos.py</c> de la compuerta de docs mide lo mismo con el puerto en Python del léxico; si uno de
/// los dos diverge, queda rojo. Las no respondibles tienen que dar «ninguna» en el 100 %: una no respondible
/// que trae ficha es una respuesta inventada.
/// </remarks>
public sealed class ConceptRecallTests(ITestOutputHelper output)
{
    private const double Threshold = 0.9;

    private sealed record Case(string Rol, string Pregunta, string Esperada, string Origen);

    [Theory]
    [InlineData(Role.Student)]
    [InlineData(Role.Administrator)]
    public void Recall_PorRol_SobreElUmbralYNingunaCompleta(Role role)
    {
        var cards = ConceptTestCorpus.Signed().Cards.Where(c => c.Audience.Contains(role)).ToList();
        var roleName = role.ToString();
        var cases = cards.SelectMany(c => c.Questions.Select(q => new Case(roleName, q, c.Id, "frontmatter")))
            .Concat(ReadSet().Where(c => c.Rol == roleName || c.Rol == "ambos"))
            .ToList();

        var answerable = cases.Where(c => c.Esperada != "ninguna").ToList();
        var unanswerable = cases.Where(c => c.Esperada == "ninguna").ToList();
        var misses = new List<string>();

        var hits = answerable.Count(c =>
        {
            var got = ConceptRouter.Route(c.Pregunta, cards).Card?.Id;
            if (got != c.Esperada) misses.Add($"«{c.Pregunta}» esperaba {c.Esperada}, trajo {got ?? "ninguna"} ({c.Origen})");
            return got == c.Esperada;
        });

        var none = unanswerable.Count(c =>
        {
            var got = ConceptRouter.Route(c.Pregunta, cards).Card?.Id;
            if (got is not null) misses.Add($"«{c.Pregunta}» no respondible, trajo {got} ({c.Origen})");
            return got is null;
        });

        output.WriteLine($"{role}: recall@3 = hit@1 {hits}/{answerable.Count}; ninguna {none}/{unanswerable.Count}");
        misses.ForEach(output.WriteLine);

        Assert.NotEmpty(unanswerable);
        Assert.True(hits >= Threshold * answerable.Count, $"{role}: recall {hits}/{answerable.Count}\n{string.Join('\n', misses)}");
        Assert.True(none == unanswerable.Count, $"{role}: ninguna {none}/{unanswerable.Count}\n{string.Join('\n', misses)}");
    }

    /// <summary>K6 §7: el conjunto tiene al menos una no respondible cercana («perímetro», «pirámide»).</summary>
    [Fact]
    public void Medicion_TieneNoRespondiblesCercanas()
    {
        var set = ReadSet();
        Assert.Contains(set, c => c.Esperada == "ninguna" && c.Pregunta.Contains("perímetro", StringComparison.Ordinal));
        Assert.Contains(set, c => c.Esperada == "ninguna" && c.Pregunta.Contains("pirámide", StringComparison.Ordinal));
    }

    private static List<Case> ReadSet()
    {
        var path = Path.Combine(RepoRoot(), "docs-as-code", "controles", "medicion-conceptos.jsonl");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return [.. File.ReadLines(path).Where(l => l.Trim().Length > 0).Select(l => JsonSerializer.Deserialize<Case>(l, options)!)];
    }

    internal static string RepoRoot()
    {
        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
        {
            if (File.Exists(Path.Combine(dir.FullName, "docs-as-code", "controles", "medicion-conceptos.jsonl")))
                return dir.FullName;
        }

        throw new InvalidOperationException("No se encontró docs-as-code/controles/medicion-conceptos.jsonl subiendo desde la prueba.");
    }
}
