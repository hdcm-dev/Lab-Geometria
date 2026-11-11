namespace GeometriaFactory.Infrastructure.Persistence;

/// <summary>
/// La fila de la huella del asistente (tabla <c>HuellaDeConsulta</c>, REQ-IA-30). Sin texto libre ni persona:
/// los campos de texto son valores de conjuntos cerrados del enrutador o la versión del corpus.
/// </summary>
public sealed class ConceptTraceRow
{
    public long Id { get; set; }

    public int Ring { get; set; }

    public string Intent { get; set; } = string.Empty;

    public int Form { get; set; }

    public string Outcome { get; set; } = string.Empty;

    public string CorpusVersion { get; set; } = string.Empty;

    public DateOnly Day { get; set; }
}
