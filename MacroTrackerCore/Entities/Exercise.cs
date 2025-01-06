namespace MacroTrackerCore.Entities;

/// <summary>
/// Represents an exercise entity with details about the exercise.
/// </summary>
public partial class Exercise
{
    /// <summary>
    /// Gets or sets the unique identifier for the exercise.
    /// </summary>
    public int ExerciseId { get; set; }

    /// <summary>
    /// Gets or sets the name of the exercise.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the calories burned per minute for the exercise.
    /// </summary>
    public double? CaloriesPerMinute { get; set; }

    /// <summary>
    /// Gets or sets the image associated with the exercise.
    /// </summary>
    public byte[]? Image { get; set; }

    /// <summary>
    /// Gets or sets the collection of log exercise items associated with the exercise.
    /// </summary>
    public virtual ICollection<LogExerciseItem> LogExerciseItems { get; set; } = [];
}
