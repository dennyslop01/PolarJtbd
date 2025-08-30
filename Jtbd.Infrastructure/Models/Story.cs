using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class Story
{
    public int IdStorie { get; set; }

    public int? ProjectIdProject { get; set; }

    public string TitleStorie { get; set; } = null!;

    public string ContextStorie { get; set; } = null!;

    public int? IdInter1 { get; set; }

    public string CreatedUser { get; set; } = null!;

    public string UpdatedUser { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual Interview? IdInter1Navigation { get; set; }

    public virtual Project? ProjectIdProjectNavigation { get; set; }
}
