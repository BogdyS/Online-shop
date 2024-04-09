using Amazon.S3;
using Core.DI;
using Core.Options;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shop.API.Common;
using Shop.Services.AWS;
using Shop.Services.Interfaces;
using System.Reflection;

namespace Shop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Add services to the container.

            builder.Services.AddControllers(
                opt => opt.Filters.Add<ExceptionFilter>());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();

                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Online Shop API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var connectionString = builder.Configuration.GetConnectionString("ShopItems")
                ?? throw new ArgumentNullException();

            builder.Services.AddDbContext<ShopItemsContext>(
                opt => opt.UseNpgsql(connectionString,
                migrations => migrations.MigrationsHistoryTable(
                    tableName: ShopItemsContext.MigrationsTableName,
                    schema: ShopItemsContext.SchemaName)));

            builder.Services.Configure<ItemsBucketOptions>(
                builder.Configuration.GetRequiredSection(nameof(ItemsBucketOptions)));

            builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
            builder.Services.AddAWSService<IAmazonS3>();
            builder.Services.AddScoped<IS3Service, S3ItemsService>();

            // Auto register DI
            var assemblyMarkers = new Type[]
            {
                typeof(Persistence.AssemblyMarker),
                typeof(AssemblyMarker),
                typeof(Services.AssemblyMarker),
                typeof(Shop.API.Controllers.Categories.MappingProfile)
            };

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(assemblyMarkers.Select(x => x.Assembly).ToArray()));

            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(assemblyMarkers));

            builder.Services.Scan(scan =>
            {
                var implementations = scan.FromAssembliesOf(assemblyMarkers);

                var lifetimes = Enum.GetValues(typeof(ServiceLifetime)).Cast<ServiceLifetime>();
                foreach (var lifetime in lifetimes)
                {
                    implementations.AddClasses(
                        classes => classes
                        .WithAttribute<AutomaticRegistrationAttribute>(atr => atr.Lifetime == lifetime))
                    .AsMatchingInterface()
                    .WithLifetime(lifetime);
                }
            });

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
                var context = scope.ServiceProvider.GetRequiredService<ShopItemsContext>();

                //context.Database.EnsureCreated();
                context.Database.Migrate();
            }


            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseCors("LocalhostPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}