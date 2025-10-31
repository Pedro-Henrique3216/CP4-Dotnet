namespace MottuChallenge.Application.Configurations;

public class Settings
{
    public ConnectionSettings ConnectionStrings { get; set; }
    public SwaggerSettings Swagger { get; set; }
    public MongoDbSettings MongoDb { get; set; }
}