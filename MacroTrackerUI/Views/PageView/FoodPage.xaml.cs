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

    public FoodPage()
    {
        this.InitializeComponent();
        ViewModel = new FoodViewModel();
    }

    private void FoodList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Lay mon an duoc chon
        var selectedFood = (Food)FoodList.SelectedItem;

        if(selectedFood != null)
        {
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
                if (double.TryParse(ServingInput.Text, out double serving))
                {
                    var nutrition = selectedFood.GetNutrition(serving);
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
            var food = addFoodDialog.GetFoodFromInput();

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
            if (selectedFood != null && selectedFood.Name == foodToDelete.Name)
            {
                FoodDetail.Visibility = Visibility.Collapsed;
                NoFoodSelectedMessage.Visibility = Visibility.Visible;
            }

            ViewModel.RemoveFood(foodToDelete.Name);
        }
    }

    private void LogFoodButton_Click(object sender, RoutedEventArgs e)
    {
        return;
    }
}
