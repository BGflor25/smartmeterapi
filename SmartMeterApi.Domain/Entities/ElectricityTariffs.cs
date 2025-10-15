using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterApi.Domain.Entities
{
    public class ElectricityTariffs
    {
        public Guid Id { get; set; }
        public double ElectricityImportLowTariff { get; set; }
        public double ElectricityImportHighTariff { get; set; }
        public double ElectricityExportLowTariff { get; set; }
        public double ElectricityExportHighTariff { get; set; }
    }
}
