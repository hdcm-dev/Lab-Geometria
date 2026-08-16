using GeometriaFactory.Application.Ports;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Infrastructure.Persistence;

/// <summary>
/// CU-03 y CU-04 — Único adaptador del puerto de repositorio de trabajos (`Infrastructure BT-10`).
/// </summary>
/// <remarks>
/// UNA UNIDAD DE TRABAJO POR OPERACIÓN (`Infrastructure ADR-02`): cada escritura confirma la
/// suya. El contexto es de alcance de petición y no se comparte.
///
/// NO HAY REPOSITORIO GENÉRICO (`ADR-01`): los miembros son los que el caso de uso necesita y las
/// dos consultas de listado están escritas, que es lo que permite verlas y verificarlas.
///
/// ESTE ADAPTADOR NO AUTORIZA (`Infrastructure CU-03` §10). No comprueba quién pide, ni desde qué
/// estado, ni si el trabajo es del solicitante: recibe pedidos **ya acotados** y los resuelve.
/// Quien decide es el dominio sobre el dato recuperado. La consecuencia de forma es que
/// <see cref="FindByIdAsync"/> devuelve el trabajo de cualquiera, y por eso ningún caso de uso lo
/// llama sin resolver después la pertenencia o el alcance.
///
/// EL RECORTE DEL ADMINISTRADOR SALE DEL DOMINIO Y NO SE REESCRIBE ACÁ. La consulta compara
/// contra <see cref="Work.StatusOutsideAdministratorScope"/>, que es el predicado que
/// `Domain CU-11` declara, en lugar de repetir «distinto de borrador». Es la diferencia entre una
/// regla con una fuente y una regla con dos.
///
/// EL DUEÑO SE RESUELVE EN LA MISMA CONSULTA Y NO EN UNA SEGUNDA (`Contracts CU-04` CA-03): las
/// dos proyecciones de listado se componen contra la tabla de cuentas dentro del mismo pedido.
/// **[decisión de la etapa `e`, declarada]**: `Application CU-07` §2 nombra al repositorio de
/// cuentas como quien aporta el dueño y su §4 paso 4 dice que el listado ya lo trae; se resuelve
/// del lado que el criterio de aceptación del contrato exige, y queda elevado al Product Owner.
///
/// Y LA PROYECCIÓN SE ARMA EN EL MOTOR, NO EN MEMORIA: la consulta selecciona los ocho campos de
/// <see cref="WorkListEntry"/> y **el texto original nunca se lee**. Traer la entidad entera para
/// proyectarla después dejaría el listado del administrador cargando el texto completo de cada
/// trabajo de la comisión, que es exactamente lo que el intake §17.4.P.10 prohíbe.
/// </remarks>
public sealed class EfCoreWorkRepository : IWorkRepository
{
    private readonly GeometriaFactoryDbContext _dbContext;

    public EfCoreWorkRepository(GeometriaFactoryDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        _dbContext = dbContext;
    }

    /// <summary>
    /// Trae el trabajo **con su interpretación**: sus piezas, sus componentes y sus observaciones.
    /// </summary>
    /// <remarks>
    /// LAS COLECCIONES SE CARGAN Y NO ES UN LUJO: la etapa `f` **reemplaza** la interpretación en
    /// cada envío, y reemplazar lo que no se cargó no borra nada. Sin estas tres cargas, el
    /// segundo envío de un trabajo intenta insertar una pieza en una posición que la del envío
    /// anterior sigue ocupando, y el índice único de `trabajo, posición` lo rechaza. Es un defecto
    /// que **no se ve en la primera entrega y aparece en la segunda**.
    ///
    /// ESTA CARGA ES DE LA CONSULTA POR IDENTIDAD Y NO DE LOS LISTADOS. El modelo de datos declara
    /// que los componentes se persisten pese a su redundancia y que eso se compensa **no
    /// cargándolos nunca en las consultas de listado** (intake §17.1.P.12): las dos proyecciones de
    /// listado de este mismo repositorio siguen sin tocarlos.
    /// </remarks>
    public Task<Work?> FindByIdAsync(Guid workId, CancellationToken cancellationToken = default) =>
        _dbContext.Works
            .Include(work => work.Pieces)
                .ThenInclude(piece => piece.Components)
            .Include(work => work.Observations)
            .FirstOrDefaultAsync(work => work.Id == workId, cancellationToken);

