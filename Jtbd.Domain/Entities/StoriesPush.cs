using Microsoft.EntityFrameworkCore;

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
