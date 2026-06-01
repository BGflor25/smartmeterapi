using MediatR;
using Serilog;
using SmartMeterApi.Application.SmartMeter.Commands.AddMeterData;
using SmartMeterApi.Domain;
using SmartMeterApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterApi.Application.WaterSoftener.Commands.AddMeterData
{
    public class AddWaterSoftenerDataCommand : IRequest<Guid>
    {
        public AddWaterSoftenerDataCommand(WaterSoftenerDataDto dto)
        {
            Dto = dto;
            _maxDistance = configuration["InfluxDB:WaterSoftenerMaxDepth"]!;

        }
        public WaterSoftenerDataDto Dto { get; }
        private readonly double _maxDistance;


        private class AddWaterSoftenerDataCommandHandler : IRequestHandler<AddWaterSoftenerDataCommand, Guid>
        {
            private readonly IInfluxService _influxService;
            private readonly ILogger _logger;

            public AddWaterSoftenerDataCommandHandler(IInfluxService influxService, ILogger logger)
            {
                _influxService = influxService;
                _logger = logger;
            }

            public async Task<Guid> Handle(AddWaterSoftenerDataCommand request, CancellationToken cancellationToken)
            {
                var waterSoftenerCapacity = new Domain.Entities.WaterSoftenerCapacity
                {
                    Percentage = request.Dto.Distance / _maxDistance
                };
                await _influxService.WriteWaterSoftenerDataAsync(waterSoftenerCapacity);
                _logger.Information($"Meter data added");
                return Guid.NewGuid();
            }
        }
    }

}

