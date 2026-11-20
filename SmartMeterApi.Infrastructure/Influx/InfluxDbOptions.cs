using System.ComponentModel.DataAnnotations;

namespace SmartMeterApi.Infrastructure.Influx;

/// <summary>Bound from the <c>InfluxDB</c> configuration section.</summary>
public sealed class InfluxDbOptions
{
    public const string SectionName = "InfluxDB";

    [Required, Url]
    public string Url { get; set; } = string.Empty;

    [Required]
    public string Token { get; set; } = string.Empty;

    [Required]
    public string Org { get; set; } = string.Empty;

    [Required]
    public string SmartMeterBucket { get; set; } = string.Empty;

    [Required]
    public string PvBucket { get; set; } = string.Empty;

    [Required]
    public string WaterSoftenerCapacityBucket { get; set; } = string.Empty;
}
