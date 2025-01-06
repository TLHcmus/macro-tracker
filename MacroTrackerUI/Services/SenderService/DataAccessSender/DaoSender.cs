using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace MacroTrackerUI.Services.SenderService.DataAccessSender;

/// <summary>
/// Provides methods to interact with the data access layer and perform various operations.
/// </summary>
public class DaoSender : IDaoSender
{
    public ServiceProvider ProviderUI { get; }
    private IDaoReceiver Receiver { get; }
    private JsonSerializerOptions Options { get; } = new() { IncludeFields = true };

    public DaoSender()
    {
        ProviderUI = ProviderService.ProviderUI.GetServiceProvider();
        Receiver = ProviderUI.GetRequiredService<IDaoReceiver>();
    }

    public DaoSender(ServiceProvider providerUI)
    {
        ProviderUI = providerUI;
        Receiver = ProviderUI.GetService<IDaoReceiver>();
    }

    /// <summary>
    /// Retrieves a list of foods.
    /// </summary>
    /// <returns>A list of <see cref="Food"/> objects.</returns>
    public List<Food> GetFoods()
    {
        return JsonSerializer.Deserialize<List<Food>>(Receiver.GetFoods());
    }

    /// <summary>
    /// Adds a new food item.
    /// </summary>
    /// <param name="food">The food item to add.</param>
    /// <returns>The ID of the added food item.</returns>
    public int AddFood(Food food)
    {
        return Receiver.AddFood(JsonSerializer.Serialize(food));
    }

    /// <summary>
    /// Removes a food item.
    /// </summary>
    /// <param name="foodId">The ID of the food item to remove.</param>
    public void RemoveFood(int foodId)
    {
        Debug.WriteLine($"food id xoa: {foodId}");
        Receiver.RemoveFood(JsonSerializer.Serialize(foodId));
    }

    /// <summary>
    /// Retrieves a food item by its ID.
    /// </summary>
    /// <param name="foodId">The ID of the food item to retrieve.</param>
    /// <returns>A <see cref="Food"/> object.</returns>
    public Food GetFoodById(int foodId)
    {
        return JsonSerializer.Deserialize<Food>(Receiver.GetFoodById(foodId));
    }

    /// <summary>
    /// Retrieves a collection of exercises.
    /// </summary>
    /// <returns>A list of <see cref="Exercise"/> objects.</returns>
    public List<Exercise> GetExercises()
    {
        return JsonSerializer.Deserialize<List<Exercise>>(Receiver.GetExercises());
    }

    /// <summary>
    /// Retrieves an exercise item by its ID.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise item to retrieve.</param>
    /// <returns>An <see cref="Exercise"/> object.</returns>
    public Exercise GetExerciseById(int exerciseId)
    {
        return JsonSerializer.Deserialize<Exercise>(Receiver.GetExerciseById(exerciseId));
    }

    /// <summary>
    /// Adds a new exercise item.
    /// </summary>
    /// <param name="exercise">The exercise item to add.</param>
    /// <returns>The ID of the added exercise item.</returns>
    public int AddExercise(Exercise exercise)
    {
        return Receiver.AddExercise(JsonSerializer.Serialize(exercise));
    }

    /// <summary>
    /// Removes an exercise item.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise item to remove.</param>
    public void RemoveExercise(int exerciseId)
    {
        Receiver.RemoveExercise(exerciseId);
    }

    /// <summary>
    /// Retrieves the goal.
    /// </summary>
    /// <returns>A <see cref="Goal"/> object.</returns>
    public Goal GetGoal()
    {
        return JsonSerializer.Deserialize<Goal>(Receiver.GetGoal());
    }

    /// <summary>
    /// Updates the goal.
    /// </summary>
    /// <param name="goal">The goal to update.</param>
    public void UpdateGoal(Goal goal)
    {
        Receiver.UpdateGoal(JsonSerializer.Serialize(goal));
    }

    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <returns>A list of <see cref="User"/> objects.</returns>
    public List<User> GetUsers()
    {
        return JsonSerializer.Deserialize<List<User>>(Receiver.GetUsers());
    }

