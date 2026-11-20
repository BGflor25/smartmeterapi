using FastEndpoints;
using MediatR;
using SmartMeterApi.Application.PvInstallation.AddPvInstallationData;

namespace SmartMeterApi.Api.Endpoints;

public sealed class PostPvInstallationDataEndpoint(ISender sender) : Endpoint<PvInstallationDataRequest>
{
    public override void Configure()
    {
        Post("/api/pvinstallation/upload");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Upload a PV inverter snapshot";
            s.Description = "Writes all present readings to the `pv_installation` measurement in the PV bucket.";
            s.Responses[204] = "Reading stored";
        });
    }

    public override async Task HandleAsync(PvInstallationDataRequest req, CancellationToken ct)
    {
        await sender.Send(new AddPvInstallationDataCommand(req), ct);
        await Send.NoContentAsync(ct);
    }
}
