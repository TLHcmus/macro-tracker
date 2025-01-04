using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for managing goals.
/// </summary>
public class GoalsViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the current goal.
    /// </summary>
    public Goal CurrentGoal { get; set; }

    private IServiceProvider Provider { get; } 

    private IDaoSender Sender { get; } 

    /// <summary>
    /// Initializes a new instance of the <see cref="GoalsViewModel"/> class.
    /// </summary>
    public GoalsViewModel()
    {
        Provider = ProviderUI.GetServiceProvider();
        Sender = Provider.GetService<IDaoSender>();
        CurrentGoal = Sender.GetGoal();
    }

    public GoalsViewModel(IServiceProvider provider)
    {
        Provider = provider;
        Sender = Provider.GetService<IDaoSender>();
        CurrentGoal = Sender.GetGoal();
    }

    public void UpdateGoal(Goal goal)
    {
        Sender.UpdateGoal(goal);

        CurrentGoal = goal;
        Debug.WriteLine($"CurrentGoal is changed, Calories: {CurrentGoal.Calories}");
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
