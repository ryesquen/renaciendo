using BancoAPI.Application.Interfaces;
using BancoAPI.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BancoAPI.Shared
{
    public static class ServiceExtensions
    {
        public static void AddShareInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}