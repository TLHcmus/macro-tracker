using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Log : INotifyPropertyChanged
{
    public int LogId { get; set; }

    public DateOnly? LogDate { get; set; }

    public double TotalCalories { get; set; }

    public List<LogExerciseItem> LogExerciseItems { get; set; }

    public List<LogFoodItem> LogFoodItems { get; set; } 

    public event PropertyChangedEventHandler PropertyChanged;
}
