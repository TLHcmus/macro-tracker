using MacroTrackerUI.Helpers;
using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage.Pickers;



// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.DialogView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class AddFoodDialog : ContentDialog
{
    private FoodViewModel ViewModel { get; set; }
    private string ImageFilePath { get; set; }
    public AddFoodDialog()
    {
        this.InitializeComponent();
        this.PrimaryButtonClick += AddFoodDialog_PrimaryButtonClick;
        this.DefaultButton = ContentDialogButton.Primary;

        ViewModel = new FoodViewModel();
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
        // Check for duplicate name
        if (ViewModel.Foods.Any(food => food.Name == FoodNameTextBox.Text))
        {
            NameErrorTextBlock.Text = $"{FoodNameTextBox.Text} already exists.";
            NameErrorTextBlock.Visibility = Visibility.Visible;

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

    public async Task<Food> GetFoodFromInput()
    {
        // Chỉ trả về Food nếu không có lỗi
        if (NameErrorTextBlock.Visibility == Visibility.Collapsed ||
            CaloriesErrorTextBlock.Visibility == Visibility.Collapsed ||
            ProteinErrorTextBlock.Visibility == Visibility.Collapsed ||
            CarbsErrorTextBlock.Visibility == Visibility.Collapsed ||
            FatErrorTextBlock.Visibility == Visibility.Collapsed)
        {
            Food food = new Food
            {
                Name = FoodNameTextBox.Text,
                CaloriesPer100g = double.Parse(CaloriesTextBox.Text),
                ProteinPer100g = double.Parse(ProteinTextBox.Text),
                CarbsPer100g = double.Parse(CarbsTextBox.Text),
                FatPer100g = double.Parse(FatTextBox.Text)
            };
            if (!string.IsNullOrEmpty(ImageFilePath))
            {
                food.Image = await ImageHelper.ReadFileToByteArrayAsync(ImageFilePath);
            }

            return food;
        }

        return null;
    }

    private async void PickFoodImageButton_Click(object sender, RoutedEventArgs e)
    {
        //disable the button to avoid double-clicking
        var senderButton = sender as Button;
        senderButton.IsEnabled = false;

        // Clear previous returned file name, if it exists, between iterations of this scenario
        PickFoodImageOutputTextBlock.Text = "";
        FilenameTextBlock.Text = "";

        // Create a file picker
        var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

        // Initialize with the current window handle
        var window = App.m_window;

        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

        // Set options for your file picker
        openPicker.ViewMode = PickerViewMode.Thumbnail;
        openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

        openPicker.FileTypeFilter.Add(".jpg");
        openPicker.FileTypeFilter.Add(".jpeg");
        openPicker.FileTypeFilter.Add(".png");

        // Open the picker for the user to pick a file
        var file = await openPicker.PickSingleFileAsync();
        if (file != null)
        {
            PickFoodImageOutputTextBlock.Text = "Picked image: ";
            FilenameTextBlock.Text = file.Name;

            ImageFilePath = file.Path;
        }
        else
        {
            PickFoodImageOutputTextBlock.Text = "Operation cancelled.";

            ImageFilePath = "";
        }

        //re-enable the button
        senderButton.IsEnabled = true;
    }
}
