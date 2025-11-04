using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(500)]
        public string EmployeeName { get; set; } = string.Empty;
        public int EmployeeRol { get; set; }
        public Deparments? Deparments { get; set; }
        public int StatusEmployee { get; set; } = 1;
    }
}
