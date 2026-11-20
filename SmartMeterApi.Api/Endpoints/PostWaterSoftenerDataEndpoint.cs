using FastEndpoints;
using MediatR;
using SmartMeterApi.Application.WaterSoftener.AddWaterSoftenerData;

namespace SmartMeterApi.Api.Endpoints;

public sealed class PostWaterSoftenerDataEndpoint(ISender sender) : Endpoint<WaterSoftenerDataRequest>
{
    public override void Configure()
    {
        Post("/api/watersoftener/upload");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Upload a water softener salt-level distance reading";
            s.Description = "Converts the distance to a ratio of the configured max depth and writes it to `water_softener_data`.";
            s.Responses[204] = "Reading stored";
        });
    }

    public override async Task HandleAsync(WaterSoftenerDataRequest req, CancellationToken ct)
    {
        await sender.Send(new AddWaterSoftenerDataCommand(req), ct);
        await Send.NoContentAsync(ct);
    }
}
