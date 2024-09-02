using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServicioApiPruebaTecnica.Data.Config
{
    public class UsuariosConfig : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            // Especificar la tabla en la base de datos
            builder.ToTable("Usuarios");

            // Definir la clave primaria
            builder.HasKey(u => u.Id);

            // Configurar la columna Username
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Usuario");

            // Definir un índice en la columna Username para que sea único
            builder.HasIndex(u => u.Username)
                .IsUnique();

            // Configurar la columna PasswordHash
            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            // Configurar la columna Role
            builder.Property(u => u.Role)
                .HasMaxLength(20)
                .HasColumnName("Rol")
                .HasDefaultValue("user");

            // Seed Data
            builder.HasData(
                new Usuarios
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "admin"
                },
                new Usuarios
                {
                    Id = 2,
                    Username = "user",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                    Role = "user"
                }
            );
        }
    }
}