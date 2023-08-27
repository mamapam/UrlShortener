using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Infrastructure.DatabaseConfig;
using UrlShortener.Infrastructure.Repositories;

namespace UrlShortener.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Singleton DI for MongoDb configuration
        services.Configure<MongoDbConfig>(configuration.GetSection("MongoDb"));
        services.AddSingleton<MongoDbContext>();

        services.AddScoped<IUrlRepository, UrlRepository>();

        return services;
    }
}