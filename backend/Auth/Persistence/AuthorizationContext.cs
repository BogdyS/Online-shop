using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Entity;

namespace Persistence
{
    public class AuthorizationContext : IdentityDbContext<User>
    {
        public const string SchemaName = "authorization";
        public const string MigrationsTableName = "__AuthMigrationsHistory";

        static AuthorizationContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public AuthorizationContext(DbContextOptions options)
            : base(options)
        {
        }

        public AuthorizationContext()
            :base()
        {}

        public DbSet<Token> TokenStorage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyMarker).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
