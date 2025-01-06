using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.DTOs;

/// <summary>
/// Data Transfer Object for logging exercise items.
/// </summary>
public class LogExerciseItemDTO
{
    /// <summary>
    /// Gets or sets the unique identifier for the log exercise.
    /// </summary>
    public int LogExerciseId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the exercise.
    /// </summary>
    public int ExerciseId { get; set; }

    /// <summary>
    /// Gets or sets the duration of the exercise in minutes.
    /// </summary>
    public double Duration { get; set; }

    /// <summary>
    /// Gets or sets the total calories burned during the exercise.
    /// </summary>
    public double TotalCalories { get; set; }
}
