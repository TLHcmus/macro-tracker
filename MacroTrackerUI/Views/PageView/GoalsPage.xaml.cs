using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
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
    private void EditGoal_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(EditGoalPage), ViewModel.CurrentGoal);
    }

    /// <summary>
    /// Called when the page is navigated to.
    /// </summary>
    /// <param name="e">The event data that contains the navigation parameter.</param>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is Goal goal)
        {
            ViewModel.CurrentGoal = goal;
        }
    }
}
