using MediatR;
using Microsoft.IdentityModel.Abstractions;
using Serilog;
using SmartMeterApi.Application.Interfaces;
using SmartMeterApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterApi.Application.Commands.AddMeterData
{
    public class AddMeterDataCommand : IRequest<Guid>
    {
        public AddMeterDataCommand(MeterDataDto dto)
        { 
            Dto = dto;
        }
        public MeterDataDto Dto { get; }

        private class AddMeterDataCommandHandler : IRequestHandler<AddMeterDataCommand, Guid>
        {
            private readonly IInfluxService _influxService;
            private readonly ILogger _logger;

            public AddMeterDataCommandHandler(IInfluxService influxService, ILogger logger)
            {
                _influxService = influxService;
                _logger = logger;
            }

            public async Task<Guid> Handle(AddMeterDataCommand request, CancellationToken cancellationToken)
            {
                var meterData = new Domain.Entities.MeterData
                {
                    MeterModel = request.Dto.MeterModel,
                    EquipmentIdentifier = request.Dto.EquipmentIdentifier,
                    Timestamp = request.Dto.Timestamp,
                    ElectricityImportLowTariff = request.Dto.ElectricityImportLowTariff,
                    ElectricityImportHighTariff = request.Dto.ElectricityImportHighTariff,
                    ElectricityExportLowTariff = request.Dto.ElectricityExportLowTariff,
                    ElectricityExportHighTariff = request.Dto.ElectricityExportHighTariff,
                    TariffIndicator = request.Dto.TariffIndicator,
                    TotalActivePowerExported = request.Dto.TotalActivePowerExported,
                    TotalImportedActiveEnergy = request.Dto.TotalImportedActiveEnergy,
                    EventLogCount = request.Dto.EventLogCount,
                    CurrentPowerUsage = request.Dto.CurrentPowerUsage,
                    CurrentPowerDelivery = request.Dto.CurrentPowerDelivery,
                    InstantaneousActivePowerL1 = request.Dto.InstantaneousActivePowerL1,
                    InstantaneousActivePowerL2 = request.Dto.InstantaneousActivePowerL2,
                    Voltage = request.Dto.Voltage,
                    Current = request.Dto.Current,
                    MeterStatus = request.Dto.MeterStatus,
                    MaxContractualPower = request.Dto.MaxContractualPower,
                    MaxCurrent = request.Dto.MaxCurrent,
                    GasDeviceId = request.Dto.GasDeviceId,
                    GasDeviceTypeId = request.Dto.GasDeviceTypeId,
                    GasConsumption = request.Dto.GasConsumption,
                    GasReadStatus = request.Dto.GasReadStatus
                };
                await _influxService.WriteAsync(meterData,new CancellationToken());
                _logger.Information($"Meter data ready for saving");
                return Guid.NewGuid();
            }
        }
    }
}
