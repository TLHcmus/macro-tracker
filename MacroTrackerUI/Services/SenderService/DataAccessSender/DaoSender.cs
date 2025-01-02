using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace MacroTrackerUI.Services.SenderService.DataAccessSender;

/// <summary>
/// Provides methods to interact with the data access layer and perform various operations.
/// </summary>
public class DaoSender
{
    private DaoReceiver Receiver { get; } = ProviderUI.GetServiceProvider().GetService<DaoReceiver>();
    private JsonSerializerOptions Options { get; } = new() { IncludeFields = true };

    /// <summary>
    /// Retrieves a list of foods.
    /// </summary>
    /// <returns>A list of <see cref="Food"/> objects.</returns>
    public List<Food> GetFoods()
    {
        return JsonSerializer.Deserialize<List<Food>>(Receiver.GetFoods());
    }

    // Add food
    public void AddFood(Food food)
    {
        Receiver.AddFood(JsonSerializer.Serialize(food));
    }

    // Remove food
    public void RemoveFood(string foodName)
    {
        Receiver.RemoveFood(JsonSerializer.Serialize(foodName));
    }

    /// <summary>
    /// Retrieves a collection of exercises.
    /// </summary>
    /// <returns>A list of <see cref="Exercise"/> objects.</returns>
    public List<Exercise> GetExercises()
    {
        return JsonSerializer.Deserialize<List<Exercise>>(Receiver.GetExercises());
    }
    // Add exercise
    public void AddExercise(Exercise exercise)
    {
        Receiver.AddExercise(JsonSerializer.Serialize(exercise));
    }
    // Remove exercise
    public void RemoveExercise(string exerciseName)
    {
        Receiver.RemoveExercise(JsonSerializer.Serialize(exerciseName));
    }

    /// <summary>
    /// Retrieves the goal.
    /// </summary>
    /// <returns>A <see cref="Goal"/> object.</returns>
    public Goal GetGoal()
    {
        return JsonSerializer.Deserialize<Goal>(Receiver.GetGoal());
    }
    // Update goal
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
    /// <returns><c>true</c> if the username and password match; otherwise, <c>false</c>.</returns>
    public bool DoesUserMatchPassword(string username, string password)
    {
        string signInJson = JsonSerializer.Serialize((username, password), Options);
        return JsonSerializer.Deserialize<bool>(Receiver.DoesUserMatchPassword(signInJson));
    }

    /// <summary>
    /// Checks if the provided username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns><c>true</c> if the username exists; otherwise, <c>false</c>.</returns>
    public bool DoesUsernameExist(string username)
    {
        string usernameJson = JsonSerializer.Serialize(username);
        return JsonSerializer.Deserialize<bool>(Receiver.DoesUsernameExist(usernameJson));
    }

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">The user to add.</param>
    public void AddUser((string, string) user)
    {
        Receiver.AddUser(JsonSerializer.Serialize(user, Options));
    }

    /// <summary>
    /// Retrieves a list of logs.
    /// </summary>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    public List<Log> GetLogs()
    {
        return JsonSerializer.Deserialize<List<Log>>(
            Receiver.GetLogs()
        );
        //return (List<Log>)Receiver.GetLogs();
    }

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="log">The log to add.</param>
    public void AddLog(Log log)
    {
        Receiver.AddLog(JsonSerializer.Serialize(log));
    }

    /// <summary>
    /// Deletes a log by its ID.
    /// </summary>
    /// <param name="logId">The ID of the log to delete.</param>
    public void DeleteLog(int logId)
    {
        Receiver.DeleteLog(JsonSerializer.Serialize(logId));
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
        return JsonSerializer.Deserialize<List<Log>>(Receiver.GetLogWithPagination(JsonSerializer.Serialize((numberItemOffset, endDate), Options)));
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
        return JsonSerializer.Deserialize<List<Log>>(Receiver.GetNLogWithPagination(JsonSerializer.Serialize((n, numberItemOffset, endDate), Options)));
    }

    /// <summary>
    /// Updates the total calories for a log.
    /// </summary>
    /// <param name="logId">The ID of the log to update.</param>
    /// <param name="totalCalories">The new total calories.</param>
    public void UpdateTotalCalories(int logId, double totalCalories)
    {
        Receiver.UpdateTotalCalories(JsonSerializer.Serialize((logId, totalCalories), Options));
    }
}
