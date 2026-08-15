using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Works;

/// <summary>
/// EL DETALLE de un trabajo: todo lo que la etapa `e` puede poblar, con su dueño.
/// </summary>
/// <remarks>
/// EL DETALLE ES EL MISMO PARA LOS DOS PAPELES, y eso está declarado **como valor y no como
/// economía** (`Api CU-07` §10): el administrador revisa exactamente lo que el alumno entregó.
/// Una vista distinta para él abriría la posibilidad de que revise algo que el alumno no vio.
///
/// LOS DOS BLOQUES QUE FALTAN, Y SU AUSENCIA ES DECLARADA. `Contracts CU-05` §4 declara **seis**
/// bloques y acá hay **cuatro**: faltan la colección de piezas con sus componentes y la de
/// observaciones. No es un recorte: **no hay nada que poblar**, porque el texto no se interpreta
/// hasta la etapa `f`. Declararlos ahora como colecciones vacías obligaría a inventar la forma de
/// `Piece`, `Component` y `Observation`, que es exactamente lo que la etapa `f` decide;
/// agregarlos entonces es un cambio **compatible** (`Contratos-Abstractions.md` §6, «agregar un
/// tipo o un campo opcional»).
///
/// EL COMENTARIO DEL ADMINISTRADOR NO ES UNA OBSERVACIÓN y viaja en su propio campo: la
/// observación la emite el producto al interpretar y hay tantas como defectos; el comentario lo
/// escribe una persona y hay a lo sumo uno.
/// </remarks>
/// <param name="WorkId">Identidad propia del trabajo.</param>
/// <param name="Name">Título que el alumno le dio.</param>
/// <param name="DeclaredDate">Fecha que declara el alumno, tal como la escribió.</param>
/// <param name="Description">Descripción. Admite ausencia.</param>
/// <param name="OriginalJson">El texto del alumno, íntegro (RN-08).</param>
/// <param name="Status">Estado del trabajo.</param>
/// <param name="AdministratorComment">Comentario del desenlace, cuando lo hay.</param>
/// <param name="OwnerId">Identidad del alumno dueño.</param>
/// <param name="OwnerEmail">Correo escrito del dueño.</param>
/// <param name="OwnerFirstName">Nombre del dueño.</param>
/// <param name="OwnerLastName">Apellido del dueño.</param>
/// <param name="CreatedAt">Momento de creación, en tiempo universal coordinado.</param>
/// <param name="UpdatedAt">Momento de la última modificación, en tiempo universal coordinado.</param>
public sealed record WorkDetail(
    Guid WorkId,
    string Name,
    string DeclaredDate,
    string? Description,
    string OriginalJson,
    WorkStatus Status,
    string? AdministratorComment,
    Guid OwnerId,
    string OwnerEmail,
    string OwnerFirstName,
    string OwnerLastName,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt)
{
    /// <summary>Compone el detalle con el trabajo y la cuenta de su dueño.</summary>
    public static WorkDetail Of(Work work, Account owner)
    {
        ArgumentNullException.ThrowIfNull(work);
        ArgumentNullException.ThrowIfNull(owner);

        return new WorkDetail(
            work.Id,
            work.Name,
            work.DeclaredDate,
            work.Description,
            work.OriginalJson,
            work.Status,
            work.AdministratorComment,
            owner.Id,
            owner.Email,
            owner.FirstName,
            owner.LastName,
            work.CreatedAt,
            work.UpdatedAt);
    }
}
