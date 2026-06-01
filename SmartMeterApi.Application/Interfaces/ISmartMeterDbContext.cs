using Microsoft.EntityFrameworkCore;
using SmartMeterApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterApi.Application.Interfaces
{
    public interface ISmartMeterDbContext
    {
        DbSet<Domain.Entities.SmartMeterData> MeterData { get; }
        DbSet<Domain.Entities.WaterSoftenerCapacity> WaterSoftenerCapacity { get; }

        Task SaveChangesAsync();
        Task<Guid> AddSmartMeterData(Domain.Entities.SmartMeterData meterData);
    }
}
