namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// El DETALLE de un trabajo: lo que hace falta para verlo entero (`Contracts CU-05`).
/// </summary>
/// <remarks>
/// EL DETALLE ES EL MISMO PARA LOS DOS PAPELES, y está declarado **como valor y no como economía**
/// (`CU-05` CA-06): el administrador ve exactamente lo que el alumno entregó. Una vista distinta
/// para él abriría la posibilidad de que revise algo que el alumno nunca vio.
///
/// EL TEXTO ORIGINAL VIAJA ÍNTEGRO Y ES EL ÚNICO LUGAR DE LA SUPERFICIE DONDE VIAJA: el listado no
/// lo lleva, y por eso el detalle es el punto por el que se comprueba que se conservó carácter por
/// carácter (RN-08).
///
/// EL COMENTARIO DEL ADMINISTRADOR TIENE CAMPO PROPIO Y **NUNCA VIAJA COMO UNA OBSERVACIÓN MÁS**
/// (`CU-05` CA-08): la observación la emite el producto al interpretar el texto y hay tantas como
/// defectos; el comentario lo escribe una persona y hay a lo sumo uno. Confundirlos haría que un
/// comentario apareciera entre las advertencias de geometría del alumno. Sin valor mientras no
/// haya desenlace, y la nulidad significa exactamente eso.
///
/// LOS DOS BLOQUES QUE FALTAN, Y NO ES UN RECORTE. `CU-05` §4 declara **seis** bloques y acá hay
/// **cuatro**: faltan las piezas con sus componentes y las observaciones. En la etapa `e` **no hay
/// nada que poblar**, porque el texto no se interpreta hasta la etapa `f`. Declararlos ahora como
/// colecciones vacías obligaría a inventar la forma de la pieza, del componente y de la
/// observación, que es justamente lo que esa etapa decide; agregarlos entonces es un cambio
/// **compatible** por `Contratos-Abstractions.md` §6.
///
/// Y LO QUE NUNCA LLEVA: ningún valor de credencial, ninguna clave de firma, ninguna dirección de
/// servicio interno y ninguna traza (`CU-05` CA-05, RA-03).
/// </remarks>
/// <param name="WorkId">Identidad propia del trabajo.</param>
/// <param name="Name">Título que el alumno le dio.</param>
/// <param name="DeclaredDate">Fecha que declara el alumno, tal como la escribió.</param>
/// <param name="Description">Descripción. Sin valor cuando el alumno no escribió ninguna.</param>
/// <param name="OriginalJson">El texto del alumno, íntegro y sin ninguna normalización.</param>
/// <param name="Status">Estado, por su nombre, del conjunto cerrado de cuatro valores.</param>
/// <param name="AdministratorComment">Comentario del desenlace, en su bloque propio. Sin valor si no hay.</param>
/// <param name="OwnerId">Identidad del alumno dueño.</param>
/// <param name="OwnerEmail">Correo escrito del dueño.</param>
/// <param name="OwnerFirstName">Nombre del dueño.</param>
/// <param name="OwnerLastName">Apellido del dueño.</param>
/// <param name="CreatedAt">Momento de creación, en tiempo universal coordinado.</param>
/// <param name="UpdatedAt">Momento de la última modificación, en tiempo universal coordinado.</param>
/// <param name="RootFigureCount">El rango de posiciones del texto. **No es derivable de las piezas**, porque el conjunto admite huecos.</param>
/// <param name="Pieces">
/// Las piezas que la interpretación reconstruyó, **tal como quedaron guardadas al enviar**.
/// </param>
/// <param name="Observations">Lo que la interpretación emitió, de las dos especies.</param>
/// <remarks>
/// LAS PIEZAS Y LAS OBSERVACIONES ENTRAN EN LA ETAPA `g`, como agregado **compatible**: la vista del
/// trabajo las necesita para dibujar y para mostrar lo que el producto observó, y **ya están
/// guardadas** desde el envío. Reinterpretar el texto para dibujarlo abriría la puerta a que la
/// vista muestre algo distinto de lo que el producto guardó.
///
/// VIENEN VACÍAS EN UN TRABAJO QUE NUNCA VERIFICÓ, y eso no es lo mismo que no haber interpretado:
/// el estado lo distingue.
/// </remarks>
public sealed record WorkDetailResponse(
    Guid WorkId,
    string Name,
    string DeclaredDate,
    string? Description,
    string OriginalJson,
    string Status,
    string? AdministratorComment,
    Guid OwnerId,
    string OwnerEmail,
    string OwnerFirstName,
    string OwnerLastName,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    /// <summary>
    /// Cuántas figuras trae el texto, **incluidas las que no se pudieron reconstruir**. Nula
    /// mientras el texto no se interpretó.
    /// </summary>
    int? RootFigureCount,
    IReadOnlyList<WorkPiece> Pieces,
    IReadOnlyList<WorkObservation> Observations);
