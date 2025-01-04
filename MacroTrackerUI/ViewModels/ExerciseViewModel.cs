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

    private IServiceProvider Provider { get; }
    private IDaoSender Sender { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExerciseViewModel"/> class.
    /// </summary>
    public ExerciseViewModel()
        : this(ProviderUI.GetServiceProvider())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExerciseViewModel"/> class with a specified service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    public ExerciseViewModel(IServiceProvider provider)
    {
        Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        Sender = Provider.GetService<IDaoSender>() ?? throw new InvalidOperationException("IDaoSender service not found.");
        Exercises = new ObservableCollection<Exercise>(Sender.GetExercises());
    }

    /// <summary>
    /// Adds a new exercise to the collection and data source.
    /// </summary>
    /// <param name="exercise">The exercise to add.</param>
    public void AddExercise(Exercise exercise)
    {
        if (exercise == null) throw new ArgumentNullException(nameof(exercise));
        Sender.AddExercise(exercise);
        Exercises.Add(exercise);
    }

    /// <summary>
    /// Removes an exercise from the collection and data source by name.
    /// </summary>
    /// <param name="exerciseName">The name of the exercise to remove.</param>
    public void RemoveExercise(string exerciseName)
    {
        if (string.IsNullOrWhiteSpace(exerciseName)) throw new ArgumentException("Exercise name cannot be null or whitespace.", nameof(exerciseName));
        Sender.RemoveExercise(exerciseName);

        var exerciseToRemove = Exercises.FirstOrDefault(exercise => exercise.Name.Equals(exerciseName, StringComparison.OrdinalIgnoreCase));
        if (exerciseToRemove != null)
        {
            Exercises.Remove(exerciseToRemove);
        }
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}
