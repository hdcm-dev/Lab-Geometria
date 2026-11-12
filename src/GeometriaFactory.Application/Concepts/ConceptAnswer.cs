using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Concepts;

/// <summary>Una porción del desglose de F2: etiqueta del estado y su cantidad.</summary>
public sealed record ConceptBreakdownItem(string Label, string State, int Count);

/// <summary>La respuesta del asistente, lista para la pantalla.</summary>
/// <param name="Form">Forma F1–F8.</param>
/// <param name="Ring">Anillo.</param>
/// <param name="Intent">Intención del enrutador.</param>
/// <param name="Text">Texto que se muestra.</param>
/// <param name="Card">La ficha en juego, si la hay (F4, y F6 cuando la pregunta nombra un concepto).</param>
/// <param name="Destination">Chip del catálogo, ya revalidado; nunca una ruta del modelo.</param>
/// <param name="Origin"><c>ficha</c>, <c>modelo</c>, <c>fijo</c> o <c>caso-de-uso</c>.</param>
/// <param name="Note">Nota de métrica (F1, F2) o motivo de IA apagada (F8).</param>
/// <param name="Breakdown">Desglose ofrecido de F2.</param>
public sealed record ConceptAnswer(
    ConceptForm Form,
    ConceptRing Ring,
    string Intent,
    string Text,
    ConceptCard? Card,
    ResolvedDestination? Destination,
    string Origin,
    string? Note,
    IReadOnlyList<ConceptBreakdownItem> Breakdown)
{
    /// <summary>Las respuestas de cada parte de una compuesta (forma <see cref="ConceptForm.Compound"/>); vacía si no lo es.</summary>
    public IReadOnlyList<ConceptAnswer> Parts { get; init; } = [];
}

/// <summary>Los textos fijos del asistente (K5; voseo, sin primera persona de acción, CD-6).</summary>
public static class ConceptTexts
{
    public const string Abstention =
        "No está en la ayuda de conceptos. Probá con otra palabra o consultalo con tu docente.";

    public const string Outside =
        "Esta ayuda contesta sobre la Fábrica de Geometría. Por ejemplo: ¿qué es un ortoedro?, "
        + "¿qué significa valor derivado?, ¿dónde veo mis trabajos?";

    public const string Adjacent =
        "Programar, depurar o serializar en C# es parte de la materia y esta ayuda no lo cubre: consultalo con tu docente "
        + "o en el material de la materia. Cuando tengas el texto que emite tu programa, cargalo acá.";

    /// <summary>Frase de las pistas en la negativa; sólo con <c>AI:HintsEnabled</c> (EXP-0005).</summary>
    public const string HintsSentence = " Si tu trabajo tiene una advertencia, las pistas están en Ver pistas.";

    public const string RefusalWithoutConcept =
        "Eso es parte del ejercicio: lo resolvés vos en tu programa. Lo que sí está en la ayuda son los conceptos: "
        + "preguntá, por ejemplo, qué es un ortoedro." + HintsSentence;

    /// <summary>F6 sin concepto; nombra las pistas sólo si están encendidas.</summary>
    public static string RefusalWithoutConceptFor(bool hintsEnabled) =>
        hintsEnabled ? RefusalWithoutConcept : RefusalWithoutConcept[..^HintsSentence.Length];

    public const string TeacherCannotEdit =
        "Una versión nueva la carga el alumno desde su trabajo. Vos podés verlo o eliminarlo.";

    public const string NotFound =
        "No encontramos ese trabajo entre los que podés ver. Abrí tus trabajos para elegir uno.";

    public const string OpenAWorkFirst =
        "Abrí un trabajo desde Mis trabajos y preguntá de nuevo ahí: la cuenta es la del trabajo que estás mirando.";

    public const string MyWorks =
        "Tus trabajos están en Mis trabajos, con su estado: borrador, pendiente, finalizado o rechazado.";

    public const string CommissionWorks =
        "Los trabajos enviados de la comisión están en la entrega de la comisión, con filtros por alumno y estado.";

    public const string NewWork =
        "En Cargar un trabajo pegás el texto entero que emite tu programa; el laboratorio te muestra lo que entendió.";

    public const string NewVersion =
        "Desde tu trabajo, en Cargar una versión nueva, pegás el texto nuevo que emite tu programa.";

    public const string NewVersionNoWork =
        "Una versión nueva se carga desde el trabajo: abrilo en Mis trabajos y elegí Cargar una versión nueva.";

