using Conduit.Entities;
using Conduit.Features.Articles.Application.Dto;
using Conduit.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Articles.Application.Commands
{
    public class Create
    {
        public class ArticleCreateRequest
        {
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? Body { get; set; }
            public List<string>? TagList { get; set; }
        }

        public record CreateingArticle(ArticleCreateRequest Article)
                : IRequest<ArticleEnvelope>;


        public class CreatingArticleHandler : IRequestHandler<CreateingArticle, ArticleEnvelope>
        {
            private readonly DataContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreatingArticleHandler(DataContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ArticleEnvelope> Handle(
                CreateingArticle request, CancellationToken cancellationToken)
            {
                var sub = _httpContextAccessor.HttpContext?.User.FindFirst(type: "sud")?.Value;

                Person person = await _context.Persons
                     .Where(x => x.Id == int.Parse(sub))
                     .SingleAsync(cancellationToken);

                Article article = Article.CreateArticle(request.Article.Title, request.Article.Description, request.Article.Body, person);

                var articleDto = new ArticleDto
                {
                    Title = request.Article.Title,
                    Description = request.Article.Description,
                    Body = request.Article.Body,
                };

                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
                return new Dto.ArticleEnvelope(articleDto);
            }
        }
    }
}
