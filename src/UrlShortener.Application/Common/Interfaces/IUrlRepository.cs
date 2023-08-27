using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Common.Interfaces;

public interface IUrlRepository
{
    Task<Url> CreateShortenedUrlAsync(Url url);

    Task<Url> GetUrlByHashAsync(string hash);
}