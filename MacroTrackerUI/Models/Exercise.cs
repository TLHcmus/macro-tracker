using System.ComponentModel;

namespace MacroTrackerUI.Models;

/// <summary>
/// Represents an exercise with a name, calories burned per minute, and an icon.
/// </summary>
public class Exercise
{
    /// <summary>
    /// Gets or sets the name of the exercise.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the calories burned per minute for the exercise.
    /// </summary>
    public double CaloriesPerMinute { get; set; }

    /// <summary>
    /// Gets or sets the file name of the icon representing the exercise.
    /// </summary>
    public string IconFileName { get; set; }

    // Get calories burned
    public double GetCaloriesBurned(double duration)
    {
        return CaloriesPerMinute * duration;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
