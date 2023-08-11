
using Conduit.Features.Users.Application;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public class ArticleReadModel
    {
        public string Slug {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> TagList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Favorite { get; set; } = false;
        public int FavoritesCount { get; set; } = 0;
        //później zmienić żeby był bez tokenu
        public UserDto author { get; set; }
        //public IEnumerable<string> Comments { get; set; }
        
    }
}
