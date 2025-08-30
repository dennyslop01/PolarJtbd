using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public partial class StoriesPush
    {
        public List<Stories>? Stories { get; set; }

        public List<PushesGroups>? PushesGroups { get; set; }
    }
}
