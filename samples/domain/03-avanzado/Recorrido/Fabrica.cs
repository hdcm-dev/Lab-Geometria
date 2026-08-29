using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Avanzado.Recorrido;

/// <summary>Arma los trabajos del recorrido. El momento lo aporta el consumidor (`ADR-02006`).</summary>
internal static class Fabrica
{
    internal static readonly DateTimeOffset Momento = new(2026, 8, 29, 0, 0, 0, TimeSpan.Zero);

    internal static Work Trabajo(Guid dueño, string nombre, WorkStatus estado)
    {
        var w = Work.Create(dueño, nombre, "2026-08-29", null, "[]", true, Momento).Value!;
        if (estado == WorkStatus.Draft) return w;

        w.Submit(parseResultDeclared: true, validationErrorsDeclared: false, updatedAt: Momento);
        if (estado == WorkStatus.Submitted) return w;

        w.ApplyOutcome(Role.Administrator,
            estado == WorkStatus.Approved ? WorkOutcome.Approve : WorkOutcome.Reject,
            comment: null, updatedAt: Momento);
        return w;
    }
}
