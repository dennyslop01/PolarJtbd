using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class Anxieties
    {
        [Key] 
        public int IdAnxie { get; set; }

        [Required]
        [MaxLength(100)]
        public string AnxieName { get; set; } = string.Empty;

        public Projects? Project { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public string UpdatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int StatusAnxie { get; set; } = 1;
    }
}
