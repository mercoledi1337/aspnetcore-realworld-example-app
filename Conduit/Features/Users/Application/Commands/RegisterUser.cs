using Conduit.Entities;
using Conduit.Infrastructure.DataAccess;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

                if (await _context.Persons
                    .Where(x => x.Username == request.User.Username)
                    .AnyAsync(cancellationToken))
                {
                    throw new ArgumentException("This username is in use");
                }

                if (await _context.Persons
                    .Where(x => x.Email == request.User.Email)
                    .AnyAsync(cancellationToken))
                {
                    throw new ArgumentException("This email is in use");
                }

                _passwordHasher.CreatePasswordHash(request.User.Password,
                 out byte[] PasswordHash,
                 out byte[] PasswordSalt);

                string bio = "12345678901112";
                Person wholeUser = Person.CreatePerson(request.User.Username, request.User.Email, PasswordHash, PasswordSalt);


                var user = new UserDto
                {
                    Username = request.User.Username,
                    Email = request.User.Email,
                };

                _context.Persons.Add(wholeUser);
                await _context.SaveChangesAsync();
                return new UserEnvelope(user);
            }
        }
    }
}
