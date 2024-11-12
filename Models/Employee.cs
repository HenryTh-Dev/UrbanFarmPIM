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
        public string CPF { get; set; } 
        public string Position { get; set; } 
        public decimal Salary { get; set; }
        public DateTimeOffset HireDate { get; set; }
    }

}