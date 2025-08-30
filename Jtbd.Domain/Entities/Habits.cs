using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class Habits
    {
        [Key] 
        public int IdHabit { get; set; }

        [Required]
        [MaxLength(100)]
        public string HabitName { get; set; } = string.Empty;

        public Projects? Project { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public string UpdatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int StatusHabit { get; set; } = 1;
    }
}
