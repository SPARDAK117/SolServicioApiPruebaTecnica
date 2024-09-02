using ServicioApiPruebaTecnica.Data;

public class UserService : IUserService
{
    private readonly PruebaTecnicaOMCContextDB _context;

    public UserService(PruebaTecnicaOMCContextDB context)
    {
        _context = context;
    }

    public Usuario ValidateUser(string username, string password)
    {
        var user = _context.Usuarios.SingleOrDefault(u => u.Username == username);
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return user;
        }
        return null;
    }
}