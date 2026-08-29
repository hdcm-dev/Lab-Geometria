using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Application.Intermedio.Actos;

/// <summary>Lleva la cuenta de los escenarios y sus desenlaces.</summary>
internal sealed class Bitacora
{
    private readonly List<string> _lineas = [];

    internal int Escenarios { get; private set; }

    internal int Enviados { get; private set; }

    internal int Retenidos { get; private set; }

    internal int Excepciones { get; private set; }

    internal IReadOnlyList<string> Lineas => _lineas;

    internal void Escribir(string linea)
    {
        _lineas.Add(linea);
        Console.WriteLine(linea);
    }

    internal void ContarEscenario(WorkStatus desenlace)
    {
        Escenarios++;
        if (desenlace == WorkStatus.Submitted) Enviados++; else Retenidos++;
    }

    internal async Task<T> InvocarAsync<T>(Func<Task<T>> operacion)
    {
        try { return await operacion().ConfigureAwait(false); }
        catch (Exception) { Excepciones++; throw; }
    }

    internal void Cerrar() => Escribir(
        $"Escenarios recorridos: {Escenarios} | Envios a Pendiente: {Enviados} "
        + $"| Retenidos en Borrador: {Retenidos} | Excepciones: {Excepciones}");
}

internal static class Vocabulario
{
    internal static string De(WorkStatus e) => e switch
    {
        WorkStatus.Draft => "Borrador",
        WorkStatus.Submitted => "Pendiente",
        WorkStatus.Approved => "Aprobado",
        WorkStatus.Rejected => "Rechazado",
        _ => e.ToString(),
    };

    internal static string De(ObservationKind k) => k switch
    {
        ObservationKind.Warning => "Advertencia",
        ObservationKind.ValidationError => "Error",
        _ => k.ToString(),
    };
}
