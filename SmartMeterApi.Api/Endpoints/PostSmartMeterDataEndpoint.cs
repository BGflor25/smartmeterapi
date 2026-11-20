using FastEndpoints;
using MediatR;
using SmartMeterApi.Application.SmartMeter.AddSmartMeterData;

namespace SmartMeterApi.Api.Endpoints;

public sealed class PostSmartMeterDataEndpoint(ISender sender) : Endpoint<SmartMeterDataRequest>
{
    public override void Configure()
    {
        Post("/api/meterdata/upload");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Upload a smart meter (P1/DSMR) telegram";
            s.Description = "Writes the reading to the `meter_data` measurement in the smart meter bucket.";
            s.Responses[204] = "Reading stored";
        });
    }

    public override async Task HandleAsync(SmartMeterDataRequest req, CancellationToken ct)
    {
        await sender.Send(new AddSmartMeterDataCommand(req), ct);
        await Send.NoContentAsync(ct);
    }
}
