using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using MacroTrackerUI.Views.DialogView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
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
        var selectedFood = (Food)FoodList.SelectedItem;

        if (selectedFood != null)
        {
            ShowFoodDetails(selectedFood);
        }
    }

    /// <summary>
    /// Displays the details of the selected food.
    /// </summary>
    private void ShowFoodDetails(Food selectedFood)
    {
        FoodDetail.Visibility = Visibility.Visible;
        NoFoodSelectedMessage.Visibility = Visibility.Collapsed;

        FoodName.Text = selectedFood.Name;
        ResetNutritionFacts();

        ServingInput.TextChanged += (s, e) =>
        {
            UpdateNutritionFacts(selectedFood);
        };
    }

    /// <summary>
    /// Resets the nutrition facts to default values.
    /// </summary>
    private void ResetNutritionFacts()
    {
        ServingInput.Text = "";
        Calories.Text = "0";
        Protein.Text = "0 g";
        Carbs.Text = "0 g";
        Fat.Text = "0 g";
    }

    /// <summary>
    /// Updates the nutrition facts based on the serving input.
    /// </summary>
    private void UpdateNutritionFacts(Food selectedFood)
    {
        if (double.TryParse(ServingInput.Text, out double serving))
        {
            var nutrition = selectedFood.GetNutrition(serving);
            Calories.Text = nutrition.Calories.ToString("0.#");
            Protein.Text = $"{nutrition.Protein:0.#} g";
            Carbs.Text = $"{nutrition.Carbs:0.#} g";
            Fat.Text = $"{nutrition.Fat:0.#} g";
        }
        else
        {
            ResetNutritionFacts();
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
            var food = addFoodDialog.GetFoodFromInput();
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
            if (selectedFood != null && selectedFood.Name == foodToDelete.Name)
            {
                FoodDetail.Visibility = Visibility.Collapsed;
                NoFoodSelectedMessage.Visibility = Visibility.Visible;
            }

            ViewModel.RemoveFood(foodToDelete.Name);
        }
    }

    /// <summary>
    /// Handles the click event of the log food button.
    /// </summary>
    private void LogFoodButton_Click(object sender, RoutedEventArgs e)
    {
        // Implementation for logging food
    }
}
