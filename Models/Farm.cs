using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UrbanFarm.Models
{
    public class Farm
    {
        public int FarmId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        [JsonIgnore]
        public ICollection<PlantingArea> PlantingAreas { get; set; } = new List<PlantingArea>();
    }
}