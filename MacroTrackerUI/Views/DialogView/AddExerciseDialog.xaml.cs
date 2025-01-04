using MacroTrackerUI.Models;
using MacroTrackerUI.Views.PageView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.DialogView
{
    /// <summary>
    /// A dialog for adding a new exercise.
    /// </summary>
    public sealed partial class AddExerciseDialog : ContentDialog
    {
        public AddExerciseDialog()
        {
            InitializeComponent();
            PrimaryButtonClick += AddExerciseDialog_PrimaryButtonClick;
            DefaultButton = ContentDialogButton.Primary;
        }

        /// <summary>
        /// Handles the primary button click event to validate input and add the exercise.
        /// </summary>
        /// <param name="sender">The content dialog.</param>
        /// <param name="args">The event arguments.</param>
        private void AddExerciseDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
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
            NameErrorTextBlock.Visibility = Visibility.Collapsed;
            CaloriesErrorTextBlock.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Validates the user inputs for exercise name and calories.
        /// </summary>
        /// <returns>True if inputs are valid, otherwise false.</returns>
        private bool ValidateInputs()
        {
            bool validInputs = true;

            if (string.IsNullOrWhiteSpace(ExerciseNameTextBox.Text))
            {
                NameErrorTextBlock.Text = "Please enter name.";
                NameErrorTextBlock.Visibility = Visibility.Visible;
                validInputs = false;
            }

            if (string.IsNullOrWhiteSpace(CaloriesTextBox.Text))
            {
                CaloriesErrorTextBlock.Text = "Please enter calories.";
                CaloriesErrorTextBlock.Visibility = Visibility.Visible;
                validInputs = false;
            }
            else if (!int.TryParse(CaloriesTextBox.Text, out int calories) || calories <= 0)
            {
                CaloriesErrorTextBlock.Text = "Calories must be a positive integer.";
                CaloriesErrorTextBlock.Visibility = Visibility.Visible;
                validInputs = false;
            }

            return validInputs;
        }

        /// <summary>
        /// Gets the exercise from the user input if there are no validation errors.
        /// </summary>
        /// <returns>An Exercise object if inputs are valid, otherwise null.</returns>
        public Exercise GetExerciseFromInput()
        {
            if (NameErrorTextBlock.Visibility == Visibility.Collapsed &&
                CaloriesErrorTextBlock.Visibility == Visibility.Collapsed)
            {
                return new Exercise
                {
                    Name = ExerciseNameTextBox.Text,
                    CaloriesPerMinute = int.Parse(CaloriesTextBox.Text)
                };
            }
            return null;
        }
    }
}
