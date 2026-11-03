using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Infrastructure.AI;

/// <summary>
/// Las doce plantillas de respaldo, una por par (figura, campo) (`ai-layer/01` §5.2 y §5.3).
/// </summary>
/// <remarks>
/// <para>
/// CONTRATO DE PISTAS, VERSIÓN 1.1 (`ContractVersion`): texto de la actuación 009, punto F del lote, con
/// Círculo · Area corregido en la actuación 010 porque su nivel 2 dictaba la operación, «el radio por el
/// radio». No es un texto definitivo: es la versión vigente, junto con
/// `ModelObservationHintWriter.SystemPrompt`, y se revisa con la puntuación docente, con la medición del
/// prompt v2 o ante una pista falsa (`ai-layer/01` §5.2). Cambiarlo sin cambiar los bloques marcados de
/// `01` hace fallar `HintContractMatchesSpecTests`. Son el piso de calidad: se
/// sirven siempre que el modelo falla, se demora o el filtro lo rechaza, así que tienen que enseñar
/// solas, sin el modelo.
/// </para>
/// <para>
/// Cada terna es un andamiaje de tres escalones que retira la ayuda de a poco (scaffolding, Wood,
/// Bruner y Ross, 1976; retroalimentación formativa, Shute, 2008; Hattie y Timperley, 2007):
/// nivel 1 dice DÓNDE mirar (la tarea), nivel 2 nombra la IDEA geométrica que rige ese campo (el
/// proceso) y nivel 3 propone una COMPROBACIÓN que el alumno hace solo (la autorregulación).
/// Ningún nivel da la operación completa ni el resultado: la última decisión es del alumno.
/// </para>
/// <para>
/// NO LLEVAN CIFRAS. Así pasan el filtro con cualquier valor derivado, y la prueba
/// `EveryTemplatePassesTheFilter` lo comprueba sobre los doce pares. Los números en palabras que
/// aparecen («dos tapas», «tres dimensiones») son estructura de la figura, no un valor del trabajo.
/// </para>
/// </remarks>
public static class TemplateHints
{
    /// <summary>Versión del contrato de pistas (prompt de sistema y doce plantillas), la de `ai-layer/01` §5.2.</summary>
    public const string ContractVersion = "1.1";

