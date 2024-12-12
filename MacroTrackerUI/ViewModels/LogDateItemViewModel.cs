using MacroTrackerUI.Models;
using System;
using System.ComponentModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

public class LogDateItemViewModel : INotifyPropertyChanged
{
    public float TotalCalories { get; set; } = 0;

    public event PropertyChangedEventHandler PropertyChanged;

    public float UpdateTotalCalories(LogDate logDate)
    {
        float updatedCalories = (float)Math.Round(logDate.LogFood.Sum(food => food.Calories) +
                                                  logDate.LogExercise.Sum(exercise => exercise.Calories), 1);
        TotalCalories = updatedCalories;

        return updatedCalories;
    }
}
