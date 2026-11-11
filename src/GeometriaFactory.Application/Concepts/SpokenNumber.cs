namespace GeometriaFactory.Application.Concepts;

/// <summary>
/// Números dichos en letras, en castellano, sobre texto ya normalizado (minúsculas, sin tildes, palabras
/// separadas por un espacio). Función pura, sin expresiones regulares ni E/S.
/// </summary>
/// <remarks>
/// <para>
/// UN PARSER Y NO UNA LISTA (verificación de la act. 003, defecto 2): la lista de la act. 003 saltaba de doce a
/// quince y de veinte a treinta, y dejaba afuera «diecisiete», «veintitrés», «treinta y cinco» o «doscientos».
/// Acá se reconoce la gramática: unidades, diez a quince, dieci-, veinti-, decenas «y» unidad, cien/ciento,
/// centenas, «mil», «y medio» y «coma».
/// </para>
/// <para>
/// «un» y «una» NO abren un número: son artículos («una arista»). «uno» sí. «medio» suelto tampoco
/// («pasa por el medio»): sólo cuenta como «y medio» detrás de un número.
/// </para>
/// </remarks>
public static class SpokenNumber
{
    private static readonly Dictionary<string, int> Units = new(StringComparer.Ordinal)
    {
        ["cero"] = 0, ["uno"] = 1, ["dos"] = 2, ["tres"] = 3, ["cuatro"] = 4, ["cinco"] = 5,
        ["seis"] = 6, ["siete"] = 7, ["ocho"] = 8, ["nueve"] = 9,
    };

    private static readonly Dictionary<string, int> Teens = new(StringComparer.Ordinal)
    {
        ["diez"] = 10, ["once"] = 11, ["doce"] = 12, ["trece"] = 13, ["catorce"] = 14, ["quince"] = 15,
        ["dieciseis"] = 16, ["diecisiete"] = 17, ["dieciocho"] = 18, ["diecinueve"] = 19, ["veinte"] = 20,
        ["veintiuno"] = 21, ["veintiun"] = 21, ["veintiuna"] = 21, ["veintidos"] = 22, ["veintitres"] = 23,
        ["veinticuatro"] = 24, ["veinticinco"] = 25, ["veintiseis"] = 26, ["veintisiete"] = 27,
        ["veintiocho"] = 28, ["veintinueve"] = 29,
    };

    private static readonly Dictionary<string, int> Tens = new(StringComparer.Ordinal)
    {
        ["treinta"] = 30, ["cuarenta"] = 40, ["cincuenta"] = 50, ["sesenta"] = 60,
        ["setenta"] = 70, ["ochenta"] = 80, ["noventa"] = 90,
    };

    private static readonly Dictionary<string, int> Hundreds = new(StringComparer.Ordinal)
    {
        ["cien"] = 100, ["ciento"] = 100, ["doscientos"] = 200, ["trescientos"] = 300, ["cuatrocientos"] = 400,
        ["quinientos"] = 500, ["seiscientos"] = 600, ["setecientos"] = 700, ["ochocientos"] = 800,
        ["novecientos"] = 900,
    };

    /// <summary>Un número hallado: tokens [<paramref name="Start"/>, <paramref name="End"/>) y su valor.</summary>
    public readonly record struct Span(int Start, int End, decimal Value);

    /// <summary>Si la palabra sola es un número en letras (sin «un/una»).</summary>
    public static bool IsNumberWord(string word) =>
        Units.ContainsKey(word) || Teens.ContainsKey(word) || Tens.ContainsKey(word)
        || Hundreds.ContainsKey(word) || word == "mil";

    /// <summary>Parsea un texto que es entero un número en letras («treinta y cinco»); <c>null</c> si no lo es.</summary>
    public static decimal? Parse(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        var tokens = Tokens(text);
        var found = Find(tokens);
        return found.Count == 1 && found[0].Start == 0 && found[0].End == tokens.Length ? found[0].Value : null;
    }

    /// <summary>Separa en palabras un texto ya normalizado.</summary>
    public static string[] Tokens(string normalized) =>
        normalized.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    /// <summary>Todos los números en letras de la secuencia de palabras, de izquierda a derecha.</summary>
    public static IReadOnlyList<Span> Find(IReadOnlyList<string> tokens)
    {
        ArgumentNullException.ThrowIfNull(tokens);
        var spans = new List<Span>();
        var i = 0;

        while (i < tokens.Count)
        {
            if (TryRead(tokens, i, out var end, out var value))
            {
                spans.Add(new(i, end, value));
                i = end;
            }
            else
            {
                i++;
            }
        }

        return spans;
    }

    // Lee el número más largo que empieza en i: [centenas] [decenas [y unidad] | diez..veintinueve | unidad] [mil …]
    // [y medio | coma <dígitos en letras>].
    private static bool TryRead(IReadOnlyList<string> t, int i, out int end, out decimal value)
    {
        end = i;
        value = 0;

        if (!IsNumberWord(t[i]))
        {
            return false;
        }

        var total = 0m;
        var group = ReadBelowThousand(t, ref end);

        if (end < t.Count && t[end] == "mil")
        {
            total = (group ?? 1) * 1000;
            end++;
            group = ReadBelowThousand(t, ref end);
        }

        total += group ?? 0;

        if (end == i)
        {
            return false;
        }

        if (end + 1 < t.Count && t[end] == "y" && t[end + 1] == "medio")
        {
            total += 0.5m;
            end += 2;
        }
        else if (end + 1 < t.Count && t[end] == "coma" && Units.ContainsKey(t[end + 1]))
        {
            var scale = 0.1m;
            end++;

            while (end < t.Count && Units.TryGetValue(t[end], out var digit))
            {
                total += digit * scale;
                scale /= 10;
                end++;
            }
        }

        value = total;
        return true;
    }

    private static int? ReadBelowThousand(IReadOnlyList<string> t, ref int k)
    {
        var start = k;
        var sum = 0;

        if (k < t.Count && Hundreds.TryGetValue(t[k], out var h))
        {
            sum += h;
            k++;

            if (t[k - 1] == "cien")
            {
                return sum;
            }
        }

        if (k < t.Count && Tens.TryGetValue(t[k], out var ten))
        {
            sum += ten;
            k++;

            if (k + 1 < t.Count && t[k] == "y" && Units.TryGetValue(t[k + 1], out var u) && u > 0)
            {
                sum += u;
                k += 2;
            }
        }
        else if (k < t.Count && Teens.TryGetValue(t[k], out var teen))
        {
            sum += teen;
            k++;
        }
        else if (k < t.Count && Units.TryGetValue(t[k], out var unit))
        {
            sum += unit;
            k++;
        }

        return k == start ? null : sum;
    }
}
