using Microsoft.EntityFrameworkCore;

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
