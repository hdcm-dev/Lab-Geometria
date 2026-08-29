using GeometriaFactory.Domain.Guards;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Avanzado.Recorrido;

/// <summary>
/// Lleva la cuenta de las condiciones provocadas y de las excepciones, que es lo que produce la
/// última línea del snapshot: `Condiciones provocadas: 12 | Devueltas por valor: 12 | Excepciones
/// de negocio: 0`.
/// </summary>
/// <remarks>
/// **ESE RECUENTO ES LO QUE CONVIERTE AL SAMPLE EN ARNÉS.** `ADR-02002` reserva las excepciones a
/// los defectos de programación del consumidor, y acá se demuestra **contándolas**: cada condición
/// se provoca a propósito y se verifica que **vuelva por valor**. Si el dominio lanzara, el número
/// dejaría de ser cero y el contrato fallaría por su propio `stdout_no_contiene`.
///
/// El recuento **no cubre las 42 condiciones del catálogo** —eso es alcance de `TC-02023` y de la
/// batería—: cubre las doce que este recorrido provoca.
/// </remarks>
internal sealed class Bitacora
{
    private readonly List<string> _lineas = [];

    internal int Provocadas { get; private set; }

    internal int PorValor { get; private set; }

    internal int Excepciones { get; private set; }

    internal IReadOnlyList<string> Lineas => _lineas;

    internal void Escribir(string linea)
    {
        _lineas.Add(linea);
        Console.WriteLine(linea);
    }

    /// <summary>
    /// Invoca una operación y, **si devolvió una condición**, la cuenta. Una operación que
    /// procede no es una condición provocada: es el camino feliz, y contarla como condición
    /// inflaría el número que §6 declara.
    /// </summary>
    internal string? Provocar(Func<DomainResult> operacion)
    {
        try
        {
            var resultado = operacion();
            if (resultado.ConditionCode is not null) { Provocadas++; PorValor++; }
            return resultado.ConditionCode;
        }
        catch (Exception)
        {
            Excepciones++;
            throw;
        }
    }

    /// <summary>Ídem, para las operaciones que devuelven el resultado con valor adentro.</summary>
    internal string? Provocar<T>(Func<DomainResult<T>> operacion)
    {
        try
        {
            var resultado = operacion();
            if (resultado.ConditionCode is not null) { Provocadas++; PorValor++; }
            return resultado.ConditionCode;
        }
        catch (Exception)
        {
            Excepciones++;
            throw;
        }
    }

    internal void Cerrar() => Escribir(
        $"[11] Condiciones provocadas: {Provocadas} | Devueltas por valor: {PorValor} "
        + $"| Excepciones de negocio: {Excepciones}");
}

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
}
