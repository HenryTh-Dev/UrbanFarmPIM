using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanFarm.Models
{
    public class SaleItem
    {
        public int SaleItemId { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}