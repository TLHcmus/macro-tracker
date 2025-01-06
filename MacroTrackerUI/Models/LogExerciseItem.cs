using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

public class LogExerciseItem : INotifyPropertyChanged
{
    public int LogExerciseId { get; set; }

    public int ExerciseId { get; set; }

    public double Duration { get; set; }

    public double TotalCalories { get; set; }
    public Exercise Exercise { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
