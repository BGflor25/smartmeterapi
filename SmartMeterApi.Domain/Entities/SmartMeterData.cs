using SmartMeterApi.Domain.ValueObjects;

namespace SmartMeterApi.Domain.Entities;

/// <summary>A single telegram read from a DSMR / P1 smart meter.</summary>
public sealed class SmartMeterData
{
    public double MeterModel { get; init; }
    public double EquipmentIdentifier { get; init; }
    public double Timestamp { get; init; }

    public Measurement? ElectricityImportLowTariff { get; init; }
    public Measurement? ElectricityImportHighTariff { get; init; }
    public Measurement? ElectricityExportLowTariff { get; init; }
    public Measurement? ElectricityExportHighTariff { get; init; }
    public double TariffIndicator { get; init; }

    public Measurement? TotalActivePowerExported { get; init; }
    public double TotalImportedActiveEnergy { get; init; }
    public double EventLogCount { get; init; }

    public Measurement? CurrentPowerUsage { get; init; }
    public Measurement? CurrentPowerDelivery { get; init; }
    public Measurement? InstantaneousActivePowerL1 { get; init; }
    public Measurement? InstantaneousActivePowerL2 { get; init; }
    public Measurement? Voltage { get; init; }
    public Measurement? Current { get; init; }

    public double MeterStatus { get; init; }
    public Measurement? MaxContractualPower { get; init; }
    public Measurement? MaxCurrent { get; init; }

    public double GasDeviceTypeId { get; init; }
    public double GasDeviceId { get; init; }
    public double GasReadStatus { get; init; }
    public double GasConsumption { get; init; }

    public double WaterConsumption { get; init; }
}
