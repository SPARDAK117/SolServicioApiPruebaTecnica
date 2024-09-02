using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicioApiPruebaTecnica.Data;

namespace ServicioApiPruebaTecnica.Data.Config
{
    public class LogEntryConfig : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            // Especificar la tabla en la base de datos
            builder.ToTable("LogEntries");

            // Definir la clave primaria
            builder.HasKey(le => le.Id);

            // Configurar la columna Username
            builder.Property(le => le.Username)
                .IsRequired() // Campo obligatorio
                .HasMaxLength(100); // Longitud máxima de 100 caracteres

            // Configurar la columna Action
            builder.Property(le => le.Action)
                .IsRequired() // Campo obligatorio
                .HasMaxLength(255); // Longitud máxima de 255 caracteres

            // Configurar la columna Timestamp
            builder.Property(le => le.Timestamp)
                .IsRequired() // Campo obligatorio
                .HasColumnType("datetime2"); // Tipo de datos SQL Server

            // Opcional: Seed Data (datos de ejemplo)
            builder.HasData(new LogEntry
            {
                Id = 1,
                Username = "admin",
                Action = "Created initial log entry",
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
