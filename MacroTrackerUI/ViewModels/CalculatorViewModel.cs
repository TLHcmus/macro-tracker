﻿using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System.Reflection;

namespace MacroTrackerUI.ViewModels;

public partial class CalculatorViewModel
{
    private DaoSender Sender { get; } =
        ProviderUI.GetServiceProvider().GetService<DaoSender>();

    // Health Info
    public int Age
    {
        get; set;
    }
    public int Weight
    {
        get; set;
    }
    public int Height
    {
        get; set;
    }
    public string ActivityLevel
    {
        get; set;
    }
    public string Gender { get; set; }

    // TDEE Calculation
    public double CalculateTDEE()
    {
            double tdee = 0;
            double bmr = 0;

            if (Gender == "Male")
            {
                bmr = 10 * Weight + 6.25 * Height - 5 * Age + 5;
            }
            else 
            {
                bmr = 10 * Weight + 6.25 * Height - 5 * Age - 161;
            }

            double activityMultiplier = 1.2; // Default value for sedentary
            switch (ActivityLevel)
            {
                case "Lightly Active":
                    activityMultiplier = 1.375;
                    break;
                case "Moderately Active":
                    activityMultiplier = 1.55;
                    break;
                case "Very Active":
                    activityMultiplier = 1.725;
                    break;
                case "Super Active":
                    activityMultiplier = 1.9;
                    break;
            }

            tdee = bmr * activityMultiplier;
            return tdee;
    }

    // Update Goal
    public void UpdateGoal(Goal goal)
    {
        Sender.UpdateGoal(goal);
    }
}
