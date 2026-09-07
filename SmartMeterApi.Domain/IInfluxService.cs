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
        public Task WriteSmartMeterDataAsync(SmartMeterData meterData);
        public Task WriteWaterSoftenerDataAsync(WaterSoftenerCapacity waterSoftenerCapacity);
        public Task WritePvInstallationDataAsync(Entities.PvInstallationData pvInstallationData);

    }
}
