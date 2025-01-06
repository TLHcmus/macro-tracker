using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MacroTrackerUI.Models;

/// <summary>
/// Represents a log entry containing details about exercises and food items.
/// </summary>
public class Log : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the unique identifier for the log.
    /// </summary>
    public int LogId { get; set; }

    /// <summary>
    /// Gets or sets the date of the log entry.
    /// </summary>
    public DateOnly? LogDate { get; set; }

    /// <summary>
    /// Gets or sets the total calories for the log entry.
    /// </summary>
    public double TotalCalories { get; set; }

    /// <summary>
    /// Gets or sets the collection of log exercise items associated with the log.
    /// </summary>
    public ObservableCollection<LogExerciseItem> LogExerciseItems { get; set; }

    /// <summary>
    /// Gets or sets the collection of log food items associated with the log.
    /// </summary>
    public ObservableCollection<LogFoodItem> LogFoodItems { get; set; }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}
