using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for managing exercises.
/// </summary>
public class ExerciseViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the collection of exercises.
    /// </summary>
    public ObservableCollection<Exercise> Exercises { get; set; }

    private DaoSender Sender { get; } =
        ProviderUI.GetServiceProvider().GetService<DaoSender>();

    /// <summary>
    /// Initializes a new instance of the <see cref="ExerciseViewModel"/> class.
    /// </summary>
    public ExerciseViewModel()
    {
        Exercises = new ObservableCollection<Exercise>(Sender.GetExercises());
    }
    public void AddExercise(Exercise exercise)
    {
        Sender.AddExercise(exercise);
        Exercises.Add(exercise);
    }

    public void RemoveExercise(string exerciseName)
    {
        Sender.RemoveExercise(exerciseName);

        var exerciseToRemove = Exercises.FirstOrDefault(exercise => exercise.Name.Equals(exerciseName, StringComparison.OrdinalIgnoreCase));

        if (exerciseToRemove != null)
        {
            // Xóa bài tập nếu tìm thấy
            Exercises.Remove(exerciseToRemove);
        }
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}
