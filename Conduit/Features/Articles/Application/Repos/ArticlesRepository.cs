using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Articles.Application.Repos
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly DataContext _context;

        public ArticlesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddArticle(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task AddArticleTag(Article article, List<Tag> tags)
        {
            await _context.ArticleTags.AddRangeAsync(tags.Select(x => new ArticleTag()
            {
                Article = article,
                Tag = x
            }));
            await _context.SaveChangesAsync();
        }

        public async Task<Article> CheckTitle(string title)
        {
            return await _context.Articles.FirstAsync(x => x.Title == title);
        }

        public Article GetArticle(int id) => _context.Articles.FirstOrDefault(a => a.ArticleId == id);
        public Task UpdateArticle(Article article) => _context.SaveChangesAsync();

    }
}
