using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilionPdtScanner.DTOs
{
    public class PackingSlip
    {
        [JsonProperty("Number")]
        public string? Number { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonProperty("ShipStreet1")]
        public string? ShipStreet1 { get; set; }

        [JsonProperty("ShipStreet2")]
        public string? ShipStreet2 { get; set; }

        [JsonProperty("ShipCity")]
        public string? ShipCity { get; set; }

        [JsonProperty("ShipState")]
        public string? ShipState { get; set; }

        [JsonProperty("ShipPostalCode")]
        public string? ShipPostalCode { get; set; }

        [JsonProperty("ShipCountry")]
        public string? ShipCountry { get; set; }

        [JsonProperty("Phone")]
        public string? Phone { get; set; }

        [JsonProperty("Items")]
        public List<Item>? Items { get; set; }

        [JsonProperty("Store")]
        public string? Store { get; set; }

        [JsonProperty("Orders")]
        public List<int>? Orders { get; set; }
    }
}


