using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Entity.Configuration
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Type)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.ExpirationTime)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Tokens)
                .HasForeignKey(x => x.UserId)
                .IsRequired(true);

            builder.HasIndex(x => x.Type);
            builder.HasIndex(x => x.UserId);
        }
    }
}
