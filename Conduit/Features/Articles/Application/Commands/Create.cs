using Conduit.Entities;
using Conduit.Features.Articles.Application.Dto;
using Conduit.Infrastructure;
using Conduit.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Conduit.Features.Articles.Application.Commands
{
    public class Create
    {
        

        public class ArticleCreateRequest
        {
            public string? title { get; set; }
            public string? description { get; set; }
            public string? body { get; set; }
            public string[]? tagList { get; set; }
        }

        public record ArticleCreateEnvelope(ArticleCreateRequest article);

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Create(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<bool> CheckTitile(string title)
        {
            var res = await _context.Articles.FirstAsync(x => x.Title == title);
            return (res != null);
        }

        private async Task<ArticleEnvelope> CreateArticle(ArticleCreateRequest request, Person person, List<Tag> tags)
        {
            
            Article article = Article.CreateArticle(request, person);

            await _context.Articles.AddAsync(article);
            await _context.ArticleTags.AddRangeAsync(tags.Select(x => new ArticleTag()
            {
                Article = article,
                Tag = x
            }));
            await _context.SaveChangesAsync();
            return new ArticleEnvelope(article);
        }

            public async Task<ArticleEnvelope> CreateArticle(ArticleCreateRequest request)
            {
                
            var sub = _httpContextAccessor.HttpContext?.User.FindFirst(type: "sud")?.Value;

            if (await CheckTitile(request.title)) 
            {
                throw new ArgumentException("Title is already in use");
            }

            var tags = new List<Tag>();
            foreach (var tag in (request.tagList ?? Enumerable.Empty<string>()))
            {
                var t = await _context.Tags.FindAsync(tag);
                if (t == null)
                {
                    t = new Tag()
                    {
                        TagId = tag
                    };
                    await _context.Tags.AddAsync(t);
                    await _context.SaveChangesAsync();
                }
                tags.Add(t);
            }

            Person person = await _context.Persons
                     .Where(x => x.Id == int.Parse(sub))
                     .SingleAsync();

                return await CreateArticle(request, person, tags);
            }
        }
    }