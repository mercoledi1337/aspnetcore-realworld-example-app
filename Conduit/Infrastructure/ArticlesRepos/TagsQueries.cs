using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.ArticlesRepos
{
    public class TagsQueries : ITagsQueries
    {
        private readonly DataContext _context;

        public TagsQueries(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<Tag> CheckTags(string name) => await _context.Tags.FirstOrDefaultAsync(x => x.Name == name);

        public List<string> GetAll() => _context.Tags.Select(x => x.Name).ToList();

    }
}
