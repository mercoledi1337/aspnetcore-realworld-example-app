using Conduit.Entities;
using System.Diagnostics;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public interface ITagsRepository
    {
        public Tag GetTag(string id);
        public void UpdateTags(Tag tag);
        public void UpdateArticleTags(Article article, List<Tag> tags);
        public List<ArticleTag> GetArticleTag(int id);
    }
}
