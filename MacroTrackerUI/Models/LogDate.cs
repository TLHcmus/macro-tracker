using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class LogDate : INotifyPropertyChanged
{
    public DateTime Date { get; set; }
    public ObservableCollection<LogExercise> LogExercise { get; set; }
    public ObservableCollection<LogFood> LogFood { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
