using Microsoft.EntityFrameworkCore;

namespace Jtbd.Domain.Entities
{
    [Keyless]
    public partial class StoriesAnxiety
    {
        public Stories? Stories { get; set; }

        public Anxieties? Anxieties { get; set; }
    }
}
