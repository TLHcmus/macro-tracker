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
        // Neu nguoi dung chua dat muc tieu
        if (CurrentGoal == null)
        {
            // Gia tri mac dinh
            CurrentGoal = new Goal
            {
                Calories = 0,
                Protein = 0,
                Carbs = 0,
                Fat = 0
            };
        }
    }

    public void UpdateGoal(Goal goal)
    {
        Sender.UpdateGoal(goal);

        CurrentGoal = goal;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}