using Conduit.Infrastructure.DataAccess;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Users.Application
{
    public class Authentication
    {
        public class UserAuthenticationRequest
        {


            public string Email { get; set; } = string.Empty;

            public string Password { get; set; } = string.Empty;

        }
        public record AuthenticationUser(UserAuthenticationRequest User)
        : IRequest<UserEnvelope>;

        public class AuthorizeUserHandler : IRequestHandler<AuthenticationUser, UserEnvelope>
        {
            private readonly DataContext _context;
            private readonly IPasswordHash _passwordHasher;
            private readonly IJwtToken _jwt;

            public AuthorizeUserHandler(DataContext context, IPasswordHash passwordHasher, IJwtToken jwt)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _jwt = jwt;
            }

            public async Task<UserEnvelope> Handle(
                AuthenticationUser request, CancellationToken cancellationToken)
            {
                var person = await _context.Persons.Where(x => x.Email == request
                .User.Email).SingleOrDefaultAsync(cancellationToken);
                if (person == null)
                {
                    throw new ArgumentException("Wrong password/email");
                }
                if (!_passwordHasher.VerifyPasswordHash(request.User.Password, person.PasswordHash, person.PasswordSalt))
                {
                    throw new ArgumentException("Wrong password/email");
                }
                //zmienić nazwe na auth
                // to jest tylko do sprawdzenia czy dobrze sprawdza hasło
                var user = new UserDto
                {
                    Username = person.Username,
                    Email = request.User.Email,
                    Token = _jwt.CreateToken(person)
                };
                return new UserEnvelope(user);
            }
        }
    }
}
