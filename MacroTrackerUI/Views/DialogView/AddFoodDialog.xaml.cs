using MacroTrackerUI.Models;
using MacroTrackerUI.Views.PageView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.DialogView
{
    /// <summary>
    /// A dialog for adding a new food item.
    /// </summary>
    public sealed partial class AddFoodDialog : ContentDialog
    {
        public AddFoodDialog()
        {
            this.InitializeComponent();
            this.PrimaryButtonClick += AddFoodDialog_PrimaryButtonClick;
            this.DefaultButton = ContentDialogButton.Primary;
        }

        /// <summary>
        /// Handles the PrimaryButtonClick event of the AddFoodDialog control.
        /// Validates the input fields and displays error messages if necessary.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void AddFoodDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ResetErrorMessages();

            bool validInputs = ValidateInputs();

            if (!validInputs)
            {
                args.Cancel = true;
            }
        }

        /// <summary>
        /// Resets the visibility of all error messages to collapsed.
        /// </summary>
        private void ResetErrorMessages()
        {
            NameErrorTextBlock.Visibility = Visibility.Collapsed;
            CaloriesErrorTextBlock.Visibility = Visibility.Collapsed;
            ProteinErrorTextBlock.Visibility = Visibility.Collapsed;
            CarbsErrorTextBlock.Visibility = Visibility.Collapsed;
            FatErrorTextBlock.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Validates the input fields and displays error messages if necessary.
        /// </summary>
        /// <returns><c>true</c> if all inputs are valid; otherwise, <c>false</c>.</returns>
        private bool ValidateInputs()
        {
            bool validInputs = true;

            if (string.IsNullOrWhiteSpace(FoodNameTextBox.Text))
            {
                NameErrorTextBlock.Text = "Please enter name.";
                NameErrorTextBlock.Visibility = Visibility.Visible;
                validInputs = false;
            }
            if (string.IsNullOrWhiteSpace(CaloriesTextBox.Text) || !int.TryParse(CaloriesTextBox.Text, out int calories) || calories <= 0)
            {
                CaloriesErrorTextBlock.Text = "Calories must be a positive integer.";
                CaloriesErrorTextBlock.Visibility = Visibility.Visible;
                validInputs = false;
            }
            if (string.IsNullOrWhiteSpace(ProteinTextBox.Text) || !double.TryParse(ProteinTextBox.Text, out double protein) || protein < 0)
            {
                ProteinErrorTextBlock.Text = "Protein must be a non-negative number.";
                ProteinErrorTextBlock.Visibility = Visibility.Visible;
                validInputs = false;
            }
            if (string.IsNullOrWhiteSpace(CarbsTextBox.Text) || !double.TryParse(CarbsTextBox.Text, out double carbs) || carbs < 0)
            {
                CarbsErrorTextBlock.Text = "Carbs must be a non-negative number.";
                CarbsErrorTextBlock.Visibility = Visibility.Visible;
                validInputs = false;
            }
            if (string.IsNullOrWhiteSpace(FatTextBox.Text) || !double.TryParse(FatTextBox.Text, out double fat) || fat < 0)
            {
                FatErrorTextBlock.Text = "Fat must be a non-negative number.";
                FatErrorTextBlock.Visibility = Visibility.Visible;
                validInputs = false;
            }

            return validInputs;
        }

        /// <summary>
        /// Gets the food item from the input fields.
        /// </summary>
        /// <returns>A <see cref="Food"/> object if all inputs are valid; otherwise, <c>null</c>.</returns>
        public Food GetFoodFromInput()
        {
            if (NameErrorTextBlock.Visibility == Visibility.Collapsed &&
                CaloriesErrorTextBlock.Visibility == Visibility.Collapsed &&
                ProteinErrorTextBlock.Visibility == Visibility.Collapsed &&
                CarbsErrorTextBlock.Visibility == Visibility.Collapsed &&
                FatErrorTextBlock.Visibility == Visibility.Collapsed)
            {
                return new Food
                {
                    Name = FoodNameTextBox.Text,
                    CaloriesPer100g = double.Parse(CaloriesTextBox.Text),
                    ProteinPer100g = double.Parse(ProteinTextBox.Text),
                    CarbsPer100g = double.Parse(CarbsTextBox.Text),
                    FatPer100g = double.Parse(FatTextBox.Text)
                };
            }

            return null;
        }
    }
}
