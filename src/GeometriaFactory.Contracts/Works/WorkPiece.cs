namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// Una pieza reconstruida tal como cruza la frontera hacia quien la dibuja.
/// </summary>
/// <remarks>
/// ES LO QUE EL VISOR RECIBE, y es la decisión de [`ADR-08006`]: el bundle **deja de recibir el
/// texto del alumno** y recibe las figuras ya interpretadas. Interpretar el formato es del
/// laboratorio, y tenerlo en dos lados es tener dos verdades sobre el mismo texto.
///
/// LA POSICIÓN VIAJA Y NO SE RECALCULA. Es la identidad de la pieza —el texto del alumno no trae
/// identificador— y **el conjunto admite huecos**: la figura que no se pudo reconstruir no está
/// acá, y su posición **no la ocupa la siguiente**. Quien dibuje y quien seleccione se apoyan en
/// ese número.
///
/// VIAJAN LOS DOS VALORES, DECLARADO Y DERIVADO, aunque el visor no los use para dibujar: son lo
/// que el alumno compara contra su propio programa, y quien arma la vista los tiene sin pedir otra
/// cosa. **El visor no los recalcula ni los juzga**: no es su trabajo.
///
/// NO VIAJA EL TEXTO ORIGINAL. Quien lo tiene ya lo tiene, y mandarlo de vuelta abriría un segundo
/// lugar donde podría venir alterado (RN-02008).
/// </remarks>
/// <param name="Position">Lugar de la figura en el conjunto raíz. **Es su identidad.**</param>
/// <param name="Type">El discriminante que el texto declara, por su nombre.</param>
/// <param name="DeclaredArea">El área que trae el texto. Nula si no la trae.</param>
/// <param name="DerivedArea">El área recalculada. Nula si el texto no permite derivarla.</param>
/// <param name="DeclaredVolume">El volumen que trae el texto. Nulo en las figuras planas.</param>
/// <param name="DerivedVolume">El volumen recalculado. Nulo en las figuras planas.</param>
/// <param name="Components">Las figuras planas que la forman. Vacío en las piezas planas.</param>
public sealed record WorkPiece(
    int Position,
    string Type,
    double? DeclaredArea,
    double? DerivedArea,
    double? DeclaredVolume,
    double? DerivedVolume,
    IReadOnlyList<WorkPieceComponent> Components);

/// <summary>
/// Un componente de una pieza, con las dimensiones desde las que se construye la malla.
/// </summary>
/// <remarks>
/// LAS TRES DIMENSIONES SON NULABLES PORQUE LA AUSENCIA ES UN DATO: un componente que no trae
/// `Radio` no es uno con radio cero. Y **un `0.00` presente es una dimensión legible**: la figura
/// no se descarta por tenerlo, que es el criterio de existencia contra veracidad.
///
/// EL TIPO SE CONSERVA COMO LLEGÓ y no se unifica: la cara del cubo viaja como `Cuadrado` o como
/// `Rectangulo` según qué programa la emitió. Los dos nombran la misma forma y **el producto no
/// elige uno**, porque el alumno ve en su código el que su programa escribió.
/// </remarks>
/// <param name="Position">Lugar dentro del conjunto de componentes de su pieza.</param>
/// <param name="Role">Tapa, cara, base, lateral o lado, con el vocabulario del emisor.</param>
/// <param name="Type">El discriminante que el texto declara, por su nombre.</param>
/// <param name="DeclaredLength">La clave `Largo`. Nula si el texto no la trae.</param>
/// <param name="DeclaredWidth">La clave `Ancho`. Nula si el texto no la trae.</param>
/// <param name="DeclaredRadius">La clave `Radio`. Nula si el texto no la trae.</param>
/// <param name="DeclaredArea">La clave `Area`. Nula si el texto no la trae.</param>
public sealed record WorkPieceComponent(
    int Position,
    string Role,
    string Type,
    double? DeclaredLength,
    double? DeclaredWidth,
    double? DeclaredRadius,
    double? DeclaredArea);
