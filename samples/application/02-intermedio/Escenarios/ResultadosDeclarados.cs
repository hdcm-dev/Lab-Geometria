using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Application.Intermedio.Escenarios;

/// <summary>
/// El resultado de interpretación de cada uno de los ocho escenarios, **compuesto a mano** desde
/// los textos del `PRODUCT-INTAKE` §20.
/// </summary>
/// <remarks>
/// Ninguno se calcula: se declaran, porque lo que el sample enseña es **qué hace la capa con el
/// resultado**, no cómo se produce. Producirlo es de la infraestructura y tiene su propio sample.
/// </remarks>
internal static class ResultadosDeclarados
{
    private static Piece P(int i, FigureType t, double? area, double? derivada,
        double? vol = null, double? volDerivado = null, IEnumerable<Component>? componentes = null) =>
        Piece.Reconstruct(i, t, area, derivada, vol, volDerivado, componentes);

    /// <summary>`E-1` · tres figuras, dos advertencias, ningún error.</summary>
    internal static FigureInterpretation E1() => FigureInterpretation.From(3,
        [P(0, FigureType.Cylinder, 113.10, 113.10, 84.82, 84.82),
         P(1, FigureType.Cube, 54.00, 54.00, 27.00, 27.00),
         P(2, FigureType.Orthohedron, 208.00, 208.00, 192.00, 192.00)],
        [Observation.ValueDiscrepancyAt(0, "Area", 113.10, 113.10),
         Observation.ValueDiscrepancyAt(1, "Volumen", 27.00, 27.00)]);

    /// <summary>`E-2` · el texto con dos comas finales. **Se interpreta igual**: una figura, una advertencia de volumen.</summary>
    internal static FigureInterpretation E2() => FigureInterpretation.From(1,
        [P(0, FigureType.Orthohedron, 490.00, 490.00, 1029.00, 1029.00)],
        [Observation.ValueDiscrepancyAt(0, "Volumen", 1029.00, 1029.00)]);

    /// <summary>`E-3` · el cubo de lado 3 con área declarada 36.00, que no coincide con la derivada.</summary>
    internal static FigureInterpretation E3() => FigureInterpretation.From(1,
        [P(0, FigureType.Cube, 36.00, 54.00, 27.00, 27.00)],
        [Observation.ValueDiscrepancyAt(0, "Area", 36.00, 54.00)]);

    /// <summary>`E-4` · el mismo cubo con área 54.00. **Cero observaciones**, y es el criterio negativo.</summary>
    internal static FigureInterpretation E4() => FigureInterpretation.From(1,
        [P(0, FigureType.Cube, 54.00, 54.00, 27.00, 27.00)], []);

    /// <summary>`E-5` · la figura del índice 1 es de un tipo desconocido: su posición queda reservada.</summary>
    internal static FigureInterpretation E5() => FigureInterpretation.From(2,
        [P(0, FigureType.Cube, 54.00, 54.00, 27.00, 27.00)],
        [Observation.ValidationErrorAt(1, "Tipo")]);

    /// <summary>`E-6` · el rectángulo de largo 0.00. **El cero es un valor**: la figura se interpreta.</summary>
    internal static FigureInterpretation E6() => FigureInterpretation.From(1,
        [P(0, FigureType.Rectangle, 0.00, 0.00)], []);

    /// <summary>`E-7` · seis piezas CON componentes, que es lo que distingue el detalle del listado.</summary>
    internal static FigureInterpretation E7() => FigureInterpretation.From(6,
        [P(0, FigureType.Cylinder, 150.80, 150.80, 141.37, 141.37,
            [Component.Declare(0, ComponentRole.Cap, FigureType.Circle, null, null, 3.00, 28.27)]),
         P(1, FigureType.Cube, 54.00, 54.00, 27.00, 27.00,
            [Component.Declare(0, ComponentRole.Face, FigureType.Square, 3.00, 3.00, null, 9.00)]),
         P(2, FigureType.Orthohedron, 208.00, 208.00, 192.00, 192.00,
            [Component.Declare(0, ComponentRole.Base, FigureType.Rectangle, 6.00, 4.00, null, 24.00)]),
         P(3, FigureType.Rectangle, 24.00, 24.00),
         P(4, FigureType.Square, 9.00, 9.00),
         P(5, FigureType.Circle, 28.27, 28.27)],
        []);

    /// <summary>`E-8` · el número entre comillas y con coma: el error se localiza en su índice y campo.</summary>
    internal static FigureInterpretation E8() => FigureInterpretation.From(2,
        [P(0, FigureType.Orthohedron, 208.00, 208.00, 192.00, 192.00)],
        [Observation.ValidationErrorAt(1, "Largo")]);
}
