using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class StoriesPush
{
    public int IdStorie { get; set; }

    public int IdPush { get; set; }

    public virtual PushesGroup IdPushNavigation { get; set; } = null!;

    public virtual Story IdStorieNavigation { get; set; } = null!;
}
