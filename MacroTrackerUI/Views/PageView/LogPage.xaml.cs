using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class LogPage : Page
{
    private LogViewModel ViewModel { get; set; }
    public LogPage()
    {
        this.InitializeComponent();
        ChatBot.ChatBotConversation = App.ChatBotConversation;
        ViewModel = new LogViewModel();
        // Dat ngay mac dinh la hom nay
        LogDatePicker.Date = DateTime.Now;
    }


    // Khi chon ngay khac
    private void LogDatePicker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
    {
        // Kiểm tra xem giá trị ngày có hợp lệ không
        if (LogDatePicker.SelectedDate.HasValue)
        {
            var selectedDate = DateOnly.FromDateTime(LogDatePicker.SelectedDate.Value.Date);

            ViewModel.GetLogByDate(selectedDate);
        }
    }

    private async void FoodsContactRemoveMenuItem_Click(object sender, RoutedEventArgs e)
    {
        // Lấy item từ context
        var menuItem = sender as MenuFlyoutItem;
        var foodItem = menuItem?.DataContext as LogFoodItem;

        if (foodItem == null)
        {
            return;
        }

        // Hop thoai xac nhan hanh dong xoa
        var confirmDialog = new ContentDialog
        {
            Title = "Confirm Removal",
            Content = $"Are you sure you want to remove {foodItem.NumberOfServings} gram of {foodItem.Food.Name}?",
            PrimaryButtonText = "Yes",
            CloseButtonText = "No",
            XamlRoot = this.XamlRoot,
            DefaultButton = ContentDialogButton.Close
        };

        var result = await confirmDialog.ShowAsync();
        if(result == ContentDialogResult.Primary)
        {
            ViewModel.Log.LogFoodItems.Remove(foodItem); // Xóa item khỏi danh sách
            // Cap nhat total calories cua log
            ViewModel.Log.TotalCalories -= foodItem.TotalCalories;

            ViewModel.UpdateLog(); // Cập nhật log
        }

    }

    private async void FoodsContactAdjustMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var menuItem = sender as MenuFlyoutItem;
        var foodItem = menuItem?.DataContext as LogFoodItem;

        if (foodItem == null)
        {
            return;
        }

        // Tạo dialog để người dùng nhập khẩu phần mới
        var inputBox = new TextBox
            {
                PlaceholderText = "Enter new portion...",
                Margin = new Thickness(0, 10, 0, 0)
            };

            var dialog = new ContentDialog
            {
                Title = "Adjust Portion",
                Content = inputBox,
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = this.XamlRoot
            };

            // Hiển thị dialog và chờ kết quả
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var newPortion = inputBox.Text;

                // Xử lý cập nhật khẩu phần mới
                if (double.TryParse(newPortion, out var portionValue))
                {
                    // Cập nhật số lượng khẩu phần
                    foodItem.NumberOfServings = portionValue;

                    // Cap nhat total calories cua log
                    ViewModel.Log.TotalCalories -= foodItem.TotalCalories + (portionValue * (foodItem.Food.CaloriesPer100g / 100));

                    // Cap nhat tong calories cua item
                    foodItem.TotalCalories = portionValue * (foodItem.Food.CaloriesPer100g / 100);
                    
                    // Cap nhat log

                    ViewModel.UpdateLog();
                }
                else
                {
                    // Hiển thị thông báo lỗi nếu giá trị nhập không hợp lệ
                    var errorDialog = new ContentDialog
                    {
                        Title = "Invalid Input",
                        Content = "Please enter a valid number for the portion size.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await errorDialog.ShowAsync();
                }
            }
        }

    private async void ExercisesContactRemoveMenuItem_Click(object sender, RoutedEventArgs e)
    {
        // Lấy item từ context
        var menuItem = sender as MenuFlyoutItem;
        var exerciseItem = menuItem?.DataContext as LogExerciseItem;

        if (exerciseItem == null)
        {
            return;
        }

        // Hop thoai xac nhan hanh dong xoa
        var confirmDialog = new ContentDialog
        {
            Title = "Confirm Removal",
            Content = $"Are you sure you want to remove {exerciseItem.Duration} minutes of {exerciseItem.Exercise.Name}?",
            PrimaryButtonText = "Yes",
            CloseButtonText = "No",
            XamlRoot = this.XamlRoot,
            DefaultButton = ContentDialogButton.Close
        };

        var result = await confirmDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            ViewModel.Log.LogExerciseItems.Remove(exerciseItem); // Xóa item khỏi danh sách
            // Cap nhat tong calories cua log
            ViewModel.Log.TotalCalories += exerciseItem.TotalCalories; 

            ViewModel.UpdateLog(); // Cập nhật log
        }
    }

    private async void ExercisesContactAdjustMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var menuItem = sender as MenuFlyoutItem;
        var exerciseItem = menuItem?.DataContext as LogExerciseItem;

        if (exerciseItem == null)
        {
            return;
        }

        // Tạo dialog để người dùng nhập khẩu phần mới
        var inputBox = new TextBox
        {
            PlaceholderText = "Enter new portion...",
            Margin = new Thickness(0, 10, 0, 0)
        };

        var dialog = new ContentDialog
        {
            Title = "Adjust Duration",
            Content = inputBox,
            PrimaryButtonText = "Save",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary,
            XamlRoot = this.XamlRoot
        };

        // Hiển thị dialog và chờ kết quả
        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            var newDuration = inputBox.Text;

            // Xử lý cập nhật khẩu phần mới
            if (double.TryParse(newDuration, out var durationValue))
            {
                // Cập nhật số lượng khẩu phần
                exerciseItem.Duration = durationValue;

                // Cap nhat tong calories cua log
                ViewModel.Log.TotalCalories += exerciseItem.TotalCalories - (durationValue * exerciseItem.Exercise.CaloriesPerMinute);

                // Cap nhat tong calories cua item
                exerciseItem.TotalCalories = durationValue * exerciseItem.Exercise.CaloriesPerMinute;

                // Cap nhat log

                ViewModel.UpdateLog();
            }
            else
            {
                // Hiển thị thông báo lỗi nếu giá trị nhập không hợp lệ
                var errorDialog = new ContentDialog
                {
                    Title = "Invalid Input",
                    Content = "Please enter a valid number for the duration.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await errorDialog.ShowAsync();
            }
        }
    }
}
