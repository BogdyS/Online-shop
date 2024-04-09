
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entity;
using Services;

namespace OnlineShop.Authorization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("AuthDatabase")
                ?? throw new ArgumentNullException();

            builder.Services.AddDbContext<AuthorizationContext>(opt => opt.UseNpgsql(connectionString,
                migrations => migrations.MigrationsHistoryTable(
                    tableName: AuthorizationContext.MigrationsTableName,
                    schema: AuthorizationContext.SchemaName)));

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequiredLength = 5;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<AuthorizationContext>();

            builder.Services.AddCors(options =>
                options.AddPolicy("LocalhostPolicy", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                })
            );

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            {
                var context = scope.ServiceProvider.GetRequiredService<AuthorizationContext>();

                context.Database.Migrate();
            }


            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseCors("LocalhostPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}