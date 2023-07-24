using Conduit.Entities;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<ArticleTag> ArticleTags { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleTag>()
                .HasKey(at => new { at.ArticleId, at.TagId });

            modelBuilder.Entity<ArticleTag>()
                .HasOne(a => a.Article)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(t => t.ArticleId);

            modelBuilder.Entity<ArticleTag>()
                .HasOne(t => t.Tag)
                .WithMany(a => a.ArticleTags)
                .HasForeignKey(t => t.TagId);
        }
    }
}
