using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterApi.Domain.Entities
{
    public class GasConsumption
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
    }
}
