using System.Net;

namespace UrlShortener.Application.Common.Models;

public class ErrorResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Title { get; set; }

    public string? Exception { get; set; }

    public List<ErrorDetail>? Entries { get; set; }

    public ErrorResponse(string title)
    {
        Title = title;
    }
}

public class ErrorResponseWrapper
{
    public List<ErrorResponse> Errors { get; set; } = new List<ErrorResponse>();
}