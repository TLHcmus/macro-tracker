using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for log items, providing functionality to update total calories.
/// </summary>
public class LogItemViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Event triggered when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets the data access sender service.
    /// </summary>
    private IDaoSender Sender { get; }
    public IServiceProvider Provider { get; }

    public LogItemViewModel()
    {
        Provider = ProviderUI.GetServiceProvider();
        Sender = Provider.GetService<IDaoSender>();
    }

    public LogItemViewModel(IServiceProvider provider)
    {
        Provider = provider;
        Sender = Provider.GetService<IDaoSender>();
    }

    /// <summary>
    /// Updates the total calories for a given log by summing the calories from food and exercise items.
    /// </summary>
    /// <param name="log">The log to update.</param>
    public void UpdateTotalCalories(Log log)
    {
        double updatedCalories = (float)Math.Round(log.LogFoodItems.Sum(food => food.TotalCalories) +
                                                  log.LogExerciseItems.Sum(exercise => exercise.TotalCalories), 1);
        Sender.UpdateTotalCalories(log.LogId, updatedCalories);
        log.TotalCalories = updatedCalories;
    }
}
