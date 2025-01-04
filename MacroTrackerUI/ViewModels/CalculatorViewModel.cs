using MacroTrackerUI.Models;
using Microsoft.UI.Xaml.Controls;
using System.Reflection;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for performing health-related calculations.
/// </summary>
public partial class CalculatorViewModel
{
    // Health Info
    public int Age { get; set; }
    public int Weight { get; set; }
    public int Height { get; set; }
    public string ActivityLevel { get; set; }
    public string Gender { get; set; }

    /// <summary>
    /// Calculates the Total Daily Energy Expenditure (TDEE) based on the individual's health information.
    /// </summary>
    /// <returns>The calculated TDEE.</returns>
    public double CalculateTDEE()
    {
        double bmr = CalculateBMR();
        double activityMultiplier = GetActivityMultiplier();
        return bmr * activityMultiplier;
    }

    /// <summary>
    /// Calculates the Basal Metabolic Rate (BMR) based on the individual's health information.
    /// </summary>
    /// <returns>The calculated BMR.</returns>
    private double CalculateBMR()
    {
        if (Gender == "Male")
        {
            return 10 * Weight + 6.25 * Height - 5 * Age + 5;
        }
        else
        {
            return 10 * Weight + 6.25 * Height - 5 * Age - 161;
        }
    }

    /// <summary>
    /// Gets the activity multiplier based on the individual's activity level.
    /// </summary>
    /// <returns>The activity multiplier.</returns>
    private double GetActivityMultiplier()
    {
        return ActivityLevel switch
        {
            "Lightly Active" => 1.375,
            "Moderately Active" => 1.55,
            "Very Active" => 1.725,
            "Super Active" => 1.9,
            _ => 1.2, // Default value for sedentary
        };
    }
}
