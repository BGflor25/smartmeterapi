using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using SmartMeterApi.Domain;
using SmartMeterApi.Domain.Entities;

namespace SmartMeterApi.Application.WaterSoftener.Commands.AddMeterData
{
    public class AddWaterSoftenerDataCommand : IRequest<Guid>
    {
        public AddWaterSoftenerDataCommand(WaterSoftenerDataDto dto)
        {
            Dto = dto;
        }

        public WaterSoftenerDataDto Dto { get; }

        private class AddWaterSoftenerDataCommandHandler : IRequestHandler<AddWaterSoftenerDataCommand, Guid>
        {
            private readonly IInfluxService _influxService;
            private readonly ILogger _logger;
            private readonly double _maxDistance;

            public AddWaterSoftenerDataCommandHandler(
                IInfluxService influxService,
                ILogger logger,
                IConfiguration configuration)
            {
                _influxService = influxService;
                _logger = logger;

                var maxDistanceString = configuration["InfluxDB:WaterSoftenerMaxDepth"];

                if (!double.TryParse(maxDistanceString, out double parsed))
                    throw new InvalidOperationException("WaterSoftenerMaxDepth configuration is missing or invalid.");

                _maxDistance = parsed;
            }

            public async Task<Guid> Handle(AddWaterSoftenerDataCommand request, CancellationToken cancellationToken)
            {
                var waterSoftenerCapacity = new WaterSoftenerCapacity
                {
                    Percentage = request.Dto.Distance / _maxDistance
                };

                await _influxService.WriteWaterSoftenerDataAsync(waterSoftenerCapacity);
                _logger.Information("Water softener data added");

                return Guid.NewGuid();
            }
        }
    }
}