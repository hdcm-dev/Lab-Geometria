using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Basico.Recorrido;

/// <summary>
/// Lleva la cuenta de lo que el recorrido invoca, y es lo que hace verificable la última
/// línea del snapshot de §6: `Operaciones invocadas: 9 | Rechazos tipados: 2 | Excepciones: 0`.
/// </summary>
/// <remarks>
/// EL CONTADOR DE EXCEPCIONES NO ES DECORATIVO. `ADR-02002` reserva las excepciones a los
/// defectos de programación del consumidor, y este sample lo demuestra **contándolas**: cada
/// invocación pasa por acá envuelta, de modo que si el dominio lanzara, el número dejaría de
/// ser cero y el contrato `VER-02001` fallaría por su propio `stdout_no_contiene`.
/// </remarks>
internal sealed class Bitacora
{
    private readonly List<string> _lineas = [];

    internal int Operaciones { get; private set; }

    internal int Rechazos { get; private set; }

    internal int Excepciones { get; private set; }

    internal IReadOnlyList<string> Lineas => _lineas;

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

    internal T Invocar<T>(Func<T> operacion)
    {
        Operaciones++;
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
        $"Operaciones invocadas: {Operaciones} | Rechazos tipados: {Rechazos} "
        + $"| Excepciones: {Excepciones}");
}

/// <summary>
/// Traduce los conjuntos cerrados del dominio al vocabulario del snapshot de §6. Vive acá y
/// no en el dominio: **es del sample**, y el dominio no habla castellano por diseño.
/// </summary>
internal static class Vocabulario
{
    internal static string De(Role papel) => papel switch
    {
        Role.Administrator => "Administrador",
        Role.Student => "Alumno",
        _ => papel.ToString(),
    };

    internal static string De(AccountStatus estado) => estado switch
    {
        AccountStatus.Pending => "Pendiente",
        AccountStatus.Enabled => "Habilitado",
        AccountStatus.Blocked => "Bloqueado",
        _ => estado.ToString(),
    };

    internal static string Credencial(string? hash) =>
        string.IsNullOrWhiteSpace(hash) ? "sin-valor" : "fijada";

    internal static string Marca(bool puesta) => puesta ? "puesta" : "levantada";
}
