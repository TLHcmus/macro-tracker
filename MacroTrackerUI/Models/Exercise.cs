using System.ComponentModel;
using System.IO;

namespace MacroTrackerUI.Models;

/// <summary>
/// Represents an exercise with a name, calories burned per minute, and an icon.
/// </summary>
public class Exercise
{
    public int ExerciseId { get; set; }
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the calories burned per minute for the exercise.
    /// </summary>
    public double CaloriesPerMinute { get; set; }

    public byte[] Image { get; set; }

    // Get calories burned
    public double GetCaloriesBurned(double duration)
    {
        return CaloriesPerMinute * duration;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
