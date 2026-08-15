using GeometriaFactory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Infrastructure.Persistence;

/// <summary>
/// Contexto de persistencia del producto. Se construye UNO POR OPERACIÓN
/// (intake §17.3.P.4; `Infrastructure/05` §3.1).
/// </summary>
/// <remarks>
/// ETAPA `c`: el modelo declaró la PRIMERA de las cinco entidades, `Account`.
///
/// ETAPA `e`: entra la SEGUNDA, `Work`, que es la que las capacidades de esta etapa necesitan.
/// Las tres que faltan —`Piece`, `Component` y `Observation`— siguen sin atributos y sin mapeo,
/// porque su modelado está anclado a la etapa `f`, que es la que interpreta el texto del alumno:
/// declararlas acá crearía tablas para un dominio que todavía no existe, y una transformación de
/// esquema ya fusionada no se edita (intake §17.3.P.7).
///
/// El mapeo se toma por ensamblado y no entrada por entrada: agregar una configuración nueva no
/// exige acordarse de registrarla acá, que es exactamente la clase de olvido silencioso que el
/// producto trata de eliminar.
/// </remarks>
public sealed class GeometriaFactoryDbContext : DbContext
{
    public GeometriaFactoryDbContext(DbContextOptions<GeometriaFactoryDbContext> options)
        : base(options)
    {
    }

    /// <summary>Las cuentas de la comisión, alumnos y administrador por igual.</summary>
    public DbSet<Account> Accounts => Set<Account>();

    /// <summary>Los trabajos de la comisión, en sus cuatro estados.</summary>
    public DbSet<Work> Works => Set<Work>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeometriaFactoryDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
