using Conduit.Features.Users.Infrastructure;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Conduit.Features.Users
{
    public class UserRegisterRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }

    public record RegisteringUser(UserRegisterRequest UserRegister)
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
                .Where(x => x.UserName == request.UserRegister.Username)
                .AnyAsync(cancellationToken))
            {
                throw new ArgumentException("This username is in use");
            }

            if (await _context.WholeUsers
                .Where(x => x.Email == request.UserRegister.EmailAddress)
                .AnyAsync(cancellationToken))
            {
                throw new ArgumentException("This email is in use");
            }

           _passwordHasher.CreatePasswordHash(request.UserRegister.Password,
            out byte[] PasswordHash,
            out byte[] PasswordSalt);

            var wholeUser = new WholeUser
            {
                UserName = request.UserRegister.Username,
                Email = request.UserRegister.EmailAddress,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt
            };

            var user = new User
            {
                UserName = request.UserRegister.Username,
                Email = request.UserRegister.EmailAddress,
            };

            _context.WholeUsers.Add(wholeUser);
            await _context.SaveChangesAsync();
            return new UserEnvelope(user);
        }
    }
}
