using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Lo que el validador de figuras devuelve: **tres cosas y no dos**.
/// </summary>
/// <remarks>
/// LA TERCERA ES LA QUE SE OLVIDA (`Definicion-Contrato-Del-Validador-De-Figuras.md` §3).
/// <see cref="RootFigureCount"/> **no es derivable de <see cref="Pieces"/>**, porque el conjunto
/// admite huecos: es el rango de posiciones válidas, y sin él el dominio no tiene contra qué
/// comprobar que la posición de una observación existe (RN-02009).
///
/// Y UNA COSA QUE NO DEVUELVE: **el estado del trabajo**. El contrato entrega el conjunto de
/// observaciones y **el dominio resuelve el estado** con <see cref="Domain.Entities.Work.Submit"/>.
/// Un validador que decidiera el estado tendría dentro una regla de negocio que no le pertenece.
///
/// TAMPOCO DEVUELVE EL TEXTO: quien lo tiene ya lo tiene, y devolverlo abriría la puerta a
/// devolverlo distinto. G-1 se cumple por construcción —este contrato no recibe nada que pueda
/// escribir— y `CU-06001` CA-09 lo verifica igual sobre el texto del llamador.
/// </remarks>
public sealed class FigureInterpretation
{
    private FigureInterpretation(
        int rootFigureCount,
        IReadOnlyList<Piece> pieces,
        IReadOnlyList<Observation> observations)
    {
        RootFigureCount = rootFigureCount;
        Pieces = pieces;
        Observations = observations;
    }

    /// <summary>
    /// Cuántas figuras trae el conjunto raíz, **incluidas las que no se pudieron reconstruir**.
    /// </summary>
    public int RootFigureCount { get; }

    /// <summary>Las piezas reconstruidas, cada una con su posición en el conjunto raíz.</summary>
    public IReadOnlyList<Piece> Pieces { get; }

    /// <summary>Las observaciones emitidas, de las dos especies.</summary>
    public IReadOnlyList<Observation> Observations { get; }

    /// <summary>Compone un resultado de interpretación.</summary>
    public static FigureInterpretation From(
        int rootFigureCount,
        IReadOnlyList<Piece> pieces,
        IReadOnlyList<Observation> observations) =>
        new(rootFigureCount, pieces, observations);
}
