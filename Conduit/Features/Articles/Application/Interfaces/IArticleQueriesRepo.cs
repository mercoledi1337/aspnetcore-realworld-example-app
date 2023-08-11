using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public interface IArticleQueriesRepo
    {
        public Task<List<ArticleReadModel>> GetAll();
        public Task<Article> Get(string title);
        public Task<ArticleReadModel> GetArticle(string slug);
        public Task<List<ArticleReadModel>> GetList(string title);
    }
}
