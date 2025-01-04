using MacroTrackerUI.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.DialogView;

/// <summary>
/// A dialog for editing nutritional goals.
/// </summary>
public sealed partial class EditGoalDialog : ContentDialog
{
    public EditGoalDialog()
    {
        this.InitializeComponent();
        this.DefaultButton = ContentDialogButton.Primary;
        this.PrimaryButtonClick += EditGoalDialog_PrimaryButtonClick;
    }

    /// <summary>
    /// Handles the primary button click event to validate inputs.
    /// </summary>
    private void EditGoalDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        ResetErrorMessages();

        bool validInputs = ValidateInputs();

        if (!validInputs)
        {
            args.Cancel = true;
        }
    }

    /// <summary>
    /// Resets the visibility of error messages.
    /// </summary>
    private void ResetErrorMessages()
    {
        CaloriesErrorTextBlock.Visibility = Visibility.Collapsed;
        ProteinErrorTextBlock.Visibility = Visibility.Collapsed;
        CarbsErrorTextBlock.Visibility = Visibility.Collapsed;
        FatErrorTextBlock.Visibility = Visibility.Collapsed;
    }

    /// <summary>
    /// Validates the user inputs and displays error messages if necessary.
    /// </summary>
    /// <returns>True if all inputs are valid, otherwise false.</returns>
    private bool ValidateInputs()
    {
        bool validInputs = true;

        if (string.IsNullOrWhiteSpace(CaloriesTextBox.Text))
        {
            ShowErrorMessage(CaloriesErrorTextBlock, "Please enter calories.");
            validInputs = false;
        }
        else if (!int.TryParse(CaloriesTextBox.Text, out int calories) || calories <= 0)
        {
            ShowErrorMessage(CaloriesErrorTextBlock, "Calories must be a positive integer.");
            validInputs = false;
        }

        if (!string.IsNullOrWhiteSpace(ProteinTextBox.Text) && (!int.TryParse(ProteinTextBox.Text, out int protein) || protein <= 0))
        {
            ShowErrorMessage(ProteinErrorTextBlock, "Protein percentage must be a positive integer.");
            validInputs = false;
        }

        if (!string.IsNullOrWhiteSpace(CarbsTextBox.Text) && (!int.TryParse(CarbsTextBox.Text, out int carbs) || carbs <= 0))
        {
            ShowErrorMessage(CarbsErrorTextBlock, "Carbs percentage must be a positive integer.");
            validInputs = false;
        }

        if (!string.IsNullOrWhiteSpace(FatTextBox.Text) && (!int.TryParse(FatTextBox.Text, out int fat) || fat <= 0))
        {
            ShowErrorMessage(FatErrorTextBlock, "Fat percentage must be a positive integer.");
            validInputs = false;
        }

        return validInputs;
    }

    /// <summary>
    /// Displays an error message in the specified TextBlock.
    /// </summary>
    /// <param name="textBlock">The TextBlock to display the error message in.</param>
    /// <param name="message">The error message to display.</param>
    private void ShowErrorMessage(TextBlock textBlock, string message)
    {
        textBlock.Text = message;
        textBlock.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Retrieves the goal from the user inputs if there are no validation errors.
    /// </summary>
    /// <returns>A Goal object if inputs are valid, otherwise null.</returns>
    public Goal GetGoalFromInput()
    {
        if (AreInputsValid())
        {
            int calories = int.Parse(CaloriesTextBox.Text);
            int proteinPercentage = GetPercentage(ProteinTextBox.Text, 25);
            int carbsPercentage = GetPercentage(CarbsTextBox.Text, 50);
            int fatPercentage = GetPercentage(FatTextBox.Text, 25);

            return new Goal
            {
                Calories = calories,
                Protein = (int)(calories * proteinPercentage / 100 / 4),
                Fat = (int)(calories * fatPercentage / 100 / 9),
                Carbs = (int)(calories * carbsPercentage / 100 / 4)
            };
        }
        return null;
    }

    /// <summary>
    /// Checks if all input fields are valid.
    /// </summary>
    /// <returns>True if all inputs are valid, otherwise false.</returns>
    private bool AreInputsValid()
    {
        return CaloriesErrorTextBlock.Visibility == Visibility.Collapsed &&
               ProteinErrorTextBlock.Visibility == Visibility.Collapsed &&
               CarbsErrorTextBlock.Visibility == Visibility.Collapsed &&
               FatErrorTextBlock.Visibility == Visibility.Collapsed;
    }

    /// <summary>
    /// Gets the percentage value from the input or returns the default value if input is invalid.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="defaultValue">The default value to return if input is invalid.</param>
    /// <returns>The percentage value.</returns>
    private int GetPercentage(string input, int defaultValue)
    {
        return int.TryParse(input, out int value) ? value : defaultValue;
    }
}
