using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanFarm.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; } // Cadastro Nacional de Pessoa Jurídica
        public string Address { get; set; }
        public string Phone { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }

}