using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using SmartMeterApi.Application;
using SmartMeterApi.Infrastructure;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog((services, logger) => logger
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services));

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services
        .AddFastEndpoints()
        .SwaggerDocument(o => o.DocumentSettings = s =>
        {
            s.Title = "SmartMeterApi";
            s.Version = "v1";
        });

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.UseFastEndpoints();
    app.UseSwaggerGen();

    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    });

    Log.Information("Starting SmartMeterApi ({Environment})", app.Environment.EnvironmentName);
    await app.RunAsync();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}

return 0;

public partial class Program;
