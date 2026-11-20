# ---------------------------------
# Build stage
# ---------------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore first for better layer caching
COPY ["Directory.Build.props", "Directory.Packages.props", "./"]
COPY ["SmartMeterApi.Api/SmartMeterApi.Api.csproj", "SmartMeterApi.Api/"]
COPY ["SmartMeterApi.Application/SmartMeterApi.Application.csproj", "SmartMeterApi.Application/"]
COPY ["SmartMeterApi.Domain/SmartMeterApi.Domain.csproj", "SmartMeterApi.Domain/"]
COPY ["SmartMeterApi.Infrastructure/SmartMeterApi.Infrastructure.csproj", "SmartMeterApi.Infrastructure/"]
RUN dotnet restore "SmartMeterApi.Api/SmartMeterApi.Api.csproj"

COPY . .
RUN dotnet publish "SmartMeterApi.Api/SmartMeterApi.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# ---------------------------------
# Runtime image
# ---------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

ENV ASPNETCORE_HTTP_PORTS=8080 \
    ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

COPY --from=build /app/publish .

# Non-root user that ships with the official .NET images
USER $APP_UID

HEALTHCHECK --interval=30s --timeout=3s --start-period=10s --retries=3 \
  CMD curl -fsS http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "SmartMeterApi.Api.dll"]
