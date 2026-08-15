using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Works;

/// <summary>
/// La PROYECCIÓN DE LISTADO de un trabajo: lo que el listado devuelve, y nada más.
/// </summary>
/// <remarks>
/// LO QUE NO DECLARA ES LO QUE IMPORTA, y es un requisito estructural y no una optimización
/// (intake §17.4.P.10; `Contracts ADR-05`): **0 campos de texto original, 0 de piezas, 0 de
/// componentes y 0 de comentario del administrador**. Que el tipo no los declare es lo que hace
/// imposible que el listado del administrador arrastre el texto completo de cada trabajo de la
/// comisión, en lugar de dejarlo librado a que ninguna consulta los pida.
///
/// EL DATO DE DUEÑO VIAJA SIEMPRE Y EN UN SOLO TIPO, sin variante por papel (`Contracts CU-04`
/// §10). Es lo que le permite al administrador **agrupar y filtrar por alumno** sin una segunda
/// solicitud (`Contracts CU-04` CA-03), y para el alumno es su propia identidad repetida, que no
/// le revela nada que no sepa.
///
/// LOS DOS RECUENTOS QUE ESTE TIPO **NO** TRAE, y por qué. `Contracts CU-04` §4 paso 3 y CA-04
/// declaran «cantidad de piezas» y «cantidad de advertencias»; `Application CU-06` §4 paso 3 y
/// `CU-07` §4 paso 4 declaran «recuento de observaciones» **y ninguna cantidad de piezas**. Son
/// dos números distintos —las observaciones son de dos especies y sólo una es advertencia— y los
/// documentos no coinciden. **La etapa `e` no elige**: no emite ninguno de los dos, la
/// contradicción queda elevada al Product Owner, y agregar el campo que él decida es un cambio
/// **compatible** por la tabla de versionado de `Contratos-Abstractions.md` §6. En esta etapa los
/// dos valdrían cero de todos modos: no hay piezas ni observaciones hasta la etapa `f`.
/// </remarks>
/// <param name="WorkId">Identidad propia del trabajo.</param>
/// <param name="Name">Título que el alumno le dio.</param>
/// <param name="DeclaredDate">Fecha que declara el alumno, tal como la escribió.</param>
/// <param name="Status">Estado del trabajo, del conjunto cerrado de cuatro valores.</param>
/// <param name="OwnerId">Identidad del alumno dueño.</param>
/// <param name="OwnerEmail">Correo escrito del dueño.</param>
/// <param name="OwnerFirstName">Nombre del dueño.</param>
/// <param name="OwnerLastName">Apellido del dueño.</param>
public sealed record WorkListEntry(
    Guid WorkId,
    string Name,
    string DeclaredDate,
    WorkStatus Status,
    Guid OwnerId,
    string OwnerEmail,
    string OwnerFirstName,
    string OwnerLastName);
