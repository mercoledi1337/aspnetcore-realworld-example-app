using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Dto
{
    public class ArticleDto
    {
        //to zostało bo nie wiem czy będę używał, potem usunę
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        // Dodać potem tagi
        //public List<Tag> TagList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatetedAt { get; set; }  
        public bool Favortited { get; set; }
        public int FavoriteCount { get; set; }
        public Person Author { get; set; }
    }
    public record ArticleEnvelope(Article Article);
}
