namespace MacroTrackerCore.Entities;

/// <summary>
/// Represents a log entry for an exercise item.
/// </summary>
public partial class LogExerciseItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the log exercise item.
    /// </summary>
    public int LogExerciseId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the associated log.
    /// </summary>
    public int? LogId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the associated exercise.
    /// </summary>
    public int? ExerciseId { get; set; }

    /// <summary>
    /// Gets or sets the duration of the exercise in minutes.
    /// </summary>
    public double? Duration { get; set; }

    /// <summary>
    /// Gets or sets the total calories burned during the exercise.
    /// </summary>
    public double? TotalCalories { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for the associated exercise.
    /// </summary>
    public virtual Exercise? Exercise { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for the associated log.
    /// </summary>
    public virtual Log? Log { get; set; }
}
