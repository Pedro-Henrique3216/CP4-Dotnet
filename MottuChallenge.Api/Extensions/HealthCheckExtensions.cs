using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using MottuChallenge.Application.Configurations;

namespace MottuChallenge.Api.Extensions;

public static class HealthCheckExtensions
{
    
    public static Task WriteResponse(HttpContext context, HealthReport report)
    {
        JsonSerializerOptions jsonSerializerOptions = new()
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var json = JsonSerializer.Serialize(
            new
            {
                Status = report.Status.ToString(),
                Duration = report.TotalDuration,
                Info = report.Entries.Select(entry =>
                    new
                    {
                        entry.Key,
                        entry.Value.Description,
                        entry.Value.Duration,
                        Status = Enum.GetName(typeof(HealthStatus), entry.Value.Status),
                        Error = entry.Value.Exception?.Message,
                        entry.Value.Data
                    }).ToList()
            }, jsonSerializerOptions);

        context.Response.ContentType = MediaTypeNames.Application.Json;

        return context.Response.WriteAsync(json);
    }
    
    public static IServiceCollection AddHealthServices(this IServiceCollection services, Settings settings)
    {
        services
            .AddHealthChecks()
            .AddMySql(settings.ConnectionStrings.MysqlConnection, name: "mysql connection")
            .AddMongoDb(clientFactory: sp => new MongoClient(settings.MongoDb.ConnectionString),
                databaseNameFactory: _ => settings.MongoDb.DatabaseName,
                name: "mongo connection")
            .AddUrlGroup(new Uri("https://viacep.com.br/"), name: "Via Cep API");
        
        return services;
    }
    
    
}