using MediatR;

namespace SmartMeterApi.Application.PvInstallation.AddPvInstallationData;

public sealed record AddPvInstallationDataCommand(PvInstallationDataRequest Data) : IRequest;
