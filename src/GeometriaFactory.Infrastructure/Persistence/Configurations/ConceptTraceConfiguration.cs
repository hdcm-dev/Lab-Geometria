using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeometriaFactory.Infrastructure.Persistence.Configurations;

/// <summary>La huella del asistente de conceptos (REQ-IA-30). Los largos acotan a valores cerrados.</summary>
public sealed class ConceptTraceConfiguration : IEntityTypeConfiguration<ConceptTraceRow>
{
    public void Configure(EntityTypeBuilder<ConceptTraceRow> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("HuellaDeConsulta");
        builder.HasKey(row => row.Id);
        builder.Property(row => row.Id).HasColumnName("Id");
        builder.Property(row => row.Ring).HasColumnName("Anillo").IsRequired();
        builder.Property(row => row.Intent).HasColumnName("Intencion").HasMaxLength(48).IsRequired();
        builder.Property(row => row.Form).HasColumnName("Forma").IsRequired();
        builder.Property(row => row.Outcome).HasColumnName("Resultado").HasMaxLength(16).IsRequired();
        builder.Property(row => row.CorpusVersion).HasColumnName("VersionDelCorpus").HasMaxLength(24).IsRequired();
        builder.Property(row => row.Day).HasColumnName("Dia").IsRequired();
        builder.HasIndex(row => row.Day);
    }
}
