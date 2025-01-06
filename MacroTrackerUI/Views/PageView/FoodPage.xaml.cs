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

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class FoodPage : Page
{
    private FoodViewModel ViewModel { get; set; }

    public FoodPage()
    {
        this.InitializeComponent();
        ChatBot.ChatBotConversation = App.ChatBotConversation;
        ViewModel = new FoodViewModel();
    }

    private void FoodList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // An thong bao log food
        SucessLogMessage.Visibility = Visibility.Collapsed;

        // Lay mon an duoc chon
        var selectedFood = (Food)FoodList.SelectedItem;

        if(selectedFood != null)
        {
            // Chuyen doi byte[] sang Image
            var image = ImageHelper.ConvertByteArrayToImage(selectedFood.Image);
            FoodImage.Source = image;

            FoodDetail.Visibility = Visibility.Visible;
            NoFoodSelectedMessage.Visibility = Visibility.Collapsed;

            FoodName.Text = selectedFood.Name;

            // Nutrition fact mac dinh
            ServingInput.Text = "";

            Calories.Text = "0";
            Protein.Text = "0 g";
            Carbs.Text = "0 g";
            Fat.Text = "0 g";

            // Dinh luong mon an thay doi
            ServingInput.TextChanged += (s, e) =>
            {
                if (double.TryParse(ServingInput.Text, out double servings) && servings >= 0)
                {
                    var nutrition = selectedFood.GetNutrition(servings);
                    Calories.Text = nutrition.Calories.ToString("0.#");
                    Protein.Text = nutrition.Protein.ToString("0.#") + " g";
                    Carbs.Text = nutrition.Carbs.ToString("0.#") + " g";
                    Fat.Text = nutrition.Fat.ToString("0.#") + " g";
                }
                else
                {
                    Calories.Text = "0";
                    Protein.Text = "0 g";
                    Carbs.Text = "0 g";
                    Fat.Text = "0 g";
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
            FoodList.ItemsSource = ViewModel.Foods;
        }
        else
        {
            FoodList.ItemsSource = ViewModel.Foods
                .Where(food => food.Name.ToLower().Contains(searchText))
                .ToList();
        }
    }

    private async void AddFoodButton_Click(object sender, RoutedEventArgs e)
    {
        // Tạo một đối tượng Food mới
        var addFoodDialog = new AddFoodDialog()
        {
            XamlRoot = this.XamlRoot
        };

        // Hiển thị dialog
        var result = await addFoodDialog.ShowAsync();

        if (result == ContentDialogResult.Primary) // Nếu nhấn "Add"
        {
            var food = await addFoodDialog.GetFoodFromInput();

            // Nếu food là null, tức là có lỗi trong quá trình nhập liệu
            if (food != null)
            {
                // Gọi phương thức AddFood của ViewModel để thêm món ăn
                ViewModel.AddFood(food);
            }
        }
    }

    private async void ContactRemoveMenuItem_Click(object sender, RoutedEventArgs e)
    {
        // Lấy đối tượng food được liên kết với item
        var menuFlyoutItem = sender as MenuFlyoutItem;
        var foodToDelete = (menuFlyoutItem?.DataContext as Food);

        if(foodToDelete == null)
        {
            return;
        }

        // Lưu lại món ăn hiện tại nếu nó đang được chọn
        var selectedFood = (Food)FoodList.SelectedItem;

        // Hop thoai xac nhan hanh dong xoa
        var confirmDialog = new ContentDialog
        {
            Title = "Confirm Removal",
            Content = $"Are you sure you want to remove {foodToDelete.Name}?",
            PrimaryButtonText = "Yes",
            CloseButtonText = "No",
            XamlRoot = this.XamlRoot,
            DefaultButton = ContentDialogButton.Close
        };

        var result = await confirmDialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            // Nếu món ăn bị xóa là món ăn đang được chọn, ẩn phần chi tiết
            if (selectedFood != null && selectedFood.FoodId == foodToDelete.FoodId)
            {
                FoodDetail.Visibility = Visibility.Collapsed;
                NoFoodSelectedMessage.Visibility = Visibility.Visible;
            }

            ViewModel.RemoveFood(foodToDelete.FoodId);
        }
    }

    private void LogFoodButton_Click(object sender, RoutedEventArgs e)
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
        var selectedFood = (Food)FoodList.SelectedItem;

        // Lay macro
        if(!double.TryParse(ServingInput.Text, out double servings) || servings <= 0)
        {
            SucessLogMessage.Text = "Please enter a valid number of servings.";
            SucessLogMessage.Foreground = new SolidColorBrush(Colors.Red);
            SucessLogMessage.Visibility = Visibility.Visible;

            return;
        }
        var nutrition = selectedFood.GetNutrition(servings);

        // Them log food item vao log
        var logFoodItem = new LogFoodItem
        {
            FoodId = selectedFood.FoodId,
            NumberOfServings = servings,
            TotalCalories = nutrition.Calories,
        };

        log.LogFoodItems.Add(logFoodItem);

        // Cap nhat lai calories
        log.TotalCalories += nutrition.Calories;

        // Cap nhat log
        ViewModel.UpdateLog(log);

        SucessLogMessage.Text = "Food logged successfully.";
        SucessLogMessage.Foreground = new SolidColorBrush(Colors.Green);
        SucessLogMessage.Visibility = Visibility.Visible;
    }
}
