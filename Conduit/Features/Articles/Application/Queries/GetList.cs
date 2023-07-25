using Conduit.Features.Articles.Application.Dto;
using Conduit.Infrastructure.DataAccess;
using MediatR;

namespace Conduit.Features.Articles.Application.Queries
{
    public class GetList
    {
        public record Query(string Tag, string Author, string FavoritedUser, int? Limit, int? Offset, bool IsFeed = false) : IRequest<ArticlesEnvelope>;

        public class QueryHandler : IRequestHandler<Query, ArticlesEnvelope>
        {
            private readonly DataContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public QueryHandler(DataContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }
            public Task<ArticlesEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
