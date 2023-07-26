using Conduit.Entities;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Articles.Application.Repos
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;

        public PersonRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Person> GetPerson(string sub) => await _context.Persons
                      .Where(x => x.Id == int.Parse(sub))
                      .SingleAsync();

    }
}
