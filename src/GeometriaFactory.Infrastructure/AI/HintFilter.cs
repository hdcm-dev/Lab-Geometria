using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace GeometriaFactory.Infrastructure.AI;

/// <summary>
/// El filtro determinista posterior al modelo (`ai-layer/01` §4.2, REQ-IA-05 y REQ-IA-06).
/// </summary>
/// <remarks>
/// <para>
/// ES UN PORT FIEL DE <c>filtrar()</c> DE <c>ai-layer/ensayos/pistas.py</c>: las mismas tres
/// expresiones, el mismo orden de motivos y la misma tolerancia de 0,01 contra el valor derivado. Las
/// ocho comprobaciones de su <c>--autoprueba</c> corren acá como teoría, con los mismos motivos.
/// </para>
/// <para>
/// AGREGA, ANTES DE COMPARAR, lo que el ensayo no necesitó y un producto sí: normalización NFKC —un
/// «５４» de ancho completo es 54—, rechazo de caracteres de control, de marcado (<c>&lt;</c>) y de
/// enlaces (<c>http</c>), y una longitud máxima por nivel. Ninguno cambia el resultado de los ocho
/// casos de la autoprueba.
/// </para>
/// <para>
/// EL FILTRO ES LA GARANTÍA; EL PROMPT NO ES UN CONTROL. Una fórmula escrita en palabras sin números
/// pasa: es el riesgo residual que cubre la revisión docente (REQ-IA-13).
/// </para>
/// </remarks>
public static class HintFilter
{
    /// <summary>Longitud máxima de un nivel. El ensayo dio 189 como máximo.</summary>
    public const int MaxLevelLength = 400;

    private static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(250);

    // Las tres expresiones de `pistas.py`, literales.
    private static readonly Regex NumberPattern =
        new(@"(?<![\w.])\d+(?:[.,]\d+)?", RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex FigureJsonPattern =
        new(@"[{}]|""\s*\w+\s*""\s*:", RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex CodePattern =
        new(@"\bclass\s+\w+|\breturn\b|=>|\bpublic\s+\w+|\bMath\.\w+|;\s*$",
            RegexOptions.CultureInvariant | RegexOptions.Multiline, RegexTimeout);

    /// <summary>
    /// Devuelve <c>null</c> si las tres pistas pasan, o el motivo del rechazo: <c>forma</c>,
    /// <c>control</c>, <c>longitud</c>, <c>valor-derivado</c>, <c>json-de-figura</c>, <c>codigo</c> o
    /// <c>marcado</c>. Los cuatro primeros nombres de <c>pistas.py</c> se conservan.
    /// </summary>
    public static string? Check(string? level1, string? level2, string? level3, double derivedValue)
    {
        string?[] levels = [level1, level2, level3];

        if (levels.Any(string.IsNullOrWhiteSpace))
        {
            return "forma";
        }

        var normalized = levels.Select(level => Fold(level!)).ToArray();

        if (normalized.Any(level => level.Any(c => char.IsControl(c) && c is not ('\n' or '\r' or '\t'))))
        {
            return "control";
        }

        if (normalized.Any(level => level.Length > MaxLevelLength))
        {
            return "longitud";
        }

        var text = string.Join(' ', normalized);

        if (ContainsValue(text, derivedValue))
        {
            return "valor-derivado";
        }

        if (FigureJsonPattern.IsMatch(text))
        {
            return "json-de-figura";
        }

        if (CodePattern.IsMatch(text))
        {
            return "codigo";
        }

        if (text.Contains('<', StringComparison.Ordinal)
            || text.Contains("http", StringComparison.OrdinalIgnoreCase))
        {
            return "marcado";
        }

        return null;
    }

    /// <summary>
    /// NFKC, y además el pliegue que NFKC no garantiza en este producto: con
    /// <c>InvariantGlobalization</c> la normalización no pliega fuera de ASCII, así que las formas de
    /// ancho completo y todo dígito decimal Unicode se llevan a ASCII a mano. «５４» y «٥٤» son 54.
    /// </summary>
    private static string Fold(string level)
    {
        string text;

        try
        {
            text = level.Normalize(NormalizationForm.FormKC);
        }
        catch (PlatformNotSupportedException)
        {
            text = level;
        }

        var folded = new StringBuilder(text.Length);

        foreach (var c in text)
        {
            if (c is >= '\uFF01' and <= '\uFF5E')
            {
                folded.Append((char)(c - 0xFEE0));
            }
            else if (c == '\u3000')
            {
                folded.Append(' ');
            }
            else if (c > '\u007F' && CharUnicodeInfo.GetDecimalDigitValue(c) is var digit and >= 0)
            {
                folded.Append((char)('0' + digit));
            }
            else
            {
                folded.Append(c);
            }
        }

        return folded.ToString();
    }

    /// <summary><c>contiene_valor()</c> de <c>pistas.py</c>: algún número a 0,01 o menos del valor.</summary>
    private static bool ContainsValue(string text, double value)
    {
        foreach (Match match in NumberPattern.Matches(text))
        {
            var candidate = match.Value.Replace(',', '.');

            if (double.TryParse(candidate, NumberStyles.Float, CultureInfo.InvariantCulture, out var number)
                && Math.Abs(number - value) <= 0.01)
            {
                return true;
            }
        }

        return false;
    }
}
