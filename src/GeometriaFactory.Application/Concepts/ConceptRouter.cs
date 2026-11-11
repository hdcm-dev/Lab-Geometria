using System.Text;
using System.Text.RegularExpressions;

namespace GeometriaFactory.Application.Concepts;

/// <summary>La decisión del enrutador: anillo, intención y, si la hay, la ficha elegida.</summary>
public sealed record ConceptRoute(ConceptRing Ring, string Intent, ConceptCard? Card);

/// <summary>
/// El enrutador del asistente de conceptos (ADR-14014). Función pura: sin DI, sin E/S y sin modelo.
/// </summary>
/// <remarks>
/// <para>
/// EL ORDEN ES LA FRONTERA. Inyección y tema ajeno primero; después lo de al lado; después **todo pedido
/// de resolver, antes de buscar fichas** (REQ-IA-40, regla F-G1): «¿cuánto da el área de mi cubo?» nombra
/// un concepto y aun así no es una pregunta de concepto. Recién entonces operar y, al final, las fichas.
/// </para>
/// <para>
/// LAS FICHAS QUE RECIBE YA ESTÁN FILTRADAS POR ROL (R4): el enrutador no sabe de roles y no puede
/// elegir una ficha que el rol no ve.
/// </para>
/// </remarks>
public static class ConceptRouter
{
    /// <summary>Tope de la pregunta, en caracteres (REQ-IA-21).</summary>
    public const int MaxQuestionLength = 300;

    private static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(250);

