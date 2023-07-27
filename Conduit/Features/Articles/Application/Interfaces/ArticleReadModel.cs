using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public class ArticleReadModel
    {
        public Guid ArticleId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
