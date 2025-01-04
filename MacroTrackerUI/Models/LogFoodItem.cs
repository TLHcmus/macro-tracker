using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

/// <summary>
/// Represents a food item logged by the user.
/// </summary>
public class LogFoodItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the logged food item.
    /// </summary>
    public int LogFoodId { get; set; }

    /// <summary>
    /// Gets or sets the name of the food.
    /// </summary>
    public string FoodName { get; set; }

    /// <summary>
    /// Gets or sets the number of servings of the food.
    /// </summary>
    public double NumberOfServings { get; set; }

    /// <summary>
    /// Gets or sets the total calories of the food.
    /// </summary>
    public double TotalCalories { get; set; }
}
