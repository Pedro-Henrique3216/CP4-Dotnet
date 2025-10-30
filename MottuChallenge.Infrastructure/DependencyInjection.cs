using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MottuChallenge.Application.Configurations;
using MottuChallenge.Infrastructure.Persistence;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Infrastructure
{
    public static class DependencyInjection
    {
        private static IServiceCollection AddDbContext(this IServiceCollection services, ConnectionSettings connectionSettings)
        {
            var connectionString = connectionSettings.MysqlConnection;
            services.AddDbContext<MottuChallengeContext>(options =>
            {
                options.UseMySQL(connectionString);
            });

            return services;
        }
        
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IYardRepository, YardRepository>();
            services.AddScoped<ISectorRepository, SectorRepository>();
            services.AddScoped<ISectorTypeRepository, SectorTypeRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            return services;
        }
        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Settings settings)
        {
            services.AddDbContext(settings.ConnectionStrings);
            services.AddRepositories();
            return services;
        }
    }
}
