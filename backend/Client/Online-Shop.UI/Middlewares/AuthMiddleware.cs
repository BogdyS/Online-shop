namespace Online_Shop.UI.Middlewares
{
    public class AuthMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var cookie = context.Request.Cookies;
            await next(context);
        }
    }
}
