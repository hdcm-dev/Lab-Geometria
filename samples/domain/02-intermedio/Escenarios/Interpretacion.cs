using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Intermedio.Escenarios;

/// <summary>
/// El resultado de interpretación que el consumidor le entrega al dominio, compuesto a mano
/// para cada uno de los seis escenarios.
/// </summary>
/// <remarks>
/// ESTO NO ES UN INTÉRPRETE, Y QUE SE VEA ASÍ DE EXPLÍCITO ES EL PUNTO DEL SAMPLE.
/// El dominio **no produce** el conjunto de piezas y observaciones: lo **adopta ya producido**
/// (`Contratos-Abstractions.md` §3, `OP-06` y `OP-07`). La frontera que este archivo enseña es
/// exactamente ésa — todo lo que sigue lo arma el consumidor, y el dominio sólo lo recibe.
///
/// Los valores salen de los textos de `Escenarios/*.txt`, transcriptos del `PRODUCT-INTAKE` §20
/// sin modificación. Ninguno se inventa acá.
/// </remarks>
internal sealed record Interpretacion(
    int FigurasRaiz,
    IReadOnlyList<Piece> Piezas,
    IReadOnlyList<Observation> Observaciones)
{
    /// <summary>`E-1` · dos figuras bien formadas y una tercera con área declarada distinta de la derivada.</summary>
    internal static Interpretacion E1() => new(
        FigurasRaiz: 3,
        Piezas:
        [
            Piece.Reconstruct(0, FigureType.Cylinder, 113.10, 113.10, 84.82, 84.82),
            Piece.Reconstruct(1, FigureType.Cube, 54.00, 54.00, 27.00, 27.00),
            Piece.Reconstruct(2, FigureType.Orthohedron, 208.00, 208.00, 192.00, 192.00),
        ],
        Observaciones:
        [
            Observation.ValueDiscrepancyAt(0, "Area", 113.10, 113.10),
            Observation.ValueDiscrepancyAt(1, "Volumen", 27.00, 27.00),
        ]);

    /// <summary>`E-3` · el cubo de lado 3 con área declarada 36.00, que NO coincide con la derivada 54.00.</summary>
    internal static Interpretacion E3() => new(
        FigurasRaiz: 1,
        Piezas: [Piece.Reconstruct(0, FigureType.Cube, 36.00, 54.00, 27.00, 27.00)],
        Observaciones: [Observation.ValueDiscrepancyAt(0, "Area", 36.00, 54.00)]);

    /// <summary>
    /// `E-4` · **el mismo cubo de lado 3**, con área declarada 54.00, que sí coincide. **Cero
    /// observaciones**, y es el criterio negativo que el intake §20.E-4 declara.
    /// </summary>
    internal static Interpretacion E4() => new(
        FigurasRaiz: 1,
        Piezas: [Piece.Reconstruct(0, FigureType.Cube, 54.00, 54.00, 27.00, 27.00)],
        Observaciones: []);

    /// <summary>
    /// `E-5` · dos figuras, y la del índice 1 es de un tipo que el conjunto cerrado no tiene.
    /// **No se adopta como pieza y su posición queda reservada** (`RN-02009`).
    /// </summary>
    internal static Interpretacion E5() => new(
        FigurasRaiz: 2,
        Piezas: [Piece.Reconstruct(0, FigureType.Cube, 54.00, 54.00, 27.00, 27.00)],
        Observaciones: [Observation.ValidationErrorAt(1, "Tipo")]);

    /// <summary>`E-6` · un rectángulo de largo 0.00. **El cero es un valor y no una ausencia.**</summary>
    internal static Interpretacion E6() => new(
        FigurasRaiz: 1,
        Piezas: [Piece.Reconstruct(0, FigureType.Rectangle, 0.00, 0.00, null, null,
            declaredLength: 0.00, declaredWidth: 5.00)],
        Observaciones: []);

    /// <summary>
    /// `E-8` · la segunda figura trae `"3,50"` entre comillas y con coma decimal: no es un número
    /// legible, y el error **se localiza en su índice y en su campo**.
    /// </summary>
    internal static Interpretacion E8() => new(
        FigurasRaiz: 2,
        Piezas: [Piece.Reconstruct(0, FigureType.Orthohedron, 208.00, 208.00, 192.00, 192.00)],
        Observaciones: [Observation.ValidationErrorAt(1, "Largo")]);
}
