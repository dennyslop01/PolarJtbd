using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    [Keyless]
    public partial class StoriesPush
    {
       
        public Stories? Stories { get; set; }

        public PushesGroups? PushesGroups{ get; set; }

        public Groups? Groups { get; set; }
    }
}
