using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class CreateGroup
    {
        public int IdGroup { get; set; }
        public int IdTipo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100)]
        public string GroupName { get; set; } = string.Empty;

        public int IdProject { get; set; }
    }
}
