﻿using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

public partial class LogExerciseItem
{
    public int LogExerciseId { get; set; }

    public int? LogId { get; set; }

    public string? ExerciseName { get; set; }

    public double? Duration { get; set; }

    public double? TotalCalories { get; set; }

    public virtual Exercise? ExerciseNameNavigation { get; set; }

    public virtual Log? Log { get; set; }
}
