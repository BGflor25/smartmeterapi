using SmartMeterApi.Domain.Entities;

namespace SmartMeterApi.Application.Abstractions;

/// <summary>Port for persisting telemetry to the time-series store.</summary>
public interface ITelemetryWriter
{
    Task WriteSmartMeterDataAsync(SmartMeterData data, CancellationToken cancellationToken = default);

    Task WritePvInstallationDataAsync(PvInstallationData data, CancellationToken cancellationToken = default);

    Task WriteWaterSoftenerDataAsync(WaterSoftenerCapacity data, CancellationToken cancellationToken = default);
}
