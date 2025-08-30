using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class Stories
    {
        [Key] 
        public int IdStorie { get; set; }
        public Projects? Project { get; set; }

        [Required]
        [MaxLength(100)]
        public string TitleStorie { get; set; } = string.Empty;

        [Required]
        [MaxLength(4000)]
        public string ContextStorie { get;set; } = string.Empty;

        public Interviews? IdInter { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public string UpdatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
