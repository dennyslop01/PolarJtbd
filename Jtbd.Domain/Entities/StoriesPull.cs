using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    [Keyless]
    public partial class StoriesPull
    {
        public Stories? Stories { get; set; }

        public PullGroups? PullGroups { get; set; }

        public Groups? Groups { get; set; }
    }
}
