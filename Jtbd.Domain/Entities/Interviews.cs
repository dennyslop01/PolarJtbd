using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class Interviews
    {
        [Key] 
        public int IdInter { get; set; }
        public Projects? Project { get; set; }

        [Required]
        [MaxLength(100)]
        public string InterName {  get; set; } = string.Empty;

        public int InterAge { get; set; }
        public int InterGender { get; set; }

        [Required]
        [MaxLength(100)] 
        public string InterOccupation { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string InterNickname { get; set; }= string.Empty;

        [Required]
        [MaxLength(3)]
        public string InterNSE { get; set; }=string.Empty;
        public DateTime DateInter { get; set; }
    }
}
