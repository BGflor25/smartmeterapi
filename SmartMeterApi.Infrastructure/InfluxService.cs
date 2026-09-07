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
    private readonly string _smartMeterBucket;
    private readonly string _pvBucket;

    private readonly string _waterSoftenerCapacityBucket;

    public InfluxService(
        InfluxDBClient client,
        IConfiguration configuration)
    {
        _client = client;
        _org = configuration["InfluxDB:Org"]!;
        _smartMeterBucket = configuration["InfluxDB:SmartMeterBucket"]!;
        _waterSoftenerCapacityBucket = configuration["InfluxDB:WaterSoftenerCapacityBucket"]!;
        _pvBucket = configuration["InfluxDB:PvBucket"]!;

    }

    public async Task WriteSmartMeterDataAsync(SmartMeterData meterData)
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
            .Field("event_log_count", meterData.EventLogCount)
            .Field("MeterStatus", meterData.MeterStatus)
            .Field("TariffIndicator", meterData.TariffIndicator)
            .Field("total_power_usage", meterData.TotalImportedActiveEnergy)
            .Field("total_power_delivery", meterData.TotalActivePowerExported)
            .Field("Pv_power_delivery", meterData.CurrentPVPowerDelivery);

        await writeApi.WritePointAsync(point, _smartMeterBucket, _org);
    }

    public async Task WriteWaterSoftenerDataAsync(WaterSoftenerCapacity waterSoftenerCapacity)
    {
        var writeApi = _client.GetWriteApiAsync();

        var point = PointData
            .Measurement("water_softener_data")
            .Field("capacity", waterSoftenerCapacity.Percentage);

        await writeApi.WritePointAsync (point, _waterSoftenerCapacityBucket, _org);
    }

    public async Task WritePvInstallationDataAsync(PvInstallationData pv)
    {
        var writeApi = _client.GetWriteApiAsync();

        var point = PointData.Measurement("pv_installation");

        if (!string.IsNullOrEmpty(pv.ModelName)) point = point.Field("model_name", pv.ModelName);
        if (!string.IsNullOrEmpty(pv.SerialNumber)) point = point.Field("serial_number", pv.SerialNumber);
        if (pv.RatedPower.HasValue) point = point.Field("rated_power", pv.RatedPower.Value);
        if (pv.InputPower.HasValue) point = point.Field("input_power", pv.InputPower.Value);
        if (pv.ActivePower.HasValue) point = point.Field("active_power", pv.ActivePower.Value);
        if (pv.ReactivePower.HasValue) point = point.Field("reactive_power", pv.ReactivePower.Value);
        if (pv.PowerFactor.HasValue) point = point.Field("power_factor", pv.PowerFactor.Value);
        if (pv.Efficiency.HasValue) point = point.Field("efficiency", pv.Efficiency.Value);
        if (pv.InternalTemperature.HasValue) point = point.Field("internal_temperature", pv.InternalTemperature.Value);
        if (!string.IsNullOrEmpty(pv.DeviceStatus)) point = point.Field("device_status", pv.DeviceStatus);
        if (pv.DayActivePowerPeak.HasValue) point = point.Field("day_active_power_peak", pv.DayActivePowerPeak.Value);
        if (pv.AccumulatedYieldEnergy.HasValue) point = point.Field("accumulated_yield_energy", pv.AccumulatedYieldEnergy.Value);
        if (pv.DailyYieldEnergy.HasValue) point = point.Field("daily_yield_energy", pv.DailyYieldEnergy.Value);

        if (pv.Pv01Voltage.HasValue) point = point.Field("pv_01_voltage", pv.Pv01Voltage.Value);
        if (pv.Pv01Current.HasValue) point = point.Field("pv_01_current", pv.Pv01Current.Value);
        if (pv.Pv02Voltage.HasValue) point = point.Field("pv_02_voltage", pv.Pv02Voltage.Value);
        if (pv.Pv02Current.HasValue) point = point.Field("pv_02_current", pv.Pv02Current.Value);
        if (pv.Pv03Voltage.HasValue) point = point.Field("pv_03_voltage", pv.Pv03Voltage.Value);
        if (pv.Pv03Current.HasValue) point = point.Field("pv_03_current", pv.Pv03Current.Value);
        if (pv.Pv04Voltage.HasValue) point = point.Field("pv_04_voltage", pv.Pv04Voltage.Value);
        if (pv.Pv04Current.HasValue) point = point.Field("pv_04_current", pv.Pv04Current.Value);

        if (pv.GridVoltage.HasValue) point = point.Field("grid_voltage", pv.GridVoltage.Value);
        if (pv.GridCurrent.HasValue) point = point.Field("grid_current", pv.GridCurrent.Value);
        if (pv.GridFrequency.HasValue) point = point.Field("grid_frequency", pv.GridFrequency.Value);

        if (pv.ActiveGridACurrent.HasValue) point = point.Field("active_grid_A_current", pv.ActiveGridACurrent.Value);
        if (pv.ActiveGridAPower.HasValue) point = point.Field("active_grid_A_power", pv.ActiveGridAPower.Value);
        if (pv.GridAVoltage.HasValue) point = point.Field("grid_A_voltage", pv.GridAVoltage.Value);

        if (pv.StorageStateOfCapacity.HasValue) point = point.Field("storage_state_of_capacity", pv.StorageStateOfCapacity.Value);
        if (pv.StorageChargeDischargePower.HasValue) point = point.Field("storage_charge_discharge_power", pv.StorageChargeDischargePower.Value);
        if (pv.StorageRunningStatus.HasValue) point = point.Field("storage_running_status", pv.StorageRunningStatus.Value);
        if (pv.StorageTotalCharge.HasValue) point = point.Field("storage_total_charge", pv.StorageTotalCharge.Value);
        if (pv.StorageTotalDischarge.HasValue) point = point.Field("storage_total_discharge", pv.StorageTotalDischarge.Value);

        await writeApi.WritePointAsync(point, _pvBucket, _org);
    }
}