    public const string NewVersionNotDraft =
        "Este trabajo ya se envió: una versión nueva sólo se carga mientras está en borrador.";

    public const string TeacherRefusal =
        "Corregir, aprobar o rechazar un trabajo lo decidís vos al revisarlo; esta ayuda explica conceptos y no "
        + "resuelve el ejercicio. Los trabajos enviados están en la entrega de la comisión.";

    public const string RemoveWarningForTeacher =
        "La advertencia la saca el alumno cargando una versión corregida de su programa; vos la ves al revisar el trabajo.";

    /// <summary>Qué hacer con una advertencia, sin decir el valor.</summary>
    public static string RemoveWarningFor(bool hintsEnabled) =>
        "La advertencia se va cuando el valor que declara tu programa coincide con el que se deriva de tus dimensiones: "
        + "revisá cómo lo calcula tu programa y cargá una versión nueva."
        + (hintsEnabled ? " Las pistas de la primera advertencia están en Ver pistas." : string.Empty);

    public const string EnableStudent =
        "Las cuentas de los alumnos se habilitan en Cuentas: ahí están las pendientes y las habilitás de a una.";

    public const string EnableStudentForStudent =
        "Tu cuenta la habilita el docente de la comisión; cuando esté habilitada, tus trabajos están en Mis trabajos.";

    public const string PendingStudentsForStudent =
        "Las cuentas pendientes las ve el docente de la comisión. Tus trabajos están en Mis trabajos.";

    public const string PendingStudentsNote = "Cuenta las cuentas de alumnos en estado Pendiente, que esperan habilitación.";

    /// <summary>F1 de cuentas pendientes.</summary>
    public static string PendingStudents(int count) => count switch
    {
        0 => "No hay alumnos pendientes de habilitar.",
        1 => "Hay 1 alumno pendiente de habilitar.",
        _ => $"Hay {count} alumnos pendientes de habilitar.",
    };

    /// <summary>El supuesto dicho: el trabajo abierto y los campos con advertencia, sin valores.</summary>
    public static string OpenWorkWarnings(IReadOnlyList<string> fields, Role role, bool hints)
    {
        ArgumentNullException.ThrowIfNull(fields);
        var whose = role == Role.Administrator ? "En el trabajo abierto" : "En tu trabajo abierto";
        if (fields.Count == 0)
        {
            return whose + " no hay advertencias.";
        }

        var list = fields.Count == 1 ? $"«{fields[0]}»" : string.Join(", ", fields.Take(fields.Count - 1).Select(f => $"«{f}»")) + $" y «{fields[^1]}»";
        return $"{whose}, la advertencia está en {list}: lo declarado no coincide con lo que se deriva de las dimensiones."
            + (hints ? " Las pistas están en Ver pistas." : string.Empty);
    }

    /// <summary>El supuesto dicho: el estado del trabajo abierto.</summary>
    public static string OpenWorkStatus(string status) => $"El trabajo abierto está en {status}.";

    public const string WarningNote = "Cuenta advertencias (discrepancias entre lo declarado y lo derivado), no errores.";

    public const string TotalNote = "Cuenta todos tus trabajos, en cualquier estado.";

    public const string CommissionTotalNote = "Cuenta los trabajos enviados de la comisión; los borradores no se ven.";

    /// <summary>F6 cuando la pregunta nombra un concepto.</summary>
    public static string RefusalFor(ConceptCard card, bool hintsEnabled = true)
    {
        ArgumentNullException.ThrowIfNull(card);
        return "Eso es parte del ejercicio: lo resolvés vos en tu programa. Lo que sí está en la ayuda es qué es "
            + $"«{card.Title}»: abrí la ficha acá abajo." + (hintsEnabled ? HintsSentence : string.Empty);
    }

    /// <summary>F8: nombra la clave que falta, nunca su valor (K2 R6).</summary>
    public static string AiOff(string missingKey) =>
        $"La redacción con IA está apagada: falta configurar {missingKey}. Te mostramos la ficha.";

    /// <summary>F1.</summary>
    public static string Warnings(int count) => count == 1
        ? "Este trabajo tiene 1 advertencia."
        : $"Este trabajo tiene {count} advertencias.";

    /// <summary>F2.</summary>
    public static string Total(int count, bool administrator) => administrator
        ? $"La comisión tiene {count} trabajos enviados. ¿Los abrís por estado?"
        : count == 1 ? "Tenés 1 trabajo. ¿Lo abrís por estado?" : $"Tenés {count} trabajos. ¿Los abrís por estado?";
}
