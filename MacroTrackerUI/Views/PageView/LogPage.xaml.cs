using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        ViewModel.GetNextLogsPage();
    }

    private void AddLogDateButton_Click(object sender, RoutedEventArgs e)
    {
        DateTime date = DateTime.Now;
        if (ViewModel.DoesContainDate(date))
            return;

        ViewModel.AddDefaultLogDate();
    }

    private void DeleteLogDate(int ID)
    {
        ViewModel.DeleteLogDate(ID);
    }

    private void DeleteLogFood(int logDateID, int logID)
    {
        ViewModel.DeleteLogFood(logDateID, logID);
    }

    private void DeleteLogExercise(int logDateID, int logID)
    {
        ViewModel.DeleteLogExercise(logDateID, logID);
    }

    private void LogsListView_Loaded(object sender, RoutedEventArgs e)
    {
        Border border = VisualTreeHelper.GetChild(LogsListView, 0) as Border;
        ScrollViewer scrollViewer = VisualTreeHelper.GetChild(border, 0) as ScrollViewer;

        if (scrollViewer != null)
        {
            scrollViewer.ViewChanged += ScrollViewer_ViewChanged;
        }
    }

    private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
        var scrollViewer = sender as ScrollViewer;
        if (scrollViewer != null && scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
        {
            OnScrollToEnd();
        }
    }

    private void OnScrollToEnd()
    {
        // Trigger the event or perform the action when scrolled to the end
        ViewModel.GetNextLogsPage();
    }

    private void CalenderViewFlyout_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
    {
        if (args.AddedDates.Count == 1)
        {
            ViewModel.EndDate = args.AddedDates[0].DateTime;
            CalendarFlyout.Hide();
        }
    }
}
