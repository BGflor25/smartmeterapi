using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Configuration;
using SmartMeterApi.Domain;
using SmartMeterApi.Domain.Entities;

public sealed class InfluxService : IInfluxService
{
    private readonly InfluxDBClient _client;
    private readonly string _org;
    private readonly string _bucket;

    public InfluxService(
        InfluxDBClient client,
        IConfiguration configuration)
    {
        _client = client;
        _org = configuration["InfluxDB:Org"]!;
        _bucket = configuration["InfluxDB:Bucket"]!;
    }

    public async Task WriteAsync(MeterData meterData, CancellationToken ct = default)
    {
        var writeApi = _client.GetWriteApiAsync();

        var point = PointData
            .Measurement("meter_data")
            .Field("import_low_tariff", meterData.ElectricityImportLowTariff.Value)
            .Field("import_high_tariff", meterData.ElectricityImportHighTariff.Value)
            .Field("export_low_tariff", meterData.ElectricityExportLowTariff.Value)
            .Field("export_high_tariff", meterData.ElectricityExportHighTariff.Value)
            .Field("current_power_usage", meterData.CurrentPowerUsage.Value)
            .Field("current_power_delivery", meterData.CurrentPowerDelivery.Value)
            .Field("power_l1", meterData.InstantaneousActivePowerL1.Value)
            .Field("power_l2", meterData.InstantaneousActivePowerL2.Value)
            .Field("voltage", meterData.Voltage.Value)
            .Field("current", meterData.Current.Value)
            .Field("gas_consumption", meterData.GasConsumption)
            .Field("event_log_count", meterData.EventLogCount);
            //.Timestamp(meterData.Timestamp, WritePrecision.S);

        await writeApi.WritePointAsync(point, _bucket, _org, ct);
    }
}
