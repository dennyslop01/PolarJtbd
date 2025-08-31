using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class Categories
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}