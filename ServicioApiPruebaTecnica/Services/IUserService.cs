using ServicioApiPruebaTecnica.Data;

public interface IUserService
{
    Usuario ValidateUser(string username, string password);
}