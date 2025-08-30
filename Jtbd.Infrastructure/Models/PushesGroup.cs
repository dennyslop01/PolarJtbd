using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class PushesGroup
{
    public int IdPush { get; set; }

    public string PushName { get; set; } = null!;

    public string PushDescription { get; set; } = null!;

    public int? ProjectIdProject { get; set; }

    public string CreatedUser { get; set; } = null!;

    public string UpdatedUser { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int StatusPush { get; set; }

    public virtual Project? ProjectIdProjectNavigation { get; set; }
}
