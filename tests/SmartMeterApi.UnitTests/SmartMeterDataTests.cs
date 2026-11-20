using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using SmartMeterApi.Application.Abstractions;
using SmartMeterApi.Application.SmartMeter.AddSmartMeterData;
using SmartMeterApi.Domain.Entities;
using SmartMeterApi.Infrastructure.Influx;

namespace SmartMeterApi.UnitTests;

public class SmartMeterDataTests
{
    private const string Payload = """
        {
          "meter_model": 5,
          "equipment_identifier": 123,
          "timestamp": 251120143000,
          "electricity_import_low_tariff": { "value": 1000.5, "unit": "kWh" },
          "electricity_import_high_tariff": { "value": 2000.25, "unit": "kWh" },
          "electricity_export_low_tariff": { "value": 300, "unit": "kWh" },
          "electricity_export_high_tariff": { "value": 400, "unit": "kWh" },
          "tariff_indicator": 2,
          "1-0:1.4.0": { "value": 1.75, "unit": "kW" },
          "1-0:1.6.0": 4.2,
          "0-0:98.1.0": 3,
          "current_power_usage": { "value": 0.8, "unit": "kW" },
          "current_power_delivery": { "value": 0, "unit": "kW" },
          "instantaneous_active_power_l1_pos": { "value": 0.5, "unit": "kW" },
          "instantaneous_active_power_l2_pos": { "value": 0.3, "unit": "kW" },
          "voltage": { "value": 231.1, "unit": "V" },
          "current": { "value": 3, "unit": "A" },
          "0-0:96.3.10": 1,
          "0-0:17.0.0": { "value": 9.9, "unit": "kW" },
          "max_current": { "value": 40, "unit": "A" },
          "0-1:24.1.0": 3,
          "gas_device_id": 42,
          "0-1:24.4.0": 1,
          "gas_consumption": 1234.567,
          "water_consumption": 12.5,
          "checksum": "ABCD"
        }
        """;

    private static SmartMeterDataRequest Deserialize() =>
        JsonSerializer.Deserialize<SmartMeterDataRequest>(Payload)!;

    [Fact]
    public void ToDomain_maps_device_json_to_entity()
    {
        var data = Deserialize().ToDomain();

        Assert.Equal(1000.5, data.ElectricityImportLowTariff!.Value);
        Assert.Equal("kWh", data.ElectricityImportLowTariff.Unit);
        Assert.Equal(1.75, data.TotalActivePowerExported!.Value);
        Assert.Equal(4.2, data.TotalImportedActiveEnergy);
        Assert.Equal(231.1, data.Voltage!.Value);
        Assert.Equal(9.9, data.MaxContractualPower!.Value);
        Assert.Equal(1234.567, data.GasConsumption);
        Assert.Equal(12.5, data.WaterConsumption);
    }

    [Fact]
    public void ToPoint_writes_total_power_export_as_number()
    {
        var lineProtocol = InfluxTelemetryWriter.ToPoint(Deserialize().ToDomain()).ToLineProtocol();

        Assert.StartsWith("meter_data ", lineProtocol);
        Assert.Contains("total_power_export=1.75", lineProtocol);
        Assert.Contains("import_low_tariff=1000.5", lineProtocol);
        Assert.Contains("total_power_usage=4.2", lineProtocol);
        Assert.DoesNotContain("total_power_delivery", lineProtocol);
        Assert.DoesNotContain("Pv_power_delivery", lineProtocol);
    }

    [Fact]
    public void ToPoint_skips_missing_measurements()
    {
        var lineProtocol = InfluxTelemetryWriter.ToPoint(new SmartMeterData { GasConsumption = 1 }).ToLineProtocol();

        Assert.DoesNotContain("voltage", lineProtocol);
        Assert.Contains("gas_consumption=1", lineProtocol);
    }

    [Fact]
    public async Task Handler_writes_mapped_data_once()
    {
        var writer = Substitute.For<ITelemetryWriter>();
        var handler = new AddSmartMeterDataCommandHandler(writer, NullLogger<AddSmartMeterDataCommandHandler>.Instance);

        await handler.Handle(new AddSmartMeterDataCommand(Deserialize()), CancellationToken.None);

        await writer.Received(1).WriteSmartMeterDataAsync(
            Arg.Is<SmartMeterData>(d => d.CurrentPowerUsage!.Value == 0.8),
            Arg.Any<CancellationToken>());
    }
}
