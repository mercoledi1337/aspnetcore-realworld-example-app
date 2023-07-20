using Conduit.Features.Users.Domain;

namespace Conduit.Infrastructure.Security
{
    public interface IJwtToken
    {
        string CreateToken(Person username);
    }
}
