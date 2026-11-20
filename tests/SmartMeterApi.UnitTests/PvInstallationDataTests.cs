using System.Text.Json;
using SmartMeterApi.Application.PvInstallation.AddPvInstallationData;
using SmartMeterApi.Infrastructure.Influx;

namespace SmartMeterApi.UnitTests;

public class PvInstallationDataTests
{
    [Fact]
    public void ToDomain_handles_partial_payload()
    {
        const string payload = """
            {
              "timestamp": 1763648000,
              "model_name": { "value": "SUN2000-5KTL", "unit": null },
              "active_power": { "value": 3.21, "unit": "kW" },
              "pv_01_voltage": { "value": null, "unit": "V" },
              "storage_running_status": { "value": 2.0, "unit": null }
            }
            """;

        var data = JsonSerializer.Deserialize<PvInstallationDataRequest>(payload)!.ToDomain();

        Assert.Equal("SUN2000-5KTL", data.ModelName);
        Assert.Equal(3.21, data.ActivePower);
        Assert.Null(data.Pv01Voltage);
        Assert.Null(data.GridVoltage);
        Assert.Equal(2, data.StorageRunningStatus);
    }

    [Fact]
    public void ToPoint_only_contains_present_fields()
    {
        var request = new PvInstallationDataRequest
        {
            ActivePower = new() { Value = 3.21, Unit = "kW" },
            DeviceStatus = new() { Value = "On-grid" },
            Pv01Voltage = new() { Value = null },
        };

        var lineProtocol = InfluxTelemetryWriter.ToPoint(request.ToDomain()).ToLineProtocol();

        Assert.StartsWith("pv_installation ", lineProtocol);
        Assert.Contains("active_power=3.21", lineProtocol);
        Assert.Contains("device_status=\"On-grid\"", lineProtocol);
        Assert.DoesNotContain("pv_01_voltage", lineProtocol);
    }
}
