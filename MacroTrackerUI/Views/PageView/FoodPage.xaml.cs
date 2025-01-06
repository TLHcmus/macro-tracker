using MacroTrackerUI.Helpers;
using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using MacroTrackerUI.Views.DialogView;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class FoodPage : Page
{
    private FoodViewModel ViewModel { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FoodPage"/> class.
    /// </summary>
    public FoodPage()
    {
        this.InitializeComponent();
        ChatBot.ChatBotConversation = App.ChatBotConversation;
        ViewModel = new FoodViewModel();
    }

    /// <summary>
    /// Handles the selection change event of the food list.
    /// </summary>
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


    /// <summary>
    /// Handles the text changed event of the search bar.
    /// </summary>
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = SearchBar.Text.ToLower();
        FoodList.ItemsSource = string.IsNullOrWhiteSpace(searchText)
            ? ViewModel.Foods
            : ViewModel.Foods.Where(food => food.Name.ToLower().Contains(searchText)).ToList();
    }

    /// <summary>
    /// Handles the click event of the add food button.
    /// </summary>
    private async void AddFoodButton_Click(object sender, RoutedEventArgs e)
    {
        var addFoodDialog = new AddFoodDialog { XamlRoot = this.XamlRoot };
        var result = await addFoodDialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            var food = await addFoodDialog.GetFoodFromInput();

            // Nếu food là null, tức là có lỗi trong quá trình nhập liệu
            if (food != null)
            {
                ViewModel.AddFood(food);
            }
        }
    }

    /// <summary>
    /// Handles the click event of the remove menu item.
    /// </summary>
    private async void ContactRemoveMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var menuFlyoutItem = sender as MenuFlyoutItem;
        var foodToDelete = menuFlyoutItem?.DataContext as Food;

        if (foodToDelete == null) return;

        var selectedFood = (Food)FoodList.SelectedItem;
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

    /// <summary>
    /// Handles the click event of the log food button.
    /// </summary>
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
