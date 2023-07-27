using Conduit.Features.Articles.Application.Commands;
using Conduit.Infrastructure;
using Conduit.Infrastructure.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
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
        public IEnumerable<Tag> Tags => _tags.ToList().AsReadOnly();
        //stowrzyć komentarze
        //public List<Comment> Comments { get; private set; }
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
        private Article(Guid id)
        {
            ArticleId = id;
        }

        private Article()
        {
        }

        public void SetTags(List<Tag> tags)
        {
            if (tags.Count() > 10)
                throw new ArgumentException("too much tags");
            _tags.Clear();
            foreach (var tag in tags)
            {
                _tags.Add(tag);
            }
        }

        public static Article Create()
            {
                return new Article(Guid.NewGuid());
            }
    }
}
