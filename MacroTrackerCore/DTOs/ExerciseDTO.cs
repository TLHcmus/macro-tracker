using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.DTOs;

/// <summary>
/// Data Transfer Object for Exercise.
/// </summary>
public class ExerciseDTO
{
    /// <summary>
    /// Gets or sets the unique identifier for the exercise.
    /// </summary>
    public int ExerciseId { get; set; }

    /// <summary>
    /// Gets or sets the name of the exercise.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the calories burned per minute for the exercise.
    /// </summary>
    public double CaloriesPerMinute { get; set; }

    /// <summary>
    /// Gets or sets the image associated with the exercise.
    /// </summary>
    public byte[] Image { get; set; }
}
