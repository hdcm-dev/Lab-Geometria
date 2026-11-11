using GeometriaFactory.Application.Ports;

namespace GeometriaFactory.Infrastructure.Persistence;

/// <summary>Guarda la huella del asistente en <c>HuellaDeConsulta</c> (K2 R7: la señal tiene almacenamiento).</summary>
public sealed class EfCoreConceptTraceStore : IConceptTraceStore
{
    private readonly GeometriaFactoryDbContext _db;

    public EfCoreConceptTraceStore(GeometriaFactoryDbContext db)
    {
        ArgumentNullException.ThrowIfNull(db);
        _db = db;
    }

    public async Task RecordAsync(ConceptTrace trace, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(trace);

        _db.ConceptTraces.Add(new ConceptTraceRow
        {
            Ring = (int)trace.Ring,
            Intent = trace.Intent.Length > 48 ? trace.Intent[..48] : trace.Intent,
            Form = (int)trace.Form,
            Outcome = trace.Outcome,
            CorpusVersion = trace.CorpusVersion,
            Day = trace.Day,
        });

        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
