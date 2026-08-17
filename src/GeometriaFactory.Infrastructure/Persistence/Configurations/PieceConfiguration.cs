using GeometriaFactory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeometriaFactory.Infrastructure.Persistence.Configurations;

/// <summary>
/// Mapeo de la tabla `Pieza` de `Modelo-Datos-Logico.md` §2.3 (`Infrastructure BT-05`, etapa `f`).
/// </summary>
/// <remarks>
/// LA CLAVE PRIMARIA ES PROPIA Y LA POSICIÓN ES LA IDENTIDAD DE DOMINIO. Las dos cosas conviven
/// porque resuelven problemas distintos: el motor necesita una clave estable y el producto necesita
/// que la posición **no se compacte** (`RC-06002`). El índice único de trabajo y posición es lo que
/// impide que dos piezas del mismo trabajo digan ocupar el mismo lugar.
///
/// LA PIEZA SÍ LLEVA SUS DIMENSIONES, y **el apartamiento que la etapa `f` había declarado queda
/// retirado**. Entonces el modelo del dominio no le asignaba ninguna y el razonamiento era correcto
/// para las volumétricas: el cubo lleva su arista en sus caras. **No lo era para las planas del
/// conjunto raíz**, que las llevan en sí mismas —`§20.E-7`, `{ "Tipo": "Circulo", "Radio": 2.50 }`
/// sin componentes—, y con la columna ausente esa medida **se perdía al guardar**: la figura se
/// contaba en el conjunto raíz y no se podía dibujar nunca.
///
/// El modelo del dominio §2.3 las declara desde la etapa `g`, con ese fundamento, y esta
/// configuración las materializa. **Siguen sin duplicarse**: son nulas en las volumétricas, que las
/// llevan en sus componentes.
///
/// LA FAMILIA TAMPOCO SE GUARDA (`RC-06004`): se deriva del tipo.
///
/// EL CONJUNTO CERRADO DE TIPOS SE GUARDA COMO TEXTO Y POR SU NOMBRE, nunca por su posición, con el
/// mismo criterio que el estado del trabajo. Es lo que hace que agregar un séptimo tipo no cambie
/// el significado de lo ya guardado.
/// </remarks>
public sealed class PieceConfiguration : IEntityTypeConfiguration<Piece>
{
    public void Configure(EntityTypeBuilder<Piece> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("Pieza");

        builder.Property<Guid>("Id").ValueGeneratedOnAdd();
        builder.HasKey("Id");

        // Clave foránea hacia `Trabajo`, declarada desde el lado del trabajo con su arrastre.
        builder.Property<Guid>("WorkId").IsRequired();

        builder.Property(p => p.Position).IsRequired();

        builder.Property(p => p.Type)
            .HasConversion<string>()
            .IsRequired();

        // LAS DIMENSIONES DE LA PROPIA FIGURA, que el modelo del dominio agregó en la etapa `g`:
        // las planas del conjunto raíz las llevan en sí mismas y sin ellas su medida se perdía.
        builder.Property(p => p.DeclaredLength);
        builder.Property(p => p.DeclaredWidth);
        builder.Property(p => p.DeclaredRadius);

        builder.Property(p => p.DeclaredArea);
        builder.Property(p => p.DerivedArea);
        builder.Property(p => p.DeclaredVolume);
        builder.Property(p => p.DerivedVolume);

        builder.HasIndex("WorkId", nameof(Piece.Position)).IsUnique();

        builder.HasMany(p => p.Components)
            .WithOne()
            .HasForeignKey("PieceId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(p => p.Components).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
