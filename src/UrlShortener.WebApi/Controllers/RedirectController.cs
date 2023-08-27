using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.UrlApplication.Queries;

namespace UrlShortener.WebApi.Controllers;

public class RedirectController : ControllerBase
{
    private readonly IMediator _mediator;

    public RedirectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("/{hash}")]
    public async Task<IActionResult> RedirectToUrl(string hash)
    {
        var result = await _mediator.Send(new GetUrlByHashQuery(hash));

        return Redirect(result.OriginalUrl);
    }
}