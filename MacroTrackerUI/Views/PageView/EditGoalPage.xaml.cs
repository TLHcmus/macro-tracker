using MacroTrackerUI.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class EditGoalPage : Page
{
    /// <summary>
    /// Gets or sets the current goal being edited.
    /// </summary>
    public Goal CurrentGoal { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EditGoalPage"/> class.
    /// </summary>
    public EditGoalPage()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Handles the click event of the Confirm button.
    /// Updates the current goal with the input values and navigates to the goals page.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    public void ConfirmButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(CaloriesInput.Text, out int calories) &&
            int.TryParse(ProteinInput.Text, out int proteinPercentage) &&
            int.TryParse(FatInput.Text, out int fatPercentage) &&
            int.TryParse(CarbsInput.Text, out int carbsPercentage))
        {
            CurrentGoal.Calories = calories;
            CurrentGoal.Protein = CalculateMacronutrient(calories, proteinPercentage, 4);
            CurrentGoal.Fat = CalculateMacronutrient(calories, fatPercentage, 9);
            CurrentGoal.Carbs = CalculateMacronutrient(calories, carbsPercentage, 4);

            // Navigate to the goals page
            Frame.Navigate(typeof(GoalsPage), CurrentGoal);
        }
        else
        {
            // Handle invalid input
            DisplayInvalidInputMessage();
        }
    }

    /// <summary>
    /// Calculates the macronutrient amount based on calories and percentage.
    /// </summary>
    /// <param name="calories">The total calories.</param>
    /// <param name="percentage">The percentage of the macronutrient.</param>
    /// <param name="caloriesPerGram">The calories per gram of the macronutrient.</param>
    /// <returns>The calculated macronutrient amount in grams.</returns>
    private int CalculateMacronutrient(int calories, int percentage, int caloriesPerGram)
    {
        return (int)(calories * percentage / 100.0 / caloriesPerGram);
    }

    /// <summary>
    /// Displays a message indicating that the input is invalid.
    /// </summary>
    private void DisplayInvalidInputMessage()
    {
        // Implementation for displaying an invalid input message
    }

    /// <summary>
    /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
    /// </summary>
    /// <param name="e">Event data that can be examined by overriding code.</param>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is Goal goal)
        {
            CurrentGoal = goal;
        }
    }
}
