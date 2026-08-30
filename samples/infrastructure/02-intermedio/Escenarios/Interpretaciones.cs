using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Infrastructure.Intermedio;

/// <summary>
/// Las interpretaciones de `E-1`, `E-2` y `E-5`, TRANSCRIPTAS del sample `01-basico`.
/// </summary>
/// <remarks>
/// NO SE RECALCULA NADA Y ES DELIBERADO (§5 del documento que gobierna esta carpeta): acá no se
/// instancia el intérprete. Los números de abajo son la salida que el sample `01` produjo sobre
/// estos mismos tres textos, congelada como dato.
///
/// POR QUÉ IMPORTA: este sample verifica **qué se guarda y cómo se recupera**, no qué produce el
/// intérprete. Si el intérprete corriera acá, una interpretación que cambiara movería la salida de
/// este sample y el lector no sabría cuál de las dos capas se movió. Con el dato congelado, una
/// diferencia en esta salida sólo puede venir del almacén.
/// </remarks>
internal static class Interpretaciones
{
    private static Component Cara(int posicion, double lado) =>
        Component.Declare(posicion, ComponentRole.Face, FigureType.Square, lado, lado, null, lado * lado);

    private static Component Rectangulo(int posicion, ComponentRole rol, double largo, double ancho) =>
        Component.Declare(posicion, rol, FigureType.Rectangle, largo, ancho, null, largo * ancho);

    /// <summary>Cilindro con dos tapas y un lado, cubo con discrepancia de área, ortoedro con discrepancia de volumen.</summary>
    internal static (int Figuras, IReadOnlyList<Piece> Piezas, IReadOnlyList<Observation> Observaciones) DeE1()
    {
        var cilindro = Piece.Reconstruct(0, FigureType.Cylinder, 113.1, 113.09, 84.82, 84.82300164692441,
        [
            Component.Declare(0, ComponentRole.Cap, FigureType.Circle, null, null, 3, 28.27),
            Component.Declare(1, ComponentRole.Cap, FigureType.Circle, null, null, 3, 28.27),
            Component.Declare(2, ComponentRole.Side, FigureType.DevelopedRectangle, 3, 18.85, null, 56.55),
        ]);

        var cubo = Piece.Reconstruct(1, FigureType.Cube, 36, 54, 27, 27,
            Enumerable.Range(0, 6).Select(i => Cara(i, 3)).ToList());

        var ortoedro = Piece.Reconstruct(2, FigureType.Orthohedron, 686, 686, 343, 1029, Ortoedro());

        return (3, [cilindro, cubo, ortoedro],
        [
            Observation.ValueDiscrepancyAt(1, "Area", 36, 54),
            Observation.ValueDiscrepancyAt(2, "Volumen", 343, 1029),
        ]);
    }

    /// <summary>El mismo ortoedro de `E-1`, leído de un texto con comas finales y la clave `Tapas`.</summary>
    internal static (int Figuras, IReadOnlyList<Piece> Piezas, IReadOnlyList<Observation> Observaciones) DeE2() =>
        (1, [Piece.Reconstruct(0, FigureType.Orthohedron, 686, 686, 343, 1029, Ortoedro())],
            [Observation.ValueDiscrepancyAt(0, "Volumen", 343, 1029)]);

    /// <summary>Dos figuras en el texto y una sola pieza: la segunda tiene el tipo mal y queda como error.</summary>
    internal static (int Figuras, IReadOnlyList<Piece> Piezas, IReadOnlyList<Observation> Observaciones) DeE5() =>
        (2, [Piece.Reconstruct(0, FigureType.Cube, 54, 54, 27, 27,
                Enumerable.Range(0, 6).Select(i => Cara(i, 3)).ToList())],
            [Observation.ValidationErrorAt(1, "Tipo")]);

    private static List<Component> Ortoedro() =>
    [
        Rectangulo(0, ComponentRole.Base, 7, 7),
        Rectangulo(1, ComponentRole.Base, 7, 7),
        Rectangulo(2, ComponentRole.Lateral, 21, 7),
        Rectangulo(3, ComponentRole.Lateral, 21, 7),
        Rectangulo(4, ComponentRole.Lateral, 21, 7),
        Rectangulo(5, ComponentRole.Lateral, 21, 7),
    ];
}
