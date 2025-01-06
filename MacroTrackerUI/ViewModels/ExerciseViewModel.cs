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
        var exerciseId = Sender.AddExercise(exercise);

        // Cap nhat exercise id cua bai tap vua them
        exercise.ExerciseId = exerciseId;
        Exercises.Add(exercise);
    }

    public void RemoveExercise(int exerciseId)
    {
        Sender.RemoveExercise(exerciseId);

        var exerciseToRemove = Exercises.FirstOrDefault(exercise => exercise.ExerciseId == exerciseId);

        if (exerciseToRemove != null)
        {
            Exercises.Remove(exerciseToRemove);
        }
    }
    // Get log by date
    public Log GetLogByDate(DateOnly date)
    {
        return Sender.GetLogByDate(date);
    }
    // Update log
    public void UpdateLog(Log log)
    {
        Sender.UpdateLog(log);
    }


    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}
