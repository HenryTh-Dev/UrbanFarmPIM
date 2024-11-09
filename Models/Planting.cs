using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanFarm.Models
{
    public class Planting
    {
        public int PlantingId { get; set; }
        public DateTime PlantingDate { get; set; }
        public int PlantingAreaId { get; set; }
        public PlantingArea PlantingArea { get; set; }
        public int ResourceId { get; set; } // O recurso resultante (ex: Produto colhido)
        public Resource Resource { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }

}