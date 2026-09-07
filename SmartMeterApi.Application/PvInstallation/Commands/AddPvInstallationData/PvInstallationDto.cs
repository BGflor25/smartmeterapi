using System.Text.Json.Serialization;

namespace SmartMeterApi.Application.PvInstallation.Commands.AddPvInstallationData
{
    public class MeasurementDouble
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class MeasurementString
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class PvInstallationDto
    {
        [JsonPropertyName("timestamp")]
        public double Timestamp { get; set; }

        [JsonPropertyName("model_name")]
        public MeasurementString ModelName { get; set; }

        [JsonPropertyName("serial_number")]
        public MeasurementString SerialNumber { get; set; }

        [JsonPropertyName("rated_power")]
        public MeasurementDouble RatedPower { get; set; }

        [JsonPropertyName("input_power")]
        public MeasurementDouble InputPower { get; set; }

        [JsonPropertyName("active_power")]
        public MeasurementDouble ActivePower { get; set; }

        [JsonPropertyName("reactive_power")]
        public MeasurementDouble ReactivePower { get; set; }

        [JsonPropertyName("power_factor")]
        public MeasurementDouble PowerFactor { get; set; }

        [JsonPropertyName("efficiency")]
        public MeasurementDouble Efficiency { get; set; }

        [JsonPropertyName("internal_temperature")]
        public MeasurementDouble InternalTemperature { get; set; }

        [JsonPropertyName("device_status")]
        public MeasurementString DeviceStatus { get; set; }

        [JsonPropertyName("day_active_power_peak")]
        public MeasurementDouble DayActivePowerPeak { get; set; }

        [JsonPropertyName("accumulated_yield_energy")]
        public MeasurementDouble AccumulatedYieldEnergy { get; set; }

        [JsonPropertyName("daily_yield_energy")]
        public MeasurementDouble DailyYieldEnergy { get; set; }

        [JsonPropertyName("pv_01_voltage")]
        public MeasurementDouble Pv01Voltage { get; set; }
        [JsonPropertyName("pv_01_current")]
        public MeasurementDouble Pv01Current { get; set; }
        [JsonPropertyName("pv_02_voltage")]
        public MeasurementDouble Pv02Voltage { get; set; }
        [JsonPropertyName("pv_02_current")]
        public MeasurementDouble Pv02Current { get; set; }
        [JsonPropertyName("pv_03_voltage")]
        public MeasurementDouble Pv03Voltage { get; set; }
        [JsonPropertyName("pv_03_current")]
        public MeasurementDouble Pv03Current { get; set; }
        [JsonPropertyName("pv_04_voltage")]
        public MeasurementDouble Pv04Voltage { get; set; }
        [JsonPropertyName("pv_04_current")]
        public MeasurementDouble Pv04Current { get; set; }

        [JsonPropertyName("grid_voltage")]
        public MeasurementDouble GridVoltage { get; set; }
        [JsonPropertyName("grid_current")]
        public MeasurementDouble GridCurrent { get; set; }
        [JsonPropertyName("grid_frequency")]
        public MeasurementDouble GridFrequency { get; set; }

        [JsonPropertyName("active_grid_A_current")]
        public MeasurementDouble ActiveGridACurrent { get; set; }
        [JsonPropertyName("active_grid_A_power")]
        public MeasurementDouble ActiveGridAPower { get; set; }
        [JsonPropertyName("grid_A_voltage")]
        public MeasurementDouble GridAVoltage { get; set; }

        [JsonPropertyName("storage_state_of_capacity")]
        public MeasurementDouble StorageStateOfCapacity { get; set; }
        [JsonPropertyName("storage_charge_discharge_power")]
        public MeasurementDouble StorageChargeDischargePower { get; set; }
        [JsonPropertyName("storage_running_status")]
        public MeasurementDouble StorageRunningStatus { get; set; }
        [JsonPropertyName("storage_total_charge")]
        public MeasurementDouble StorageTotalCharge { get; set; }
        [JsonPropertyName("storage_total_discharge")]
        public MeasurementDouble StorageTotalDischarge { get; set; }
    }
}
