using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using MacroTrackerUI.Views.PageView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace MacroTrackerUI.Views.UserControlView;

public sealed partial class LogItem : UserControl, INotifyPropertyChanged
{
    private LogItemViewModel ViewModel { get; set; } = new LogItemViewModel();
    public delegate void DeleteLogEventHandler(int ID);
    public event DeleteLogEventHandler DeleteLog;

    public delegate void DeleteLogItemEventHandler(int logDateID, int logID);
    public event DeleteLogItemEventHandler DeleteLogFood;
    public event DeleteLogItemEventHandler DeleteLogExercise;

    public static readonly DependencyProperty LogProperty = DependencyProperty.Register(
            "Log",
            typeof(Log),
            typeof(LogItem),
            new PropertyMetadata(null)
        );

    public event PropertyChangedEventHandler PropertyChanged;

    public Log Log
    {
        get { return (Log)GetValue(LogProperty); }
        set { SetValue(LogProperty, value); }
    }

    public LogItem()
    {
        this.InitializeComponent();
    }

    private void DeleteLogFoodButton_Click(object sender, RoutedEventArgs e)
    {
        int? tag = (sender as Button).Tag as int?;
        if (tag == null)
            throw new System.Exception("Tag is null.");

       // DeleteLogFood?.Invoke(LogDate.ID, (int)tag);
       // ViewModel.UpdateTotalCalories(LogDate);
    }

    private void AddLogFoodButton_Click(object sender, RoutedEventArgs e)
    {
        return;
    }

    private void DeleteLogExerciseButton_Click(object sender, RoutedEventArgs e)
    {
        int? tag = (sender as Button).Tag as int?;
        if (tag == null)
            throw new System.Exception("Tag is null.");

      //  DeleteLogExercise?.Invoke(LogDate.ID, (int)tag);
        //ViewModel.UpdateTotalCalories(LogDate);
    }

    private void AddLogExerciseButton_Click(object sender, RoutedEventArgs e)
    {
        return;
    }

    private void DeleteLogButton_Click(object sender, RoutedEventArgs e)
    {
        DeleteLog?.Invoke(Log.LogId);
    }
}
