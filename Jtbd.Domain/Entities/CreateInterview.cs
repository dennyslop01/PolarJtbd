using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class CreateInterview
    {
        public int IdInter { get; set; }

        public int IdProject { get; set; }

        [Required]
        [MaxLength(100)]
        public string InterName { get; set; } = string.Empty;

        [Range(12, 99, ErrorMessage = "Solo se permiten valores entre 12 y 99")]
        public int InterAge { get; set; }

        [Range(0, 2, ErrorMessage = "El sexo es requerido")]
        public int InterGender { get; set; }

        [MaxLength(100)]
        public string InterOccupation { get; set; } = string.Empty;

        [MaxLength(100)]
        public string InterNickname { get; set; } = string.Empty;

        [MaxLength(3, ErrorMessage = "Solo puede ingresar 3 caracteres")]
        public string InterNSE { get; set; } = string.Empty;

        [Required]
        public DateTime DateInter { get; set; }
    }
}
