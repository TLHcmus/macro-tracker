namespace MacroTrackerCore.Entities;

/// <summary>
/// Represents a logged food item.
/// </summary>
public partial class LogFoodItem
{
    /// <summary>
    /// Gets or sets the log food ID.
    /// </summary>
    public int LogFoodId { get; set; }

    /// <summary>
    /// Gets or sets the log ID.
    /// </summary>
    public int? LogId { get; set; }

    /// <summary>
    /// Gets or sets the name of the food.
    /// </summary>
    public string? FoodName { get; set; }

    /// <summary>
    /// Gets or sets the number of servings.
    /// </summary>
    public double? NumberOfServings { get; set; }

    /// <summary>
    /// Gets or sets the total calories.
    /// </summary>
    public double? TotalCalories { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for the food.
    /// </summary>
    public virtual Food? FoodNameNavigation { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for the log.
    /// </summary>
    public virtual Log? Log { get; set; }
}
