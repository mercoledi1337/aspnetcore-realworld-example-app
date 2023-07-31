using Microsoft.EntityFrameworkCore;


namespace Conduit.Entities
{
    [Owned]
    public class Comment
    {
        public Guid CommentId { get; private set; }
        public DateTime CreatedAt { get; private set;}
        public DateTime UpdatedAt { get; private set;}
        public string Body { get; private set;}
        public int AuthorId { get; private set; }
        public Person Author { get; private set; } = null!;

        private Comment(Guid id, Person author)
        {
            CommentId = id;
            Author = author;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        private Comment()
        {
        }

        public static Comment Create(Person author)
        {
            return new Comment(Guid.NewGuid(), author);
        }

        public void UpdateBody(string body)
        {
            Body = body;
            UpdatedAt = DateTime.UtcNow;
        }





    }


}
