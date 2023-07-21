using Conduit.Entities;
using Conduit.Infrastructure.DataAccess;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Users.Application
{
    public class Update
    {
        
        public class UserUpdateRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string Image { get; set; } = string.Empty;
            public string Bio { get; set; } = string.Empty;
            public string Username { get; set; } = string.Empty;
        }

        public record UpdateUser(UserUpdateRequest User)
        : IRequest<UserEnvelope>;

        public class UpdateUserHandler : IRequestHandler<UpdateUser, UserEnvelope>
        {
            private readonly DataContext _context;
            private readonly IPasswordHash _passwordHasher;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateUserHandler(DataContext context, IPasswordHash passwordHasher, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<UserEnvelope> Handle(
                UpdateUser request, CancellationToken cancellationToken)
            {

                var sub = _httpContextAccessor.HttpContext?.User.FindFirst(type: "sud")?.Value;

                Person person = await _context.Persons
                     .Where(x => x.Id == int.Parse(sub))
                     .SingleAsync(cancellationToken);

                _passwordHasher.CreatePasswordHash(request.User.Password,
                 out byte[] PasswordHash,
                 out byte[] PasswordSalt);

                person.UpdatePerson(request.User, PasswordHash, PasswordSalt);

                var user = new UserDto
                {
                    Username = request.User.Username,
                    Email = request.User.Email,
                };

                //_context.Persons.Add(wholeUser);
                await _context.SaveChangesAsync();
                return new UserEnvelope(user);
            }
        }
    }
}
