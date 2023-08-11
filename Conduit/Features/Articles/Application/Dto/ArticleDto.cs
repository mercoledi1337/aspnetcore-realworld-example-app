using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;

namespace Conduit.Features.Articles.Application.Dto
{
    public record ArticleEnvelope(Article article);
    public record ArticleEnvelope1(ArticleReadModel article);

    public class ArticlesEnvelope
    { 
        public List<ArticleReadModel> Articles { get; set; } = new();
        public int ArticlesCount { get; set; }
    }

    public record TagsEnvelope(List<string> tags);
}
