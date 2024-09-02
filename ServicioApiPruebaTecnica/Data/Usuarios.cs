using System.ComponentModel.DataAnnotations;

namespace ServicioApiPruebaTecnica.Data
{
    public class Usuarios  
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "user";  
    }
}