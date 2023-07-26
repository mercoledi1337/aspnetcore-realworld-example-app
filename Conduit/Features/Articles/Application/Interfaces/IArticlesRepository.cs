using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public interface IArticlesRepository
    {
        public Article GetArticle(int id);
        public Task UpdateArticle(Article article);
        public Task AddArticle(Article article);
        public Task AddArticleTag(Article article, List<Tag> tags);
        public Task<Article>? CheckTitle(string title);
    }
}
