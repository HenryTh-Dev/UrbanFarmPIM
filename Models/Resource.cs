using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanFarm.Models
    {
    public class Resource
    {
        public int ResourceId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // "Produto", "Trator", "Adubo", etc.
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public ICollection<Planting> Plantings { get; set; } // Associado ao plantio, se for um produto
    }

}