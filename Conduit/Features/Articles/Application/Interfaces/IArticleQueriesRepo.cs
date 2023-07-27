using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public interface IArticleQueriesRepo
    {
        public Task<List<ArticleReadModel>> GetAll();
        public Task<Article> Get(string title);
    }
}
