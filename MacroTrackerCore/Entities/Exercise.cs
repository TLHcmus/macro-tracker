using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

public partial class Exercise
{
    public int ExerciseId { get; set; }

    public string? Name { get; set; }

    public double? CaloriesPerMinute { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<LogExerciseItem> LogExerciseItems { get; set; } = new List<LogExerciseItem>();
}
