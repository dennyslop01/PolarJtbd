using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class CreatePull
    {
        public int IdPull { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100)]
        public string PullName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string PullDescription { get; set; } = string.Empty;

        public int IdProject { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public string UpdatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Range(0, 1, ErrorMessage = "El estatus es obligatorio.")] 
        public int StatusPull { get; set; } = 1;
    }
}
