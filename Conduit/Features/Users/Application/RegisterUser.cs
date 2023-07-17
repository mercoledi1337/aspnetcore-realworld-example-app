using Conduit.Features.Users.Domain;
using Conduit.Infrastructure.DataAccess;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Conduit.Features.Users.Application
{
    public class Register
    {
        public class UserRegisterRequest
        {
            public string? Username { get; set; }

            public string? Email { get; set; }

            public string? Password { get; set; }
        }

        public record RegisteringUser(UserRegisterRequest User)
                : IRequest<UserEnvelope>;

        public class RegisteringUserHandler : IRequestHandler<RegisteringUser, UserEnvelope>
        {
            private readonly DataContext _context;
            private readonly IPasswordHash _passwordHasher;

            public RegisteringUserHandler(DataContext context, IPasswordHash passwordHasher)
            {
                _context = context;
                _passwordHasher = passwordHasher;
            }

            public async Task<UserEnvelope> Handle(
                RegisteringUser request, CancellationToken cancellationToken)
            {

                if (await _context.WholeUsers
                    .Where(x => x.UserName == request.User.Username)
                    .AnyAsync(cancellationToken))
                {
                    throw new ArgumentException("This username is in use");
                }

                if (await _context.WholeUsers
                    .Where(x => x.Email == request.User.Email)
                    .AnyAsync(cancellationToken))
                {
                    throw new ArgumentException("This email is in use");
                }

                _passwordHasher.CreatePasswordHash(request.User.Password,
                 out byte[] PasswordHash,
                 out byte[] PasswordSalt);

                var wholeUser = new WholeUser
                {
                    UserName = request.User.Username,
                    Email = request.User.Email,
                    PasswordHash = PasswordHash,
                    PasswordSalt = PasswordSalt
                };

                var user = new User
                {
                    UserName = request.User.Username,
                    Email = request.User.Email,
                };

                _context.WholeUsers.Add(wholeUser);
                await _context.SaveChangesAsync();
                return new UserEnvelope(user);
            }
        }
    }
}
