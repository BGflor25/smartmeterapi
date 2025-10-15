using FastEndpoints;
using MediatR;
using SmartMeterApi.Domain;
using SmartMeterApi.Application.Commands.AddMeterData;

namespace SmartMeterApi.Api.Endpoints
{
    public class PostMeterDataEndpoint : Endpoint<MeterDataDto>
    {
        private readonly ISender _mediator;
        public PostMeterDataEndpoint(ISender mediator)
        {
            _mediator = mediator;
        }
        public override void Configure()
        {
            Post("/api/meterdata/upload");
            AllowAnonymous();
        }

        public override async Task HandleAsync(MeterDataDto req, CancellationToken ct)
        {
            await _mediator.Send(new AddMeterDataCommand(req), ct);
            await Send.NoContentAsync();
        }
    }
}
