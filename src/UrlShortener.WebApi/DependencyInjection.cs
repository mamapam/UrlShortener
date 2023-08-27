using UrlShortener.Application.Common.Exceptions;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.WebApi.Middleware;

namespace UrlShortener.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionHandler, ExceptionHandler>();

        return services;
    }

    public static IApplicationBuilder UseAppExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<AppResponseHandlerMiddleware>();

        return builder;
    }
}