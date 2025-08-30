using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class PullGroups
    {
        [Key] 
        public int IdPull { get; set; }

        [Required]
        [MaxLength(100)] 
        public string PullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)] 
        public string PullDescription { get; set; } = string.Empty;

        public Projects? Project { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public string UpdatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int StatusPull { get; set; } = 1;
    }
}
