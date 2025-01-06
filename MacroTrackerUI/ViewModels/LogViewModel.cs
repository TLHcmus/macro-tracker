
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
    /// <summary>
    /// Gets or sets the log.
    /// </summary>
    public Log Log { get; set; }

    /// <summary>
    /// Gets or sets the goal calories.
    /// </summary>
    public int GoalCalories { get; set; }

    /// <summary>
    /// Gets or sets the total calories from foods.
    /// </summary>
    public int FoodsTotalCalories { get; set; }

    /// <summary>
    /// Gets or sets the total calories from exercises.
    /// </summary>
    public int ExercisesTotalCalories { get; set; }

    /// <summary>
    /// Gets or sets the remaining calories.
    /// </summary>
    public int RemainingCalories { get; set; }

    /// <summary>
    /// Gets the data access sender.
    /// </summary>
    private IDaoSender Sender { get; } =
         ProviderUI.GetServiceProvider().GetRequiredService<IDaoSender>();

    /// <summary>
    /// Initializes a new instance of the <see cref="LogViewModel"/> class.
    /// </summary>
    public LogViewModel()
    {
        // Get today's log
        var today = DateOnly.FromDateTime(DateTime.Today);

        GetLogByDate(today);
        // Get goal calories
        GoalCalories = Sender.GetGoal().Calories;

        // Get total calories from foods and exercises
        GetFoodsTotalCalories();
        GetExercisesTotalCalories();
        GetRemainingCalories();
    }

    /// <summary>
    /// Gets the log by the specified date.
    /// </summary>
    /// <param name="date">The date to get the log for.</param>
    public void GetLogByDate(DateOnly date)
    {
        Log = Sender.GetLogByDate(date);

        // If log does not exist, create a new log
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

        // Get food by ID for each item
        foreach (var logFoodItem in Log.LogFoodItems)
        {
            logFoodItem.Food = Sender.GetFoodById(logFoodItem.FoodId);
        }
        // Get exercise by ID for each item
        foreach (var logExerciseItem in Log.LogExerciseItems)
        {
            logExerciseItem.Exercise = Sender.GetExerciseById(logExerciseItem.ExerciseId);
        }
        // Update total calories from foods and exercises
        GetFoodsTotalCalories();
        GetExercisesTotalCalories();
        GetRemainingCalories();
    }

    /// <summary>
    /// Gets the total calories from foods.
    /// </summary>
    /// <returns>The total calories from foods.</returns>
    public int GetFoodsTotalCalories()
    {
        FoodsTotalCalories = (int)Log.LogFoodItems.Sum(food => food.TotalCalories);
        return FoodsTotalCalories;
    }

    /// <summary>
    /// Gets the total calories from exercises.
    /// </summary>
    /// <returns>The total calories from exercises.</returns>
    public int GetExercisesTotalCalories()
    {
        foreach (var logExerciseItem in Log.LogExerciseItems)
        {
            Debug.WriteLine($"Exercise: {logExerciseItem.Exercise.Name}, Calories: {logExerciseItem.TotalCalories}");
        }

        ExercisesTotalCalories = (int)Log.LogExerciseItems.Sum(exercise => exercise.TotalCalories);
        Debug.WriteLine($"Total calories from exercises: {ExercisesTotalCalories}");
        return ExercisesTotalCalories;
    }

    /// <summary>
    /// Gets the remaining calories.
    /// </summary>
    /// <returns>The remaining calories.</returns>
    public int GetRemainingCalories()
    {
        RemainingCalories = GoalCalories - FoodsTotalCalories + ExercisesTotalCalories;
        return RemainingCalories;
    }

    /// <summary>
    /// Updates the log.
    /// </summary>
    public void UpdateLog()
    {
        Sender.UpdateLog(Log);
        // Update total calories from foods and exercises
        GetFoodsTotalCalories();
        GetExercisesTotalCalories();
        GetRemainingCalories();
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}
