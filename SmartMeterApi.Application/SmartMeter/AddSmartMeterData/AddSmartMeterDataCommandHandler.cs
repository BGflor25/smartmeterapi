using MediatR;
using Microsoft.Extensions.Logging;
using SmartMeterApi.Application.Abstractions;

namespace SmartMeterApi.Application.SmartMeter.AddSmartMeterData;

internal sealed class AddSmartMeterDataCommandHandler(
    ITelemetryWriter writer,
    ILogger<AddSmartMeterDataCommandHandler> logger) : IRequestHandler<AddSmartMeterDataCommand>
{
    public async Task Handle(AddSmartMeterDataCommand request, CancellationToken cancellationToken)
    {
        await writer.WriteSmartMeterDataAsync(request.Data.ToDomain(), cancellationToken);
        logger.LogInformation("Smart meter data written for timestamp {Timestamp}", request.Data.Timestamp);
    }
}
