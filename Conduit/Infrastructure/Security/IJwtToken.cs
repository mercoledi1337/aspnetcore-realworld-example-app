namespace Conduit.Infrastructure.Security
{
    public interface IJwtToken
    {
        string CreateToken(string username);
    }
}
