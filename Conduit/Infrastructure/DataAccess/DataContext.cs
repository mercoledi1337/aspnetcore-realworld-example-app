using Conduit.Entities;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}
