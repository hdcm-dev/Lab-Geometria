using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Application.Basico.Actos;

/// <summary>Lleva la cuenta de los actos, los rechazos tipados y las excepciones.</summary>
internal sealed class Bitacora
{
    private readonly List<string> _lineas = [];

    internal int Actos { get; private set; }

    internal int Rechazos { get; private set; }

    internal int Excepciones { get; private set; }

    internal IReadOnlyList<string> Lineas => _lineas;

    internal void Acto() => Actos++;

    internal void Escribir(string linea)
    {
        _lineas.Add(linea);
        Console.WriteLine(linea);
    }

    internal void Rechazo(string linea)
    {
        Rechazos++;
        Escribir(linea);
    }

    internal async Task<T> InvocarAsync<T>(Func<Task<T>> operacion)
    {
        try
        {
            return await operacion().ConfigureAwait(false);
        }
        catch (Exception)
        {
            Excepciones++;
            throw;
        }
    }

    internal void Cerrar() => Escribir(
        $"Actos recorridos: {Actos} | Rechazos tipados: {Rechazos} | Excepciones: {Excepciones}");
}

internal static class Vocabulario
{
    /// <summary>La SITUACIÓN de la cuenta, en femenino, que es como la nombra este documento.</summary>
    internal static string Situacion(AccountStatus estado) => estado switch
    {
        AccountStatus.Pending => "Pendiente",
        AccountStatus.Enabled => "Habilitada",
        AccountStatus.Blocked => "Bloqueada",
        _ => estado.ToString(),
    };

    internal static string De(Role papel) => papel switch
    {
        Role.Administrator => "Administrador",
        Role.Student => "Alumno",
        _ => papel.ToString(),
    };
}
