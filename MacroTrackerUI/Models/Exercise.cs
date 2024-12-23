﻿using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Exercise : Item, INotifyPropertyChanged
{
    public double? CaloriesPerMinute { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
