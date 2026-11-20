using System.Text.Json.Serialization;
using SmartMeterApi.Application.Contracts;

namespace SmartMeterApi.Application.PvInstallation.AddPvInstallationData;

/// <summary>Payload sent by the PV inverter reader. Every reading is optional.</summary>
public sealed class PvInstallationDataRequest
{
    [JsonPropertyName("timestamp")]
    public double Timestamp { get; init; }

    [JsonPropertyName("model_name")]
    public TextMeasurementDto? ModelName { get; init; }

    [JsonPropertyName("serial_number")]
    public TextMeasurementDto? SerialNumber { get; init; }

    [JsonPropertyName("rated_power")]
    public NumericMeasurementDto? RatedPower { get; init; }

    [JsonPropertyName("input_power")]
    public NumericMeasurementDto? InputPower { get; init; }

    [JsonPropertyName("active_power")]
    public NumericMeasurementDto? ActivePower { get; init; }

    [JsonPropertyName("reactive_power")]
    public NumericMeasurementDto? ReactivePower { get; init; }

    [JsonPropertyName("power_factor")]
    public NumericMeasurementDto? PowerFactor { get; init; }

    [JsonPropertyName("efficiency")]
    public NumericMeasurementDto? Efficiency { get; init; }

    [JsonPropertyName("internal_temperature")]
    public NumericMeasurementDto? InternalTemperature { get; init; }

    [JsonPropertyName("device_status")]
    public TextMeasurementDto? DeviceStatus { get; init; }

    [JsonPropertyName("day_active_power_peak")]
    public NumericMeasurementDto? DayActivePowerPeak { get; init; }

    [JsonPropertyName("accumulated_yield_energy")]
    public NumericMeasurementDto? AccumulatedYieldEnergy { get; init; }

    [JsonPropertyName("daily_yield_energy")]
    public NumericMeasurementDto? DailyYieldEnergy { get; init; }

    [JsonPropertyName("pv_01_voltage")]
    public NumericMeasurementDto? Pv01Voltage { get; init; }

    [JsonPropertyName("pv_01_current")]
    public NumericMeasurementDto? Pv01Current { get; init; }

    [JsonPropertyName("pv_02_voltage")]
    public NumericMeasurementDto? Pv02Voltage { get; init; }

    [JsonPropertyName("pv_02_current")]
    public NumericMeasurementDto? Pv02Current { get; init; }

    [JsonPropertyName("pv_03_voltage")]
    public NumericMeasurementDto? Pv03Voltage { get; init; }

    [JsonPropertyName("pv_03_current")]
    public NumericMeasurementDto? Pv03Current { get; init; }

    [JsonPropertyName("pv_04_voltage")]
    public NumericMeasurementDto? Pv04Voltage { get; init; }

    [JsonPropertyName("pv_04_current")]
    public NumericMeasurementDto? Pv04Current { get; init; }

    [JsonPropertyName("grid_voltage")]
    public NumericMeasurementDto? GridVoltage { get; init; }

    [JsonPropertyName("grid_current")]
    public NumericMeasurementDto? GridCurrent { get; init; }

    [JsonPropertyName("grid_frequency")]
    public NumericMeasurementDto? GridFrequency { get; init; }

    [JsonPropertyName("active_grid_A_current")]
    public NumericMeasurementDto? ActiveGridACurrent { get; init; }

    [JsonPropertyName("active_grid_A_power")]
    public NumericMeasurementDto? ActiveGridAPower { get; init; }

    [JsonPropertyName("grid_A_voltage")]
    public NumericMeasurementDto? GridAVoltage { get; init; }

    [JsonPropertyName("storage_state_of_capacity")]
    public NumericMeasurementDto? StorageStateOfCapacity { get; init; }

    [JsonPropertyName("storage_charge_discharge_power")]
    public NumericMeasurementDto? StorageChargeDischargePower { get; init; }

    [JsonPropertyName("storage_running_status")]
    public NumericMeasurementDto? StorageRunningStatus { get; init; }

    [JsonPropertyName("storage_total_charge")]
    public NumericMeasurementDto? StorageTotalCharge { get; init; }

    [JsonPropertyName("storage_total_discharge")]
    public NumericMeasurementDto? StorageTotalDischarge { get; init; }
}
