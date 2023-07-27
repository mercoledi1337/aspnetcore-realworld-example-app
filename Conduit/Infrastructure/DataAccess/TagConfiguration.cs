using Conduit.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.DataAccess
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");
            builder.HasKey(t => t.TagId);
            builder.Property(t => t.Name).IsRequired();
            builder.Metadata.FindNavigation(nameof(Tag.Articles))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
