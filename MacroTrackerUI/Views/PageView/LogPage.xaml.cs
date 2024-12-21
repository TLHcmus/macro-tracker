using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    private void AddLogButton_Click(object sender, RoutedEventArgs e)
    {
        DateTime date = DateTime.Now;
        ViewModel.EndDate = date;

        TodayButton.BorderThickness = new Thickness(2);
        Calendar.SelectedDates.Clear();
        Calendar.SelectedDates.Add(date);
        Calendar.SetDisplayDate(date);

        DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Now);
        if (ViewModel.DoesContainDate(dateOnly))
            return;

        ViewModel.AddLog(
            new Log
            {
                LogDate = dateOnly,
                LogFoodItems = [],
                LogExerciseItems = []
            }
        );
        return;
    }

    private void DeleteLog(int ID)
    {
      //  ViewModel.DeleteLog(ID);
        return;
    }

    private void DeleteLogFood(int logDateID, int logID)
    {
       // ViewModel.DeleteLogFood(logDateID, logID);
    }

    private void DeleteLogExercise(int logDateID, int logID)
    {
        //ViewModel.DeleteLogExercise(logDateID, logID);
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

    private void CalenderViewFlyout_SelectedDatesChanged(
        CalendarView sender, 
        CalendarViewSelectedDatesChangedEventArgs args)
    {
        if (args.AddedDates.Count == 1)
        {
            DateTime date = args.AddedDates[0].DateTime;

            if (date.Date == DateTime.Now.Date)
                TodayButton.BorderThickness = new Thickness(2);
            else
                TodayButton.BorderThickness = new Thickness(0.5);
            ViewModel.EndDate = args.AddedDates[0].DateTime;
            CalendarFlyout.Hide();
        }
    }

    private void TodayButton_Click(object sender, RoutedEventArgs e)
    {
        DateTime date = DateTime.Now;
        ViewModel.EndDate = date;
        Calendar.SelectedDates.Clear();
        Calendar.SelectedDates.Add(date);
        Calendar.SetDisplayDate(date);
        CalendarFlyout.Hide();

        TodayButton.BorderThickness = new Thickness(2);
    }
}
