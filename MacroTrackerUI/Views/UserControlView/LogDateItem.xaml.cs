using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace MacroTrackerUI.Views.UserControlView;

public sealed partial class LogDateItem : UserControl, INotifyPropertyChanged
{
    private LogDateItemViewModel ViewModel { get; set; } = new LogDateItemViewModel();
    public delegate void DeleteLogDateEventHandler(int ID);
    public delegate void DeleteLogEventHandler(int logDateID, int logID);

    public event DeleteLogDateEventHandler DeleteLogDate;
    public event DeleteLogEventHandler DeleteLogFood;
    public event DeleteLogEventHandler DeleteLogExercise;

    public static readonly DependencyProperty LogDateProperty = DependencyProperty.Register(
            "LogDate",
            typeof(LogDate),
            typeof(LogDateItem),
            new PropertyMetadata(null)
        );

    public event PropertyChangedEventHandler PropertyChanged;

    public LogDate LogDate
    {
        get { return (LogDate)GetValue(LogDateProperty); }
        set 
        { 
            SetValue(LogDateProperty, value);
            ViewModel.UpdateTotalCalories(LogDate);
        }
    }

    public LogDateItem()
    {
        this.InitializeComponent();
    }

    private void DeleteLogFoodButton_Click(object sender, RoutedEventArgs e)
    {
        int? tag = (sender as Button).Tag as int?;
        if (tag == null)
            throw new System.Exception("Tag is null.");

        DeleteLogFood?.Invoke(LogDate.ID, (int) tag);
        ViewModel.UpdateTotalCalories(LogDate);
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

        DeleteLogExercise?.Invoke(LogDate.ID, (int)tag);
        ViewModel.UpdateTotalCalories(LogDate);
    }

    private void AddLogExerciseButton_Click(object sender, RoutedEventArgs e)
    {
        return;
    }

    private void DeleteLogDateButton_Click(object sender, RoutedEventArgs e)
    {
        DeleteLogDate?.Invoke(LogDate.ID);
    }
}
