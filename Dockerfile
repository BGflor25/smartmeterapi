# ---------------------------------
# Base runtime image
# ---------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# ASP.NET Core will listen on 8080 inside container
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# ---------------------------------
# Build stage
# ---------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["SmartMeterApi.Api/SmartMeterApi.Api.csproj", "SmartMeterApi.Api/"]
COPY ["SmartMeterApi.Application/SmartMeterApi.Application.csproj", "SmartMeterApi.Application/"]
COPY ["SmartMeterApi.Domain/SmartMeterApi.Domain.csproj", "SmartMeterApi.Domain/"]
COPY ["SmartMeterApi.Infrastructure/SmartMeterApi.Infrastructure.csproj", "SmartMeterApi.Infrastructure/"]

RUN dotnet restore "SmartMeterApi.Api/SmartMeterApi.Api.csproj"

COPY . .
WORKDIR "/src/SmartMeterApi.Api"
RUN dotnet build "SmartMeterApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ---------------------------------
# Publish stage
# ---------------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SmartMeterApi.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

# ---------------------------------
# Final runtime image
# ---------------------------------
FROM base AS final
WORKDIR /app

# IMPORTANT: do NOT hardcode Development in containers
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SmartMeterApi.Api.dll"]
