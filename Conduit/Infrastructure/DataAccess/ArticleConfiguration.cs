using Conduit.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.DataAccess
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(a => a.ArticleId);
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Body).IsRequired();
            builder.Metadata.FindNavigation(nameof(Article.Tags))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany(a => a.Tags)
                .WithMany(t => t.Articles)
                .UsingEntity(j => j.ToTable("ArticleTags"));
        }
    }

}
