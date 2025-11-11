using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    [Keyless]
    public class StoriesClusters
    {
        public Projects? Project { get; set; }
        public Stories? Stories { get; set; }
        public int IdCluster { get; set; }
    }
}
