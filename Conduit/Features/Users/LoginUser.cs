using Conduit.Features.Users.Infrastructure;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Conduit.Features.Users
{
    public class UserLoginRequest
    {

        [Required, EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

    }
        public record LoginUser(UserLoginRequest UserLogin)
        : IRequest<UserEnvelope>;

        public class LoginUserHandler : IRequestHandler<LoginUser, UserEnvelope>
        {
            private readonly DataContext _context;
            private readonly IPasswordHash _passwordHasher;

            public LoginUserHandler(DataContext context, IPasswordHash passwordHasher)
            {
                _context = context;
                _passwordHasher = passwordHasher;
            }

            public async Task<UserEnvelope> Handle(
                LoginUser request, CancellationToken cancellationToken)
            {
                var wholeUser = await _context.WholeUsers.Where(x => x.Email == request
                .UserLogin.EmailAddress).SingleOrDefaultAsync(cancellationToken);
                if (wholeUser == null)
                {
                    throw new ArgumentException("Wrong password/email");
                }
                if(!_passwordHasher.VerifyPasswordHash(request.UserLogin.Password, wholeUser.PasswordHash, wholeUser.PasswordSalt))
                {
                    throw new ArgumentException("Wrong password/email");
                }
                // to jest tylko do sprawdzenia czy dobrze sprawdza hasło
                var user = new User
                {
                    UserName = request.UserLogin.EmailAddress,
                };
                return new UserEnvelope(user);
            }
        }
    }

