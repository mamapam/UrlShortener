using UrlShortener.Application.Common.Models;

namespace UrlShortener.Application.Common.Interfaces;

public interface IExceptionHandler
{
    ErrorResponse HandleException(Exception exception);
}