using MediatR;
using System.Text;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UrlApplication.Commands;

public class CreateShortenedUrlCommand : IRequest<Url>
{
    public string Url { get; set; }

    public CreateShortenedUrlCommand(string url)
    {
        Url = url;
    }
}

public class CreateShortenedUrlCommandHandler : IRequestHandler<CreateShortenedUrlCommand, Url>
{
    private readonly IUrlRepository _urlRepository;

    public CreateShortenedUrlCommandHandler(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    public async Task<Url> Handle(CreateShortenedUrlCommand request, CancellationToken cancellationToken)
    {
        var hash = GenerateUrlHash();
        var doesHashExist = true;

        var newUrl = new Url();

        while (doesHashExist)
        {
            var urlData = await _urlRepository.GetUrlByHashAsync(hash);

            if (urlData == null)
            {
                doesHashExist = false;
                newUrl = await _urlRepository.CreateShortenedUrlAsync(new Url { OriginalUrl = request.Url, UrlHash = hash });
            }
            else
            {
                hash = GenerateUrlHash();
            }
        }

        return newUrl;
    }

    private string GenerateUrlHash(int hashLength = 6)
    {
        var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        var sb = new StringBuilder();

        var rnd = new Random();
        for (var i = 0; i < hashLength; i++)
        {
            var randomIndex = rnd.Next(letters.Length);
            sb.Append(letters[randomIndex]);
        }

        return sb.ToString();
    }
}