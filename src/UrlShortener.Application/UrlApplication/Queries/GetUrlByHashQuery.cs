using MediatR;
using UrlShortener.Application.Common.Exceptions;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UrlApplication.Queries;

public class GetUrlByHashQuery : IRequest<Url>
{
    public string Hash { get; set; }

    public GetUrlByHashQuery(string hash)
    {
        Hash = hash;
    }
}

public class GetUrlByHashQueryHandler : IRequestHandler<GetUrlByHashQuery, Url>
{
    private readonly IUrlRepository _urlRepository;

    public GetUrlByHashQueryHandler(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    public async Task<Url> Handle(GetUrlByHashQuery request, CancellationToken cancellationToken)
    {
        var url = await _urlRepository.GetUrlByHashAsync(request.Hash);

        if (url == null)
        {
            throw new NotFoundException("Url hash not found");
        }

        return url;
    }
}