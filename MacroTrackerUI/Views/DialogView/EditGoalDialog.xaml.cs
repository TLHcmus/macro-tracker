using MacroTrackerUI.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.DialogView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class EditGoalDialog : ContentDialog
{
    public EditGoalDialog()
    {
        this.InitializeComponent();
        this.DefaultButton = ContentDialogButton.Primary; 
        this.PrimaryButtonClick += EditGoalDialog_PrimaryButtonClick;
    }

    public void EditGoalDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        CaloriesErrorTextBlock.Visibility = Visibility.Collapsed;
        ProteinErrorTextBlock.Visibility = Visibility.Collapsed;
        CarbsErrorTextBlock.Visibility = Visibility.Collapsed;
        FatErrorTextBlock.Visibility = Visibility.Collapsed;

        bool validInputs = true;

        // Check for missing calories input
        if (string.IsNullOrWhiteSpace(CaloriesTextBox.Text))
        {
            CaloriesErrorTextBlock.Text = "Please enter calories.";
            CaloriesErrorTextBlock.Visibility = Visibility.Visible;

            validInputs = false;
        }
        // Check for invalid inputs
        if (!string.IsNullOrWhiteSpace(CaloriesTextBox.Text) && (!int.TryParse(CaloriesTextBox.Text, out int calories) || calories <= 0))
        {
            CaloriesErrorTextBlock.Text = "Calories must be a positive integer.";
            CaloriesErrorTextBlock.Visibility = Visibility.Visible;

            validInputs = false;
        }
        if (!string.IsNullOrWhiteSpace(ProteinTextBox.Text) && (!int.TryParse(ProteinTextBox.Text, out int protein) || protein <= 0))
        {
            ProteinErrorTextBlock.Text = "Protein percentage must be a positive integer.";
            ProteinErrorTextBlock.Visibility = Visibility.Visible;

            validInputs = false;
        }
        if (!string.IsNullOrWhiteSpace(CarbsTextBox.Text) && (!int.TryParse(CarbsTextBox.Text, out int carbs) || carbs <= 0))
        {
            CarbsErrorTextBlock.Text = "Carbs percentage must be a positive integer.";
            CarbsErrorTextBlock.Visibility = Visibility.Visible;

            validInputs = false;
        }
        if (!string.IsNullOrEmpty(FatTextBox.Text) && (!int.TryParse(FatTextBox.Text, out int fat) || fat <= 0))
        {
            FatErrorTextBlock.Text = "Fat percentage must be a positive integer.";
            FatErrorTextBlock.Visibility = Visibility.Visible;

            validInputs = false;
        }
        // Neu input khong hop le
        if (!validInputs)
        {
            // Huy hanh dong dong dialog
            args.Cancel = true;
        }
    }


    public Goal GetGoalFromInput()
    {
        // Chỉ trả về Goal nếu không có lỗi
        if (CaloriesErrorTextBlock.Visibility == Visibility.Collapsed && 
            ProteinErrorTextBlock.Visibility == Visibility.Collapsed &&
            CarbsErrorTextBlock.Visibility == Visibility.Collapsed &&
            FatErrorTextBlock.Visibility == Visibility.Collapsed )
        {
            var calories = int.Parse(CaloriesTextBox.Text);
            // Gia tri phan tram macro mac dinh
            int defaulProteinPercentage = 25; // 25%
            int defaulCarbsPercentage = 50;
            int defaulFatPercentage = 25;


            var proteinPercentage = int.TryParse(ProteinTextBox.Text, out var value) ? value : defaulProteinPercentage ;
            var carbsPercentage = int.TryParse(CarbsTextBox.Text, out value) ? value : defaulCarbsPercentage;
            var fatPercentage = int.TryParse(FatTextBox.Text, out value) ? value : defaulFatPercentage;

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
}
