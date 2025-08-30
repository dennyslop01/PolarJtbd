using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class Anxiety
{
    public int IdAnxie { get; set; }

    public string AnxieName { get; set; } = null!;

    public int? ProjectIdProject { get; set; }

    public string CreatedUser { get; set; } = null!;

    public string UpdatedUser { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int StatusAnxie { get; set; }

    public virtual Project? ProjectIdProjectNavigation { get; set; }
}
