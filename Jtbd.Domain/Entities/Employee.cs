using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string EmployeeName { get; set; } = string.Empty;
        public int EmployeeRol { get; set; }
        public Deparments? Deparments { get; set; }
        public int StatusEmployee { get; set; } = 1;
    }
}
