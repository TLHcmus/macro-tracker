using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using MacroTrackerUI.Views.DialogView;
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
