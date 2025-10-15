using Microsoft.EntityFrameworkCore;
using SmartMeterApi.Application;
using SmartMeterApi.Application.Interfaces;
using SmartMeterApi.Domain.Entities;

namespace SmartMeterApi.Infrastructure
{
    public class SmartMeterDbContext: DbContext, ISmartMeterDbContext
    {
        public SmartMeterDbContext(DbContextOptions<SmartMeterDbContext> options) : base(options)
        {
            
        }
        public virtual DbSet<MeterData> MeterData { get; set; }

        public async Task<Guid> AddMeterData(MeterData meterData)
        {
            try
            {
                await MeterData.AddAsync(meterData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add meterdata {meterData.Timestamp}.", ex);
            }
            return meterData.Id;
        }
        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeterData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property<bool>("IsLogAnnul");

                entity.OwnsOne(e => e.ElectricityImportLowTariff);
                entity.OwnsOne(e => e.ElectricityImportHighTariff);
                entity.OwnsOne(e => e.ElectricityExportLowTariff);
                entity.OwnsOne(e => e.ElectricityExportHighTariff);
                entity.OwnsOne(e => e.TotalActivePowerExported);
                entity.OwnsOne(e => e.CurrentPowerUsage);
                entity.OwnsOne(e => e.CurrentPowerDelivery);
                entity.OwnsOne(e => e.InstantaneousActivePowerL1);
                entity.OwnsOne(e => e.InstantaneousActivePowerL2);
                entity.OwnsOne(e => e.Voltage);
                entity.OwnsOne(e => e.Current);
                entity.OwnsOne(e => e.MaxContractualPower);
                entity.OwnsOne(e => e.MaxCurrent);
                entity.HasQueryFilter(e => EF.Property<bool>(e, "IsLogAnnul") == false);
            });

        }
    }
}
