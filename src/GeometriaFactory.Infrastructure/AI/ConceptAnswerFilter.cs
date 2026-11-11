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

    // PRIMERA PERSONA DEL ASISTENTE (H1, act. 003): no sólo la de acción («te lo armo») sino la que habla de sí
    // («no puedo generar», «como asistente», «lo siento»). La ficha habla del concepto, nunca de quien responde.
    private static readonly Regex FirstPerson = new(
        @"\b(te (lo |la )?(armo|hago|corrijo|calculo|paso|llevo|muestro|doy|digo|ayudo)|voy a|armar[eé]|pondr[eé]|calcular[eé]"
        + @"|(no )?(puedo|podr[eé]|podr[ií]a|quiero|debo|tengo permitido)|yo s[eé]|no s[eé] (c[oó]mo|qu[eé]|si|eso|la respuesta)|lo siento|disculp[aá]|perd[oó]n"
        + @"|como (asistente|modelo|ia|inteligencia)|mi (funci[oó]n|rol|tarea|objetivo)|me (piden|ped[ií]s|pediste|corresponde)"
        + @"|(yo|m[ií]|conmigo)\b)",
        RegexOptions.CultureInvariant | RegexOptions.IgnoreCase, RegexTimeout);

    // PROCEDIMIENTO OPERATIVO (H1, act. 003): el modelo dictaba los pasos («primero tomá la arista, después…»,
    // «tenés que poner…») sin decir un número ni una palabra de la lista de operaciones. La ficha explica qué es,
    // no qué hacer en el campo.
    private static readonly Regex Procedure = new(
        @"\b(paso (uno|dos|tres|[0-9])|primer paso|los pasos|primero[,]? |a continuaci[oó]n|por [uú]ltimo|finalmente"
        + @"|ten[eé]s que|tendr[ií]as que|deber[ií]as|hay que (poner|calcular|escribir|cargar|completar|tomar|elevar|obtener)"
        + @"|(calcul|tom|elev|obten|reemplaz|pon|escrib|agreg|complet|carg|aplic|multiplic|sum|us|med)[áéí]\b"
        + @"|(calcul|elev|obten|multiplic|sum|reemplaz)(ar|as|alo|ala|ando)\b"
        + @"|en el campo|en (el|la) (area|[aá]rea|volumen|per[ií]metro)\b (pon|va|escrib|carg))",
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

        if (FirstPerson.IsMatch(text!))
        {
            return "primera-persona";
        }

        return Procedure.IsMatch(text!) ? "procedimiento" : null;
    }
}
