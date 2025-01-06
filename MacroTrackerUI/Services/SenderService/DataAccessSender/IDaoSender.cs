using System.Collections.Generic;
using System;
using MacroTrackerUI.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

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
    /// <returns>The ID of the added food item.</returns>
    int AddFood(Food food);

    /// <summary>
    /// Removes a food item by its ID.
    /// </summary>
    /// <param name="foodId">The ID of the food item to remove.</param>
    void RemoveFood(int foodId);

    /// <summary>
    /// Retrieves a food item by its ID.
    /// </summary>
    /// <param name="foodId">The ID of the food item to retrieve.</param>
    /// <returns>The <see cref="Food"/> object.</returns>
    Food GetFoodById(int foodId);

    /// <summary>
    /// Retrieves a list of all exercises.
    /// </summary>
    /// <returns>A list of <see cref="Exercise"/> objects.</returns>
    List<Exercise> GetExercises();

    /// <summary>
    /// Retrieves an exercise by its ID.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise to retrieve.</param>
    /// <returns>The <see cref="Exercise"/> object.</returns>
    Exercise GetExerciseById(int exerciseId);

    /// <summary>
    /// Adds a new exercise.
    /// </summary>
    /// <param name="exercise">The <see cref="Exercise"/> object to add.</param>
    /// <returns>The ID of the added exercise.</returns>
    int AddExercise(Exercise exercise);

    /// <summary>
    /// Removes an exercise by its ID.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise to remove.</param>
    void RemoveExercise(int exerciseId);

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
    /// Checks if a username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>True if the username exists, otherwise false.</returns>
    bool DoesUsernameExist(string username);

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">A tuple containing the username and password of the user to add.</param>
    void AddUser((string, string) user);

    /// <summary>
    /// Retrieves a list of all logs.
    /// </summary>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    List<Log> GetLogs();

    /// <summary>
    /// Retrieves a log by its date.
    /// </summary>
    /// <param name="date">The date of the log to retrieve.</param>
    /// <returns>The <see cref="Log"/> object.</returns>
    Log GetLogByDate(DateOnly date);

    /// <summary>
    /// Updates a log.
    /// </summary>
    /// <param name="log">The <see cref="Log"/> object to update.</param>
    void UpdateLog(Log log);

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="log">The <see cref="Log"/> object to add.</param>
    void AddLog(Log log);

    /// <summary>
    /// Deletes a log by its ID.
    /// </summary>
    /// <param name="logId">The ID of the log to delete.</param>
    void DeleteLog(int logId);

    /// <summary>
    /// Deletes a food item from a log by log date ID and log ID.
    /// </summary>
    /// <param name="logDateID">The log date ID.</param>
    /// <param name="logID">The log ID.</param>
    void DeleteLogFood(int logDateID, int logID);

    /// <summary>
    /// Deletes an exercise item from a log by log date ID and log ID.
    /// </summary>
    /// <param name="logDateID">The log date ID.</param>
    /// <param name="logID">The log ID.</param>
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

    public void UpdateTotalCalories(int logId, double totalCalories);
}
