namespace UrlShortener.Application.Common.Models;

public class ErrorDetail
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
}