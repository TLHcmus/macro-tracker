using MacroTrackerUI.Helpers;
using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using MacroTrackerUI.Views.PageView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
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
public sealed partial class AddExerciseDialog : ContentDialog
{
    private string ImageFilePath { get; set; }
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

    public async Task<Exercise> GetExerciseFromInput()
    {
        // Chỉ trả về Exercise nếu không có lỗi
        if (NameErrorTextBlock.Visibility == Visibility.Collapsed ||
            CaloriesErrorTextBlock.Visibility == Visibility.Collapsed)
        {
            var exercise =  new Exercise
            {
                Name = ExerciseNameTextBox.Text,
                CaloriesPerMinute = int.Parse(CaloriesTextBox.Text)
            };
            if (!string.IsNullOrEmpty(ImageFilePath))
            {
                exercise.Image = await ImageHelper.ReadFileToByteArrayAsync(ImageFilePath);
            }
            return exercise;
        }
        return null;
    }

    private async void PickExerciseImageButton_Click(object sender, RoutedEventArgs e)
    {
        //disable the button to avoid double-clicking
        var senderButton = sender as Button;
        senderButton.IsEnabled = false;

        // Clear previous returned file name, if it exists, between iterations of this scenario
        PickExerciseImageOutputTextBlock.Text = "";
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
            PickExerciseImageOutputTextBlock.Text = "Picked image: ";
            FilenameTextBlock.Text = file.Name;

            ImageFilePath = file.Path;
        }
        else
        {
            PickExerciseImageOutputTextBlock.Text = "Operation cancelled.";

            ImageFilePath = "";
        }

        //re-enable the button
        senderButton.IsEnabled = true;
    }
}
