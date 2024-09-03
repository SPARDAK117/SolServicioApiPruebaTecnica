using ServicioApiPruebaTecnica.Data;
using ServicioApiPruebaTecnica.MyLogging;

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
        private readonly IMyLogger _logger;

        public AuthService(PruebaTecnicaOMCContextDB context, ITokenService tokenService,IMyLogger logger)
        {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
        }

        public string Authenticate(string username, string password)
        {
            try
            {
                _logger.Log($"Init Autenticación de UserName y password");
                var user = _context.Usuarios.SingleOrDefault(u => u.Username == username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    _logger.Log($"Ah fallado la autenticación de Username y Password");
                    return null;
                }
                    

                return _tokenService.GenerateToken(user);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error in Authenticate: {ex.Message}");
                return ("Error retrieving data from the database.");

            }
        }
    }
}