using FastEndpoints;
using InfluxDB.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using SmartMeterApi.Application.Interfaces;
using SmartMeterApi.Application.SmartMeter.Commands.AddMeterData;
using SmartMeterApi.Domain;
using SmartMeterApi.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Replace default logging with Serilog
    builder.Host.UseSerilog();

    ConfigureServices(builder.Services, builder.Configuration);
    Log.Information("Environment: {env}", builder.Environment.EnvironmentName);

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseFastEndpoints();

    // Health checks
    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.UseHttpsRedirection();

    app.Run();
    Log.Information("Application started successfully");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // API basics
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddFastEndpoints();

    // MediatR
    services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(
            typeof(AddSmartMeterDataCommand).Assembly));

    // Logging
    services.AddSingleton(Log.Logger);

    // Health checks
    services.AddHealthChecks()
        .AddCheck<SmartMeterApi.Infrastructure.HealthChecks.InfluxWriteHealthCheck>("influxdb_write");

    // ----------------------
    // InfluxDB
    // ----------------------
    services.AddSingleton<InfluxDBClient>(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();

        return new InfluxDBClient(
            configuration["InfluxDB:Url"]!,
            configuration["InfluxDB:Token"]!
        );
    });

    services.AddScoped<IInfluxService, InfluxService>();

}
