using MediatR;
using Microsoft.Extensions.Logging;
using SmartMeterApi.Application.Abstractions;

namespace SmartMeterApi.Application.PvInstallation.AddPvInstallationData;

internal sealed class AddPvInstallationDataCommandHandler(
    ITelemetryWriter writer,
    ILogger<AddPvInstallationDataCommandHandler> logger) : IRequestHandler<AddPvInstallationDataCommand>
{
    public async Task Handle(AddPvInstallationDataCommand request, CancellationToken cancellationToken)
    {
        await writer.WritePvInstallationDataAsync(request.Data.ToDomain(), cancellationToken);
        logger.LogInformation("PV installation data written for timestamp {Timestamp}", request.Data.Timestamp);
    }
}
