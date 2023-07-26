using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public interface IArticlesRepository
    {
        public Article GetArticle(int id);
        public Task UpdateArticle(Article article);
    }
}
