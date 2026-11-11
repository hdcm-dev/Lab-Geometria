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

    // Ronda 6: «cuánto pinto si la pared es de tres metros por dos metros» pide un cálculo sin nombrar la
    // magnitud: «N unidad por N unidad» con un verbo de cálculo es dato.
    private static readonly Regex Dimensions = new(
        @"\b(metros?|centimetros?|milimetros?|kilometros?|cm|m|mm|km) por\b",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex CalcVerb = new(
        @"\b(pinto|pintar|necesito|necesita|alcanza|alcanzan|compro|comprar|gasto|gastar|cubro|cubrir|lleva|llevan)\b",
        RegexOptions.CultureInvariant, RegexTimeout);

    // El pedido de resolver: el valor, la fórmula, el resultado, el JSON, el código o la corrección.
    private static readonly Regex AsksToSolve = new(
        @"\bcuanto (da|tiene que dar|deberia|debe|vale|mide|es)\b|\bcual es (el valor|la formula|el resultado|el area|el volumen)"
        // Ronda 4: «fórmula» en singular pide LA fórmula; en plural («las dos fórmulas del cono») se habla de
        // fórmulas, no se pide un valor.
        + @"|\bformula\b|\bpasame|\bescribime|\bhaceme|\bhace(me)? (el|la|mi)\b|\bdame (el|la)\b|\bresolv|\bcorregi|\bcorrigi"
        + @"|(?<!\bse )\bcalcula|\barmame|\bdecime (el|la|cuanto)|\bque valor (va|pongo|tiene que)|\bcomo (lo )?calculo"
        // H1 (act. 003): el pedido del valor que va en un campo, dicho sin «cuánto» ni «fórmula».
        + @"|\bque (tengo|tenes|hay|tendria|deberia) que (poner|escribir|cargar|completar)|\bque (pongo|escribo|cargo) en"
        + @"|\bque numero (va|pongo|tengo|le|hay|deberia|corresponde)|\bque (va|iria) en (el |la )?(area|volumen|perimetro|campo)",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex OwnFigure = new(
        @"\bde mi (trabajo|cubo|cilindro|ortoedro|rectangulo|circulo|figura)|\bmi (cubo|cilindro|ortoedro|rectangulo|circulo|figura)",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly Regex Magnitude = new(
        @"\barea|\bvolumen|\bperimetro|\bsuperficie|\bradio\b|\baltura\b|\bdiagonal|\bhipotenusa|\bcateto"
        // Ronda 3: «cuánto ocupa una caja de dos por tres por cuatro» pide un volumen sin nombrarlo.
        + @"|\bocupa|\bcabe|\bcapacidad"
        // Ronda 5: «cuántos centímetros cuadrados tiene…» pide un área por su unidad, sin nombrarla.
        + @"|\b(centimetros|metros|milimetros|kilometros|unidades) (cuadrados|cubicos|cuadradas|cubicas)\b|\b(cm|m|mm)[23]\b",
        RegexOptions.CultureInvariant, RegexTimeout);

    // EL DÍGITO CUENTA COMO DATO junto a una magnitud. El número EN LETRAS, en cambio, sólo cuando funciona como
    // valor de una medida (ver SpokenValue): la act. 003 lo contaba siempre y mandaba a la negativa «qué es el
    // área de las dos bases» o «la altura, la que pasa por el medio» (regresión de la verificación, defecto 1).
    private static readonly Regex Digit = new(@"\d", RegexOptions.CultureInvariant, RegexTimeout);

    // Lo que viene ANTES de un valor: «es tres», «mide tres», «igual a tres», «= tres».
    private static readonly HashSet<string> ValueVerbs = new(StringComparer.Ordinal)
    {
        "es", "son", "mide", "miden", "mida", "midan", "medira", "vale", "valen", "valga", "sea", "sean", "fuera",
        "fueran", "da", "dan", "igual", "tiene", "tienen",
    };

    // La medida de la que el número es valor: «arista tres», «de radio dos».
    private static readonly HashSet<string> MeasureNouns = new(StringComparer.Ordinal)
    {
        "arista", "lado", "radio", "altura", "base", "largo", "ancho", "alto", "diametro", "profundidad", "apotema",
        "generatriz", "area", "volumen", "perimetro", "superficie", "diagonal", "medida", "cateto", "hipotenusa",
    };

    // Medidas en plural: sólo DELANTE del valor («catetos tres y cuatro», «lados dos y cinco»). Detrás del número
    // cuentan («tiene doce aristas»), por eso no van en MeasureNouns (ronda 3).
    private static readonly HashSet<string> MeasurePlurals = new(StringComparer.Ordinal)
    {
        "catetos", "lados", "aristas", "radios", "alturas", "bases", "diagonales", "medidas", "dimensiones",
    };

    // Preguntas sobre la fórmula, no pedido de ella (ronda 3): «hay dos fórmulas…», «qué significa el dos en la
    // fórmula…». Igual que «por qué», sólo un valor de medida las vuelve pedido.
    private static readonly Regex Explains = new(
        @"^ (por que|que significa|que quiere decir|hay|existe|existen|de donde sale|para que sirven?) "
        // Ronda 5: la pregunta explicativa también va al final («…aparece cuatro tercios, de dónde sale»).
        + @"|\b(de donde salen?|para que sirven?|que significa|que quiere decir)\b",
        RegexOptions.CultureInvariant, RegexTimeout);

    private static readonly HashSet<string> UnitWords = new(StringComparer.Ordinal)
    {
        "cm", "centimetro", "centimetros", "m", "metro", "metros", "mm", "milimetro", "milimetros", "km",
        "kilometros", "unidad", "unidades", "u", "cm2", "cm3", "m2", "m3", "pulgadas",
    };

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

        // «por qué …» pide una explicación: la palabra «fórmula» ahí no pide la fórmula (ronda 2); sólo un valor
        // de medida lo vuelve pedido.
        var explains = Explains.IsMatch(text);

        if ((!explains && AsksToSolve.IsMatch(text))
            || (OwnFigure.IsMatch(text) && Magnitude.IsMatch(text))
            || (SpokenValue(question) && Magnitude.IsMatch(text))
            || (Dimensions.IsMatch(text) && CalcVerb.IsMatch(text) && SpokenValue(question)))
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
    /// Si la pregunta trae un número en letras que funciona como VALOR de una medida. La regla mira la FUNCIÓN
    /// SINTÁCTICA del número, no una lista de sustantivos (EXP-0004, sonda de la ronda 1):
    /// <list type="bullet">
    /// <item>Es dato: delante de una unidad («dos metros»), de «por» y otro número («tres por cuatro»), de «de» +
    /// medida («siete de lado», «tres de radio»); detrás de una medida o figura («arista tres», «un cuadrado
    /// tres»), de «el número», de «es/mide/vale/igual a/=», o de «de» («cubo de tres»).</item>
    /// <item>No es dato: cuando CUENTA un sustantivo, es decir, lo sigue cualquier palabra de contenido que no
    /// sea unidad («dos bases», «tres cubos iguales», «son seis cuadrados», «tiene doce aristas»), o en «uno de
    /// los/las».</item>
    /// </list>
    /// Ante la duda se prefiere concepto: la ficha es inocua y la negativa a un concepto legítimo no lo es.
    /// </summary>
    public static bool SpokenValue(string question)
    {
        ArgumentNullException.ThrowIfNull(question);

        // «=» se pliega a espacio en Normalize; se lo nombra antes para no perderlo.
        var tokens = SpokenNumber.Tokens(Normalize(question.Replace("=", " igual ", StringComparison.Ordinal)));

        // El dígito sigue la misma regla que la letra (ronda 2: «el área de 6 cuadrados» cuenta cuadrados).
        var spans = SpokenNumber.Find(tokens).ToList();
        for (var d = 0; d < tokens.Length; d++)
        {
            if (Digit.IsMatch(tokens[d]) && !spans.Any(s => s.Start <= d && d < s.End))
            {
                spans.Add(new(d, d + 1, 0));
            }
        }

        foreach (var span in spans)
        {
            string? At(int i) => i >= 0 && i < tokens.Length ? tokens[i] : null;
            var next = At(span.End);
            var next2 = At(span.End + 1);
            var prev = At(span.Start - 1);
            var prev2 = At(span.Start - 2);

            // Delante: unidad, «por N», «de <medida>».
            if (next is not null && UnitWords.Contains(next))
            {
                return true;
            }

            if (next == "por" && next2 is not null && (SpokenNumber.IsNumberWord(next2) || char.IsDigit(next2[0])))
            {
                return true;
            }

            if (next == "de" && next2 is not null && MeasureNouns.Contains(next2))
            {
                return true;
            }

            // «uno de los dos conceptos»: partitivo, no valor.
            if (next == "de" && next2 is "los" or "las" or "sus" or "estos" or "estas")
            {
                continue;
            }

            // Cuenta un sustantivo: cualquier palabra de contenido detrás del número. Una medida en singular no se
            // cuenta: abre otra cláusula («lado siete, área», «radio veinte, volumen»). «N veces» es multiplicativo.
            if (next is not null && !FunctionWords.Contains(next) && !SpokenNumber.IsNumberWord(next)
                && (next == "veces" || !MeasureNouns.Contains(next)))
            {
                continue;
            }

            // Detrás: verbo de valor, medida o figura, «el número», «de», «igual a».
            if (prev is not null
                && (ValueVerbs.Contains(prev) || MeasureNouns.Contains(prev) || MeasurePlurals.Contains(prev)
                    || FigureNouns.Contains(prev)
                    || prev == "numero" || prev == "de"))
            {
                return true;
            }

            if (prev == "a" && prev2 == "igual")
            {
                return true;
            }
        }

        return false;
    }

    // Palabras de función: detrás de un número no son el sustantivo que cuenta («la arista es tres y el área»,
    // «un cuadrado tres, su área»). Clase cerrada: no crece con el vocabulario de la geometría.
    private static readonly HashSet<string> FunctionWords = new(StringComparer.Ordinal)
    {
        "y", "e", "o", "ni", "pero", "entonces", "si", "que", "cual", "cuales", "cuanto", "cuanta", "cuantos",
        "cuantas", "como", "donde", "cuando", "el", "la", "los", "las", "lo", "un", "una", "al", "del", "de", "a",
        "en", "con", "para", "sin", "su", "sus", "mi", "mis", "tu", "tus", "este", "esta", "ese", "esa", "no", "se",
        "es", "son", "da", "dan", "mide", "miden", "vale", "valen", "seria", "sera", "queda", "tiene", "hay", "me",
        "te", "le", "les", "ya", "mas", "menos", "por", "igual", "medio", "coma",
    };

    // Figuras: «un cuadrado tres» nombra la figura y su medida en el mismo giro.
    private static readonly HashSet<string> FigureNouns = new(StringComparer.Ordinal)
    {
        "cubo", "cuadrado", "circulo", "cilindro", "ortoedro", "rectangulo", "triangulo", "prisma", "esfera",
        "cono", "piramide",
    };

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
