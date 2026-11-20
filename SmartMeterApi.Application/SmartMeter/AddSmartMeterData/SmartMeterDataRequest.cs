using System.Text.Json.Serialization;
using SmartMeterApi.Application.Contracts;

namespace SmartMeterApi.Application.SmartMeter.AddSmartMeterData;

/// <summary>Payload sent by the P1 / DSMR reader. Property names follow the reader's JSON output.</summary>
public sealed class SmartMeterDataRequest
{
    [JsonPropertyName("meter_model")]
    public double MeterModel { get; init; }

    [JsonPropertyName("equipment_identifier")]
    public double EquipmentIdentifier { get; init; }

    [JsonPropertyName("timestamp")]
    public double Timestamp { get; init; }

    [JsonPropertyName("electricity_import_low_tariff")]
    public NumericMeasurementDto? ElectricityImportLowTariff { get; init; }

    [JsonPropertyName("electricity_import_high_tariff")]
    public NumericMeasurementDto? ElectricityImportHighTariff { get; init; }

    [JsonPropertyName("electricity_export_low_tariff")]
    public NumericMeasurementDto? ElectricityExportLowTariff { get; init; }

    [JsonPropertyName("electricity_export_high_tariff")]
    public NumericMeasurementDto? ElectricityExportHighTariff { get; init; }

    [JsonPropertyName("tariff_indicator")]
    public double TariffIndicator { get; init; }

    [JsonPropertyName("1-0:1.4.0")]
    public NumericMeasurementDto? TotalActivePowerExported { get; init; }

    [JsonPropertyName("1-0:1.6.0")]
    public double TotalImportedActiveEnergy { get; init; }

    [JsonPropertyName("0-0:98.1.0")]
    public double EventLogCount { get; init; }

    [JsonPropertyName("current_power_usage")]
    public NumericMeasurementDto? CurrentPowerUsage { get; init; }

    [JsonPropertyName("current_power_delivery")]
    public NumericMeasurementDto? CurrentPowerDelivery { get; init; }

    [JsonPropertyName("instantaneous_active_power_l1_pos")]
    public NumericMeasurementDto? InstantaneousActivePowerL1 { get; init; }

    [JsonPropertyName("instantaneous_active_power_l2_pos")]
    public NumericMeasurementDto? InstantaneousActivePowerL2 { get; init; }

    [JsonPropertyName("voltage")]
    public NumericMeasurementDto? Voltage { get; init; }

    [JsonPropertyName("current")]
    public NumericMeasurementDto? Current { get; init; }

    [JsonPropertyName("0-0:96.3.10")]
    public double MeterStatus { get; init; }

    [JsonPropertyName("0-0:17.0.0")]
    public NumericMeasurementDto? MaxContractualPower { get; init; }

    [JsonPropertyName("max_current")]
    public NumericMeasurementDto? MaxCurrent { get; init; }

    [JsonPropertyName("0-1:24.1.0")]
    public double GasDeviceTypeId { get; init; }

    [JsonPropertyName("gas_device_id")]
    public double GasDeviceId { get; init; }

    [JsonPropertyName("0-1:24.4.0")]
    public double GasReadStatus { get; init; }

    [JsonPropertyName("gas_consumption")]
    public double GasConsumption { get; init; }

    [JsonPropertyName("water_consumption")]
    public double WaterConsumption { get; init; }

    [JsonPropertyName("checksum")]
    public string? Checksum { get; init; }
}
