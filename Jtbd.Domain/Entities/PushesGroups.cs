using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class PushesGroups
    {
        [Key] 
        public int IdPush { get; set; }

        [Required]
        [MaxLength(100)] 
        public string PushName { get; set; }= string.Empty;

        [Required]
        [MaxLength(1000)] 
        public string PushDescription { get; set; } = string.Empty;

        public Projects? Project { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public string UpdatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int StatusPush { get; set; } = 1;
    }
}
