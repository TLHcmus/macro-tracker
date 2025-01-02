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
    public sealed partial class AddFoodDialog : ContentDialog
    {
        public AddFoodDialog()
        {
            this.InitializeComponent();
            this.PrimaryButtonClick += AddFoodDialog_PrimaryButtonClick;
            this.DefaultButton = ContentDialogButton.Primary;
        }

        public void AddFoodDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            NameErrorTextBlock.Visibility = Visibility.Collapsed;
            CaloriesErrorTextBlock.Visibility = Visibility.Collapsed;
            ProteinErrorTextBlock.Visibility = Visibility.Collapsed;
            CarbsErrorTextBlock.Visibility = Visibility.Collapsed;
            FatErrorTextBlock.Visibility = Visibility.Collapsed;

            bool validInputs = true;

            // Check for missing input
            if (string.IsNullOrWhiteSpace(FoodNameTextBox.Text))
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
            if (string.IsNullOrWhiteSpace(ProteinTextBox.Text))
            {
                ProteinErrorTextBlock.Text = "Please enter protein.";
                ProteinErrorTextBlock.Visibility = Visibility.Visible;

                validInputs = false;
            }
            if (string.IsNullOrWhiteSpace(CarbsTextBox.Text))
            {
                CarbsErrorTextBlock.Text = "Please enter carbs.";
                CarbsErrorTextBlock.Visibility = Visibility.Visible;

                validInputs = false;
            }
            if (string.IsNullOrWhiteSpace(FatTextBox.Text))
            {
                FatErrorTextBlock.Text = "Please enter fat.";
                FatErrorTextBlock.Visibility = Visibility.Visible;

                validInputs = false;
            }

            // Check for invalid input
            if ((!int.TryParse(CaloriesTextBox.Text, out int calories) || calories <= 0) && CaloriesErrorTextBlock.Visibility == Visibility.Collapsed)
            {
                CaloriesErrorTextBlock.Text = "Calories must be a positive integer.";
                CaloriesErrorTextBlock.Visibility = Visibility.Visible;

                validInputs = false;
            }
            if ((!double.TryParse(ProteinTextBox.Text, out double protein) || protein < 0) && ProteinErrorTextBlock.Visibility == Visibility.Collapsed)
            {
                ProteinErrorTextBlock.Text = "Protein must be a non-negative number.";
                ProteinErrorTextBlock.Visibility = Visibility.Visible;

                validInputs = false;
            }
            if ((!double.TryParse(CarbsTextBox.Text, out double carbs) || carbs < 0) && CarbsErrorTextBlock.Visibility == Visibility.Collapsed)
            {
                CarbsErrorTextBlock.Text = "Carbs must be a non-negative number.";
                CarbsErrorTextBlock.Visibility = Visibility.Visible;

                validInputs = false;
            }
            if ((!double.TryParse(FatTextBox.Text, out double fat) || fat < 0) && FatErrorTextBlock.Visibility == Visibility.Collapsed)
            {
                FatErrorTextBlock.Text = "Fat must be a non-negative number.";
                FatErrorTextBlock.Visibility = Visibility.Visible;

                validInputs = false;
            }
            // Huy hanh dong dong dialog neu co loi nhap lieu
            if (!validInputs)
            {
                args.Cancel = true;
            }
        }

        public Food GetFoodFromInput()
        {
            // Chỉ trả về Food nếu không có lỗi
            if (NameErrorTextBlock.Visibility == Visibility.Collapsed ||
                CaloriesErrorTextBlock.Visibility == Visibility.Collapsed ||
                ProteinErrorTextBlock.Visibility == Visibility.Collapsed ||
                CarbsErrorTextBlock.Visibility == Visibility.Collapsed ||
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
