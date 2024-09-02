using ServicioApiPruebaTecnica.Data;

public interface ITokenService
{
    string GenerateToken(Usuario user);
}