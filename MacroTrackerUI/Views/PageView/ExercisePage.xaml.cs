using MacroTrackerUI.Helpers;
using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using MacroTrackerUI.Views.DialogView;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using Windows.Media.Devices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ExercisePage : Page
{
    private ExerciseViewModel ViewModel { get; set; }
    public ExercisePage()
    {
        this.InitializeComponent();
        ChatBot.ChatBotConversation = App.ChatBotConversation;
        ViewModel = new ExerciseViewModel();
    }

    private void ExerciseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // An thong bao log exercise
        SucessLogMessage.Visibility = Visibility.Collapsed;

        // Lay mon an duoc chon
        var selectedExercise = (Exercise)ExerciseList.SelectedItem;         

        if (selectedExercise != null)
        {
            // Chuyen doi byte[] sang Image
            var image = ImageHelper.ConvertByteArrayToImage(selectedExercise.Image);
            ExerciseImage.Source = image;

            ExerciseDetail.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            NoExerciseSelectedMessage.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;

            ExerciseName.Text = selectedExercise.Name;

            // Calories burned mac dinh
            DurationInput.Text = "";

            Calories.Text = "0";
     
            // Dinh luong mon an thay doi
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
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = SearchBar.Text.ToLower();
        // Loc danh sach 
        if (string.IsNullOrWhiteSpace(searchText))
        {
            ExerciseList.ItemsSource = ViewModel.Exercises;
        }
        else
        {
            ExerciseList.ItemsSource = ViewModel.Exercises
                .Where(food => food.Name.ToLower().Contains(searchText))
                .ToList();
        }
    }

    private async void AddExerciseButton_Click(object sender, RoutedEventArgs e)
    {
        // Tạo một đối tượng Food mới
        var addExerciseDialog = new AddExerciseDialog()
        {
            XamlRoot = this.XamlRoot
        };

        // Hiển thị dialog
        var result = await addExerciseDialog.ShowAsync();

        if (result == ContentDialogResult.Primary) // Nếu nhấn "Add"
        {
            var exercise = await addExerciseDialog.GetExerciseFromInput();

            // Nếu exercise là null, tức là có lỗi trong quá trình nhập liệu
            if (exercise != null)
            {
                // Gọi phương thức AddFood của ViewModel để thêm món ăn
                ViewModel.AddExercise(exercise);
            }
        }
    }

    private async void ContactRemoveMenuItem_Click(object sender, RoutedEventArgs e)
    {
        // Lấy đối tượng exercise được liên kết với item
        var menuFlyoutItem = sender as MenuFlyoutItem;
        var exerciseToDelete = (menuFlyoutItem?.DataContext as Exercise);

        if (exerciseToDelete == null)
        {
            return;
        }

        // Lưu lại bài tập hiện tại nếu nó đang được chọn
        var selectedExercise = (Exercise)ExerciseList.SelectedItem;

        // Hop thoai xac nhan hanh dong xoa
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
            // Nếu bài tập bị xóa là món ăn đang được chọn, ẩn phần chi tiết
            if (selectedExercise != null && selectedExercise.ExerciseId == exerciseToDelete.ExerciseId)
            {
                ExerciseDetail.Visibility = Visibility.Collapsed;
                NoExerciseSelectedMessage.Visibility = Visibility.Visible;
            }

            ViewModel.RemoveExercise(exerciseToDelete.ExerciseId);
        }
    }

    private void LogExerciseButton_Click(object sender, RoutedEventArgs e)
    {
        SucessLogMessage.Visibility = Visibility.Collapsed;

        if (DatePicker.Date == null)
        {
            SucessLogMessage.Text = "Please select a date.";
            SucessLogMessage.Foreground = new SolidColorBrush(Colors.Red);
            SucessLogMessage.Visibility = Visibility.Visible;

            return;
        }
        // Lay log cua ngay duoc chon
        var selectedDate = DateOnly.FromDateTime(DatePicker.Date.Value.DateTime);

        var log = ViewModel.GetLogByDate(selectedDate);
        // Neu chua ton tai thi tao log moi
        if (log == null)
        {
            log = new Log
            {
                LogDate = selectedDate,
                TotalCalories = 0,
                LogFoodItems = new ObservableCollection<LogFoodItem>(),
                LogExerciseItems = new ObservableCollection<LogExerciseItem>(),
            };
        }

        // Lay ngay duoc chon
        var selectedExercise = (Exercise)ExerciseList.SelectedItem;

        // Lay macro
        if (!double.TryParse(DurationInput.Text, out double duration) || duration <= 0)
        {
            SucessLogMessage.Text = "Please enter a valid duration.";
            SucessLogMessage.Foreground = new SolidColorBrush(Colors.Red);
            SucessLogMessage.Visibility = Visibility.Visible;

            return;
        }
        var caloriesBurned = selectedExercise.CaloriesPerMinute * duration;

        // Them log exercise item vao log
        var logExerciseItem = new LogExerciseItem
        {
            ExerciseId = selectedExercise.ExerciseId,
            Duration = duration,
            TotalCalories = caloriesBurned,
        };

        log.LogExerciseItems.Add(logExerciseItem);

        // Cap nhat lai calories
        log.TotalCalories -= caloriesBurned;

        // Cap nhat log
        ViewModel.UpdateLog(log);

        SucessLogMessage.Text = "Exercise logged successfully.";
        SucessLogMessage.Foreground = new SolidColorBrush(Colors.Green);
        SucessLogMessage.Visibility = Visibility.Visible;
    }
}
