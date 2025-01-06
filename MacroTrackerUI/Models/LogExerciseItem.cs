using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

public class LogExerciseItem : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the unique identifier for the log exercise item.
    /// </summary>
    public int LogExerciseId { get; set; }

    public int ExerciseId { get; set; }

    /// <summary>
    /// Gets or sets the duration of the exercise in minutes.
    /// </summary>
    public double Duration { get; set; }

    /// <summary>
    /// Gets or sets the total calories burned during the exercise.
    /// </summary>
    public double TotalCalories { get; set; }
    public Exercise Exercise { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
