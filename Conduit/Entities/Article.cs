using Conduit.Infrastructure.Security;

namespace Conduit.Entities
{
    public class Article
    {
        public int Id { get; set; }
        //To trzeba żeby z wyszukiwania działało
        //public string? Slug { get; private set; }
        public string? Title { get; private set; }
        public string? Description { get; private set; } 
        public string? Body { get; private set; }
        public Person? Author { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        private Article(string title, string description, string body, Person autor)
        {
            Title = title;
            Description = description;
            Body = body;
            Author = autor;
            CreatedAt = DateTime.UtcNow;
        }


        public Article()
        {
        }
    }
}
