using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class Projects
    {
        [Key] 
        public int IdProject { get; set; }

        [Required]
        [MaxLength(500)]
        public string ProjectName { get; set; }
        public DateTime ProjectDate { get; set; }
        public Deparments? Deparment { get; set; }
        public Categories? Categories { get; set; }

        [Required]
        [MaxLength(1000)] 
        public string ProjectDescription { get; set; } = string.Empty;
        public int MaxPushes { get; set; } = 11;
        public int MaxPulls { get; set; } = 11;
        public string RutaImage { get; set; } = string.Empty;
        public string CreatedUser { get; set; } = string.Empty;
        public string UpdatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int StatusProject { get; set; } = 1;

    }
}
