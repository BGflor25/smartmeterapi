using InfluxDB.Client;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Options;
using SmartMeterApi.Application.Abstractions;
using SmartMeterApi.Domain.Entities;

namespace SmartMeterApi.Infrastructure.Influx;

internal sealed class InfluxTelemetryWriter(InfluxDBClient client, IOptions<InfluxDbOptions> options) : ITelemetryWriter
{
    internal const string SmartMeterMeasurement = "meter_data";
    internal const string PvInstallationMeasurement = "pv_installation";
    internal const string WaterSoftenerMeasurement = "water_softener_data";

    private readonly InfluxDbOptions _options = options.Value;

    public Task WriteSmartMeterDataAsync(SmartMeterData data, CancellationToken cancellationToken = default) =>
        WriteAsync(ToPoint(data), _options.SmartMeterBucket, cancellationToken);

    public Task WritePvInstallationDataAsync(PvInstallationData data, CancellationToken cancellationToken = default) =>
        WriteAsync(ToPoint(data), _options.PvBucket, cancellationToken);

    public Task WriteWaterSoftenerDataAsync(WaterSoftenerCapacity data, CancellationToken cancellationToken = default) =>
        WriteAsync(ToPoint(data), _options.WaterSoftenerCapacityBucket, cancellationToken);

    internal static PointData ToPoint(SmartMeterData data) =>
        PointData.Measurement(SmartMeterMeasurement)
            .OptionalField("import_low_tariff", data.ElectricityImportLowTariff)
            .OptionalField("import_high_tariff", data.ElectricityImportHighTariff)
            .OptionalField("export_low_tariff", data.ElectricityExportLowTariff)
            .OptionalField("export_high_tariff", data.ElectricityExportHighTariff)
            .OptionalField("current_power_usage", data.CurrentPowerUsage)
            .OptionalField("current_power_delivery", data.CurrentPowerDelivery)
            .OptionalField("power_l1", data.InstantaneousActivePowerL1)
            .OptionalField("power_l2", data.InstantaneousActivePowerL2)
            .OptionalField("voltage", data.Voltage)
            .OptionalField("current", data.Current)
            .OptionalField("total_power_export", data.TotalActivePowerExported)
            .Field("gas_consumption", data.GasConsumption)
            .Field("event_log_count", data.EventLogCount)
            .Field("MeterStatus", data.MeterStatus)
            .Field("TariffIndicator", data.TariffIndicator)
            .Field("total_power_usage", data.TotalImportedActiveEnergy);

    internal static PointData ToPoint(PvInstallationData pv) =>
        PointData.Measurement(PvInstallationMeasurement)
            .OptionalField("model_name", pv.ModelName)
            .OptionalField("serial_number", pv.SerialNumber)
            .OptionalField("device_status", pv.DeviceStatus)
            .OptionalField("rated_power", pv.RatedPower)
            .OptionalField("input_power", pv.InputPower)
            .OptionalField("active_power", pv.ActivePower)
            .OptionalField("reactive_power", pv.ReactivePower)
            .OptionalField("power_factor", pv.PowerFactor)
            .OptionalField("efficiency", pv.Efficiency)
            .OptionalField("internal_temperature", pv.InternalTemperature)
            .OptionalField("day_active_power_peak", pv.DayActivePowerPeak)
            .OptionalField("accumulated_yield_energy", pv.AccumulatedYieldEnergy)
            .OptionalField("daily_yield_energy", pv.DailyYieldEnergy)
            .OptionalField("pv_01_voltage", pv.Pv01Voltage)
            .OptionalField("pv_01_current", pv.Pv01Current)
            .OptionalField("pv_02_voltage", pv.Pv02Voltage)
            .OptionalField("pv_02_current", pv.Pv02Current)
            .OptionalField("pv_03_voltage", pv.Pv03Voltage)
            .OptionalField("pv_03_current", pv.Pv03Current)
            .OptionalField("pv_04_voltage", pv.Pv04Voltage)
            .OptionalField("pv_04_current", pv.Pv04Current)
            .OptionalField("grid_voltage", pv.GridVoltage)
            .OptionalField("grid_current", pv.GridCurrent)
            .OptionalField("grid_frequency", pv.GridFrequency)
            .OptionalField("active_grid_A_current", pv.ActiveGridACurrent)
            .OptionalField("active_grid_A_power", pv.ActiveGridAPower)
            .OptionalField("grid_A_voltage", pv.GridAVoltage)
            .OptionalField("storage_state_of_capacity", pv.StorageStateOfCapacity)
            .OptionalField("storage_charge_discharge_power", pv.StorageChargeDischargePower)
            .OptionalField("storage_running_status", pv.StorageRunningStatus)
            .OptionalField("storage_total_charge", pv.StorageTotalCharge)
            .OptionalField("storage_total_discharge", pv.StorageTotalDischarge);

    internal static PointData ToPoint(WaterSoftenerCapacity data) =>
        PointData.Measurement(WaterSoftenerMeasurement)
            .Field("capacity", data.Ratio);

    private Task WriteAsync(PointData point, string bucket, CancellationToken cancellationToken) =>
        client.GetWriteApiAsync().WritePointAsync(point, bucket, _options.Org, cancellationToken);
}
