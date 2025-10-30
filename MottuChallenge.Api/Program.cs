using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MottuChallenge.Api.Extensions;
using MottuChallenge.Application.Configurations;
using MottuChallenge.Application.Services;
using MottuChallenge.Infrastructure;

namespace MottuChallenge.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configs = builder.Configuration.Get<Settings>();
            builder.Services.AddInfrastructure(configs);
            builder.Services.AddControllers();
            builder.Services.AddScoped<IYardService, YardService>();
            builder.Services.AddHttpClient<IAddressService, AddressService>();
            builder.Services.AddScoped<ISectorService, SectorService>();
            builder.Services.AddScoped<ISpotService, SpotService>();
            builder.Services.AddScoped<ISectorTypeService, SectorTypeService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger(configs.Swagger);
            builder.Services.AddHealthServices(configs.ConnectionStrings);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.MapControllers();
            
            app.MapHealthChecks("/api/health-check", new HealthCheckOptions()
            {
                ResponseWriter = HealthCheckExtensions.WriteResponse
            });

            app.Run();
        }
    }
}
