using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class LogPage : Page
{
    private LogViewModel ViewModel { get; set; } = new LogViewModel();

    public LogPage()
    {
        this.InitializeComponent();
        ViewModel.GetAllLogs();
    }

    private void AddLogDateButton_Click(object sender, RoutedEventArgs e)
    {
        DateTime date = DateTime.Now;
        if (ViewModel.DoesContainDate(date))
            return;

        ViewModel.AddDefaultLogDate();
    }
}
