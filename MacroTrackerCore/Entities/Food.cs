using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

/// <summary>
/// Represents a food item with nutritional information.
/// </summary>
public partial class Food
{
    /// <summary>
    /// Gets or sets the name of the food.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the calories per 100 grams of the food.
    /// </summary>
    public double? CaloriesPer100g { get; set; }

    /// <summary>
    /// Gets or sets the protein per 100 grams of the food.
    /// </summary>
    public double? ProteinPer100g { get; set; }

    /// <summary>
    /// Gets or sets the carbohydrates per 100 grams of the food.
    /// </summary>
    public double? CarbsPer100g { get; set; }

    /// <summary>
    /// Gets or sets the fat per 100 grams of the food.
    /// </summary>
    public double? FatPer100g { get; set; }

    /// <summary>
    /// Gets or sets the file name of the icon representing the food.
    /// </summary>
    public string? IconFileName { get; set; }

    /// <summary>
    /// Gets or sets the collection of log food items associated with the food.
    /// </summary>
    public virtual ICollection<LogFoodItem> LogFoodItems { get; set; } = [];
}
