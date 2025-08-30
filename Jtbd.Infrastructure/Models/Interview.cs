using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class Interview
{
    public int IdInter { get; set; }

    public string InterName { get; set; } = null!;

    public int InterAge { get; set; }

    public int InterGender { get; set; }

    public string InterOccupation { get; set; } = null!;

    public string InterNickname { get; set; } = null!;

    public string InterNse { get; set; } = null!;

    public DateTime DateInter { get; set; }

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();
}
