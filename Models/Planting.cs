using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UrbanFarm.Models
{
    public class Planting
    {
        public int PlantingId { get; set; }

        // Alterado de DateTime para DateTimeOffset
        public DateTimeOffset PlantingDate { get; set; }

        [JsonIgnore]
        public int PlantingAreaId { get; set; }

        [JsonIgnore]
        public PlantingArea PlantingArea { get; set; }

        public int ResourceId { get; set; }

        public Resource Resource { get; set; }

        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }
    }
}
