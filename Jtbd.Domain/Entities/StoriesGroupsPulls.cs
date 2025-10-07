using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    [Keyless]
    public class StoriesGroupsPulls
    {
        public Stories? Stories { get; set; }

        public Groups? Groups { get; set; }

        public int ValorPull { get; set; }
    }
}
