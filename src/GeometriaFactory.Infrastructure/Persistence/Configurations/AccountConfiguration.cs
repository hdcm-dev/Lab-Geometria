using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeometriaFactory.Infrastructure.Persistence.Configurations;

/// <summary>
/// Mapeo de la tabla `Cuenta` de `Modelo-Datos-Logico.md` §2.1 (`Infrastructure BT-05`).
/// </summary>
/// <remarks>
/// LOS DOS CONJUNTOS CERRADOS SE GUARDAN COMO TEXTO Y POR SU NOMBRE, nunca por su posición
/// (intake §17.3.P.4; `Contratos-REST.md` §2.2). Guardarlos por posición ataría el dato a un
/// orden de declaración que cualquiera puede cambiar sin darse cuenta.
///
/// DOS ÍNDICES ÚNICOS, Y LOS DOS SON LA SEGUNDA DEFENSA DE UN INVARIANTE:
///   · sobre el correo normalizado, INV-01 y RN-02 (`Infrastructure ADR-03`);
///   · sobre el papel, filtrado por `Administrator`, INV-05 y RN-01 — es lo que hace imposible
///     una segunda cuenta de administrador aunque dos peticiones simultáneas pasaran las dos
///     por la comprobación previa de la capa de aplicación.
/// La primera defensa sigue siendo la capa de aplicación; el índice existe porque una
/// comprobación previa no es una garantía por sí sola (`Application CU-10` FA-03).
///
/// EL MOMENTO DE ALTA VA EN TIEMPO UNIVERSAL COORDINADO (`RC-06`). SQLite no tiene tipo de
/// momento con desplazamiento, de modo que se guarda como texto en formato ordenable, que es lo
/// que permite compararlo sin convertir.
/// </remarks>
public sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("Account");

        builder.HasKey(account => account.Id);

        builder.Property(account => account.Email).IsRequired();
        builder.Property(account => account.NormalizedEmail).IsRequired();
        builder.Property(account => account.FirstName).IsRequired();
        builder.Property(account => account.LastName).IsRequired();

        builder.Property(account => account.Role)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(account => account.Status)
            .HasConversion<string>()
            .IsRequired();

        // Nula mientras la cuenta está `Pending`: toma valor en el acto que fija la credencial.
        builder.Property(account => account.PasswordHash);

        builder.Property(account => account.MustChangePassword)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(account => account.CreatedAt).IsRequired();

        builder.HasIndex(account => account.NormalizedEmail)
            .IsUnique()
            .HasDatabaseName("UX_Account_NormalizedEmail");

        builder.HasIndex(account => account.Role)
            .IsUnique()
            .HasFilter($"\"Role\" = '{nameof(Role.Administrator)}'")
            .HasDatabaseName("UX_Account_SingleAdministrator");
    }
}
