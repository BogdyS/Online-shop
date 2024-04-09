using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OnlineShop.Gateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddOcelot();

            builder.Services.AddCors(builder =>
            {
                builder.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            await app.UseOcelot();
            app.UseCors("AllowAll");

            await app.RunAsync();
        }
    }
}
