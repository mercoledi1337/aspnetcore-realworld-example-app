using Conduit.Infrastructure;
using System.Text.Json.Serialization;
using static Conduit.Features.Articles.Application.Commands.Create;

namespace Conduit.Entities
{
    public class Article
    {
        [JsonIgnore]
        public Guid ArticleId { get; private set; }
        //To trzeba żeby z wyszukiwania działało
        public string? Slug { get; private set; }
        public string? Title { get; private set; }
        public string? Description { get; private set; }
        public string? Body { get; private set; }
        public Person? Author { get; private set; }
        public bool? Favorited { get; private set; }
        public int FavoriteCount { get; private set; }
        private ICollection<Tag> _tags = new List<Tag>();
        public IEnumerable<Tag> Tags => _tags;
        public ICollection<Comment> Comments { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public void SetArticleDetails(ArticleCreateRequest request, Person autor)
        {
            Title = request.title;
            Description = request.description;
            Body = request.body;
            Author = autor;
            Slug = request.title.GenerateSlug();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        private Article(Guid id, List<Comment> comments)
        {
            ArticleId = id;
            Comments = comments;
        }

        private Article()
        {
        }

        public void SetTags(List<Tag> tags)
        {
            if (tags.Count() + _tags.Count > 10)
                throw new ArgumentException("too much tags");
            _tags.Clear();
            foreach (var tag in tags)
            {
                _tags.Add(tag);
            }
        }
        public void DeleteTag(Tag tag)
        {
            _tags.Remove(_tags.Single(x => x.Name == tag.Name));
        }

        public void DeleteComment(Guid commentId)
        {
            Comments.Remove(Comments.Single(x => x.CommentId == commentId));
        }

        public void UpdateWithComments(string body)
        {
            foreach (var com in Comments)
            {
                com.UpdateBody(com.Body + " updated");
            }
            Body = body;
        }
        public static Article Create(Person author)
            {
                var comments = new List<Comment>();
            for (int i = 0; i < 2; i++)
            {
                Comment tmp = Comment.Create(author);
                string tmpComment = "test" + i.ToString();
                tmp.UpdateBody(tmpComment);
                comments.Add(tmp);
            }
            
                return new Article(Guid.NewGuid(), comments);
            }
    }
}
