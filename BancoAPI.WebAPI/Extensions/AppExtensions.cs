using BancoAPI.WebAPI.Middlewares;

namespace BancoAPI.WebAPI.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHandlingMiddelware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddelware>();
        }
    }
}
