namespace SmartMeterApi.Application.SmartMeter.Commands.AddMeterData
{
    using SmartMeterApi.Domain.Entities;
    using System.Text.Json.Serialization;

    public class SmartMeterDataDto
    {
        [JsonPropertyName("meter_model")]
        public double MeterModel { get; set; }

        [JsonPropertyName("equipment_identifier")]
        public double EquipmentIdentifier { get; set; }

        [JsonPropertyName("timestamp")]
        public double Timestamp { get; set; }

        [JsonPropertyName("electricity_import_low_tariff")]
        public ElectricityTariff ElectricityImportLowTariff { get; set; }

        [JsonPropertyName("electricity_import_high_tariff")]
        public ElectricityTariff ElectricityImportHighTariff { get; set; }

        [JsonPropertyName("electricity_export_low_tariff")]
        public ElectricityTariff ElectricityExportLowTariff { get; set; }

        [JsonPropertyName("electricity_export_high_tariff")]
        public ElectricityTariff ElectricityExportHighTariff { get; set; }

        [JsonPropertyName("tariff_indicator")]
        public double TariffIndicator { get; set; }

        [JsonPropertyName("1-0:1.4.0")]
        public PowerMeasurement TotalActivePowerExported { get; set; }

        [JsonPropertyName("1-0:1.6.0")]
        public double TotalImportedActiveEnergy { get; set; }

        [JsonPropertyName("0-0:98.1.0")]
        public double EventLogCount { get; set; }

        [JsonPropertyName("current_power_usage")]
        public PowerMeasurement CurrentPowerUsage { get; set; }

        [JsonPropertyName("current_power_delivery")]
        public PowerMeasurement CurrentPowerDelivery { get; set; }

        [JsonPropertyName("instantaneous_active_power_l1_pos")]
        public PowerMeasurement InstantaneousActivePowerL1 { get; set; }

        [JsonPropertyName("instantaneous_active_power_l2_pos")]
        public PowerMeasurement InstantaneousActivePowerL2 { get; set; }

        [JsonPropertyName("voltage")]
        public VoltageCurrent Voltage { get; set; }

        [JsonPropertyName("current")]
        public VoltageCurrent Current { get; set; }

        [JsonPropertyName("0-0:96.3.10")]
        public double MeterStatus { get; set; }

        [JsonPropertyName("0-0:17.0.0")]
        public PowerMeasurement MaxContractualPower { get; set; }

        [JsonPropertyName("max_current")]
        public VoltageCurrent MaxCurrent { get; set; }

        [JsonPropertyName("0-1:24.1.0")]
        public double GasDeviceTypeId { get; set; }

        [JsonPropertyName("gas_device_id")]
        public double GasDeviceId { get; set; }

        [JsonPropertyName("0-1:24.4.0")]
        public double GasReadStatus { get; set; }

        [JsonPropertyName("gas_consumption")]
        public double GasConsumption { get; set; }

        [JsonPropertyName("checksum")]
        public string Checksum { get; set; }
        [JsonPropertyName("water_consumption")]
        public double WaterConsumption { get; set; }
    }
}
