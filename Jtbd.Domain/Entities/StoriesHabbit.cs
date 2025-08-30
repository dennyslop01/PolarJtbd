using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public partial class StoriesHabbit
    {
        public List<Stories>? Stories { get; set; }

        public List<Habits>? Habits { get; set; }
    }
}
