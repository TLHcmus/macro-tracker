using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

public class ExerciseViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Exercise> Exercises { get; set; }

    private DaoSender Sender { get; } =
        ProviderUI.GetServiceProvider().GetService<DaoSender>();

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

    public event PropertyChangedEventHandler PropertyChanged;
}
