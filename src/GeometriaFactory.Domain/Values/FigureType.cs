namespace GeometriaFactory.Domain.Values;

/// <summary>
/// Discriminante de figura que el texto del alumno declara. Conjunto cerrado de siete valores.
/// </summary>
/// <remarks>
/// SEIS SON TIPOS DE PIEZA Y EL SÉPTIMO NO LO ES.
/// <see cref="DevelopedRectangle"/> aparece **sólo como componente** —el `Lado` del cilindro— y
/// ninguna fuente lo documenta como salida real en el conjunto raíz
/// (`Definicion-Contrato-Del-Validador-De-Figuras.md` §5). Vive en el mismo conjunto porque es el
/// mismo discriminante `Tipo` del texto, y quien reconstruye una pieza comprueba
/// <see cref="IsPieceType"/> en lugar de suponer que los siete valen.
///
/// LA FAMILIA —plana o volumétrica— NO ES UN ATRIBUTO Y NO SE GUARDA: se deriva del tipo
/// (`CU-06001` §10, `RC-06004`). Por eso está acá como predicado y no como columna.
///
/// SE SERIALIZA POR NOMBRE, nunca por posición (`Norma-De-Nomenclatura.md` §6.4 y `RC-06002`).
/// El nombre inglés es el del tipo geométrico; **el valor que el texto del alumno trae es dato del
/// alumno** y se lee tal cual, con las dos sinonimias que el propio texto impone —`Cuadrado` y
/// `Rectangulo` para la misma cara del cubo (T3)—.
/// </remarks>
public enum FigureType
{
    /// <summary>Etiqueta del emisor: `Cilindro`. Volumétrico.</summary>
    Cylinder = 1,

    /// <summary>Etiqueta del emisor: `Cubo`. Volumétrico.</summary>
    Cube = 2,

    /// <summary>Etiqueta del emisor: `Ortoedro`. Volumétrico.</summary>
    Orthohedron = 3,

    /// <summary>Etiqueta del emisor: `Rectangulo`. Plano.</summary>
    Rectangle = 4,

    /// <summary>Etiqueta del emisor: `Cuadrado`. Plano.</summary>
    Square = 5,

    /// <summary>Etiqueta del emisor: `Circulo`. Plano.</summary>
    Circle = 6,

    /// <summary>
    /// Etiqueta del emisor: `RectanguloDesarrollado`. Plano, y **sólo componente**: es la
    /// superficie lateral del cilindro desenrollada, con `Ancho = 2πr` y `Largo = altura`.
    /// </summary>
    DevelopedRectangle = 7
}
