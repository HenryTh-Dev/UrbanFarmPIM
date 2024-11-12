using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanFarm.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTimeOffset SaleDate { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; }
        public decimal TotalAmount { get; set; }
    }

}