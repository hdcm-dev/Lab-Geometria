using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Intermedio.Recorrido;

/// <summary>
/// Lleva la cuenta del recorrido y produce la última línea del snapshot de §6:
/// `Trabajos recorridos: 6 | Envios a Pendiente: 4 | Envios retenidos en Borrador: 2 | Excepciones: 0`.
/// </summary>
internal sealed class Bitacora
{
    private readonly List<string> _lineas = [];

    internal int Trabajos { get; private set; }

    internal int Enviados { get; private set; }

    internal int Retenidos { get; private set; }

    internal int Excepciones { get; private set; }

    internal IReadOnlyList<string> Lineas => _lineas;

    internal void Escribir(string linea)
    {
        _lineas.Add(linea);
        Console.WriteLine(linea);
    }

    internal void ContarTrabajo() => Trabajos++;

    internal void ContarDesenlace(WorkStatus estado)
    {
        if (estado == WorkStatus.Submitted) Enviados++;
        else Retenidos++;
    }

    internal T Invocar<T>(Func<T> operacion)
    {
        try
        {
            return operacion();
        }
        catch (Exception)
        {
            Excepciones++;
            throw;
        }
    }

    internal void Cerrar() => Escribir(
        $"Trabajos recorridos: {Trabajos} | Envios a Pendiente: {Enviados} "
        + $"| Envios retenidos en Borrador: {Retenidos} | Excepciones: {Excepciones}");
}

/// <summary>El vocabulario del snapshot. Vive en el sample: el dominio no habla castellano.</summary>
internal static class Vocabulario
{
    internal static string De(WorkStatus estado) => estado switch
    {
        WorkStatus.Draft => "Borrador",
        WorkStatus.Submitted => "Pendiente",
        WorkStatus.Approved => "Finalizado",
        WorkStatus.Rejected => "Rechazado",
        _ => estado.ToString(),
    };

    internal static string De(ObservationKind especie) => especie switch
    {
        ObservationKind.Warning => "Advertencia",
        ObservationKind.ValidationError => "Error",
        _ => especie.ToString(),
    };
}
