using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Entities.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
               .IsRequired(true)
               .HasMaxLength(ItemSpecification.CategoryNameMaxLength);

            builder.Property(x => x.FileKey)
               .IsRequired(false)
               .HasMaxLength(200);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired(true);
        }
    }
}
