using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Common.Models;

namespace UrlShortener.Application.Common.Exceptions;

public class ExceptionHandler : IExceptionHandler
{
    private readonly IHostEnvironment _environment;
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(IHostEnvironment environment, ILogger<ExceptionHandler> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public ErrorResponse HandleException(Exception exception)
    {
        var error = exception switch
        {
            NotFoundException notFoundException => HandleNotFoundException(notFoundException),
            ValidateException validateException => HandleValidateException(validateException),
            _ => HandleUnhandledException(exception)
        };

        if (_environment.IsDevelopment())
        {
            error.Exception = exception.ToString();
        }

        return error;
    }

    private ErrorResponse HandleNotFoundException(NotFoundException notFoundException)
    {
        _logger.LogError(notFoundException, "{NotFoundException}", notFoundException.Message);

        var error = new ErrorResponse(notFoundException.Message)
        {
            StatusCode = HttpStatusCode.NotFound
        };

        return error;
    }

    private ErrorResponse HandleValidateException(ValidateException validateException)
    {
        _logger.LogError(validateException, "{ValidationException}", validateException.Message);

        var error = new ErrorResponse(validateException.Message)
        {
            StatusCode = HttpStatusCode.BadRequest
        };

        if (validateException.Errors != null && validateException.Errors.Any())
        {
            error.Entries = new List<ErrorDetail>();

            error.Entries.AddRange(validateException.Errors.Select(validationError => new ErrorDetail
            {
                Code = validationError.ErrorCode,
                Title = validationError.ErrorMessage,
                Source = validationError.PropertyName
            }));
        }

        return error;
    }

    private ErrorResponse HandleUnhandledException(Exception exception)
    {
        _logger.LogError(exception, "{UnhandledException}", exception.Message);

        var error = new ErrorResponse("An unhandled error occurred!")
        {
            StatusCode = HttpStatusCode.InternalServerError
        };

        return error;
    }
}