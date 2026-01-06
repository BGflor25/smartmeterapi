using SmartMeterApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterApi.Domain
{
    public interface IInfluxService
    {
        public Task WriteAsync(MeterData meterData, CancellationToken ct = default);
    }
}
