using InfluxDB.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartMeterApi.Application.Abstractions;
using SmartMeterApi.Application.WaterSoftener;
using SmartMeterApi.Infrastructure.HealthChecks;
using SmartMeterApi.Infrastructure.Influx;

namespace SmartMeterApi.Infrastructure;

public static class DependencyInjection
{
    /// <summary>Configuration key kept for backwards compatibility with existing deployments.</summary>
    public const string WaterSoftenerMaxDepthKey = "InfluxDB:WaterSoftenerMaxDepth";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<InfluxDbOptions>()
            .Bind(configuration.GetSection(InfluxDbOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<WaterSoftenerOptions>()
            .Configure(o => o.MaxDepth = configuration.GetValue<double>(WaterSoftenerMaxDepthKey))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<InfluxDbOptions>>().Value;
            return new InfluxDBClient(options.Url, options.Token);
        });

        services.AddScoped<ITelemetryWriter, InfluxTelemetryWriter>();

        services.AddHealthChecks()
            .AddCheck<InfluxPingHealthCheck>("influxdb", tags: ["ready"]);

        return services;
    }
}
