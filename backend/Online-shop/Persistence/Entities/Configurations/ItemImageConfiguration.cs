using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Entities.Configurations
{
    public class ItemImageConfiguration : IEntityTypeConfiguration<ItemImage>
    {
        public void Configure(EntityTypeBuilder<ItemImage> builder)
        {
            builder.Property(x => x.FileKey)
                .IsRequired(true)
                .HasMaxLength(200);

            builder.Property(x => x.Order)
                .IsRequired(true)
                .HasDefaultValue(1);

            builder.HasOne(x => x.Item)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ItemId)
                .IsRequired(true);
        }
    }
}
