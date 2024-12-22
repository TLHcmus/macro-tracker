using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

public class LogItemViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private DaoSender Sender { get; } = ProviderUI.GetServiceProvider().GetRequiredService<DaoSender>();

    public void UpdateTotalCalories(Log log)
    {
        double updatedCalories = (float)Math.Round(log.LogFoodItems.Sum(food => food.TotalCalories) +
                                                  log.LogExerciseItems.Sum(exercise => exercise.TotalCalories), 1);
        Sender.UpdateTotalCalories(log.LogId, updatedCalories);
        log.TotalCalories = updatedCalories;
    }
}
