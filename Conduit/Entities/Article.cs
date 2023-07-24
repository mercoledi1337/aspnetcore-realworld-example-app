using Conduit.Infrastructure.Security;

namespace Conduit.Entities
{
    public class Article
    {
        public int Id { get; set; }
        //To trzeba żeby z wyszukiwania działało
        public string? Slug { get; private set; }
        public string? Title { get; private set; }
        public string? Description { get; private set; } 
        public string? Body { get; private set; }
        public Person? Author { get; private set; }
        public bool? Favortited { get; private set; }
        public int FavoriteCount { get; private set; }
        //public List<string>? TagList 
        //obmyśleć jak to zrobić
        //public List<ArticleTag>? ArticlesTags { get; private set; }
        //stowrzyć komentarze
        //public List<Comment> Comments { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        private Article(string title, string description, string body, Person autor)
        {
            Title = title;
            Description = description;
            Body = body;
            Author = autor;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public Article()
        {
        }

        public static Article CreateArticle(string title, string description, string body,  Person autor)
        {
            Article article = new(title, description, body, autor);
            return article;

        }
    }
}
