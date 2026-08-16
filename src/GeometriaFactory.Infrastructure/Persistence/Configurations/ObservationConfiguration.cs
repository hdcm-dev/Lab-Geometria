using GeometriaFactory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeometriaFactory.Infrastructure.Persistence.Configurations;

/// <summary>
/// Mapeo de la tabla `Observacion` de `Modelo-Datos-Logico.md` §2.5 (`Infrastructure BT-05`, etapa `f`).
/// </summary>
/// <remarks>
/// LA OBSERVACIÓN CUELGA DEL TRABAJO Y NO DE LA PIEZA, y ésa es **la decisión de modelado que hace
/// posible observar una figura que no se pudo reconstruir**: designa una posición, que puede no
/// tener pieza. Colgarla de la pieza dejaría sin poder guardar exactamente la observación que más
/// le importa al alumno.
///
/// LA POSICIÓN ES NULABLE, y es un **apartamiento declarado** de §2.5 del modelo de datos, que la
/// declara no nulable. El fundamento es de su propio §1 —manda el modelo del dominio— y del caso de
/// uso: `Definicion-Modelo-De-Dominio.md` §2.5 la declara «obligatoria **cuando la observación es
/// atribuible a una figura**», y `CU-06001` FA-03 y FA-04 declaran dos observaciones que no lo son
/// —el conjunto raíz vacío y el texto que no se pudo leer—. Con la columna no nulable, esas dos no
/// se pueden guardar, y son **lo único que el alumno tiene para entender qué pasó** cuando su texto
/// no se pudo interpretar. La alternativa —un valor centinela como `-1`— guardaría una posición
/// falsa en la columna que RN-02009 existe para hacer confiable.
/// **[apartamiento de la etapa `f`, declarado y elevado al punto de control.]**
///
/// LA ESPECIE SE GUARDA COMO TEXTO Y POR SU NOMBRE, conjunto cerrado de dos valores. Sólo el error
/// de validación impide el paso a estado `Pendiente`.
///
/// LOS DOS VALORES VAN JUNTOS O NO VAN (`RC-06003`): son de la advertencia de discrepancia, y por
/// eso los dos son nulables. Quien los exige es el dominio, en la adopción.
/// </remarks>
public sealed class ObservationConfiguration : IEntityTypeConfiguration<Observation>
{
    public void Configure(EntityTypeBuilder<Observation> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("Observacion");

        builder.Property<Guid>("Id").ValueGeneratedOnAdd();
        builder.HasKey("Id");

        builder.Property<Guid>("WorkId").IsRequired();

        builder.Property(o => o.Kind)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(o => o.PiecePosition);

        builder.Property(o => o.Field).IsRequired();

        builder.Property(o => o.DeclaredValue);
        builder.Property(o => o.DerivedValue);
    }
}
