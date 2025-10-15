using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartMeterApi.Application.Interfaces;
using SmartMeterApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseFastEndpoints();

app.UseHttpsRedirection();

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddFastEndpoints();
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SmartMeterApi.Application.Commands.AddMeterData.AddMeterDataCommand).Assembly));

    services.AddDbContext<ISmartMeterDbContext, SmartMeterDbContext>(x =>
    {
        x.UseSqlServer(configuration.GetConnectionString("SmartMeterDb"));
        x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

    //services.AddDbContext<ISmartMeterDbContext, SmartMeterDbContext>(x =>
    //{
    //    var connectionString = configuration.GetConnectionString("DrinkManagerDb");

    //    // Specify MariaDB version (adjust as needed, e.g., 10.5, 10.6, etc.)
    //    var serverVersion = new MariaDbServerVersion(new Version(10, 6, 0));

    //    x.UseMySql(connectionString, serverVersion);
    //    x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //});
    //services.AddScoped<ISmartMeterDbContext>(provider =>
    //    provider.GetRequiredService<SmartMeterDbContext>());

}

