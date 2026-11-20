using SmartMeterApi.Domain.Entities;

namespace SmartMeterApi.Application.PvInstallation.AddPvInstallationData;

public static class PvInstallationDataMapping
{
    public static PvInstallationData ToDomain(this PvInstallationDataRequest request) => new()
    {
        Timestamp = request.Timestamp,
        ModelName = request.ModelName?.Value,
        SerialNumber = request.SerialNumber?.Value,
        DeviceStatus = request.DeviceStatus?.Value,
        RatedPower = request.RatedPower?.Value,
        InputPower = request.InputPower?.Value,
        ActivePower = request.ActivePower?.Value,
        ReactivePower = request.ReactivePower?.Value,
        PowerFactor = request.PowerFactor?.Value,
        Efficiency = request.Efficiency?.Value,
        InternalTemperature = request.InternalTemperature?.Value,
        DayActivePowerPeak = request.DayActivePowerPeak?.Value,
        AccumulatedYieldEnergy = request.AccumulatedYieldEnergy?.Value,
        DailyYieldEnergy = request.DailyYieldEnergy?.Value,
        Pv01Voltage = request.Pv01Voltage?.Value,
        Pv01Current = request.Pv01Current?.Value,
        Pv02Voltage = request.Pv02Voltage?.Value,
        Pv02Current = request.Pv02Current?.Value,
        Pv03Voltage = request.Pv03Voltage?.Value,
        Pv03Current = request.Pv03Current?.Value,
        Pv04Voltage = request.Pv04Voltage?.Value,
        Pv04Current = request.Pv04Current?.Value,
        GridVoltage = request.GridVoltage?.Value,
        GridCurrent = request.GridCurrent?.Value,
        GridFrequency = request.GridFrequency?.Value,
        ActiveGridACurrent = request.ActiveGridACurrent?.Value,
        ActiveGridAPower = request.ActiveGridAPower?.Value,
        GridAVoltage = request.GridAVoltage?.Value,
        StorageStateOfCapacity = request.StorageStateOfCapacity?.Value,
        StorageChargeDischargePower = request.StorageChargeDischargePower?.Value,
        StorageRunningStatus = request.StorageRunningStatus?.Value is { } status ? Convert.ToInt32(status) : null,
        StorageTotalCharge = request.StorageTotalCharge?.Value,
        StorageTotalDischarge = request.StorageTotalDischarge?.Value,
    };
}
