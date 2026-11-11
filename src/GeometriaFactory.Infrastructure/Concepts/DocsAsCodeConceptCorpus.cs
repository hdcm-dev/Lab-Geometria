using System.Security.Cryptography;
using System.Text;
using GeometriaFactory.Application.Concepts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Infrastructure.Concepts;

/// <summary>
/// Las fichas de `docs-as-code/ayuda/conceptos/`, embebidas al compilar (ARQ-4).
/// </summary>
/// <remarks>
/// EL LECTOR DE FRONTMATTER ES MÍNIMO A PROPÓSITO: «clave: valor» y listas en línea, la misma forma que
/// acepta <c>.docs-guard/dg.py</c>. No se suma un paquete de YAML para nueve archivos que valida la compuerta
/// de docs. Una ficha sin <c>roles</c> no es candidata para nadie (REQ-IA-26).
/// <para>
/// SÓLO LO VIGENTE SE SIRVE (K6 R1). En este corpus nuevo <c>status</c> es obligatorio: una ficha con
/// <c>status: borrador</c>, <c>retirado</c> o sin <c>status</c> no se lee, no se rankea y no se cita, para
/// ningún rol, y la pregunta cae en «No está en la ayuda». Pasa a <c>vigente</c> la ficha que firmó el PO
/// (H-10); las pruebas que necesitan las fichas como si estuvieran firmadas usan una copia sintética
/// (<see cref="ReadEmbeddedFiles"/>), nunca un <c>vigente</c> puesto para que pasen.
/// </para>
/// </remarks>
public sealed class DocsAsCodeConceptCorpus : IConceptCorpus
{
    private const string Prefix = "Conceptos/";

    /// <summary>Valor de <c>status</c> que se sirve.</summary>
    public const string ServedStatus = "vigente";

    public DocsAsCodeConceptCorpus()
        : this(ReadEmbeddedFiles())
    {
    }

    /// <summary>Corpus desde textos dados (las pruebas lo usan con fichas propias).</summary>
    public DocsAsCodeConceptCorpus(IReadOnlyDictionary<string, string> files)
    {
        ArgumentNullException.ThrowIfNull(files);

        var parsed = files.OrderBy(f => f.Key, StringComparer.Ordinal).Select(f => Parse(f.Key, f.Value)).OfType<(ConceptCard Card, string Status)>().ToList();
        Cards = [.. parsed.Where(p => p.Status == ServedStatus).Select(p => p.Card)];
        Unserved = parsed.Count - Cards.Count;

        var joined = string.Join("\n", files.OrderBy(f => f.Key, StringComparer.Ordinal).Select(f => f.Key + "\n" + f.Value));
        Version = "conceptos-" + Convert.ToHexStringLower(SHA256.HashData(Encoding.UTF8.GetBytes(joined)))[..10];
    }

    public string Version { get; }

    /// <summary>Las fichas vigentes: las únicas que el enrutador puede elegir.</summary>
    public IReadOnlyList<ConceptCard> Cards { get; }

    /// <summary>Fichas escritas que no se sirven (borrador, retirado o sin <c>status</c>).</summary>
    public int Unserved { get; }

    /// <summary>Los textos de las fichas embebidas, por nombre de archivo.</summary>
    public static Dictionary<string, string> ReadEmbeddedFiles()
    {
        var assembly = typeof(DocsAsCodeConceptCorpus).Assembly;
        var files = new Dictionary<string, string>(StringComparer.Ordinal);

        foreach (var name in assembly.GetManifestResourceNames().Where(n => n.StartsWith(Prefix, StringComparison.Ordinal)))
        {
            using var stream = assembly.GetManifestResourceStream(name)!;
            using var reader = new StreamReader(stream, Encoding.UTF8);
            files[name[Prefix.Length..]] = reader.ReadToEnd();
        }

        return files;
    }

    private static (ConceptCard Card, string Status)? Parse(string fileName, string text)
    {
        var normalized = text.Replace("\r\n", "\n", StringComparison.Ordinal);

        if (!normalized.StartsWith("---\n", StringComparison.Ordinal))
        {
            return null;
        }

        var end = normalized.IndexOf("\n---\n", 4, StringComparison.Ordinal);

        if (end < 0)
        {
            return null;
        }

        var front = new Dictionary<string, string>(StringComparer.Ordinal);

        foreach (var line in normalized[4..end].Split('\n'))
        {
            var colon = line.IndexOf(':', StringComparison.Ordinal);

            if (colon > 0)
            {
                front[line[..colon].Trim()] = line[(colon + 1)..].Trim().Trim('"');
            }
        }

        var body = string.Join(' ', normalized[(end + 5)..]
            .Split('\n')
            .Where(line => line.Trim().Length > 0 && !line.StartsWith('#'))
            .Select(line => line.Trim()));

        var roles = ListOf(front.GetValueOrDefault("roles"))
            .Select(r => Enum.TryParse<Role>(r, ignoreCase: false, out var role) && Enum.IsDefined(role) ? (Role?)role : null)
            .OfType<Role>()
            .ToList();

        if (body.Length == 0 || !front.TryGetValue("title", out var title))
        {
            return null;
        }

        var card = new ConceptCard(
            Path.GetFileNameWithoutExtension(fileName),
            title,
            body,
            ListOf(front.GetValueOrDefault("sinonimos")),
            roles,
            front.GetValueOrDefault("destino") ?? string.Empty,
            "docs-as-code/ayuda/conceptos/" + fileName,
            ListOf(front.GetValueOrDefault("preguntas")));

        return (card, front.GetValueOrDefault("status") ?? string.Empty);
    }

    private static List<string> ListOf(string? value) =>
        value is null
            ? []
            : [.. value.Trim('[', ']').Split(',').Select(v => v.Trim().Trim('"')).Where(v => v.Length > 0)];
}
