using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartMeterApi.Application.WaterSoftener.Commands.AddMeterData
{
    public class WaterSoftenerDataDto
    {
        [JsonPropertyName("distance")]
        public double Distance { get; set; }
    }
}
