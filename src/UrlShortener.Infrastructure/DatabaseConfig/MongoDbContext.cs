using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UrlShortener.Infrastructure.DatabaseConfig;

public class MongoDbContext
{
    public MongoClient Client { get; }
    public IMongoDatabase Database { get; }

    public MongoDbContext(IOptions<MongoDbConfig> options)
    {
        var config = options.Value;
        var connectionString = $"mongodb://{config.Username}:{config.Password}@{config.Server}:{config.Port}";

        Client = new MongoClient(connectionString);
        Database = Client.GetDatabase(config.Database);
    }
}