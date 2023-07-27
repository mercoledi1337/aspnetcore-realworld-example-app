using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.Repos
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _ctxt;
        public PersonRepository(DataContext ctxt)
        {
            _ctxt = ctxt;
        }
        public async Task<Person> GetPerson(string sub) => await _ctxt.Persons
                      .Where(x => x.Id == int.Parse(sub))
                      .SingleAsync();

    }
}
