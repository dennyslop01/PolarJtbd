using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class StoriesPull
{
    public int IdStorie { get; set; }

    public int IdPull { get; set; }

    public virtual PullGroup IdPullNavigation { get; set; } = null!;

    public virtual Story IdStorieNavigation { get; set; } = null!;
}
