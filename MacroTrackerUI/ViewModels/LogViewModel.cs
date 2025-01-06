
using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for managing logs.
/// </summary>
public class LogViewModel
{
    public Log Log { get; set; }
    // Calories muc tieu
    public int GoalCalories { get; set; }
    public int FoodsTotalCalories { get; set; }
    public int ExercisesTotalCalories { get; set; }
    public int RemainingCalories {  get; set; }
    private IDaoSender Sender { get; } =
         ProviderUI.GetServiceProvider().GetRequiredService<IDaoSender>();

    public LogViewModel()
    {
        // Lay log cua ngay hom nay
        var today = DateOnly.FromDateTime(DateTime.Today);

        GetLogByDate(today);
        // Lay calories muc tieu
        GoalCalories = Sender.GetGoal().Calories;

        // Lay tong calories cua foods va exercises
        GetFoodsTotalCalories();
        GetExercisesTotalCalories();
        GetRemainingCalories();
    }

    // Lay log theo ngay tuong ung
    public void GetLogByDate(DateOnly date)
    {
        Log = Sender.GetLogByDate(date);

        // Neu chua ton tai log thi tao log moi
        if (Log == null)
        {
             Log = new Log
             {
                LogDate = date,
                TotalCalories = 0,
                LogFoodItems = new ObservableCollection<LogFoodItem>(),
                LogExerciseItems = new ObservableCollection<LogExerciseItem>(),
             };
        }
        Debug.WriteLine($"Number of exercise items: {Log.LogExerciseItems.Count()}");

        // Lay Food bang Id cho tung item
        foreach (var logFoodItem in Log.LogFoodItems)
        {
            logFoodItem.Food = Sender.GetFoodById(logFoodItem.FoodId); 
        }
        // Lay Exercise bang Id cho tung item
        foreach (var logExerciseItem in Log.LogExerciseItems)
        {

            logExerciseItem.Exercise = Sender.GetExerciseById(logExerciseItem.ExerciseId);
        }
        // Cap nhat total calories cua foods va exercises
        GetFoodsTotalCalories();
        GetExercisesTotalCalories();
        GetRemainingCalories();
    }

    // Lay tong calories cua foods
    public int GetFoodsTotalCalories()
    {
        FoodsTotalCalories = (int)Log.LogFoodItems.Sum(food => food.TotalCalories);
        return FoodsTotalCalories;
    }

    // Lay tong caclories cua exercises
    public int GetExercisesTotalCalories()
    {
        foreach (var logExerciseItem in Log.LogExerciseItems)
        {
            Debug.WriteLine($"Exercise: {logExerciseItem.Exercise.Name}, Calories: {logExerciseItem.TotalCalories}");
        }

        ExercisesTotalCalories = (int)Log.LogExerciseItems.Sum(exercise => exercise.TotalCalories);
        Debug.WriteLine($"Tong calories cua exercises: {ExercisesTotalCalories}");
        return ExercisesTotalCalories;
    }
    // Lay tong calories con lai
    public int GetRemainingCalories()
    {
        RemainingCalories = GoalCalories - FoodsTotalCalories + ExercisesTotalCalories;

        return RemainingCalories;
    }

    // Update log
    public void UpdateLog()
    {
        Sender.UpdateLog(Log);
        // Cap nhat tong calories cua mon an va bai tap
        GetFoodsTotalCalories();
        GetExercisesTotalCalories();
        GetRemainingCalories();
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
