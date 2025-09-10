using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    [Keyless]
    public partial class StoriesAnxiety
    {
        public Stories? Stories { get; set; }

        public Anxieties? Anxieties { get; set; }
    }
}
