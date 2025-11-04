using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    [Keyless]
    public class CreateStoriesAnxiety
    {
        public int IdStories { get; set; }

        [Range(0, 10000, ErrorMessage = "Debe seleccionar uno.")]
        public int IdAnxie { get; set; }
    }
}
