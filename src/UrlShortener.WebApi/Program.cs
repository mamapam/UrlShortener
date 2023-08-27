using Serilog;
using Serilog.Events;
using UrlShortener.Application;
using UrlShortener.Infrastructure;
using UrlShortener.WebApi;

// ****************** Setup Logger *******************
// Logger setup at startup time. It can read from appsettings.json later
// https://github.com/serilog/serilog-aspnetcore#two-stage-initialization
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    // ************** Build Web Application **************
    Log.Information("Setting up builder for web application");

    var builder = WebApplication.CreateBuilder(args);

    // Make Serilog read from appsettings.json and complete setup
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    // Add services to the container.
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddExceptionHandler();

    builder.Services.AddControllersWithViews();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // **************** Web Application *****************
    Log.Information("Building Web Application");

    var app = builder.Build();

    app.UseSerilogRequestLogging(configure =>
    {
        configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.000}ms";
    });

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAppExceptionHandler();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseEndpoints(config =>
    {
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSpa(spa =>
        {
            spa.UseProxyToSpaDevelopmentServer("https://localhost:5173");
        });
    }
    else
    {
        app.MapFallbackToFile("index.html");
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}