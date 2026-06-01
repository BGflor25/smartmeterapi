namespace SmartMeterApi.Domain.Entities
{
    using System.Text.Json.Serialization;

    public class SmartMeterData
    {
        public Guid Id { get; set; }
        public double MeterModel { get; set; }

        public double EquipmentIdentifier { get; set; }

        public double Timestamp { get; set; }
        public ElectricityTariff ElectricityImportLowTariff { get; set; }

        public ElectricityTariff ElectricityImportHighTariff { get; set; }

        public ElectricityTariff ElectricityExportLowTariff { get; set; }

        public ElectricityTariff ElectricityExportHighTariff { get; set; }

        public double TariffIndicator { get; set; }

        public PowerMeasurement TotalActivePowerExported { get; set; }

        public double TotalImportedActiveEnergy { get; set; }

        public double EventLogCount { get; set; }

        public PowerMeasurement CurrentPowerUsage { get; set; }

        public PowerMeasurement CurrentPowerDelivery { get; set; }

        public PowerMeasurement InstantaneousActivePowerL1 { get; set; }

        public PowerMeasurement InstantaneousActivePowerL2 { get; set; }

        public VoltageCurrent Voltage { get; set; }

        public VoltageCurrent Current { get; set; }

        public double MeterStatus { get; set; }

        public PowerMeasurement MaxContractualPower { get; set; }

        public VoltageCurrent MaxCurrent { get; set; }

        public double GasDeviceTypeId { get; set; }

        public double GasDeviceId { get; set; }

        public double GasReadStatus { get; set; }

        public double GasConsumption { get; set; }

    }

    public class ElectricityTariff
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class PowerMeasurement
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class VoltageCurrent
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}