    private static readonly Dictionary<(FigureType, string), (string, string, string)> Texts = new()
    {
        [(FigureType.Cube, "Area")] = (
            "Mirá la parte de tu programa que calcula el área del Cubo: ahí está la diferencia.",
            "El área total de un cuerpo es la suma de las superficies de todas sus caras, y en el cubo todas las caras son iguales.",
            "Imaginá el cubo desarmado sobre la mesa: contá las caras y fijate si tu cálculo las incluye a todas, ni una más ni una menos."),
        [(FigureType.Cube, "Volumen")] = (
            "Mirá la parte de tu programa que calcula el volumen del Cubo: ahí está la diferencia.",
            "El volumen mide el espacio que ocupa el cuerpo, no la superficie de una cara: la arista interviene en largo, ancho y alto.",
            "Contá cuántas veces aparece la arista como factor en tu cálculo y compará con la cantidad de direcciones que tiene el espacio."),
        [(FigureType.Orthohedron, "Area")] = (
            "Mirá la parte de tu programa que calcula el área del Ortoedro: ahí está la diferencia.",
            "El ortoedro tiene seis caras rectangulares que vienen en pares iguales y opuestos.",
            "Escribí al lado de tu cálculo qué par de dimensiones forma cada cara y verificá que cada par aparezca dos veces."),
        [(FigureType.Orthohedron, "Volumen")] = (
            "Mirá la parte de tu programa que calcula el volumen del Ortoedro: ahí está la diferencia.",
            "El volumen de un ortoedro combina sus tres dimensiones: largo, ancho y altura.",
            "Señalá en tu cálculo dónde aparece cada una de las tres dimensiones; si falta alguna o una se repite, ahí está el problema."),
        [(FigureType.Cylinder, "Area")] = (
            "Mirá la parte de tu programa que calcula el área del Cilindro: ahí está la diferencia.",
            "El área total del cilindro tiene dos partes distintas: la superficie lateral, que desenrollada es un rectángulo, y las dos tapas circulares.",
            "Separá tu cálculo en la parte lateral y las tapas y verificá que estén las tres superficies: la lateral y cada una de las dos tapas."),
        [(FigureType.Cylinder, "Volumen")] = (
            "Mirá la parte de tu programa que calcula el volumen del Cilindro: ahí está la diferencia.",
            "El volumen de un cilindro es la superficie de su base multiplicada por la altura, y la base es un círculo.",
            "Revisá qué medida del círculo usás como base: tiene que ser la superficie encerrada, no el largo del borde."),
        [(FigureType.Rectangle, "Area")] = (
            "Mirá la parte de tu programa que calcula el área del Rectángulo: ahí está la diferencia.",
            "El área mide la superficie encerrada por la figura; el contorno es otra magnitud, el perímetro.",
            "Preguntate si tu cálculo cuenta cuadraditos dentro de la figura o recorre su borde: el área combina el largo con el ancho."),
        [(FigureType.Rectangle, "Volumen")] = (
            "Mirá qué volumen declara tu programa para el Rectángulo.",
            "Una figura plana tiene largo y ancho pero no altura: no ocupa espacio, así que no tiene volumen.",
            "Buscá en tu programa qué valor de volumen escribe para una figura plana y preguntate qué debería valer algo que no ocupa espacio."),
        [(FigureType.Square, "Area")] = (
            "Mirá la parte de tu programa que calcula el área del Cuadrado: ahí está la diferencia.",
            "El área mide la superficie encerrada por la figura; el contorno es otra magnitud, el perímetro.",
            "Fijate si tu cálculo usa el lado en las dos direcciones de la figura o si suma los lados del borde."),
        [(FigureType.Square, "Volumen")] = (
            "Mirá qué volumen declara tu programa para el Cuadrado.",
            "Una figura plana tiene largo y ancho pero no altura: no ocupa espacio, así que no tiene volumen.",
            "Buscá en tu programa qué valor de volumen escribe para una figura plana y preguntate qué debería valer algo que no ocupa espacio."),
        [(FigureType.Circle, "Area")] = (
            "Mirá la parte de tu programa que calcula el área del Círculo: ahí está la diferencia.",
            "La superficie del círculo y el largo de su borde dependen del radio de forma distinta: si el radio se duplica, el borde se duplica, pero la superficie aumenta en una proporción mucho mayor.",
            "Probá tu cálculo con un radio y después con el doble de ese radio: si el área que obtenés también sale apenas el doble, estás midiendo el borde y no la superficie."),
        [(FigureType.Circle, "Volumen")] = (
            "Mirá qué volumen declara tu programa para el Círculo.",
            "Una figura plana no tiene altura: no ocupa espacio, así que no tiene volumen.",
            "Buscá en tu programa qué valor de volumen escribe para una figura plana y preguntate qué debería valer algo que no ocupa espacio."),
    };

    /// <summary>Los doce pares que la plantilla cubre.</summary>
    public static IEnumerable<(FigureType FigureType, string Field)> Pairs => Texts.Keys;

    /// <summary>
    /// La plantilla del par. Un par fuera de los doce —que el caso de uso no arma— recibe una pista
    /// genérica sin números, para que el respaldo nunca falte.
    /// </summary>
    public static Hints For(FigureType figureType, string field)
    {
        var (level1, level2, level3) = Texts.TryGetValue((figureType, field), out var text)
            ? text
            : (
                $"Revisá cómo calcula tu programa el campo {field} de esta figura.",
                "Compará la fórmula que usaste con la definición geométrica del campo.",
                "Verificá paso a paso qué dimensiones intervienen en tu cálculo.");

        return new Hints(level1, level2, level3, HintOrigin.Template);
    }
}
