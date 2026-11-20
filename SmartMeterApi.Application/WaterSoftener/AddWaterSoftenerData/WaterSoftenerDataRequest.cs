using System.Text.Json.Serialization;

namespace SmartMeterApi.Application.WaterSoftener.AddWaterSoftenerData;

/// <summary>Payload sent by the distance sensor mounted above the salt tank.</summary>
public sealed class WaterSoftenerDataRequest
{
    [JsonPropertyName("distance")]
    public double Distance { get; init; }
}
