using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApiVersionExtensions(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}
