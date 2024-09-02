using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Crypto.Generators;
using ServicioApiPruebaTecnica.Data;

namespace ServicioApiPruebaTecnica.Data.Config
{
    public class UsuariosConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Especificar la tabla en la base de datos
            builder.ToTable("Usuarios");

            // Definir la clave primaria
            builder.HasKey(u => u.Id);

            // Configurar la columna Username
            builder.Property(u => u.Username)
                .IsRequired() // Campo obligatorio
                .HasMaxLength(50) // Longitud máxima de 50 caracteres
                .HasColumnName("Usuario");

            // Definir un índice en la columna Username
            builder.HasIndex(u => u.Username)
                .IsUnique(); // Asegurar que el nombre de usuario sea único

            // Configurar la columna PasswordHash
            builder.Property(u => u.PasswordHash)
                .IsRequired() // Campo obligatorio
                .HasMaxLength(255) // Longitud máxima para el hash de la contraseña
                .HasColumnName("PasswordHash");

            // Configurar la columna Role
            builder.Property(u => u.Role)
                .HasMaxLength(20) // Longitud máxima de 20 caracteres
                .HasColumnName("Rol")
                .HasDefaultValue("user"); // Valor por defecto para el rol

            // Seed Data (Opcional): Insertar datos de ejemplo
            builder.HasData(new Usuario
            {
                Id = 5,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "admin"
            },
            new Usuario
            {
                Id = 6,
                Username = "user",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                Role = "user"
            }
            );
        }
    }
}

