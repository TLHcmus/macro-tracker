using MacroTrackerUI.Models;
using System.ComponentModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

public class LogDateItemViewModel : INotifyPropertyChanged
{
    public float TotalCalories { get; set; } = 0;

    public event PropertyChangedEventHandler PropertyChanged;

    public float UpdateTotalCalories(LogDate logDate)
    {
        TotalCalories = logDate.LogFood.Sum(food => food.Calories) + 
                        logDate.LogExercise.Sum(exercise => exercise.Calories);

        return TotalCalories;
    }
}
