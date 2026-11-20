using System.Text.Json.Serialization;
using SmartMeterApi.Domain.ValueObjects;

namespace SmartMeterApi.Application.Contracts;

/// <summary>JSON shape <c>{ "value": 1.23, "unit": "kWh" }</c>.</summary>
public sealed class NumericMeasurementDto
{
    [JsonPropertyName("value")]
    public double? Value { get; init; }

    [JsonPropertyName("unit")]
    public string? Unit { get; init; }
}

/// <summary>JSON shape <c>{ "value": "SUN2000", "unit": null }</c>.</summary>
public sealed class TextMeasurementDto
{
    [JsonPropertyName("value")]
    public string? Value { get; init; }

    [JsonPropertyName("unit")]
    public string? Unit { get; init; }
}

internal static class MeasurementDtoExtensions
{
    public static Measurement? ToMeasurement(this NumericMeasurementDto? dto) =>
        dto?.Value is { } value ? new Measurement(value, dto.Unit) : null;
}
