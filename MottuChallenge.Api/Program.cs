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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "MottuChallenge API",
                    Version = "v1",
                    Description = "API para gerenciamento de setores e p�tios"
                });
            });

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

            app.Run();
        }
    }
}
