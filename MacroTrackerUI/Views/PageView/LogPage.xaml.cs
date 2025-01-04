using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class LogPage : Page
{
    /// <summary>
    /// Gets or sets the ViewModel for managing logs.
    /// </summary>
    private LogViewModel ViewModel { get; set; } = new LogViewModel();

    /// <summary>
    /// Initializes a new instance of the <see cref="LogPage"/> class.
    /// </summary>
    public LogPage()
    {
        this.InitializeComponent();
        ViewModel.GetNextLogsPage();

        // Deduce the paging size on the first load
        ViewModel.PagingSize = ViewModel.LogList.Count;
        ChatBot.ChatBotConversation = App.ChatBotConversation;
    }

    /// <summary>
    /// Handles the click event of the AddLogButton.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void AddLogButton_Click(object sender, RoutedEventArgs e)
    {
        DateTime date = DateTime.Now;
        ViewModel.EndDate = date;

        TodayButton.BorderThickness = new Thickness(2);
        Calendar.SelectedDates.Clear();
        Calendar.SelectedDates.Add(date);
        Calendar.SetDisplayDate(date);

        DateOnly dateOnly = DateOnly.FromDateTime(date);
        if (ViewModel.DoesContainDate(dateOnly))
            return;

        ViewModel.AddLog(new Log
        {
            LogDate = dateOnly,
            LogFoodItems = [],
            LogExerciseItems = []
        });
    }

    /// <summary>
    /// Deletes a log by its ID.
    /// </summary>
    /// <param name="ID">The ID of the log to delete.</param>
    private void DeleteLog(int ID)
    {
        ViewModel.DeleteLog(ID);

        if (ViewModel.LogList.Count < ViewModel.PagingSize)
            ViewModel.GetNextLogsItem(ViewModel.PagingSize - ViewModel.LogList.Count);
    }

    /// <summary>
    /// Deletes a log food item by log date ID and log ID.
    /// </summary>
    /// <param name="logDateID">The log date ID.</param>
    /// <param name="logID">The log ID.</param>
    private void DeleteLogFood(int logDateID, int logID)
    {
        ViewModel.DeleteLogFood(logDateID, logID);
    }

    /// <summary>
    /// Deletes a log exercise item by log date ID and log ID.
    /// </summary>
    /// <param name="logDateID">The log date ID.</param>
    /// <param name="logID">The log ID.</param>
    private void DeleteLogExercise(int logDateID, int logID)
    {
        ViewModel.DeleteLogExercise(logDateID, logID);
    }

    /// <summary>
    /// Handles the Loaded event of the LogsListView.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void LogsListView_Loaded(object sender, RoutedEventArgs e)
    {
        if (VisualTreeHelper.GetChild(LogsListView, 0) is Border border &&
            VisualTreeHelper.GetChild(border, 0) is ScrollViewer scrollViewer)
        {
            scrollViewer.ViewChanged += ScrollViewer_ViewChanged;
        }
    }

    /// <summary>
    /// Handles the ViewChanged event of the ScrollViewer.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
        if (sender is ScrollViewer scrollViewer && scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
        {
            OnScrollToEnd();
        }
    }

    /// <summary>
    /// Called when scrolled to the end.
    /// </summary>
    private void OnScrollToEnd()
    {
        ViewModel.GetNextLogsPage();
    }

    /// <summary>
    /// Handles the SelectedDatesChanged event of the CalendarViewFlyout.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The event data.</param>
    private void CalenderViewFlyout_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
    {
        if (args.AddedDates.Count == 1)
        {
            DateTime date = args.AddedDates[0].DateTime;

            TodayButton.BorderThickness = date.Date == DateTime.Now.Date ? new Thickness(2) : new Thickness(0.5);
            ViewModel.EndDate = date;
            CalendarFlyout.Hide();
        }
    }

    /// <summary>
    /// Handles the click event of the TodayButton.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
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
