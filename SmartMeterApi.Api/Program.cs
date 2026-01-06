using FastEndpoints;
using InfluxDB.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using SmartMeterApi.Application.Interfaces;
using SmartMeterApi.Domain;
using SmartMeterApi.Infrastructure;

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
            typeof(SmartMeterApi.Application.Commands.AddMeterData.AddMeterDataCommand).Assembly));

    // Logging
    services.AddSingleton(Log.Logger);

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
