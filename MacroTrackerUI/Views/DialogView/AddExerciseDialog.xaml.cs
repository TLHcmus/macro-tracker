using MacroTrackerUI.Models;
using MacroTrackerUI.Views.PageView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.DialogView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddExerciseDialog : ContentDialog
    {
        public AddExerciseDialog()
        {
            this.InitializeComponent();
            this.PrimaryButtonClick += AddExerciseDialog_PrimaryButtonClick;
            this.DefaultButton = ContentDialogButton.Primary;
        }

        public void AddExerciseDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            NameErrorTextBlock.Visibility = Visibility.Collapsed;
            CaloriesErrorTextBlock.Visibility = Visibility.Collapsed;

            bool validInputs = true;

            // Check for missing input
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
            // Check for invalid input
            if ((!int.TryParse(CaloriesTextBox.Text, out int calories) || calories <= 0) && CaloriesErrorTextBlock.Visibility == Visibility.Collapsed)
            {
                CaloriesErrorTextBlock.Text = "Calories must be a positive integer.";
                CaloriesErrorTextBlock.Visibility = Visibility.Visible;

                validInputs = false;
            }

            // Huy hanh dong dong dialog neu co loi nhap lieu
            if (!validInputs)
            {
                args.Cancel = true;
            }
        }

        public Exercise GetExerciseFromInput()
        {
            // Chỉ trả về Exercise nếu không có lỗi
            if (NameErrorTextBlock.Visibility == Visibility.Collapsed ||
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
