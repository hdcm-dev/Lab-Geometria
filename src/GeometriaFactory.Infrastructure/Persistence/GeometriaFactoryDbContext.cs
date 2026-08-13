using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Infrastructure.Persistence;

/// <summary>
/// Contexto de persistencia del producto. Se construye UNO POR OPERACIÓN
/// (intake §17.3.P.4; `Infrastructure/05` §3.1).
/// </summary>
/// <remarks>
/// ETAPA `a`: el contexto existe y su modelo está VACÍO. No declara ningún conjunto de
/// entidades ni aplica ningún mapeo, porque mapear exige los atributos de las cinco entidades
/// y el Product Owner ancló el modelado a la etapa `c` (`Domain BT-06`). Es el riesgo `R-02` de
/// `Plan-Etapa-A.md` §7 —`Infrastructure BT-05` de etapa `a` mapeando entidades de etapa `c`—,
/// resuelto a favor de la etapa `c`. Por el mismo motivo `Persistence/Configurations/` no existe
/// todavía y `Persistence/Migrations/` no tiene ninguna transformación generada.
///
/// Lo que sí es de la etapa `a` y está: que el contexto se construya, que se abra contra el
/// almacén y que `StorePreparation` lo prepare antes de atender la primera petición.
/// </remarks>
public sealed class GeometriaFactoryDbContext : DbContext
{
    public GeometriaFactoryDbContext(DbContextOptions<GeometriaFactoryDbContext> options)
        : base(options)
    {
    }
}