    private static readonly Regex Injection = new(
        @"\bignora|\bolvida(te)? (de )?(lo|las|todo)|instrucciones (anteriores|previas)|system prompt|prompt del sistema"
        + @"|llevame a /|\s/[a-z][\w-]*|contrasena|password|clave de (la|el|otr)|token",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex Unrelated = new(
        @"\bfutbol|\bpartido\b|\bclima\b|\breceta|\bchiste|\bbitcoin|\bnoticias|\bpelicula|\bquien gana",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex Adjacent = new(
        @"\bdepur|\bdebug|breakpoint|\bserializ|\bcompil|visual studio|\bexcepcion|\bnullreference",
        RegexOptions.CultureInvariant, RegexTimeout);

    // El pedido de resolver: el valor, la fórmula, el resultado, el JSON, el código o la corrección.
    private static readonly Regex AsksToSolve = new(
        @"\bcuanto (da|tiene que dar|deberia|debe|vale|mide|es)\b|\bcual es (el valor|la formula|el resultado|el area|el volumen)"
        + @"|\bformula|\bpasame|\bescribime|\bhaceme|\bhace(me)? (el|la|mi)\b|\bdame (el|la)\b|\bresolv|\bcorregi|\bcorrigi"
        + @"|\bcalcula|\bcalculame|\barmame|\bdecime (el|la|cuanto)|\bque valor (va|pongo|tiene que)|\bcomo (lo )?calculo"
        // H1 (act. 003): el pedido del valor que va en un campo, dicho sin «cuánto» ni «fórmula».
        + @"|\bque (tengo|tenes|hay|tendria|deberia) que (poner|escribir|cargar|completar)|\bque (pongo|escribo|cargo) en"
        + @"|\bque numero (va|pongo|tengo|le|hay|deberia|corresponde)|\bque (va|iria) en (el |la )?(area|volumen|perimetro|campo)",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex OwnFigure = new(
        @"\bde mi (trabajo|cubo|cilindro|ortoedro|rectangulo|circulo|figura)|\bmi (cubo|cilindro|ortoedro|rectangulo|circulo|figura)",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex Magnitude = new(
        @"\barea|\bvolumen|\bperimetro|\bsuperficie|\bradio\b|\baltura\b",
        RegexOptions.CultureInvariant, RegexTimeout);

    // EL NÚMERO EN LETRAS CUENTA COMO NÚMERO (H1, act. 003): «si la arista es tres» esquivaba la regla del dígito.
    // «uno» y «una» quedan afuera porque son artículos («una arista»).
    private static readonly Regex Digit = new(
        @"\d|\b(dos|tres|cuatro|cinco|seis|siete|ocho|nueve|diez|once|doce|quince|veinte|treinta|cuarenta|cincuenta|cien|ciento|mil|medio)\b",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex CountWarnings = new(
        @"\bcuantas (advertencias|observaciones)", RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex CountWorks = new(
        @"\bcuantos trabajos", RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex NewVersion = new(
        @"version nueva|nueva version|\breeditar|\beditar (el|mi|este) trabajo|volver a cargar|cargar (otra|de nuevo)",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex MyWorks = new(
        @"\bdonde (veo|estan|encuentro|quedan) mis trabajos|\bver mis (trabajos|entregas)|\bmis entregas",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex NewWork = new(
        @"\b(cargar|subir|enviar|entregar|pegar) (un |el |mi )?trabajo", RegexOptions.CultureInvariant, RegexTimeout);

    /// <summary>Intenciones fijas del enrutador (las de ficha usan el id de la ficha).</summary>
    public static class Intents
    {
        public const string Injection = "inyeccion";
        public const string Unrelated = "tema-ajeno";
        public const string Adjacent = "lo-de-al-lado";
        public const string AsksToSolve = "pide-resolver";
        public const string CountWarnings = "operar:cuantas-advertencias";
        public const string CountWorks = "operar:cuantos-trabajos";
        public const string NewVersion = "operar:version-nueva";
        public const string MyWorks = "operar:mis-trabajos";
        public const string NewWork = "operar:trabajo-nuevo";
        public const string NoCard = "sin-ficha";
        public const string TooLong = "sobre-el-tope";
    }

    /// <summary>Enruta una pregunta contra las fichas que el rol ve.</summary>
    public static ConceptRoute Route(string? question, IReadOnlyList<ConceptCard> visibleCards)
    {
        ArgumentNullException.ThrowIfNull(visibleCards);

        if (string.IsNullOrWhiteSpace(question))
        {
            return new(ConceptRing.Outside, Intents.Unrelated, null);
        }

        if (question.Length > MaxQuestionLength)
        {
            return new(ConceptRing.Outside, Intents.TooLong, null);
        }

        var text = Normalize(question);

        if (Injection.IsMatch(text))
        {
            return new(ConceptRing.Outside, Intents.Injection, null);
        }

        if (Unrelated.IsMatch(text))
        {
            return new(ConceptRing.Outside, Intents.Unrelated, null);
        }

        if (Adjacent.IsMatch(text))
        {
            return new(ConceptRing.Adjacent, Intents.Adjacent, null);
        }

        if (AsksToSolve.IsMatch(text)
            || (OwnFigure.IsMatch(text) && Magnitude.IsMatch(text))
            || (Digit.IsMatch(text) && Magnitude.IsMatch(text)))
        {
            return new(ConceptRing.Outside, Intents.AsksToSolve, null);
        }

        if (CountWarnings.IsMatch(text))
        {
            return new(ConceptRing.Operate, Intents.CountWarnings, null);
        }

        if (CountWorks.IsMatch(text))
        {
            return new(ConceptRing.Operate, Intents.CountWorks, null);
        }

        if (NewVersion.IsMatch(text))
        {
            return new(ConceptRing.Operate, Intents.NewVersion, null);
        }

        if (MyWorks.IsMatch(text))
        {
            return new(ConceptRing.Operate, Intents.MyWorks, null);
        }

        if (NewWork.IsMatch(text))
        {
            return new(ConceptRing.Operate, Intents.NewWork, null);
        }

        var best = MatchCard(text, visibleCards);

        return best is null
            ? new(ConceptRing.Concepts, Intents.NoCard, null)
            : new(ConceptRing.Concepts, best.Id, best);
    }

    /// <summary>
    /// La ficha que nombra la pregunta, o <c>null</c>. El sinónimo más largo gana («rectángulo
    /// desarrollado» antes que «desarrollado») y una palabra sólo cuenta entera.
    /// </summary>
    public static ConceptCard? MatchCard(string question, IReadOnlyList<ConceptCard> visibleCards)
    {
        ArgumentNullException.ThrowIfNull(question);
        ArgumentNullException.ThrowIfNull(visibleCards);

        var text = question.StartsWith(' ') ? question : Normalize(question);

        // Una pregunta real del frontmatter pesa más que cualquier sinónimo (K6 §4 y §6).
        foreach (var card in visibleCards)
        {
            if (card.Questions.Any(q => Normalize(q) == text))
            {
                return card;
            }
        }

        ConceptCard? best = null;
        var bestLength = 0;

        foreach (var card in visibleCards)
        {
            foreach (var synonym in card.Synonyms)
            {
                var needle = Normalize(synonym);

                if (needle.Length > bestLength && ContainsWord(text, needle))
                {
                    best = card;
                    bestLength = needle.Length;
                }
            }
        }

        return best;
    }

    /// <summary>Minúsculas, sin tildes y sin signos: la forma en que se comparan pregunta y sinónimos.</summary>
    public static string Normalize(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        // EL PLIEGUE ES A MANO Y NO CON NormalizationForm.FormD: con InvariantGlobalization la
        // normalización no pliega fuera de ASCII en este producto (lo mismo que encontró HintFilter).
        var folded = new StringBuilder(value.Length);

        foreach (var c in value.ToLowerInvariant())
        {
            var plain = c switch
            {
                'á' or 'à' or 'ä' or 'â' => 'a',
                'é' or 'è' or 'ë' or 'ê' => 'e',
                'í' or 'ì' or 'ï' or 'î' => 'i',
                'ó' or 'ò' or 'ö' or 'ô' => 'o',
                'ú' or 'ù' or 'ü' or 'û' => 'u',
                'ñ' => 'n',
                _ => c,
            };

            folded.Append(char.IsLetterOrDigit(plain) || plain is '/' or '-' ? plain : ' ');
        }

        return " " + Regex.Replace(folded.ToString(), @"\s+", " ", RegexOptions.CultureInvariant, RegexTimeout).Trim() + " ";
    }

    private static bool ContainsWord(string text, string needle)
    {
        var trimmed = needle.Trim();
        return trimmed.Length > 0
            && (text.Contains(" " + trimmed + " ", StringComparison.Ordinal)
                || text.Contains(" " + trimmed + "s ", StringComparison.Ordinal)
                || text.Contains(" " + trimmed + "es ", StringComparison.Ordinal));
    }
}
