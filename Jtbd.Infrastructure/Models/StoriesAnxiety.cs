using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class StoriesAnxiety
{
    public int IdStorie { get; set; }

    public int IdAnxie { get; set; }

    public virtual Anxiety IdAnxieNavigation { get; set; } = null!;

    public virtual Story IdStorieNavigation { get; set; } = null!;
}
