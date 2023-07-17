using Conduit.Infrastructure.DataAccess;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Conduit.Features.Users.Application
{
    public class Login
    {
        public class UserLoginRequest
        {


            public string Email { get; set; } = string.Empty;

            public string Password { get; set; } = string.Empty;

        }
        public record LoginUser(UserLoginRequest User)
        : IRequest<UserEnvelope>;

        public class LoginUserHandler : IRequestHandler<LoginUser, UserEnvelope>
        {
            private readonly DataContext _context;
            private readonly IPasswordHash _passwordHasher;
            private readonly IJwtToken _jwt;

            public LoginUserHandler(DataContext context, IPasswordHash passwordHasher, IJwtToken jwt)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _jwt = jwt;
            }

            public async Task<UserEnvelope> Handle(
                LoginUser request, CancellationToken cancellationToken)
            {
                var wholeUser = await _context.WholeUsers.Where(x => x.Email == request
                .User.Email).SingleOrDefaultAsync(cancellationToken);
                if (wholeUser == null)
                {
                    throw new ArgumentException("Wrong password/email");
                }
                if (!_passwordHasher.VerifyPasswordHash(request.User.Password, wholeUser.PasswordHash, wholeUser.PasswordSalt))
                {
                    throw new ArgumentException("Wrong password/email");
                }
                //zmienić nazwe na auth
                // to jest tylko do sprawdzenia czy dobrze sprawdza hasło
                var user = new User
                {
                    UserName = wholeUser.UserName,
                    Email = request.User.Email,
                    Token = _jwt.CreateToken(wholeUser.UserName)
                };
                return new UserEnvelope(user);
            }
        }
    }
}
