using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class Habit
{
    public int IdHabit { get; set; }

    public string HabitName { get; set; } = null!;

    public int? ProjectIdProject { get; set; }

    public string CreatedUser { get; set; } = null!;

    public string UpdatedUser { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int StatusHabit { get; set; }

    public virtual Project? ProjectIdProjectNavigation { get; set; }
}
