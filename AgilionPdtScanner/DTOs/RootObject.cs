using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilionPdtScanner.DTOs
{
    public class RootObject
    {
        [JsonProperty("Version")]
        public string? Version { get; set; }

        [JsonProperty("PackingSlips")]
        public List<PackingSlip>? PackingSlips { get; set; }
    }
}
