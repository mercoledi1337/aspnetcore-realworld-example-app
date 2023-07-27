namespace Conduit.Features.Articles.Application.Interfaces
{
    public interface IArticleQueriesRepo
    {
        public Task<List<ArticleReadModel>> GetAll();
    }
}
