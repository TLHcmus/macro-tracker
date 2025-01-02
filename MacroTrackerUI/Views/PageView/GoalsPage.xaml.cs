using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using MacroTrackerUI.Views.DialogView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class GoalsPage : Page
{
    /// <summary>
    /// Gets or sets the ViewModel for managing goals.
    /// </summary>
    public GoalsViewModel ViewModel { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GoalsPage"/> class.
    /// </summary>
    public GoalsPage()
    {
        this.InitializeComponent();
        ViewModel = new GoalsViewModel();
        ChatBot.ChatBotConversation = App.ChatBotConversation;
    }

    /// <summary>
    /// Handles the click event for editing a goal.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private async void EditGoal_Click(object sender, RoutedEventArgs e)
    {
        var editGoalDialog = new EditGoalDialog()
        {
            XamlRoot = this.XamlRoot
        };
        // Hien thi diaglog
        var result = await editGoalDialog.ShowAsync();
        if(result == ContentDialogResult.Primary) // Neu nhan nut Confirm
        {
            var goal = editGoalDialog.GetGoalFromInput();

            if (goal != null)
            {
                ViewModel.UpdateGoal(goal);
            }
    }
        }
}
