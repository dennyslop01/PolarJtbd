using Microsoft.EntityFrameworkCore;

namespace Jtbd.Domain.Entities
{
    [Keyless]
    public class StoriesGroupsPushes
    {
        public Stories? Stories { get; set; }

        public Groups? Groups { get; set; }

        public int ValorPush { get; set; }
    }
}
