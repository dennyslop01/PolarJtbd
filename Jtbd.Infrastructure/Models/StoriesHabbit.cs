using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class StoriesHabbit
{
    public int IdStorie { get; set; }

    public int IdHabit { get; set; }

    public virtual Habit IdHabitNavigation { get; set; } = null!;

    public virtual Story IdStorieNavigation { get; set; } = null!;
}
