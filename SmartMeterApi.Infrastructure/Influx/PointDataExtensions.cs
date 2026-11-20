using InfluxDB.Client.Writes;
using SmartMeterApi.Domain.ValueObjects;

namespace SmartMeterApi.Infrastructure.Influx;

/// <summary>Helpers that only add a field when a value is present (Influx has no null fields).</summary>
internal static class PointDataExtensions
{
    public static PointData OptionalField(this PointData point, string name, double? value) =>
        value is { } v ? point.Field(name, v) : point;

    public static PointData OptionalField(this PointData point, string name, int? value) =>
        value is { } v ? point.Field(name, v) : point;

    public static PointData OptionalField(this PointData point, string name, string? value) =>
        string.IsNullOrEmpty(value) ? point : point.Field(name, value);

    public static PointData OptionalField(this PointData point, string name, Measurement? measurement) =>
        point.OptionalField(name, measurement?.Value);
}
