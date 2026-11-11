using GeometriaFactory.Infrastructure.Concepts;

namespace GeometriaFactory.Integration.Tests.Concepts;

/// <summary>
/// Las nueve fichas embebidas con <c>status: vigente</c>, SÓLO DENTRO DE LAS PRUEBAS (K6 §4: «no pongas
/// <c>status: vigente</c> para que una prueba pase; usá una página sintética dentro de la prueba»).
/// </summary>
/// <remarks>
/// Las fichas del repositorio siguen en <c>borrador</c> hasta que el PO las firme (H-10). Esta copia mide
/// el texto exacto que se serviría el día de la firma: cambia sólo la línea de <c>status</c>.
/// </remarks>
internal static class ConceptTestCorpus
{
    public static Dictionary<string, string> SignedFiles() =>
        DocsAsCodeConceptCorpus.ReadEmbeddedFiles().ToDictionary(
            f => f.Key,
            f => f.Value.Replace("\nstatus: borrador\n", "\nstatus: vigente\n", StringComparison.Ordinal));

    public static DocsAsCodeConceptCorpus Signed() => new(SignedFiles());

    /// <summary>Las fichas firmadas más fixtures propias de una prueba (borrador, canario).</summary>
    public static DocsAsCodeConceptCorpus SignedWith(IReadOnlyDictionary<string, string> extra)
    {
        var files = SignedFiles();

        foreach (var (name, text) in extra)
        {
            files[name] = text;
        }

        return new(files);
    }
}
