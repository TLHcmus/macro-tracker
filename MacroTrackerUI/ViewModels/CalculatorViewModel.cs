using MacroTrackerUI.Models;
using Microsoft.UI.Xaml.Controls;
using System.Reflection;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for performing health-related calculations.
/// </summary>
public partial class CalculatorViewModel
{
    /// <summary>
    /// Gets or sets the health information of the individual.
    /// </summary>
    public HealthInfo HealthInfo { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorViewModel"/> class.
    /// </summary>
    public CalculatorViewModel()
    {
        HealthInfo = new HealthInfo();
    }

    /// <summary>
    /// Calculates the Total Daily Energy Expenditure (TDEE) based on the individual's health information.
    /// </summary>
    /// <returns>The calculated TDEE.</returns>
    public double CalculateTDEE()
    {
        double tdee = 0;
        double bmr = 0;

        if (HealthInfo.Gender == "Male")
        {
            bmr = 10 * HealthInfo.Weight + 6.25 * HealthInfo.Height - 5 * HealthInfo.Age + 5;
        }
        else
        {
            bmr = 10 * HealthInfo.Weight + 6.25 * HealthInfo.Height - 5 * HealthInfo.Age - 161;
        }

        double activityMultiplier = 1.2; // Default value for sedentary
        switch (HealthInfo.ActivityLevel)
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
