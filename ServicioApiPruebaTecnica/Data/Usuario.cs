using System.ComponentModel.DataAnnotations;

namespace ServicioApiPruebaTecnica.Data
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Almacenamos un hash de la contraseña, no la contraseña en sí

        public string Role { get; set; } // Si quieres implementar roles de usuario, como "admin" o "user"
    }
}