using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class PullGroup
{
    public int IdPull { get; set; }

    public string PullName { get; set; } = null!;

    public string PullDescription { get; set; } = null!;

    public int? ProjectIdProject { get; set; }

    public string CreatedUser { get; set; } = null!;

    public string UpdatedUser { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int StatusPull { get; set; }

    public virtual Project? ProjectIdProjectNavigation { get; set; }
}
