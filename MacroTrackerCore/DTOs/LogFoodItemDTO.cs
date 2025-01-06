using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.DTOs;

/// <summary>
/// Data Transfer Object for logging food items.
/// </summary>
public class LogFoodItemDTO
{
    /// <summary>
    /// Gets or sets the unique identifier for the logged food item.
    /// </summary>
    public int LogFoodId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the food item.
    /// </summary>
    public int FoodId { get; set; }

    /// <summary>
    /// Gets or sets the number of servings of the food item.
    /// </summary>
    public double NumberOfServings { get; set; }

    /// <summary>
    /// Gets or sets the total calories for the logged food item.
    /// </summary>
    public double TotalCalories { get; set; }
}
