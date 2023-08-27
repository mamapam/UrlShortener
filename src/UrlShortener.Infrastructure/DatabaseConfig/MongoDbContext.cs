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
        string connectionString;

        if (config.UseSrv)
        {
            connectionString = $"mongodb+srv://{config.Username}:{config.Password}@{config.Server}/?retryWrites=true&w=majority";
        }
        else
        {
            connectionString = $"mongodb://{config.Username}:{config.Password}@{config.Server}:{config.Port}";
        }

        Client = new MongoClient(connectionString);
        Database = Client.GetDatabase(config.Database);
    }
}