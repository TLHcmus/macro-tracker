using System;
using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Log : INotifyPropertyChanged
{
    public int Id { get; set; }
    public DateTime Time { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
