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
            // Xóa bài tập nếu tìm thấy
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


    public event PropertyChangedEventHandler PropertyChanged;
}
