using MongoDB.Driver;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.DatabaseConfig;

namespace UrlShortener.Infrastructure.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly IMongoCollection<Url> _urls;

    public UrlRepository(MongoDbContext dbContext)
    {
        _urls = dbContext.Database.GetCollection<Url>(MongoCollectionMapping.Url);
    }

    public async Task<Url> CreateShortenedUrlAsync(Url url)
    {
        await _urls.InsertOneAsync(url);
        return url;
    }

    public async Task<Url> GetUrlByHashAsync(string hash)
    {
        var url = await _urls.Find(u => u.UrlHash == hash).FirstOrDefaultAsync();
        return url;
    }
}