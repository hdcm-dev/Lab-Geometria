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
/// LA PIEZA NO LLEVA COLUMNA DE DIMENSIONES, y es un **apartamiento declarado** de §2.3 del modelo
/// de datos, que sí la lista. El fundamento es del propio modelo de datos: su §1 declara que cuando
/// él y `Definicion-Modelo-De-Dominio.md` difieren **manda el modelo del dominio**, y §2.3 del
/// modelo del dominio enumera siete atributos de la pieza y **ninguno es un conjunto de
/// dimensiones**: las dimensiones viven en sus componentes. Guardarlas también acá crearía el
/// segundo lugar donde el mismo dato puede decir otra cosa, que es lo que `RC-06004` evita para la
/// familia. **[apartamiento de la etapa `f`, declarado y elevado al punto de control.]**
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
