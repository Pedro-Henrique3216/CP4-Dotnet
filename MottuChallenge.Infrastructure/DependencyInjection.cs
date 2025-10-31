using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MottuChallenge.Application.Configurations;
using MottuChallenge.Application.Interfaces;
using MottuChallenge.Infrastructure.Persistence;
using MottuChallenge.Infrastructure.Repositories;
using MottuChallenge.Infrastructure.Services;

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
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
        
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAddressProvider, FindAddressByApiViaCep>();
            return services;
        }
        
        private static IServiceCollection AddMongoDB(this IServiceCollection services)
        {
            services.AddScoped<MongoContext>();
            return services;
        }
        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Settings settings)
        {
            services.AddDbContext(settings.ConnectionStrings);
            services.AddRepositories();
            services.AddMongoDB();
            services.AddServices();
            return services;
        }
    }
}
