using MacroTrackerUI.Models;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using Windows.Gaming.Input.ForceFeedback;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class CalculatorPage : Page
{
    /// <summary>
    /// Gets or sets the view model for the CalculatorPage.
    /// </summary>
    public CalculatorViewModel ViewModel { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorPage"/> class.
    /// </summary>
    public CalculatorPage()
    {
        this.InitializeComponent();
        ViewModel = new CalculatorViewModel();
        ChatBot.ChatBotConversation = App.ChatBotConversation;
    }

    /// <summary>
    /// Handles the Click event of the CalculateButton control.
    /// Calculates the Total Daily Energy Expenditure (TDEE) and displays the result.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private async void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        // Missing value
        if (string.IsNullOrWhiteSpace(WeightTextBox.Text) ||
            string.IsNullOrWhiteSpace(HeightTextBox.Text) ||
            string.IsNullOrWhiteSpace(AgeTextBox.Text) || 
            ActivityLevelComboBox.SelectedItem == null ||
            (!(MaleRadioButton.IsChecked == true) && !(FemaleRadioButton.IsChecked == true)))
        {
            // Tao va hien thi thong bao loi
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Missing Information",
                Content = "Please enter all values.",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };
            await errorDialog.ShowAsync();
        }
        // Valid
        else if (int.TryParse(WeightTextBox.Text, out int weight) && int.TryParse(HeightTextBox.Text, out int height) && int.TryParse(AgeTextBox.Text, out int age)) 
        {
            ViewModel.Weight = weight;
            ViewModel.Height = height;
            ViewModel.Age = age;

            int tdee = (int)ViewModel.CalculateTDEE();
            ResultTextBlock.Text = $"Your Maintaince Calories is {tdee} calories.";

            // Hien thi nut de set as goal
            SetAsGoalButton.Visibility = Visibility.Visible;
        }
        // Invalid value
        else
        {
            // Tao va hien thi thong bao loi
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Invalid Information",
                Content = "Please enter valid values.",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };
            await errorDialog.ShowAsync();
        }
    }

    private void SetAsGoalButton_Click(object sender, RoutedEventArgs e)
    {
        int tdee = (int)ViewModel.CalculateTDEE();

        // Set goal
        var goal = new Goal
        {
            Calories = tdee,
            // Mac dinh ti le 25:50:25%
            Protein = (int)(tdee * 25 / 100 / 4),
            Fat = (int)(tdee * 50 / 100 / 9),
            Carbs = (int)(tdee * 25 / 100 / 4)
        };
        ViewModel.UpdateGoal(goal);

        // Hien thi thong bao thanh cong
        SetAsGoalMessage.Text = "Sucessfully set as your goal!";
    }

    private void RadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            ViewModel.Gender = radioButton.Content.ToString();
        }
    }
}
