using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class CreateInterview
    {
        public int IdInter { get; set; }

        public int IdProject { get; set; }

        [Required]
        [MaxLength(100)]
        public string InterName { get; set; } = string.Empty;

        public int InterAge { get; set; }

        public int InterGender { get; set; }

        [MaxLength(100)]
        public string InterOccupation { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string InterNickname { get; set; } = string.Empty;

        [MaxLength(3, ErrorMessage = "Solo puede ingresar 3 caracteres")]
        public string InterNSE { get; set; } = string.Empty;

        [Required]
        public DateTime DateInter { get; set; }
    }
}
