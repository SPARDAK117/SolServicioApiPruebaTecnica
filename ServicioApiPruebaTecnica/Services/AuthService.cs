using ServicioApiPruebaTecnica.Data;

namespace ServicioApiPruebaTecnica.Services
{
    public interface IAuthService
    {
        string Authenticate(string username, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly PruebaTecnicaOMCContextDB _context;
        private readonly ITokenService _tokenService;

        public AuthService(PruebaTecnicaOMCContextDB context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public string Authenticate(string username, string password)
        {
            var user = _context.Usuarios.SingleOrDefault(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return _tokenService.GenerateToken(user);
        }
    }
}