using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class Groups
    {
        [Key]
        public int IdGroup { get; set; }
        public int IdTipo { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string GroupName { get; set; } = string.Empty;

        public Projects? Project { get; set; }
    }
}
