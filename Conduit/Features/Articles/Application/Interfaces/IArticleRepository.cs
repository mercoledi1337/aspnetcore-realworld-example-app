using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public interface IArticleRepository
    {
        public Article GetArticle(int id);
        public Task Updatearticle(Article article);
    }
}
