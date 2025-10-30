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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger(configs.Swagger);
            builder.Services.AddHealthServices(configs.ConnectionStrings);
            builder.Services.AddVersioning();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(ui =>
                    {
                        ui.SwaggerEndpoint("/swagger/v1/swagger.json",  "MottuGrid.API v1");
                        ui.SwaggerEndpoint("/swagger/v2/swagger.json",  "MottuGrid.API v2");
                    }
                );;
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
