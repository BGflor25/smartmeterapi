using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterApi.Application.Interfaces
{
    public interface ISmartMeterDbContext
    {
        DbSet<Domain.Entities.MeterData> MeterData { get; }

        Task SaveChangesAsync();
        Task<Guid> AddMeterData(Domain.Entities.MeterData meterData);
    }
}
