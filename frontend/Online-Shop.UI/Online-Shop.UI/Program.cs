using MyNamespace;
using Online_Shop.UI.Options;

namespace Online_Shop.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            var baseUrl = builder.Configuration.GetSection("ApiGateway")["BaseUrl"];
            builder.Services.AddScoped<IProductsClient>(c => new ProductsClient(baseUrl, new HttpClient() { BaseAddress = new Uri(baseUrl)}));
            builder.Services.Configure<ApiGatewayOptions>(builder.Configuration.GetRequiredSection("ApiGateway"));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers["Cache-Control"] = "no-cache";
                await next();
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
