using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class Project
{
    public int IdProject { get; set; }

    public string ProjectName { get; set; } = null!;

    public DateTime ProjectDate { get; set; }

    public int? DeparmentId { get; set; }

    public int? CategoriesId { get; set; }

    public string ProjectDescription { get; set; } = null!;

    public int MaxPushes { get; set; }

    public int MaxPulls { get; set; }

    public string RutaImage { get; set; } = null!;

    public string CreatedUser { get; set; } = null!;

    public string UpdatedUser { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int StatusProject { get; set; }

    public virtual ICollection<Anxiety> Anxieties { get; set; } = new List<Anxiety>();

    public virtual Category? Categories { get; set; }

    public virtual Deparment? Deparment { get; set; }

    public virtual ICollection<Habit> Habits { get; set; } = new List<Habit>();

    public virtual ICollection<PullGroup> PullGroups { get; set; } = new List<PullGroup>();

    public virtual ICollection<PushesGroup> PushesGroups { get; set; } = new List<PushesGroup>();

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();
}
