using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public interface ITagsQueries
    {
        public Task<Tag> CheckTags(string name);
        public List<string> GetAll();
    }
}
