using FastEndpoints;
using MediatR;
using SmartMeterApi.Domain;
using SmartMeterApi.Application.SmartMeter.Commands.AddMeterData;

namespace SmartMeterApi.Api.Endpoints
{
    public class PostSmartMeterDataEndpoint : Endpoint<SmartMeterDataDto>
    {
        private readonly ISender _mediator;
        public PostSmartMeterDataEndpoint(ISender mediator)
        {
            _mediator = mediator;
        }
        public override void Configure()
        {
            Post("/api/meterdata/upload");
            AllowAnonymous();
        }

        public override async Task HandleAsync(SmartMeterDataDto req, CancellationToken ct)
        {
            Logger.LogInformation("Received meter data upload request.");
            await _mediator.Send(new AddSmartMeterDataCommand(req), ct);
            await Send.NoContentAsync();
        }
    }
}
