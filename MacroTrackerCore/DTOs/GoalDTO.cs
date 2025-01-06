using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.DTOs;

/// <summary>
/// Represents a goal with nutritional information.
/// </summary>
public class GoalDTO
{
    /// <summary>
    /// Gets or sets the unique identifier for the goal.
    /// </summary>
    public int GoalId { get; set; }

    /// <summary>
    /// Gets or sets the number of calories for the goal.
    /// </summary>
    public int Calories { get; set; }

    /// <summary>
    /// Gets or sets the amount of protein (in grams) for the goal.
    /// </summary>
    public int Protein { get; set; }

    /// <summary>
    /// Gets or sets the amount of carbohydrates (in grams) for the goal.
    /// </summary>
    public int Carbs { get; set; }

    /// <summary>
    /// Gets or sets the amount of fat (in grams) for the goal.
    /// </summary>
    public int Fat { get; set; }
}
