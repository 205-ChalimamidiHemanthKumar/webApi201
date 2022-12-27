using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi201.Model
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public DateTime DOJ { get; set; }
        public string Address { get; set; }
    }
}
