namespace UrlShortener.Infrastructure.DatabaseConfig;

public class MongoDbConfig
{
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Server { get; set; } = String.Empty;
    public string Port { get; set; } = String.Empty;
    public string Database { get; set; } = String.Empty;
}