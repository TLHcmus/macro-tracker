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
    public GoalsViewModel ViewModel
    {
        get; set;
    }
    public GoalsPage()
    {
        this.InitializeComponent();
        ViewModel = new GoalsViewModel();
    }

    // Edit goal click event handler
    private void EditGoal_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(EditGoalPage), ViewModel.CurrentGoal);
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is Goal goal)
        {
            ViewModel.CurrentGoal = goal;
        }
    }
}
