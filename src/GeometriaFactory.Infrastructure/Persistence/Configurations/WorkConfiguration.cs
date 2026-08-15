using System.Globalization;
using GeometriaFactory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GeometriaFactory.Infrastructure.Persistence.Configurations;

/// <summary>
/// Mapeo de la tabla `Trabajo` de `Modelo-Datos-Logico.md` §2.2 (`Infrastructure BT-05`).
/// </summary>
/// <remarks>
/// EL CONJUNTO CERRADO DE ESTADOS SE GUARDA COMO TEXTO Y POR SU NOMBRE, nunca por su posición
/// (intake §17.3.P.4; `Contratos-REST.md` §2.2). Es `RE-07`. Guardarlo por posición ataría el
/// dato a un orden de declaración que cualquiera puede cambiar sin darse cuenta.
///
/// LA FECHA DECLARADA VA COMO TEXTO Y NO COMO MOMENTO, y es la decisión que más fácil se rompe
/// «mejorándola»: **la escribe el alumno y no es un sello** (`RC-06`). Guardarla como momento
/// obligaría a elegirle una zona horaria que la persona no declaró, y a devolvérsela convertida.
/// Los otros dos tiempos —creación y última modificación— sí son sellos del sistema y van en
/// tiempo universal coordinado.
///
/// UN SOLO ÍNDICE, Y ES `IX-03`: dueño compuesto con estado. Sostiene **las dos** consultas de
/// listado del producto con una sola estructura —la del alumno, acotada por dueño; la del
/// administrador, acotada por estado y agrupable por dueño—. `Modelo-Datos-Logico.md` §3 declara
/// seis índices para las cinco tablas y **éste es el único que le toca a esta**.
///
/// LA CLAVE FORÁNEA DE DUEÑO ARRASTRA EL RETIRO (`RE-06`, RN-07). Es lo que hace que la baja de
/// una cuenta se lleve sus trabajos **en la misma unidad de trabajo** y que el retiro físico sea
/// comprobable por ausencia. La relación se declara **sin propiedad de navegación en `Work`**:
/// la entidad de dominio no conoce a `Account`, sólo su identidad (INV-02), y agregarle una
/// referencia le daría al dominio un camino para recorrer el conjunto de cuentas que
/// `Domain ADR-06` le niega.
///
/// LAS OTRAS TRES TABLAS DEL MODELO SIGUEN SIN MAPEO, con el mismo fundamento con el que la etapa
/// `c` dejó fuera a ésta: `Piece`, `Component` y `Observation` siguen sin atributos porque su
/// modelado está anclado a la etapa `f`, que es la que interpreta el texto. Declararlas acá
/// crearía tablas para un dominio que todavía no existe, y **una transformación de esquema ya
/// fusionada no se edita** (intake §17.3.P.7).
///
/// Y POR ESO NO ESTÁN TRES COLUMNAS DE §2.2 DEL MODELO DE DATOS, con su motivo escrito:
///   · **Momento del comentario** y **Autor del comentario**: `Definicion-Modelo-De-Dominio.md`
///     §2.2 —que §1 del modelo de datos declara **prevaleciente** cuando los dos difieren— no
///     declara esos dos atributos en la entidad, y el punto de acceso que produciría un comentario
///     (`A-15`) es de la etapa `h`. Entran con esa etapa, junto con el autor y el momento que
///     recién entonces existen.
/// </remarks>
public sealed class WorkConfiguration : IEntityTypeConfiguration<Work>
{
    /// <summary>
    /// Un momento guardado como texto ordenable, en tiempo universal coordinado.
    /// </summary>
    /// <remarks>
    /// EL VALOR SE NORMALIZA A TIEMPO UNIVERSAL ANTES DE ESCRIBIRSE, y eso es lo que hace que la
    /// comparación de texto coincida con la comparación de momentos: dos sellos con
    /// desplazamientos distintos escritos tal cual se ordenarían mal aunque el formato fuera
    /// ordenable. Al leer, el valor vuelve como momento con desplazamiento cero, que es el que se
    /// escribió.
    /// </remarks>
    private static readonly ValueConverter<DateTimeOffset, string> SortableMoment = new(
        moment => moment.ToUniversalTime().ToString("O", CultureInfo.InvariantCulture),
        text => DateTimeOffset.Parse(text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));

    public void Configure(EntityTypeBuilder<Work> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("Work");

        builder.HasKey(work => work.Id);

        builder.Property(work => work.OwnerId).IsRequired();
        builder.Property(work => work.Name).IsRequired();

        // Texto, no momento: la escribe el alumno (`RC-06`).
        builder.Property(work => work.DeclaredDate).IsRequired();

        // Admite ausencia: el alumno puede no explicar nada.
        builder.Property(work => work.Description);

        // El texto del alumno, conservado literal (`RC-01`, RN-08). No se consulta por su
        // contenido y por eso no lleva índice.
        builder.Property(work => work.OriginalJson).IsRequired();

        builder.Property(work => work.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(work => work.AdministratorComment);

        // Nula mientras el texto no se interpretó, que en esta etapa es siempre.
        builder.Property(work => work.RootFigureCount);

        // LOS DOS SELLOS VAN COMO TEXTO EN FORMATO ORDENABLE, EN TIEMPO UNIVERSAL COORDINADO, y no
        // es una preferencia de formato: **SQLite no ordena por un momento con desplazamiento**.
        // Sin esta conversión, la consulta de listado —que ordena por el sello— no se puede
        // traducir, y la única salida sería ordenar del lado del cliente, es decir traer las filas
        // para acomodarlas afuera. El formato de ida y vuelta normalizado a tiempo universal es
        // lexicográficamente ordenable, que es exactamente lo que `Modelo-Datos-Logico.md` §2.1 y
        // `RC-06` piden de un momento guardado en este motor.
        //
        // **[decisión de la etapa `e`, declarada]** `Account.CreatedAt` NO se convierte y queda
        // como estaba: su transformación de esquema ya está fusionada y **no se edita** (intake
        // §17.3.P.7), y ninguna consulta de cuentas ordena por él —el listado de la comisión
        // ordena por correo normalizado—. La asimetría es la consecuencia de esa regla, no un
        // olvido, y se cierra el día que una consulta de cuentas necesite ordenar por el momento.
        builder.Property(work => work.CreatedAt).HasConversion(SortableMoment).IsRequired();
        builder.Property(work => work.UpdatedAt).HasConversion(SortableMoment).IsRequired();

        builder.HasIndex(work => new { work.OwnerId, work.Status })
            .HasDatabaseName("IX_Work_Owner_Status");

        builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey(work => work.OwnerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
