using MacroTrackerCore.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.DTOs;

/// <summary>
/// Data Transfer Object for logging daily activities and food consumption.
/// </summary>
public class LogDTO
{
    /// <summary>
    /// Gets or sets the unique identifier for the log.
    /// </summary>
    public int LogId { get; set; }

    /// <summary>
    /// Gets or sets the date of the log.
    /// </summary>
    public DateOnly? LogDate { get; set; }

    /// <summary>
    /// Gets or sets the total calories consumed or burned in the log.
    /// </summary>
    public double TotalCalories { get; set; }

    /// <summary>
    /// Gets or sets the list of exercise items in the log.
    /// </summary>
    public List<LogExerciseItemDTO> LogExerciseItems { get; set; }

    /// <summary>
    /// Gets or sets the list of food items in the log.
    /// </summary>
    public List<LogFoodItemDTO> LogFoodItems { get; set; }
}
