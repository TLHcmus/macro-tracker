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
    
}
