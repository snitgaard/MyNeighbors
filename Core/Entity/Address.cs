using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MyNeighbors.Core.Entity
{
    [Serializable]
    public class Address
    {
        [NotMapped]
        public string Id { get; set; }

        [JsonProperty("id")]
        public string CityId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("darstatus")]
        public int DarStatus { get; set; }

        [JsonProperty("vejkode")]
        public string StreetCode { get; set; }

        [JsonProperty("vejnavn")]
        public string StreetName { get; set; }

        [JsonProperty("husnr")]
        public string HouseNumber { get; set; }

        [JsonProperty("etage")]
        public string Floor { get; set; }

        [JsonProperty("dør")]
        public string Door { get; set; }

        [JsonProperty("postnr")]
        public string ZipCode { get; set; }

        [JsonProperty("postnrnavn")]
        public string ZipCodeName { get; set; }

        [JsonProperty("kommunekode")]
        public string MunicipalityCode { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("betegnelse")]
        public string Description { get; set; }
    }
}
