using Conduit.Entities;

namespace Conduit.Features.Articles.Application.Interfaces
{
    public interface IPersonRepository
    {
        public Task<Person> GetPerson(string sub);
    }
}
