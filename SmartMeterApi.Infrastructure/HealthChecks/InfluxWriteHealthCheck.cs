using Microsoft.Extensions.Diagnostics.HealthChecks;
using SmartMeterApi.Domain;
using SmartMeterApi.Domain.Entities;

namespace SmartMeterApi.Infrastructure.HealthChecks
{
    public class InfluxWriteHealthCheck : IHealthCheck
    {
        private readonly IInfluxService _influxService;

        public InfluxWriteHealthCheck(IInfluxService influxService)
        {
            _influxService = influxService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Perform a lightweight write to validate connectivity. Use a benign value.
                var sample = new WaterSoftenerCapacity { Percentage = 0 };
                await _influxService.WriteWaterSoftenerDataAsync(sample);

                return HealthCheckResult.Healthy("InfluxDB write succeeded");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("InfluxDB write failed", ex);
            }
        }
    }
}
