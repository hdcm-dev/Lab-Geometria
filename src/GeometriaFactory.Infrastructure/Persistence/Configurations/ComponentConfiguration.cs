using GeometriaFactory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeometriaFactory.Infrastructure.Persistence.Configurations;

/// <summary>
/// Mapeo de la tabla `Componente` de `Modelo-Datos-Logico.md` §2.4 (`Infrastructure BT-05`, etapa `f`).
/// </summary>
/// <remarks>
/// LOS COMPONENTES SE PERSISTEN PESE A SU REDUNDANCIA —un cubo de lado 3 guarda seis caras
/// idénticas para expresar un solo número— **porque son parte del ejercicio**, y el modelo declara
/// cómo se compensa: **no se cargan nunca en las consultas de listado** (intake §17.1.P.12).
///
/// LAS TRES DIMENSIONES VAN EN COLUMNAS PROPIAS Y NO EN UNA DE TEXTO, y es un **apartamiento
/// declarado** de §2.4 del modelo de datos. El fundamento vuelve a ser de su propio §1 —cuando él y
/// el modelo del dominio difieren, manda el dominio—, y el del dominio §2.4 declara «dimensiones
/// declaradas» como atributo del componente sin fijarles forma. Las tres claves que el texto del
/// alumno usa son un conjunto cerrado y chico —`Largo`, `Ancho`, `Radio`—, de modo que la razón que
/// el modelo de datos da para el texto libre —«el conjunto depende del tipo y el esquema no lo
/// fija»— no se sostiene sobre los seis tipos que el producto reconstruye. Y la comparación de
/// `CU-06002` se vuelve verificable contra el esquema en lugar de contra una cadena.
/// **[apartamiento de la etapa `f`, declarado y elevado al punto de control.]**
///
/// LAS TRES SON NULABLES PORQUE LA AUSENCIA ES UN DATO: un componente que no trae `Radio` no es un
/// componente con radio cero. Es la distinción de existencia contra veracidad, en el esquema.
/// </remarks>
public sealed class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("Componente");

        builder.Property<Guid>("Id").ValueGeneratedOnAdd();
        builder.HasKey("Id");

        builder.Property<Guid>("PieceId").IsRequired();

        builder.Property(c => c.Position).IsRequired();

        builder.Property(c => c.Role)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.DeclaredLength);
        builder.Property(c => c.DeclaredWidth);
        builder.Property(c => c.DeclaredRadius);
        builder.Property(c => c.DeclaredArea);
    }
}
