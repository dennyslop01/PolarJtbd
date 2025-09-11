using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    [Keyless]
    public class CreateStoriesHabbit
    {
        public int IdStories { get; set; }

        [Range(0, 10000, ErrorMessage = "Debe seleccionar uno.")]
        public int IdHabit { get; set; }
    }
}
