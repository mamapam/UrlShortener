using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Common.Models;

namespace UrlShortener.WebApi.Middleware;

public class AppResponseHandlerMiddleware
{
    private readonly IExceptionHandler _exceptionHandler;
    private readonly RequestDelegate _next;

    public AppResponseHandlerMiddleware(IExceptionHandler exceptionHandler, RequestDelegate next)
    {
        _exceptionHandler = exceptionHandler;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var errorList = new ErrorResponseWrapper();
            var error = _exceptionHandler.HandleException(ex);

            errorList.Errors.Add(error);
            if (!httpContext.Response.HasStarted)
            {
                httpContext.Response.Clear();
                httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                httpContext.Response.StatusCode = (int)error.StatusCode;

                var jsonOptions = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorList, jsonOptions));
            }
        }
    }
}