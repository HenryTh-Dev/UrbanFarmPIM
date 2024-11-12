using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UrbanFarm.Models
    {
    public class Resource
    {
        public int ResourceId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } 
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public ICollection<Planting> Plantings { get; set; } // Associado ao plantio, se for um produto
    }

}