using MacroTrackerUI.Models;
using System;
using System.ComponentModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

public class LogItemViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public float TotalCalories { get; set; }

    public float UpdateTotalCalories(Log logDate)
    {
        float updatedCalories = (float)Math.Round(logDate.LogFoodItems.Sum(food => food.TotalCalories) +
                                                  logDate.LogExerciseItems.Sum(exercise => exercise.TotalCalories), 1);
        TotalCalories = updatedCalories;

        return updatedCalories;
    }
}
