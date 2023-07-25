using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Dto
{
    public record ArticleEnvelope(Article article);

    public class ArticlesEnvelope
    { 
        public List<Article> Articles { get; set; } = new();
        public int ArticlesCount { get; set; }
    }
}
