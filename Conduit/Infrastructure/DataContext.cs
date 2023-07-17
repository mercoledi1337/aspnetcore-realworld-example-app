using Conduit.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<WholeUser> WholeUsers { get; set; }
    }
}
