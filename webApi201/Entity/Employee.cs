using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi201.Entity
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string Address { get; set; }
    }
}
