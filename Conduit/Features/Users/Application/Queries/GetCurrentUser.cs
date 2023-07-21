using Conduit.Entities;
using Conduit.Infrastructure.DataAccess;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Users.Application.Queries
{
    public class GetCurrentUser
    {
      
        public record CurrentUser(string User)
        : IRequest<UserEnvelope>;

        public class CurrentUserHandler : IRequestHandler<CurrentUser, UserEnvelope>
        {
            private readonly DataContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IJwtToken _jwt;

            public CurrentUserHandler(DataContext context, IHttpContextAccessor httpContextAccessor, IJwtToken jwt)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
                _jwt = jwt;
            }


            public async Task<UserEnvelope> Handle(
                CurrentUser request, CancellationToken cancellationToken)
            {

                var sub = _httpContextAccessor.HttpContext?.User.FindFirst(type: "sud")?.Value;

                Person person = await _context.Persons
                     .Where(x => x.Id == int.Parse(sub))
                     .SingleAsync(cancellationToken);

                var user = _context.Persons
                    .Where(x => x.Id == int
                    .Parse(sub))
                    .Select(x => new UserDto
                    {
                        Username = x.Username,
                        Email = x.Email,
                        Bio = x.Bio,
                        Image = x.Image
                    }).Single();
                user.Token = _jwt.CreateToken(person);
                return new UserEnvelope(user);
            }
        }
    }
}
