namespace SmartMeterApi.Domain.Entities;

/// <summary>
/// Fill level of the water-softener salt tank, expressed as the ratio
/// measured distance / configured maximum depth (0 = sensor touching the salt).
/// </summary>
public sealed record WaterSoftenerCapacity(double Ratio)
{
    public static WaterSoftenerCapacity FromDistance(double distance, double maxDepth)
    {
        if (maxDepth <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxDepth), maxDepth, "Max depth must be greater than zero.");
        }

        return new WaterSoftenerCapacity(distance / maxDepth);
    }
}