    public async Task AddAsync(Work work, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(work);

        await _dbContext.Works.AddAsync(work, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(Work work, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(work);

        _dbContext.Works.Update(work);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task RemoveAsync(Work work, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(work);

        // RETIRO FÍSICO Y DEFINITIVO: no hay marca de borrado lógico, no hay papelera y no hay
        // historial (`RE-15`). Cuando existan las piezas, los componentes y las observaciones,
        // los arrastran las claves foráneas de `RE-09`, `RE-11` y `RE-12`, en esta misma unidad
        // de trabajo y no en escrituras aparte.
        _dbContext.Works.Remove(work);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// La proyección de listado del alumno, ordenada por fecha de creación descendente.
    /// </summary>
    /// <remarks>
    /// EL ORDEN LO FIJA EL ADAPTADOR Y NO EL CASO DE USO, con el mismo criterio con el que lo fijó
    /// el listado de cuentas: **ninguna fuente declara un orden para el listado de trabajos**. Se
    /// toma el de la última entrega primero, que es el que un alumno espera de su propio panel.
    /// **[decisión de la etapa `e`, declarada]**
    /// </remarks>
    public async Task<IReadOnlyList<WorkListEntry>> ListOwnedByAsync(
        Guid ownerId,
        CancellationToken cancellationToken = default) =>
        await Project(_dbContext.Works.Where(work => work.OwnerId == ownerId))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// La proyección de listado del administrador: la comisión entera **menos los borradores**,
    /// con filtro opcional por alumno.
    /// </summary>
    /// <remarks>
    /// EL ORDEN AGRUPA POR ALUMNO Y DESPUÉS POR FECHA, y no es una preferencia de presentación:
    /// el criterio del roadmap dice «agrupados y filtrados por alumno», y un listado que llega
    /// agrupado se puede recorrer sin reordenarlo del otro lado. El agrupamiento es por correo
    /// normalizado, que es la forma que decide la identidad y que no depende de la cultura.
    /// **[decisión de la etapa `e`, declarada]**
    ///
    /// EL RECORTE SE APLICA ANTES QUE EL FILTRO, y ése es el orden que importa: el filtro por
    /// alumno acota lo que el alcance ya dejó pasar. No hay valor del parámetro que lo amplíe.
    /// </remarks>
    public async Task<IReadOnlyList<WorkListEntry>> ListInAdministratorScopeAsync(
        Guid? ownerFilter,
        CancellationToken cancellationToken = default)
    {
        var scoped = _dbContext.Works
            .Where(work => work.Status != Work.StatusOutsideAdministratorScope);

        if (ownerFilter is { } filter)
        {
            scoped = scoped.Where(work => work.OwnerId == filter);
        }

        return await ProjectGroupedByOwner(scoped).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>La proyección del alumno: los ocho campos, sin tocar el texto original.</summary>
    private IQueryable<WorkListEntry> Project(IQueryable<Work> works) =>
        from work in works
        join owner in _dbContext.Accounts on work.OwnerId equals owner.Id
        orderby work.CreatedAt descending
        select new WorkListEntry(
            work.Id,
            work.Name,
            work.DeclaredDate,
            work.Status,
            owner.Id,
            owner.Email,
            owner.FirstName,
            owner.LastName);

    /// <summary>La proyección del administrador: los mismos ocho campos, agrupados por alumno.</summary>
    private IQueryable<WorkListEntry> ProjectGroupedByOwner(IQueryable<Work> works) =>
        from work in works
        join owner in _dbContext.Accounts on work.OwnerId equals owner.Id
        orderby owner.NormalizedEmail, work.CreatedAt descending
        select new WorkListEntry(
            work.Id,
            work.Name,
            work.DeclaredDate,
            work.Status,
            owner.Id,
            owner.Email,
            owner.FirstName,
            owner.LastName);
}
