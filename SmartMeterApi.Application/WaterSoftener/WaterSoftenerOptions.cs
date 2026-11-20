using System.ComponentModel.DataAnnotations;

namespace SmartMeterApi.Application.WaterSoftener;

public sealed class WaterSoftenerOptions
{
    /// <summary>Distance (same unit as the sensor) from the sensor to the bottom of an empty salt tank.</summary>
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "WaterSoftenerMaxDepth must be greater than zero.")]
    public double MaxDepth { get; set; }
}
