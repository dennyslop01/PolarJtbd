using Microsoft.EntityFrameworkCore;

namespace Jtbd.Domain.Entities
{
    [Keyless]
    public partial class StoriesHabbit
    {
        public Stories? Stories { get; set; }

        public Habits? Habits { get; set; }
    }
}
