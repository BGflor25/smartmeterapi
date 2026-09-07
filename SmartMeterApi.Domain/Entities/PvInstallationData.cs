namespace SmartMeterApi.Domain.Entities
{
    using System.Text.Json.Serialization;

    public class PvInstallationData
    {
        public double Timestamp { get; set; }

        public string ModelName { get; set; }

        public string SerialNumber { get; set; }

        public double? RatedPower { get; set; }

        public double? InputPower { get; set; }

        public double? ActivePower { get; set; }

        public double? ReactivePower { get; set; }

        public double? PowerFactor { get; set; }

        public double? Efficiency { get; set; }

        public double? InternalTemperature { get; set; }

        public string DeviceStatus { get; set; }

        public double? DayActivePowerPeak { get; set; }

        public double? AccumulatedYieldEnergy { get; set; }

        public double? DailyYieldEnergy { get; set; }

        public double? Pv01Voltage { get; set; }
        public double? Pv01Current { get; set; }
        public double? Pv02Voltage { get; set; }
        public double? Pv02Current { get; set; }
        public double? Pv03Voltage { get; set; }
        public double? Pv03Current { get; set; }
        public double? Pv04Voltage { get; set; }
        public double? Pv04Current { get; set; }

        public double? GridVoltage { get; set; }
        public double? GridCurrent { get; set; }
        public double? GridFrequency { get; set; }

        public double? ActiveGridACurrent { get; set; }
        public double? ActiveGridAPower { get; set; }
        public double? GridAVoltage { get; set; }

        public double? StorageStateOfCapacity { get; set; }
        public double? StorageChargeDischargePower { get; set; }
        public int? StorageRunningStatus { get; set; }
        public double? StorageTotalCharge { get; set; }
        public double? StorageTotalDischarge { get; set; }
    }
}
