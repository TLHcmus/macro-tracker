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
    public GoalsViewModel() : this(ProviderUI.GetServiceProvider())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GoalsViewModel"/> class with a specified service provider.
    /// </summary>
    /// <param name="provider">The service provider to use.</param>
    public GoalsViewModel(IServiceProvider provider)
    {
        Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        Sender = Provider.GetService<IDaoSender>() ?? throw new InvalidOperationException("IDaoSender service not found.");
        CurrentGoal = Sender.GetGoal();
    }

    /// <summary>
    /// Updates the current goal.
    /// </summary>
    /// <param name="goal">The new goal to set.</param>
    public void UpdateGoal(Goal goal)
    {
        if (goal == null) throw new ArgumentNullException(nameof(goal));

        Sender.UpdateGoal(goal);
        CurrentGoal = goal;
        Debug.WriteLine($"CurrentGoal is changed, Calories: {CurrentGoal.Calories}");
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
