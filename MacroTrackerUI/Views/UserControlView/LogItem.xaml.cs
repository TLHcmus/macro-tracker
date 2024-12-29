using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using MacroTrackerUI.Views.PageView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace MacroTrackerUI.Views.UserControlView;

/// <summary>
/// Represents a log item user control.
/// </summary>
public sealed partial class LogItem : UserControl, INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the view model for the log item.
    /// </summary>
    private LogItemViewModel ViewModel { get; set; } = new LogItemViewModel();

    /// <summary>
    /// Delegate for handling log deletion events.
    /// </summary>
    /// <param name="ID">The ID of the log to delete.</param>
    public delegate void DeleteLogEventHandler(int ID);

    /// <summary>
    /// Event triggered when a log is deleted.
    /// </summary>
    public event DeleteLogEventHandler DeleteLog;

    /// <summary>
    /// Delegate for handling log item deletion events.
    /// </summary>
    /// <param name="logDateID">The log date ID.</param>
    /// <param name="logID">The log ID.</param>
    public delegate void DeleteLogItemEventHandler(int logDateID, int logID);

    /// <summary>
    /// Event triggered when a log food item is deleted.
    /// </summary>
    public event DeleteLogItemEventHandler DeleteLogFood;

    /// <summary>
    /// Event triggered when a log exercise item is deleted.
    /// </summary>
    public event DeleteLogItemEventHandler DeleteLogExercise;

    /// <summary>
    /// Identifies the Log dependency property.
    /// </summary>
    public static readonly DependencyProperty LogProperty = DependencyProperty.Register(
        "Log",
        typeof(Log),
        typeof(LogItem),
        new PropertyMetadata(null)
    );

    /// <summary>
    /// Event triggered when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets or sets the log associated with this log item.
    /// </summary>
    public Log Log
    {
        get { return (Log)GetValue(LogProperty); }
        set { SetValue(LogProperty, value); }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogItem"/> class.
    /// </summary>
    public LogItem()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Handles the click event for deleting a log food item.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void DeleteLogFoodButton_Click(object sender, RoutedEventArgs e)
    {
        int? tag = (sender as Button).Tag as int?;
        if (tag == null)
            throw new System.Exception("Tag is null.");

        DeleteLogFood?.Invoke(Log.LogId, (int)tag);
        ViewModel.UpdateTotalCalories(Log);
    }

    /// <summary>
    /// Handles the click event for adding a log food item.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void AddLogFoodButton_Click(object sender, RoutedEventArgs e)
    {
        return;
    }

    /// <summary>
    /// Handles the click event for deleting a log exercise item.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void DeleteLogExerciseButton_Click(object sender, RoutedEventArgs e)
    {
        int? tag = (sender as Button).Tag as int?;
        if (tag == null)
            throw new System.Exception("Tag is null.");

        DeleteLogExercise?.Invoke(Log.LogId, (int)tag);
        ViewModel.UpdateTotalCalories(Log);
    }

    /// <summary>
    /// Handles the click event for adding a log exercise item.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void AddLogExerciseButton_Click(object sender, RoutedEventArgs e)
    {
        return;
    }

    /// <summary>
    /// Handles the click event for deleting a log.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void DeleteLogButton_Click(object sender, RoutedEventArgs e)
    {
        DeleteLog?.Invoke(Log.LogId);
    }
}
