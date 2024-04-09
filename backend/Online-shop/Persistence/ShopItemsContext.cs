using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Entities;
using Persistense.Abstractions.Entities.Interfaces;

namespace Persistence
{
    public class ShopItemsContext : DbContext
    {
        public const string SchemaName = "shop";
        public const string MigrationsTableName = "__ShopItemsMigrationsHistory";

        static ShopItemsContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public ShopItemsContext(DbContextOptions options)
            : base(options)
        {
        }

        public ShopItemsContext()
            :base()
        {}

        public virtual DbSet<Item> Items { get; set; }

        public virtual DbSet<ItemImage> ItemsImages { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyMarker).Assembly);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IEntityWithId).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder
                    .Entity(entity.ClrType)
                    .Property<int>(nameof(IEntityWithId.Id))
                    .IsRequired(true);

                    modelBuilder
                    .Entity(entity.ClrType)
                    .HasKey(nameof(IEntityWithId.Id));
                }

                if (typeof(ITimeTrackable).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder
                    .Entity(entity.ClrType)
                    .Property<DateTime>(nameof(ITimeTrackable.CreatedAt))
                    .HasColumnType("timestamp without time zone")
                    .IsRequired(true);

                    modelBuilder
                    .Entity(entity.ClrType)
                    .Property<DateTime>(nameof(ITimeTrackable.UpdatedAt))
                    .HasColumnType("timestamp without time zone")
                    .IsRequired(true);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ProcessTimeTrackableItemsSave();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            ProcessTimeTrackableItemsSave();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ProcessTimeTrackableItemsSave();
         
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void ProcessTimeTrackableItemsSave()
        {
            var entries = ChangeTracker.Entries()
                .Where(x => x.Entity is ITimeTrackable);

            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.State is EntityState.Modified or EntityState.Added)
                {
                    ((ITimeTrackable)entry.Entity).UpdatedAt = utcNow;
                }

                if (entry.State is EntityState.Added)
                {
                    ((ITimeTrackable)entry.Entity).CreatedAt = utcNow;
                }
            }
        }
    }
}
