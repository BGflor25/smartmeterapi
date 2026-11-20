using MediatR;

namespace SmartMeterApi.Application.SmartMeter.AddSmartMeterData;

public sealed record AddSmartMeterDataCommand(SmartMeterDataRequest Data) : IRequest;
