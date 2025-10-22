using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class CreateStorie
    {
        public int IdStorie { get; set; }
        public int IdProject { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio.")]
        [MaxLength(100)]
        public string TitleStorie { get; set; } = string.Empty;

        [MaxLength(4000)]
        public string? ContextStorie { get; set; }

        [MaxLength(4000)]
        public string? ObservacionStorie { get; set; }

        [Range(0, 10000, ErrorMessage = "El entrevistado es obligatorio.")] 
        public int IdInter { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public string UpdatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
