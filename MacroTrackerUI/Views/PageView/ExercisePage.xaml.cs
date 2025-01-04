using MacroTrackerUI.Models;
using MacroTrackerUI.Services.PathService;
using MacroTrackerUI.ViewModels;
using MacroTrackerUI.Views.DialogView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ExercisePage : Page
{
    /// <summary>
    /// Gets or sets the ViewModel for managing exercises.
    /// </summary>
    private ExerciseViewModel ViewModel { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExercisePage"/> class.
    /// </summary>
    public ExercisePage()
    {
        this.InitializeComponent();
        ChatBot.ChatBotConversation = App.ChatBotConversation;
        ViewModel = new ExerciseViewModel();
    }

    /// <summary>
    /// Handles the selection changed event for the exercise list.
    /// </summary>
    private void ExerciseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedExercise = (Exercise)ExerciseList.SelectedItem;

        if (selectedExercise != null)
        {
            UpdateExerciseDetails(selectedExercise);
        }
    }

    /// <summary>
    /// Updates the exercise details UI with the selected exercise.
    /// </summary>
    private void UpdateExerciseDetails(Exercise selectedExercise)
    {
        ExerciseImage.Source = new BitmapImage(new Uri($"ms-appx:///Assets/ExerciseIcons/{selectedExercise.IconFileName}"));
        ExerciseDetail.Visibility = Visibility.Visible;
        NoExerciseSelectedMessage.Visibility = Visibility.Collapsed;
        ExerciseName.Text = selectedExercise.Name;
        DurationInput.Text = "";
        Calories.Text = "0";

        DurationInput.TextChanged += (s, e) =>
        {
            if (double.TryParse(DurationInput.Text, out double duration))
            {
                var caloriesBurned = selectedExercise.GetCaloriesBurned(duration);
                Calories.Text = ((int)caloriesBurned).ToString();
            }
            else
            {
                Calories.Text = "0";
            }
        };
    }

    /// <summary>
    /// Handles the text changed event for the search bar.
    /// </summary>
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = SearchBar.Text.ToLower();
        ExerciseList.ItemsSource = string.IsNullOrWhiteSpace(searchText)
            ? ViewModel.Exercises
            : ViewModel.Exercises.Where(exercise => exercise.Name.ToLower().Contains(searchText)).ToList();
    }

    /// <summary>
    /// Handles the click event for the add exercise button.
    /// </summary>
    private async void AddExerciseButton_Click(object sender, RoutedEventArgs e)
    {
        var addExerciseDialog = new AddExerciseDialog { XamlRoot = this.XamlRoot };
        var result = await addExerciseDialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            var exercise = addExerciseDialog.GetExerciseFromInput();
            if (exercise != null)
            {
                ViewModel.AddExercise(exercise);
            }
        }
    }

    /// <summary>
    /// Handles the click event for the remove exercise menu item.
    /// </summary>
    private async void ContactRemoveMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var menuFlyoutItem = sender as MenuFlyoutItem;
        var exerciseToDelete = menuFlyoutItem?.DataContext as Exercise;

        if (exerciseToDelete == null)
        {
            return;
        }

        var selectedExercise = (Exercise)ExerciseList.SelectedItem;
        var confirmDialog = new ContentDialog
        {
            Title = "Confirm Removal",
            Content = $"Are you sure you want to remove {exerciseToDelete.Name}?",
            PrimaryButtonText = "Yes",
            CloseButtonText = "No",
            XamlRoot = this.XamlRoot,
            DefaultButton = ContentDialogButton.Close
        };

        var result = await confirmDialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            if (selectedExercise != null && selectedExercise.Name == exerciseToDelete.Name)
            {
                ExerciseDetail.Visibility = Visibility.Collapsed;
                NoExerciseSelectedMessage.Visibility = Visibility.Visible;
            }

            ViewModel.RemoveExercise(exerciseToDelete.Name);
        }
    }

    /// <summary>
    /// Handles the click event for the log exercise button.
    /// </summary>
    private void LogExerciseButton_Click(object sender, RoutedEventArgs e)
    {
        // Implementation for logging exercise
    }

    /// <summary>
    /// Handles the image failed event for the exercise image.
    /// </summary>
    private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
    {
        string exerciseIconDir = AssetsPathRegistry.RegisteredAssetsPath["ExerciseIcons"];
        (sender as Image).Source = new BitmapImage(new Uri($"{exerciseIconDir}/default.png"));
    }
}
