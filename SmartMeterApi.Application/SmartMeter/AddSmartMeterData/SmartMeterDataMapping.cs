using SmartMeterApi.Application.Contracts;
using SmartMeterApi.Domain.Entities;

namespace SmartMeterApi.Application.SmartMeter.AddSmartMeterData;

public static class SmartMeterDataMapping
{
    public static SmartMeterData ToDomain(this SmartMeterDataRequest request) => new()
    {
        MeterModel = request.MeterModel,
        EquipmentIdentifier = request.EquipmentIdentifier,
        Timestamp = request.Timestamp,
        ElectricityImportLowTariff = request.ElectricityImportLowTariff.ToMeasurement(),
        ElectricityImportHighTariff = request.ElectricityImportHighTariff.ToMeasurement(),
        ElectricityExportLowTariff = request.ElectricityExportLowTariff.ToMeasurement(),
        ElectricityExportHighTariff = request.ElectricityExportHighTariff.ToMeasurement(),
        TariffIndicator = request.TariffIndicator,
        TotalActivePowerExported = request.TotalActivePowerExported.ToMeasurement(),
        TotalImportedActiveEnergy = request.TotalImportedActiveEnergy,
        EventLogCount = request.EventLogCount,
        CurrentPowerUsage = request.CurrentPowerUsage.ToMeasurement(),
        CurrentPowerDelivery = request.CurrentPowerDelivery.ToMeasurement(),
        InstantaneousActivePowerL1 = request.InstantaneousActivePowerL1.ToMeasurement(),
        InstantaneousActivePowerL2 = request.InstantaneousActivePowerL2.ToMeasurement(),
        Voltage = request.Voltage.ToMeasurement(),
        Current = request.Current.ToMeasurement(),
        MeterStatus = request.MeterStatus,
        MaxContractualPower = request.MaxContractualPower.ToMeasurement(),
        MaxCurrent = request.MaxCurrent.ToMeasurement(),
        GasDeviceTypeId = request.GasDeviceTypeId,
        GasDeviceId = request.GasDeviceId,
        GasReadStatus = request.GasReadStatus,
        GasConsumption = request.GasConsumption,
        WaterConsumption = request.WaterConsumption,
    };
}
