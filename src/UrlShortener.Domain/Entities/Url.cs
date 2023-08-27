using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrlShortener.Domain.Entities;

public class Url
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string UrlHash { get; set; } = null!;
    public string OriginalUrl { get; set; } = null!;
}