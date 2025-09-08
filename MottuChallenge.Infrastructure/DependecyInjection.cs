using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MottuChallengeContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("MySqlConnection"));
            });

            return services;
        } 
    }
}
