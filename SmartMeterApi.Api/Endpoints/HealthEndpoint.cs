using FastEndpoints;
using InfluxDB.Client;

namespace SmartMeterApi.Api.Endpoints;

public class HealthEndpoint : EndpointWithoutRequest
{

    public override void Configure()
    {
        Get("/health");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            // Resolve client from request services to avoid failing startup when Influx config is missing
            var client = HttpContext.RequestServices.GetService(typeof(InfluxDBClient)) as InfluxDBClient;

            if (client == null)
            {
                HttpContext.Response.StatusCode = 503;
                await HttpContext.Response.WriteAsJsonAsync(new { status = "Unhealthy", error = "InfluxDB client not configured" }, cancellationToken: ct);
                return;
            }

            // Use PingAsync to check connectivity.
            var pingResult = await client.PingAsync();

            if (pingResult)
            {
                await Send.OkAsync(new { status = "Healthy", influx = true }, ct);
                return;
            }

            // Ping returned false -> degraded
            HttpContext.Response.StatusCode = 503;
            await HttpContext.Response.WriteAsJsonAsync(new { status = "Degraded", influx = false }, cancellationToken: ct);
        }
        catch (Exception ex)
        {
            // Could not reach InfluxDB
            HttpContext.Response.StatusCode = 503;
            await HttpContext.Response.WriteAsJsonAsync(new { status = "Unhealthy", error = ex.Message }, cancellationToken: ct);
        }
    }
}
