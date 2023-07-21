using Conduit.Entities;

namespace Conduit.Infrastructure.Security
{
    public interface IJwtToken
    {
        string CreateToken(Person username);
    }
}
