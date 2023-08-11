using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;

namespace Conduit.Features.Articles.Application.Commands
{
    public class List
    {
        private readonly IArticleQueriesRepo _articleQueriesRepo;

        public List(IArticleQueriesRepo articleQueriesRepo)
        {
            _articleQueriesRepo = articleQueriesRepo;
        }

        public async Task<List<ArticleReadModel>> GetAll() => await _articleQueriesRepo.GetAll();

        public async Task<ArticleReadModel> GetArticle(string slug) => await _articleQueriesRepo.GetArticle(slug);
    }
}
