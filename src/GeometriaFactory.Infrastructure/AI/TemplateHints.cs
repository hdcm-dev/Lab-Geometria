using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Infrastructure.AI;

/// <summary>
/// Las doce plantillas de respaldo, una por par (figura, campo) (`ai-layer/01` §5.2).
/// </summary>
/// <remarks>
/// <para>
/// PROVISIONALES, PENDIENTES DEL DOCENTE (punto F del lote de la actuación 004; decisión D-2 de la
/// mesa del expediente 0002). Las redactó el agente sobre el modelo Cubo · Area que fija la
/// especificación; el texto final lo fija el docente. No se presentan como definitivas.
/// </para>
/// <para>
/// NO LLEVAN NÚMEROS. Así pasan el filtro con cualquier valor derivado, y la prueba
/// `EveryTemplatePassesTheFilter` lo comprueba sobre los doce pares.
/// </para>
/// </remarks>
public static class TemplateHints
{
    private static readonly Dictionary<(FigureType, string), (string, string, string)> Texts = new()
    {
        [(FigureType.Cube, "Area")] = (
            "Revisá cómo calcula el área tu figura Cubo.",
            "El área total suma el área de todas las caras.",
            "Contá cuántas caras tiene la figura y verificá que tu cálculo las sume todas."),
        [(FigureType.Cube, "Volumen")] = (
            "Revisá cómo calcula el volumen tu figura Cubo.",
            "El volumen mide el espacio que ocupa la figura, no la superficie de una cara.",
            "Verificá que tu cálculo use la arista en las tres direcciones del espacio."),
        [(FigureType.Orthohedron, "Area")] = (
            "Revisá cómo calcula el área tu figura Ortoedro.",
            "El área total suma las seis caras, que vienen en pares iguales.",
            "Listá los tres pares de caras distintos y verificá que tu cálculo incluya los dos de cada par."),
        [(FigureType.Orthohedron, "Volumen")] = (
            "Revisá cómo calcula el volumen tu figura Ortoedro.",
            "El volumen depende de las tres dimensiones de la figura.",
            "Verificá que tu cálculo use el largo, el ancho y la altura, y no sólo dos de ellas."),
        [(FigureType.Cylinder, "Area")] = (
            "Revisá cómo calcula el área tu figura Cilindro.",
            "El área total es la superficie lateral más las dos tapas.",
            "Separá el cálculo en la parte lateral y las tapas, y verificá que tu programa sume las tres partes."),
        [(FigureType.Cylinder, "Volumen")] = (
            "Revisá cómo calcula el volumen tu figura Cilindro.",
            "El volumen es el área de la base por la altura, y la base es un círculo.",
            "Verificá que tu cálculo use el área del círculo de la base y no su contorno."),
        [(FigureType.Rectangle, "Area")] = (
            "Revisá cómo calcula el área tu figura Rectángulo.",
            "El área mide la superficie encerrada, no el contorno.",
            "Verificá que tu cálculo multiplique las dos dimensiones en lugar de sumar los lados."),
        [(FigureType.Rectangle, "Volumen")] = (
            "Revisá qué volumen declara tu figura Rectángulo.",
            "Una figura plana no ocupa espacio: no tiene volumen.",
            "Verificá qué valor de volumen escribe tu programa para una figura plana."),
        [(FigureType.Square, "Area")] = (
            "Revisá cómo calcula el área tu figura Cuadrado.",
            "El área mide la superficie encerrada, no el contorno.",
            "Verificá que tu cálculo use el lado en las dos direcciones en lugar de sumar los lados."),
        [(FigureType.Square, "Volumen")] = (
            "Revisá qué volumen declara tu figura Cuadrado.",
            "Una figura plana no ocupa espacio: no tiene volumen.",
            "Verificá qué valor de volumen escribe tu programa para una figura plana."),
        [(FigureType.Circle, "Area")] = (
            "Revisá cómo calcula el área tu figura Círculo.",
            "El área del círculo depende del radio de otra forma que la circunferencia.",
            "Compará tu cálculo con la diferencia entre medir el borde y medir la superficie encerrada."),
        [(FigureType.Circle, "Volumen")] = (
            "Revisá qué volumen declara tu figura Círculo.",
            "Una figura plana no ocupa espacio: no tiene volumen.",
            "Verificá qué valor de volumen escribe tu programa para una figura plana."),
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
