using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.ArticlesRepos
{
    public class ArticleQueriesRepo : IArticleQueriesRepo
    {
        private readonly DataContext _ctxt;
        public ArticleQueriesRepo(DataContext ctxt)
        {
            _ctxt = ctxt;
        }
        public async Task<List<ArticleReadModel>> GetAll()
        {
            var articles = await _ctxt.Articles
                .Include(a => a.Tags)
                .AsNoTracking()
                .Select(a => new ArticleReadModel
                {
                    ArticleId = a.ArticleId,
                    Title = a.Title,
                    Body = a.Body,
                    Tags = a.Tags.Select(x => x.Name).ToList()
                })
                .ToListAsync();
            return articles;
        }

        public async Task<Article> Get(string title) => await _ctxt.Articles.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Title == title);


    }
}
