using Microsoft.EntityFrameworkCore;

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
