namespace MacroTrackerCore.Entities;

/// <summary>
/// Represents a nutritional goal with specific macronutrient targets.
/// </summary>
public partial class Goal
{
    /// <summary>
    /// Gets or sets the unique identifier for the goal.
    /// </summary>
    public int GoalId { get; set; }

    /// <summary>
    /// Gets or sets the target number of calories.
    /// </summary>
    public int? Calories { get; set; }

    /// <summary>
    /// Gets or sets the target amount of protein in grams.
    /// </summary>
    public int? Protein { get; set; }

    /// <summary>
    /// Gets or sets the target amount of carbohydrates in grams.
    /// </summary>
    public int? Carbs { get; set; }

    /// <summary>
    /// Gets or sets the target amount of fat in grams.
    /// </summary>
    public int? Fat { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the user associated with the goal.
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the goal.
    /// </summary>
    public virtual User? User { get; set; }
}
