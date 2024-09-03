//using ServicioApiPruebaTecnica.Data;

//namespace ServicioApiPruebaTecnica.Services
//{
//    public interface ICookies
//    {
//        Usuarios Authenticate(string username, string password);
//    }

//    public class CookieService : ICookies
//    {
//        private readonly PruebaTecnicaOMCContextDB _context;

//        public CookieService(PruebaTecnicaOMCContextDB context)
//        {
//            _context = context;
//        }

//        public Usuarios Authenticate(string username, string password)
//        {
//            var user = _context.Usuarios.SingleOrDefault(u => u.Username == username);

//            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
//                return null;

//            return user;
//        }
//    }
//}