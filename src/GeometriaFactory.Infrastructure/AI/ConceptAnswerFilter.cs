using System.Text.RegularExpressions;

namespace GeometriaFactory.Infrastructure.AI;

/// <summary>
/// Filtro posterior de la redacción de conceptos (REQ-IA-13, REQ-IA-22, REQ-IA-27). Reusa
/// <see cref="HintFilter"/> para forma, control, longitud, JSON, código y marcado, y suma lo propio del
/// anillo 2: **ningún** número, ninguna operación dicha con palabras de la lista cerrada, ninguna ruta y
/// ninguna primera persona de acción.
/// </summary>
/// <remarks>
/// EL FILTRO NO VE LA OPERACIÓN DICHA CON OTRAS PALABRAS NI EL TÉRMINO CORROMPIDO (LLM09): eso lo juzga la
/// lectura docente de la batería. Por eso la redacción con modelo nace apagada y la ficha es el respaldo.
/// </remarks>
public static class ConceptAnswerFilter
{
    private static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(250);

    private static readonly Regex AnyDigit = new(@"\d", RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex Operation = new(
        @"\b(sum[aá]|sumando|multiplic|divid|rest[aá]|al cuadrado|al cubo|por (dos|tres|cuatro|seis)|(seis|cinco|cuatro|tres|dos) caras|f[oó]rmula|π|\bpi\b)",
        RegexOptions.CultureInvariant | RegexOptions.IgnoreCase, RegexTimeout);

    private static readonly Regex Route = new(@"(^|\s)/[a-z][\w-]*", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase, RegexTimeout);

    private static readonly Regex FirstPerson = new(
        @"\b(te (lo |la )?(armo|hago|corrijo|calculo|paso|llevo|muestro)|voy a|armar[eé]|pondr[eé]|calcular[eé])\b",
        RegexOptions.CultureInvariant | RegexOptions.IgnoreCase, RegexTimeout);

    /// <summary>Devuelve <c>null</c> si el texto pasa, o el motivo del rechazo.</summary>
    public static string? Check(string? text)
    {
        // NaN: el anillo 2 no tiene valor derivado; el chequeo de dígitos de abajo es más estricto.
        if (HintFilter.Check(text, text, text, double.NaN) is { } reason)
        {
            return reason;
        }

        if (AnyDigit.IsMatch(text!))
        {
            return "numero";
        }

        if (Operation.IsMatch(text!))
        {
            return "operacion";
        }

        if (Route.IsMatch(text!))
        {
            return "ruta";
        }

        return FirstPerson.IsMatch(text!) ? "primera-persona" : null;
    }
}
