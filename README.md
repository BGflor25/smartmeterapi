# SmartMeterApi

A small ASP.NET Core API that collects home telemetry and stores it in **InfluxDB 2.x**:

- **Smart meter**: P1 / DSMR telegrams (electricity, gas, water)
- **PV installation**: inverter and battery-storage readings
- **Water softener**: salt-tank level from a distance sensor

Devices (e.g. an ESP32 or a Raspberry Pi reader script) `POST` JSON to the API. The API maps each payload to an InfluxDB point. Dashboards such as Grafana can then query the data.

## Tech stack

| Concern        | Choice                                                    |
|----------------|-----------------------------------------------------------|
| Runtime        | .NET 10 (ASP.NET Core)                                    |
| HTTP endpoints | [FastEndpoints](https://fast-endpoints.com) + Swagger UI  |
| Use cases      | MediatR 12 (commands + handlers)                          |
| Storage        | InfluxDB 2.x via `InfluxDB.Client`                        |
| Logging        | Serilog (configured from `appsettings.json`)              |
| Tests          | xUnit + NSubstitute                                       |
| Packaging      | Docker image published to GHCR by GitHub Actions          |

## Architecture

The solution follows Clean Architecture. Dependencies point inwards only:

```
SmartMeterApi.Api  ──►  SmartMeterApi.Application  ──►  SmartMeterApi.Domain
        │                         ▲
        └──►  SmartMeterApi.Infrastructure ┘
```

| Project | Responsibility |
|---------|----------------|
| `SmartMeterApi.Domain` | Entities (`SmartMeterData`, `PvInstallationData`, `WaterSoftenerCapacity`) and the `Measurement` value object. No dependencies. |
| `SmartMeterApi.Application` | One folder per feature: request contract (JSON shape), MediatR command, handler and mapping to the domain. Defines the `ITelemetryWriter` port and `WaterSoftenerOptions`. |
| `SmartMeterApi.Infrastructure` | `InfluxTelemetryWriter` (implements `ITelemetryWriter`), `InfluxDbOptions`, the InfluxDB ping health check and `AddInfrastructure()` DI registration. |
| `SmartMeterApi.Api` | Composition root: `Program.cs`, FastEndpoints endpoints, Serilog, Swagger, `/health`. |
| `tests/SmartMeterApi.UnitTests` | Unit tests for mapping, Influx point creation and handlers. |

Package versions are managed centrally in `Directory.Packages.props`. Shared build settings (target framework, nullable, analyzers) are in `Directory.Build.props`.

## Endpoints

All upload endpoints accept `application/json` and return **204 No Content** on success. They are currently anonymous, so run the API only on a trusted network.

| Method | Route | Purpose |
|--------|-------|---------|
| `POST` | `/api/meterdata/upload` | Smart meter telegram |
| `POST` | `/api/pvinstallation/upload` | PV inverter snapshot |
| `POST` | `/api/watersoftener/upload` | Water softener distance reading |
| `GET`  | `/health` | Health check (pings InfluxDB) |
| `GET`  | `/swagger` | Swagger UI / OpenAPI document |

### Smart meter: `POST /api/meterdata/upload`

Measurements use the `{ "value": <number>, "unit": "<unit>" }` shape. Some keys are OBIS codes.

```json
{
  "meter_model": 5,
  "equipment_identifier": 123,
  "timestamp": 251120143000,
  "electricity_import_low_tariff":  { "value": 1000.5,  "unit": "kWh" },
  "electricity_import_high_tariff": { "value": 2000.25, "unit": "kWh" },
  "electricity_export_low_tariff":  { "value": 300,     "unit": "kWh" },
  "electricity_export_high_tariff": { "value": 400,     "unit": "kWh" },
  "tariff_indicator": 2,
  "1-0:1.4.0": { "value": 1.75, "unit": "kW" },
  "1-0:1.6.0": 4.2,
  "0-0:98.1.0": 3,
  "current_power_usage":    { "value": 0.8, "unit": "kW" },
  "current_power_delivery": { "value": 0,   "unit": "kW" },
  "instantaneous_active_power_l1_pos": { "value": 0.5, "unit": "kW" },
  "instantaneous_active_power_l2_pos": { "value": 0.3, "unit": "kW" },
  "voltage": { "value": 231.1, "unit": "V" },
  "current": { "value": 3,     "unit": "A" },
  "0-0:96.3.10": 1,
  "0-0:17.0.0": { "value": 9.9, "unit": "kW" },
  "max_current": { "value": 40, "unit": "A" },
  "0-1:24.1.0": 3,
  "gas_device_id": 42,
  "0-1:24.4.0": 1,
  "gas_consumption": 1234.567,
  "water_consumption": 12.5,
  "checksum": "ABCD"
}
```

### PV installation: `POST /api/pvinstallation/upload`

Every property is optional. Missing properties, or properties whose `value` is `null`, are not written.

```json
{
  "timestamp": 1763648000,
  "model_name":    { "value": "SUN2000-5KTL", "unit": null },
  "serial_number": { "value": "HV1234567890", "unit": null },
  "device_status": { "value": "On-grid",      "unit": null },
  "active_power":  { "value": 3.21, "unit": "kW" },
  "daily_yield_energy": { "value": 12.4, "unit": "kWh" },
  "pv_01_voltage": { "value": 380.2, "unit": "V" },
  "pv_01_current": { "value": 4.1,   "unit": "A" },
  "grid_frequency": { "value": 50.01, "unit": "Hz" },
  "storage_state_of_capacity": { "value": 76, "unit": "%" },
  "storage_running_status":    { "value": 2,  "unit": null }
}
```

All supported properties: `rated_power`, `input_power`, `active_power`, `reactive_power`, `power_factor`, `efficiency`, `internal_temperature`, `device_status`, `day_active_power_peak`, `accumulated_yield_energy`, `daily_yield_energy`, `pv_01..04_voltage`, `pv_01..04_current`, `grid_voltage`, `grid_current`, `grid_frequency`, `active_grid_A_current`, `active_grid_A_power`, `grid_A_voltage`, `storage_state_of_capacity`, `storage_charge_discharge_power`, `storage_running_status`, `storage_total_charge`, `storage_total_discharge`, `model_name`, `serial_number`.

### Water softener: `POST /api/watersoftener/upload`

```json
{ "distance": 20 }
```

The API stores `distance / WaterSoftenerMaxDepth` as `capacity`. With a max depth of 80, a distance of 20 gives `0.25`.

## InfluxDB data model

The InfluxDB server sets the timestamp when it receives the write. The device's `timestamp` field is not used yet.

| Bucket (config key) | Measurement | Fields |
|---|---|---|
| `SmartMeterBucket` | `meter_data` | `import_low_tariff`, `import_high_tariff`, `export_low_tariff`, `export_high_tariff`, `current_power_usage`, `current_power_delivery`, `power_l1`, `power_l2`, `voltage`, `current`, `total_power_export`, `total_power_usage`, `gas_consumption`, `event_log_count`, `MeterStatus`, `TariffIndicator` |
| `PvBucket` | `pv_installation` | Same names as the JSON properties above (only the fields that are present) |
| `WaterSoftenerCapacityBucket` | `water_softener_data` | `capacity` |

> **Migration note:** before the refactor, `meter_data.total_power_delivery` held a broken string value (a .NET type name). It is no longer written. The numeric value of OBIS `1-0:1.4.0` is now stored as **`total_power_export`**. `Pv_power_delivery` was always `0` and has been removed. Update any dashboards that used these fields.

## Configuration

Settings are read from `appsettings.json`, environment variables and user secrets (in Development). Environment variables use `__` as the section separator.

| Key | Environment variable | Description |
|-----|----------------------|-------------|
| `InfluxDB:Url` | `InfluxDB__Url` | InfluxDB base URL, e.g. `http://influxdb:8086` |
| `InfluxDB:Token` | `InfluxDB__Token` | API token with write access to the buckets |
| `InfluxDB:Org` | `InfluxDB__Org` | Organisation name |
| `InfluxDB:SmartMeterBucket` | `InfluxDB__SmartMeterBucket` | Bucket for smart meter data |
| `InfluxDB:PvBucket` | `InfluxDB__PvBucket` | Bucket for PV data |
| `InfluxDB:WaterSoftenerCapacityBucket` | `InfluxDB__WaterSoftenerCapacityBucket` | Bucket for water softener data |
| `InfluxDB:WaterSoftenerMaxDepth` | `InfluxDB__WaterSoftenerMaxDepth` | Sensor distance for an empty tank (must be > 0) |
| `Serilog:*` | `Serilog__MinimumLevel__Default`, … | Logging, see [Serilog.Settings.Configuration](https://github.com/serilog/serilog-settings-configuration) |

All settings are validated at startup. If any are missing or invalid, the app stops with a clear error message and does not start with a broken configuration.

## Running locally

Prerequisites: [.NET 10 SDK](https://dotnet.microsoft.com/download) and a reachable InfluxDB 2.x instance.

```bash
# Optional: throwaway InfluxDB
docker run -d --name influxdb -p 8086:8086 \
  -e DOCKER_INFLUXDB_INIT_MODE=setup \
  -e DOCKER_INFLUXDB_INIT_USERNAME=admin -e DOCKER_INFLUXDB_INIT_PASSWORD=change-me-please \
  -e DOCKER_INFLUXDB_INIT_ORG=home -e DOCKER_INFLUXDB_INIT_BUCKET=smartmeter \
  -e DOCKER_INFLUXDB_INIT_ADMIN_TOKEN=dev-token \
  influxdb:2
# The pv and watersoftener buckets need to be created as well (UI or `influx bucket create`).

# Store settings as user secrets (keeps tokens out of git)
cd SmartMeterApi.Api
dotnet user-secrets set "InfluxDB:Url" "http://localhost:8086"
dotnet user-secrets set "InfluxDB:Token" "dev-token"
dotnet user-secrets set "InfluxDB:Org" "home"
dotnet user-secrets set "InfluxDB:SmartMeterBucket" "smartmeter"
dotnet user-secrets set "InfluxDB:PvBucket" "pv"
dotnet user-secrets set "InfluxDB:WaterSoftenerCapacityBucket" "watersoftener"
dotnet user-secrets set "InfluxDB:WaterSoftenerMaxDepth" "80"

dotnet run --launch-profile http
```

Then open <http://localhost:5197/swagger>.

## Docker

The `master` branch is built and published to `ghcr.io/bgflor25/smartmeterapi` (tags `latest` and the commit SHA).

```bash
docker run -d --name smartmeterapi -p 8080:8080 \
  -e InfluxDB__Url=http://influxdb:8086 \
  -e InfluxDB__Token=... \
  -e InfluxDB__Org=home \
  -e InfluxDB__SmartMeterBucket=smartmeter \
  -e InfluxDB__PvBucket=pv \
  -e InfluxDB__WaterSoftenerCapacityBucket=watersoftener \
  -e InfluxDB__WaterSoftenerMaxDepth=80 \
  ghcr.io/bgflor25/smartmeterapi:latest
```

To build locally, run `docker build -t smartmeterapi .`, or use `docker compose up --build`. The compose file reads `INFLUXDB_URL`, `INFLUXDB_TOKEN`, … from your shell or a `.env` file.

The container listens on port **8080**, runs as the non-root `app` user and has a Docker `HEALTHCHECK` that calls `/health`.

## Health check

`GET /health` pings InfluxDB. The check is read-only and never writes data. It returns `200` with `"status": "Healthy"` when InfluxDB is reachable, and `503` with `"status": "Unhealthy"` otherwise. The response uses the HealthChecks UI JSON format.

## Tests

```bash
dotnet test
```

The unit tests cover JSON-to-domain mapping, InfluxDB line-protocol generation (field names, skipped null values) and the command handlers.

## CI/CD

`.github/workflows/docker-publish.yml` has two jobs:

1. **build-test**: restores, builds and tests the solution on every push and pull request to `master`.
2. **docker**: runs only for pushes to `master` and only after build-test succeeds. It builds the image and pushes it to GHCR.
