using MediatR;
using Serilog;
using SmartMeterApi.Application.PvInstallation.Commands.AddPvInstallationData;
using SmartMeterApi.Application.Interfaces;
using SmartMeterApi.Domain;
using SmartMeterApi.Domain.Entities;

namespace SmartMeterApi.Application.PvInstallation.Commands.AddPvInstallationData
{
    public class AddPvInstallationDataCommand : IRequest<System.Guid>
    {
        public AddPvInstallationDataCommand(PvInstallationDto dto)
        {
            Dto = dto;
        }

        public PvInstallationDto Dto { get; }

        private class AddPvInstallationDataCommandHandler : IRequestHandler<AddPvInstallationDataCommand, System.Guid>
        {
            private readonly IInfluxService _influxService;
            private readonly ILogger _logger;

            public AddPvInstallationDataCommandHandler(IInfluxService influxService, ILogger logger)
            {
                _influxService = influxService;
                _logger = logger;
            }

            public async Task<System.Guid> Handle(AddPvInstallationDataCommand request, CancellationToken cancellationToken)
            {
                var dto = request.Dto;

                var pv = new PvInstallationData
                {
                    Timestamp = dto.Timestamp,
                    ModelName = dto.ModelName?.Value,
                    SerialNumber = dto.SerialNumber?.Value,
                    RatedPower = dto.RatedPower?.Value,
                    InputPower = dto.InputPower?.Value,
                    ActivePower = dto.ActivePower?.Value,
                    ReactivePower = dto.ReactivePower?.Value,
                    PowerFactor = dto.PowerFactor?.Value,
                    Efficiency = dto.Efficiency?.Value,
                    InternalTemperature = dto.InternalTemperature?.Value,
                    DeviceStatus = dto.DeviceStatus?.Value,
                    DayActivePowerPeak = dto.DayActivePowerPeak?.Value,
                    AccumulatedYieldEnergy = dto.AccumulatedYieldEnergy?.Value,
                    DailyYieldEnergy = dto.DailyYieldEnergy?.Value,
                    Pv01Voltage = dto.Pv01Voltage?.Value,
                    Pv01Current = dto.Pv01Current?.Value,
                    Pv02Voltage = dto.Pv02Voltage?.Value,
                    Pv02Current = dto.Pv02Current?.Value,
                    Pv03Voltage = dto.Pv03Voltage?.Value,
                    Pv03Current = dto.Pv03Current?.Value,
                    Pv04Voltage = dto.Pv04Voltage?.Value,
                    Pv04Current = dto.Pv04Current?.Value,
                    GridVoltage = dto.GridVoltage?.Value,
                    GridCurrent = dto.GridCurrent?.Value,
                    GridFrequency = dto.GridFrequency?.Value,
                    ActiveGridACurrent = dto.ActiveGridACurrent?.Value,
                    ActiveGridAPower = dto.ActiveGridAPower?.Value,
                    GridAVoltage = dto.GridAVoltage?.Value,
                    StorageStateOfCapacity = dto.StorageStateOfCapacity?.Value,
                    StorageChargeDischargePower = dto.StorageChargeDischargePower?.Value,
                    StorageRunningStatus = dto.StorageRunningStatus?.Value.HasValue == true ? (int?)Convert.ToInt32(dto.StorageRunningStatus.Value) : null,
                    StorageTotalCharge = dto.StorageTotalCharge?.Value,
                    StorageTotalDischarge = dto.StorageTotalDischarge?.Value
                };

                await _influxService.WritePvInstallationDataAsync(pv);
                _logger.Information("PV installation data added");

                return System.Guid.NewGuid();
            }
        }
    }
}
