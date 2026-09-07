using FastEndpoints;
using MediatR;
using SmartMeterApi.Application.PvInstallation.Commands.AddPvInstallationData;

namespace SmartMeterApi.Api.Endpoints
{
    public class PostPvInstallationEndpoint : Endpoint<PvInstallationDto>
    {
        private readonly ISender _mediator;

        public PostPvInstallationEndpoint(ISender mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("/api/pvinstallation/upload");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PvInstallationDto req, CancellationToken ct)
        {
            Logger.LogInformation("Received PV installation upload request.");
            await _mediator.Send(new AddPvInstallationDataCommand(req), ct);
            await Send.NoContentAsync();
        }
    }
}
