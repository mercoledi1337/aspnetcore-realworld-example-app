using Conduit.Entities;
using Conduit.Features.Articles.Application.Dto;
using Conduit.Infrastructure;
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
            public string[]? TagList { get; set; }
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

                var tags = new List<Tag>();
                foreach (var tag in (request.Article.TagList ?? Enumerable.Empty<string>()))
                {
                    var t = await _context.Tags.FindAsync(tag);
                    if (t == null)
                    {
                        t = new Tag()
                        {
                            TagId = tag
                        };
                        await _context.Tags.AddAsync(t, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    tags.Add(t);
                }

                Article article = Article.CreateArticle(request.Article, person);

                //var articleDto = new ArticleDto
                //{
                //    Title = request.Article.Title,
                //    Description = request.Article.Description,
                //    Body = request.Article.Body,
                //};

                await _context.Articles.AddAsync(article, cancellationToken);
                await _context.ArticleTags.AddRangeAsync(tags.Select(x => new ArticleTag()
                {
                    Article = article,
                    Tag = x
                }), cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
                return new ArticleEnvelope(article);
            }
        }
    }
}
