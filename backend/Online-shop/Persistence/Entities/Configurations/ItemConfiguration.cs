using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Entities.Configurations
{
    internal class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(ItemSpecification.NameMaxLength);

            builder.Property(x => x.Description)
                .IsRequired(false);

            builder.Property(x => x.Price)
                .IsRequired(true);

            builder.HasIndex(x => x.Name);
        }
    }
}
