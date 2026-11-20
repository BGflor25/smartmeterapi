using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using SmartMeterApi.Application.Abstractions;
using SmartMeterApi.Application.WaterSoftener;
using SmartMeterApi.Application.WaterSoftener.AddWaterSoftenerData;
using SmartMeterApi.Domain.Entities;

namespace SmartMeterApi.UnitTests;

public class WaterSoftenerDataTests
{
    [Theory]
    [InlineData(25, 100, 0.25)]
    [InlineData(0, 50, 0)]
    [InlineData(50, 50, 1)]
    public void FromDistance_returns_ratio_of_max_depth(double distance, double maxDepth, double expected) =>
        Assert.Equal(expected, WaterSoftenerCapacity.FromDistance(distance, maxDepth).Ratio, precision: 6);

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void FromDistance_rejects_non_positive_max_depth(double maxDepth) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => WaterSoftenerCapacity.FromDistance(10, maxDepth));

    [Fact]
    public async Task Handler_uses_configured_max_depth()
    {
        var writer = Substitute.For<ITelemetryWriter>();
        var options = Options.Create(new WaterSoftenerOptions { MaxDepth = 80 });
        var handler = new AddWaterSoftenerDataCommandHandler(writer, options, NullLogger<AddWaterSoftenerDataCommandHandler>.Instance);

        await handler.Handle(new AddWaterSoftenerDataCommand(new WaterSoftenerDataRequest { Distance = 20 }), CancellationToken.None);

        await writer.Received(1).WriteWaterSoftenerDataAsync(
            new WaterSoftenerCapacity(0.25),
            Arg.Any<CancellationToken>());
    }
}
