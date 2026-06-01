using FastEndpoints;
using MediatR;
using SmartMeterApi.Domain;
using SmartMeterApi.Application.SmartMeter.Commands.AddMeterData;
using SmartMeterApi.Application.WaterSoftener.Commands.AddMeterData;

namespace SmartMeterApi.Api.Endpoints
{
    public class PostWaterSoftenerDataEndpoint : Endpoint<WaterSoftenerDataDto>
    {
        private readonly ISender _mediator;
        public PostWaterSoftenerDataEndpoint(ISender mediator)
        {
            _mediator = mediator;
        }
        public override void Configure()
        {
            Post("/api/watersoftener/upload");
            AllowAnonymous();
        }

        public override async Task HandleAsync(WaterSoftenerDataDto req, CancellationToken ct)
        {
            Logger.LogInformation("Received meter data upload request.");
            await _mediator.Send(new AddWaterSoftenerDataCommand(req), ct);
            await Send.NoContentAsync();
        }
    }
}
