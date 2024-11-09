using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanFarm.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public ICollection<Resource> Resources { get; set; } // Produtos ou recursos fornecidos
    }
}