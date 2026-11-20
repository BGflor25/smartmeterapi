using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartMeterApi.Application.Abstractions;
using SmartMeterApi.Domain.Entities;

namespace SmartMeterApi.Application.WaterSoftener.AddWaterSoftenerData;

internal sealed class AddWaterSoftenerDataCommandHandler(
    ITelemetryWriter writer,
    IOptions<WaterSoftenerOptions> options,
    ILogger<AddWaterSoftenerDataCommandHandler> logger) : IRequestHandler<AddWaterSoftenerDataCommand>
{
    public async Task Handle(AddWaterSoftenerDataCommand request, CancellationToken cancellationToken)
    {
        var capacity = WaterSoftenerCapacity.FromDistance(request.Data.Distance, options.Value.MaxDepth);

        await writer.WriteWaterSoftenerDataAsync(capacity, cancellationToken);
        logger.LogInformation("Water softener data written (ratio {Ratio:F3})", capacity.Ratio);
    }
}
