using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using PropertyChanged;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MacroTrackerUI.ViewModels;

public class GoalsViewModel : INotifyPropertyChanged
{
    public Goal CurrentGoal { get; set; }


    private DaoSender Sender { get; } =
        ProviderUI.GetServiceProvider().GetService<DaoSender>();

    public GoalsViewModel()
    {
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