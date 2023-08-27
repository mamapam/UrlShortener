using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Dtos;
using UrlShortener.Application.UrlApplication.Commands;
using UrlShortener.Application.UrlApplication.Queries;

namespace UrlShortener.WebApi.Controllers;

[ApiController]
[Route("url")]
public class UrlController : ControllerBase
{
    private readonly IMediator _mediator;

    public UrlController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{hash}")]
    public async Task<IActionResult> GetUrlByHashAsync(string hash)
    {
        var result = await _mediator.Send(new GetUrlByHashQuery(hash));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShortenedUrl([FromBody] CreateShortenedUrlDto url)
    {
        var result = await _mediator.Send(new CreateShortenedUrlCommand(url.Url));

        return CreatedAtAction(nameof(GetUrlByHashAsync), new { hash = result.UrlHash }, result);
    }
}