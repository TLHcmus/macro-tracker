using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

public partial class Exercise
{
    public string Name { get; set; } = null!;

    public double? CaloriesPerMinute { get; set; }

    public string? IconFileName { get; set; }

    public virtual ICollection<LogExerciseItem> LogExerciseItems { get; set; } = new List<LogExerciseItem>();
}
