using InfluxDB.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SmartMeterApi.Infrastructure.HealthChecks;

/// <summary>Checks InfluxDB reachability via <c>/ping</c>. Read-only: it never writes telemetry.</summary>
internal sealed class InfluxPingHealthCheck(InfluxDBClient client) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var reachable = await client.PingAsync().WaitAsync(cancellationToken);
            return reachable
                ? HealthCheckResult.Healthy("InfluxDB is reachable")
                : new HealthCheckResult(context.Registration.FailureStatus, "InfluxDB ping failed");
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, "InfluxDB is unreachable", ex);
        }
    }
}
