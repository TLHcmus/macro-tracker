using System.Collections.Generic;
using System;
using MacroTrackerUI.Models;

namespace MacroTrackerUI.Services.SenderService.DataAccessSender;

/// <summary>
/// Interface for data access operations related to sending data.
/// </summary>
public interface IDaoSender
{
    /// <summary>
    /// Retrieves a list of all foods.
    /// </summary>
    /// <returns>A list of <see cref="Food"/> objects.</returns>
    List<Food> GetFoods();

    /// <summary>
    /// Adds a new food item.
    /// </summary>
    /// <param name="food">The <see cref="Food"/> object to add.</param>
    void AddFood(Food food);

    /// <summary>
    /// Removes a food item by name.
    /// </summary>
    /// <param name="foodName">The name of the food to remove.</param>
    void RemoveFood(string foodName);

    /// <summary>
    /// Retrieves a list of all exercises.
    /// </summary>
    /// <returns>A list of <see cref="Exercise"/> objects.</returns>
    List<Exercise> GetExercises();

    /// <summary>
    /// Adds a new exercise.
    /// </summary>
    /// <param name="exercise">The <see cref="Exercise"/> object to add.</param>
    void AddExercise(Exercise exercise);

    /// <summary>
    /// Removes an exercise by name.
    /// </summary>
    /// <param name="exerciseName">The name of the exercise to remove.</param>
    void RemoveExercise(string exerciseName);

    /// <summary>
    /// Retrieves the current goal.
    /// </summary>
    /// <returns>The <see cref="Goal"/> object.</returns>
    Goal GetGoal();

    /// <summary>
    /// Updates the current goal.
    /// </summary>
    /// <param name="goal">The <see cref="Goal"/> object to update.</param>
    void UpdateGoal(Goal goal);

    /// <summary>
    /// Retrieves a list of all users.
    /// </summary>
    /// <returns>A list of <see cref="User"/> objects.</returns>
    List<User> GetUsers();

    /// <summary>
    /// Checks if the provided username and password match.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <param name="password">The password to check.</param>
    /// <returns>True if the username and password match, otherwise false.</returns>
    bool DoesUserMatchPassword(string username, string password);

    /// <summary>
    /// Checks if the username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>True if the username exists, otherwise false.</returns>
    bool DoesUsernameExist(string username);

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">A tuple containing the username and password.</param>
    void AddUser((string username, string password) user);

    /// <summary>
    /// Retrieves a list of all logs.
    /// </summary>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    List<Log> GetLogs();

    /// <summary>
    /// Adds a new log entry.
    /// </summary>
    /// <param name="log">The <see cref="Log"/> object to add.</param>
    void AddLog(Log log);

    /// <summary>
    /// Deletes a log entry by ID.
    /// </summary>
    /// <param name="logId">The ID of the log to delete.</param>
    void DeleteLog(int logId);

    /// <summary>
    /// Deletes a food item from a log entry.
    /// </summary>
    /// <param name="logDateID">The date ID of the log.</param>
    /// <param name="logID">The ID of the log.</param>
    void DeleteLogFood(int logDateID, int logID);

    /// <summary>
    /// Deletes an exercise item from a log entry.
    /// </summary>
    /// <param name="logDateID">The date ID of the log.</param>
    /// <param name="logID">The ID of the log.</param>
    void DeleteLogExercise(int logDateID, int logID);

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="numberItemOffset">The number of items to offset.</param>
    /// <param name="endDate">The end date for the logs.</param>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    List<Log> GetLogWithPagination(int numberItemOffset, DateOnly endDate);

    /// <summary>
    /// Retrieves a specified number of logs with pagination.
    /// </summary>
    /// <param name="n">The number of logs to retrieve.</param>
    /// <param name="numberItemOffset">The number of items to offset.</param>
    /// <param name="endDate">The end date for the logs.</param>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    List<Log> GetNLogWithPagination(int n, int numberItemOffset, DateOnly endDate);

    /// <summary>
    /// Updates the total calories for a log entry.
    /// </summary>
    /// <param name="logId">The ID of the log to update.</param>
    /// <param name="totalCalories">The new total calories value.</param>
    void UpdateTotalCalories(int logId, double totalCalories);
}
