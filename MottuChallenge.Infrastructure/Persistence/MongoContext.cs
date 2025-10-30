using MongoDB.Driver;
using MottuChallenge.Application.Configurations;

namespace MottuChallenge.Infrastructure.Persistence;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(Settings settings)
    {
        var client = new MongoClient(settings.MongoDb.ConnectionString);
        _database = client.GetDatabase(settings.MongoDb.DatabaseName);
    }
    
    public IMongoDatabase Database => _database;
}