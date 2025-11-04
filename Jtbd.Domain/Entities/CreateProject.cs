using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class CreateProject
    {
        public int IdProject { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(500)]
        
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime ProjectDate { get; set; }

        [Range(0, 1000, ErrorMessage = "El departamento es obligatorio.")] 
        public int IdDeparmento { get; set; }

        [Range(0, 1000, ErrorMessage = "La categoria es obligatoria.")] 
        public int idCategoria { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ProjectDescription { get; set; } = string.Empty;

        [Range(0, 11, ErrorMessage = "Solo se permiten has 11 pushes.")] 
        public int MaxPushes { get; set; } = 11;


        [Range(0, 11, ErrorMessage = "Solo se permiten has 11 pull.")]
        public int MaxPulls { get; set; } = 11;
        public string RutaImage { get; set; } = string.Empty;
        public string CreatedUser { get; set; } = string.Empty;
        public string UpdatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Range(0, 1, ErrorMessage = "El estatus es obligatoria.")] 
        public int StatusProject { get; set; } = 1;
    }
}
