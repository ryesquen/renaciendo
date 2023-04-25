using BancoAPI.Application.Interfaces;
using BancoAPI.Persistence.Contexts;
using BancoAPI.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BancoAPI.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersintenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("cn"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                });
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));

            services.AddStackExchangeRedisCache(
                options =>
                {
                    options.Configuration = configuration.GetValue<string>("Caching:RedisConnection");
                });
        }
    }
}
