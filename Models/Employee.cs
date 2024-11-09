using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanFarm.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; } // Cadastro de Pessoa Física
        public string Position { get; set; } // Cargo
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }

}