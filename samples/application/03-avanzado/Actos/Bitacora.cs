using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Application.Avanzado.Actos;

/// <summary>Recoge la salida del recorrido.</summary>
internal sealed class Bitacora
{
    private readonly List<string> _lineas = [];

    internal IReadOnlyList<string> Lineas => _lineas;

    internal void Escribir(string linea)
    {
        _lineas.Add(linea);
        Console.WriteLine(linea);
    }
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

    internal static string Situacion(AccountStatus s) => s switch
    {
        AccountStatus.Pending => "Pendiente",
        AccountStatus.Enabled => "Habilitada",
        AccountStatus.Blocked => "Bloqueada",
        _ => s.ToString(),
    };
}
