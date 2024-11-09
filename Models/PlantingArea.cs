using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanFarm.Models
{
    public class PlantingArea
    {
        public int PlantingAreaId { get; set; }
        public string Name { get; set; }
        public double Size { get; set; } 
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
        public ICollection<Planting> Plantings { get; set; }
    }
}