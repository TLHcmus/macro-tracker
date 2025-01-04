using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

public partial class Log
{
    public int LogId { get; set; }

    public DateOnly? LogDate { get; set; }

    public double? TotalCalories { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<LogExerciseItem> LogExerciseItems { get; set; } = new List<LogExerciseItem>();

    public virtual ICollection<LogFoodItem> LogFoodItems { get; set; } = new List<LogFoodItem>();

    public virtual User? User { get; set; }
}
