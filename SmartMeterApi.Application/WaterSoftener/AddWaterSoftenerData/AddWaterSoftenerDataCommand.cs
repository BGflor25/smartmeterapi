using MediatR;

namespace SmartMeterApi.Application.WaterSoftener.AddWaterSoftenerData;

public sealed record AddWaterSoftenerDataCommand(WaterSoftenerDataRequest Data) : IRequest;
