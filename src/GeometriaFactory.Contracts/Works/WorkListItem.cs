namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// Un elemento de la PROYECCIÓN DE LISTADO. Deliberadamente pobre (`Contracts CU-04`).
/// </summary>
/// <remarks>
/// LO QUE ESTE TIPO NO DECLARA ES SU RAZÓN DE SER: **0 campos de texto original, 0 de pieza, 0 de
/// componente y 0 de comentario del administrador** (`CU-04` CA-01; intake §17.4.P.10;
/// `Contracts ADR-05`). No es una optimización, es lo que hace verificable el requisito: con un
/// único punto de lectura, la exigencia de que el listado no arrastre el texto completo de cada
/// trabajo de la comisión no tendría dónde comprobarse.
///
/// UN SOLO TIPO PARA LOS DOS PAPELES, SIN VARIANTE (`CU-04` §10). Lo que cambia entre el alumno y
/// el administrador es **el alcance de la colección**, no la forma de sus elementos: el alumno
/// recibe sus cuatro estados y el administrador la comisión entera **menos los borradores**.
///
/// EL DATO DE DUEÑO VIAJA EN CADA ELEMENTO Y NO EN UNA SEGUNDA SOLICITUD (`CU-04` CA-03): es lo
/// que le permite al administrador agrupar y filtrar por alumno sin volver a preguntar.
///
/// LOS DOS RECUENTOS QUE FALTAN Y POR QUÉ NO SE ELIGE NINGUNO. `Contracts CU-04` §4 paso 3 y
/// CA-04 declaran «cantidad de piezas» y «cantidad de advertencias»; `Application CU-06` §4
/// paso 3 y `CU-07` §4 paso 4 declaran «recuento de observaciones» y **ninguna cantidad de
/// piezas**. Son números distintos —las observaciones tienen dos especies y sólo una es
/// advertencia—, los documentos se contradicen, y **la etapa `e` no elige por su cuenta**: no
/// emite ninguno, deja la contradicción elevada al Product Owner, y agregar el campo que él
/// decida es un cambio compatible. En esta etapa los dos valdrían cero.
/// </remarks>
/// <param name="WorkId">Identidad propia del trabajo.</param>
/// <param name="Name">Título que el alumno le dio.</param>
/// <param name="DeclaredDate">Fecha que declara el alumno, tal como la escribió.</param>
/// <param name="Status">Estado, por su nombre, del conjunto cerrado de cuatro valores.</param>
/// <param name="OwnerId">Identidad del alumno dueño.</param>
/// <param name="OwnerEmail">Correo escrito del dueño.</param>
/// <param name="OwnerFirstName">Nombre del dueño.</param>
/// <param name="OwnerLastName">Apellido del dueño.</param>
public sealed record WorkListItem(
    Guid WorkId,
    string Name,
    string DeclaredDate,
    string Status,
    Guid OwnerId,
    string OwnerEmail,
    string OwnerFirstName,
    string OwnerLastName);