    /// <summary>
    /// Checks if the provided username and password match.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <param name="password">The password to check.</param>
    /// <returns>
    /// <c>true</c> if the username and password match; otherwise, <c>false</c>.
    /// </returns>
    public bool DoesUserMatchPassword(string username, string password)
    {
        return Receiver.DoesUserMatchPassword(username, password);
    }

    /// <summary>
    /// Checks if the provided username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>
    /// <c>true</c> if the username exists; otherwise, <c>false</c>.
    /// </returns>
    public bool DoesUsernameExist(string username)
    {
        return Receiver.DoesUsernameExist(username);
    }

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">The user to add.</param>
    public void AddUser((string, string) user)
    {
        Receiver.AddUser(user);
    }

    /// <summary>
    /// Retrieves a list of logs.
    /// </summary>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    public List<Log> GetLogs()
    {
        return JsonSerializer.Deserialize<List<Log>>(Receiver.GetLogs());
    }

    /// <summary>
    /// Retrieves a log entry by its date.
    /// </summary>
    /// <param name="date">The date of the log entry to retrieve.</param>
    /// <returns>A <see cref="Log"/> object.</returns>
    public Log GetLogByDate(DateOnly date)
    {
        return JsonSerializer.Deserialize<Log>(Receiver.GetLogByDate(date));
    }

    /// <summary>
    /// Updates an existing log entry.
    /// </summary>
    /// <param name="log">The log entry to update.</param>
    public void UpdateLog(Log log)
    {
        Receiver.UpdateLog(JsonSerializer.Serialize(log));
    }

    /// <summary>
    /// Adds a new log entry.
    /// </summary>
    /// <param name="log">The log entry to add.</param>
    public void AddLog(Log log)
    {
        Receiver.AddLog(JsonSerializer.Serialize(log));
    }

    /// <summary>
    /// Deletes a log entry.
    /// </summary>
    /// <param name="logId">The ID of the log entry to delete.</param>
    public void DeleteLog(int logId)
    {
        Receiver.DeleteLog(logId);
    }

    /// <summary>
    /// Deletes a log food item by log date and log ID.
    /// </summary>
    /// <param name="logDateID">The log date ID.</param>
    /// <param name="logID">The log ID.</param>
    public void DeleteLogFood(int logDateID, int logID)
    {
        Receiver.DeleteLogFood(JsonSerializer.Serialize((logDateID, logID), Options));
    }

    /// <summary>
    /// Deletes a log exercise item by log date and log ID.
    /// </summary>
    /// <param name="logDateID">The log date ID.</param>
    /// <param name="logID">The log ID.</param>
    public void DeleteLogExercise(int logDateID, int logID)
    {
        Receiver.DeleteLogExercise(JsonSerializer.Serialize((logDateID, logID), Options));
    }

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="numberItemOffset">The number of items to offset.</param>
    /// <param name="endDate">The end date for the logs.</param>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    public List<Log> GetLogWithPagination(int numberItemOffset, DateOnly endDate)
    {
        return JsonSerializer.Deserialize<List<Log>>(
            Receiver.GetLogWithPagination(
                JsonSerializer.Serialize((numberItemOffset, endDate), Options)
            )
        );
    }

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="n">The number of items to retrieve.</param>
    /// <param name="numberItemOffset">The number of items to offset.</param>
    /// <param name="endDate">The end date for the logs.</param>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    public List<Log> GetNLogWithPagination(int n, int numberItemOffset, DateOnly endDate)
    {
        return JsonSerializer.Deserialize<List<Log>>(
            Receiver.GetNLogWithPagination(
                JsonSerializer.Serialize((n, numberItemOffset, endDate), Options)
            )
        );
    }

    public void UpdateTotalCalories(int logId, double totalCalories)
    {
        Receiver.UpdateTotalCalories(
            JsonSerializer.Serialize((logId, totalCalories), Options)
        );
    }
}
