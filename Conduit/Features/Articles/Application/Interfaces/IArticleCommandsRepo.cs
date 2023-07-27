using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public interface IArticleCommandsRepo
    {
        public Task<Article> Get(Guid id);
        public Task Update(Article article);
        public Task Add(Article article);
        public Task<bool> IsInUse(string title);

    }
}
