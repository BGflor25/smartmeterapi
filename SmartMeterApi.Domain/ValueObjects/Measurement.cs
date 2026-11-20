namespace SmartMeterApi.Domain.ValueObjects;

/// <summary>A numeric reading together with its unit (e.g. 1.234 kWh).</summary>
public sealed record Measurement(double Value, string? Unit = null);
