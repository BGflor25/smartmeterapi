namespace SmartMeterApi.Domain.Entities;

/// <summary>A snapshot of a PV inverter (and optional battery storage). Every reading is optional.</summary>
public sealed class PvInstallationData
{
    public double Timestamp { get; init; }

    public string? ModelName { get; init; }
    public string? SerialNumber { get; init; }
    public string? DeviceStatus { get; init; }

    public double? RatedPower { get; init; }
    public double? InputPower { get; init; }
    public double? ActivePower { get; init; }
    public double? ReactivePower { get; init; }
    public double? PowerFactor { get; init; }
    public double? Efficiency { get; init; }
    public double? InternalTemperature { get; init; }
    public double? DayActivePowerPeak { get; init; }
    public double? AccumulatedYieldEnergy { get; init; }
    public double? DailyYieldEnergy { get; init; }

    public double? Pv01Voltage { get; init; }
    public double? Pv01Current { get; init; }
    public double? Pv02Voltage { get; init; }
    public double? Pv02Current { get; init; }
    public double? Pv03Voltage { get; init; }
    public double? Pv03Current { get; init; }
    public double? Pv04Voltage { get; init; }
    public double? Pv04Current { get; init; }

    public double? GridVoltage { get; init; }
    public double? GridCurrent { get; init; }
    public double? GridFrequency { get; init; }
    public double? ActiveGridACurrent { get; init; }
    public double? ActiveGridAPower { get; init; }
    public double? GridAVoltage { get; init; }

    public double? StorageStateOfCapacity { get; init; }
    public double? StorageChargeDischargePower { get; init; }
    public int? StorageRunningStatus { get; init; }
    public double? StorageTotalCharge { get; init; }
    public double? StorageTotalDischarge { get; init; }
}
