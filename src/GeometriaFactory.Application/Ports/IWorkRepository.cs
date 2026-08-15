using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Repositorio de trabajos. Puerto DECLARADO por el intake §13, §14 y §17.2.P.1.
/// </summary>
/// <remarks>
/// ETAPA `a`: el puerto se DECLARÓ y sus miembros no se escribieron, porque transportaban
/// atributos de `Work`, que no estaba modelada.
///
/// ETAPA `e`: los miembros se escriben y el puerto se conecta con su adaptador,
/// `EfCoreWorkRepository` (`Infrastructure BT-10`). **No entra ningún puerto nuevo**: los del
/// producto siguen siendo cuatro y la puerta `QG-10` sigue cuadrando.
///
/// DOS FORMAS DE LECTURA Y NO UNA (`Infrastructure/05 Contratos-Abstractions` OP-01;
/// `Contracts ADR-05`): la **proyección de listado** —<see cref="WorkListEntry"/>, sin texto
/// original, sin piezas, sin componentes y sin comentario— y el **detalle completo**, que
/// devuelve la entidad. Con un solo camino de lectura, la exigencia de que el listado no arrastre
/// el texto de cada trabajo no tendría dónde comprobarse.
///
/// SIN RECORTE NO HAY CONSULTA, Y ES ESTRUCTURAL. La condición `QUERY_WITHOUT_DECLARED_SCOPE`
/// (`Infrastructure CU-03` §6) describe un listado pedido sin dueño y sin predicado de alcance:
/// **este puerto no declara ninguna operación que pueda pedirlo**. Las dos consultas de listado
/// llevan su recorte en el nombre y en los parámetros, y **el conjunto completo de trabajos de la
/// comisión nunca cruza la frontera**. Por eso la condición queda sin camino que la produzca, y
/// se declara acá en lugar de dejar la ausencia sin explicar.
///
/// EL ARRASTRE DE LA BAJA DE CUENTA NO ESTÁ ACÁ: es del otro puerto (`IAccountRepository`), y
/// tiene que seguir siendo **una sola unidad de trabajo** con el retiro de la cuenta (RN-07,
/// `RETIRO_PARCIAL_NO_ADMITIDO`). Partirlo en dos llamadas abriría la baja a medias que
/// `Infrastructure CU-04` prohíbe.
/// </remarks>
public interface IWorkRepository
{
    /// <summary>Recupera un trabajo entero por su identidad, o nulo si no existe.</summary>
    /// <remarks>
    /// DEVUELVE «NADA ENCONTRADO» Y NUNCA UN ERROR DE AUTORIZACIÓN (`Infrastructure CU-03`
    /// FA-01): quién puede verlo lo decide el dominio sobre el dato recuperado, no el adaptador.
    /// </remarks>
    Task<Work?> FindByIdAsync(Guid workId, CancellationToken cancellationToken = default);

    /// <summary>Materializa un trabajo recién constituido, en una única unidad de trabajo.</summary>
    Task AddAsync(Work work, CancellationToken cancellationToken = default);

    /// <summary>Materializa el cambio sobre un trabajo existente, en una única unidad de trabajo.</summary>
    /// <remarks>
    /// EL TEXTO ORIGINAL SE ESCRIBE UNA SOLA VEZ, AL CREARSE, salvo que la operación sea la
    /// reedición, que es el único camino por el que el alumno vuelve a pegar **otro texto suyo**
    /// (RN-08 §3). Ninguna escritura del producto lo reemplaza por una versión corregida.
    /// </remarks>
    Task UpdateAsync(Work work, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retira un trabajo con todo lo que cuelga de él, en una única unidad de trabajo.
    /// </summary>
    /// <remarks>
    /// EL RETIRO ES FÍSICO Y DEFINITIVO: no hay marca de borrado lógico, no hay papelera y no hay
    /// historial (`Infrastructure CU-04`; `RE-15`). Es comprobable **por ausencia**.
    /// </remarks>
    Task RemoveAsync(Work work, CancellationToken cancellationToken = default);

    /// <summary>
    /// La proyección de listado de los trabajos **de un dueño**, en sus cuatro estados.
    /// </summary>
    /// <remarks>
    /// EL RECORTE VIAJA EN EL PEDIDO Y NO SE APLICA DESPUÉS (`Application CU-06` §4 paso 2): se
    /// piden los trabajos cuyo dueño es ese alumno, y **no se trae un conjunto mayor para
    /// filtrarlo acá**. El alumno ve sus borradores; es su propio trabajo.
    /// </remarks>
    Task<IReadOnlyList<WorkListEntry>> ListOwnedByAsync(
        Guid ownerId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// La proyección de listado de los trabajos que **entran en el alcance del administrador**,
    /// con filtro opcional por alumno resuelto en el mismo pedido.
    /// </summary>
    /// <remarks>
    /// EL RECORTE DE BORRADORES RIGE TAMBIÉN DENTRO DEL FILTRO (`Application CU-07` FA-02): el
    /// filtro por alumno **acota** lo que el alcance ya dejó pasar, y no lo amplía. No existe
    /// ningún valor del parámetro con el que pedir un borrador ajeno.
    ///
    /// EL PREDICADO SALE DEL DOMINIO —<see cref="Work.StatusOutsideAdministratorScope"/>— y el
    /// adaptador lo usa tal cual, para que RN-11 no tenga un segundo lugar donde decir otra cosa.
    /// </remarks>
    Task<IReadOnlyList<WorkListEntry>> ListInAdministratorScopeAsync(
        Guid? ownerFilter,
        CancellationToken cancellationToken = default);
}
