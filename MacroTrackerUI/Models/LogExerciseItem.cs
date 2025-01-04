using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

/// <summary>
/// Represents a log entry for an exercise item.
/// </summary>
public class LogExerciseItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the log exercise item.
    /// </summary>
    public int LogExerciseId { get; set; }

    /// <summary>
    /// Gets or sets the name of the exercise.
    /// </summary>
    public string ExerciseName { get; set; }

    /// <summary>
    /// Gets or sets the duration of the exercise in minutes.
    /// </summary>
    public double Duration { get; set; }

    /// <summary>
    /// Gets or sets the total calories burned during the exercise.
    /// </summary>
    public double TotalCalories { get; set; }
}
