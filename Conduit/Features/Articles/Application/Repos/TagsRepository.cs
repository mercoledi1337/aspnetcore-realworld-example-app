using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Infrastructure.DataAccess;

namespace Conduit.Features.Articles.Application.Repos
{
    public class TagsRepository : ITagsRepository
    {
        private readonly DataContext _context;

        public TagsRepository(DataContext context)
        {
            _context = context;
        }
        public Tag GetTag(string tag) => _context.Tags.Find(tag);

        public void UpdateTags(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public void UpdateArticleTags(Article article, List<Tag> tags)
        {
            _context.ArticleTags.AddRangeAsync(tags.Select(x => new ArticleTag()
            {
                Article = article,
                Tag = x
            }));
            _context.SaveChanges();
        }
    }
}
