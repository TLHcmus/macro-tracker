using System;
using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Log : INotifyPropertyChanged
{
    public int ID { get; set; }
    public DateTime Time { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
