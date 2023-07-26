using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Infrastructure.DataAccess;

namespace Conduit.Features.Articles.Application.Repos
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly DataContext _context;

        public ArticlesRepository(DataContext context)
        {
            _context = context;
        }
        public Article GetArticle(int id) => _context.Articles.FirstOrDefault(a => a.Id == id);
        public Task UpdateArticle(Article article) => _context.SaveChangesAsync();

    }
}
