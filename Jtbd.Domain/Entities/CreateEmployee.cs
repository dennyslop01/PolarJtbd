using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class CreateEmployee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(500)]
        public string EmployeeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [Range(0, 1000, ErrorMessage = "El rol es obligatorio.")]
        public int EmployeeRol { get; set; }

        [Required(ErrorMessage = "El departamento es obligatorio.")]
        [Range(0, 1000, ErrorMessage = "El departamento es obligatorio.")]
        public int IdDeparment { get; set; }

        [Required(ErrorMessage = "El estatus es obligatorio.")]
        [Range(0, 1000, ErrorMessage = "El estatus es obligatorio.")]
        public int StatusEmployee { get; set; } = 1;
    }
}
