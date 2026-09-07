# ---------------------------------
# Base runtime image
# ---------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Bind to all interfaces inside container
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

# ---------------------------------
# Build stage
# ---------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files first for better layer caching
COPY ["SmartMeterApi.Api/SmartMeterApi.Api.csproj", "SmartMeterApi.Api/"]
COPY ["SmartMeterApi.Application/SmartMeterApi.Application.csproj", "SmartMeterApi.Application/"]
COPY ["SmartMeterApi.Domain/SmartMeterApi.Domain.csproj", "SmartMeterApi.Domain/"]
COPY ["SmartMeterApi.Infrastructure/SmartMeterApi.Infrastructure.csproj", "SmartMeterApi.Infrastructure/"]

RUN dotnet restore "SmartMeterApi.Api/SmartMeterApi.Api.csproj"

# Copy remaining source
COPY . .

WORKDIR "/src/SmartMeterApi.Api"

RUN dotnet publish "SmartMeterApi.Api.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ---------------------------------
# Final runtime image
# ---------------------------------
FROM base AS final
WORKDIR /app

RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

RUN useradd -m appuser
USER appuser

COPY --from=build /app/publish .

HEALTHCHECK --interval=30s --timeout=3s --start-period=10s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "SmartMeterApi.Api.dll"]
