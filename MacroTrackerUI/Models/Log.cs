using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Log : INotifyPropertyChanged
{
    public int LogId { get; set; }

    public DateOnly? LogDate { get; set; }

    public double TotalCalories { get; set; }
    public int? UserId { get; set; }

    public ObservableCollection<LogExerciseItem> LogExerciseItems { get; set; }

    public ObservableCollection<LogFoodItem> LogFoodItems { get; set; } 

    public event PropertyChangedEventHandler PropertyChanged;
}
