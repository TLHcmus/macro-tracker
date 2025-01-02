using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

/// <summary>
/// Represents a nutritional goal with specific macronutrient targets.
/// </summary>
public partial class Goal
{
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
}
