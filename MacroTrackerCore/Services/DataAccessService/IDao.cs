﻿using System.Collections.ObjectModel;
using MacroTrackerCore.Entities;

/// <summary>
/// Interface for Data Access Object (DAO) services
/// </summary>
namespace MacroTrackerCore.Services.DataAccessService;
public interface IDao
{

    // Food

    /// <summary>
    /// Retrieves a list of foods.
    /// </summary>
    /// <returns>A list of <see cref="Food"/> objects.</returns>
    List<Food> GetFoods();

    // Add food
    void AddFood(Food food);

    // Remove food
    void RemoveFood(string foodName);

    // Exercise

    /// <summary>
    /// Retrieves a collection of exercises.
    /// </summary>
    /// <returns>An <see cref="ObservableCollection{ExerciseInfo}"/> containing exercise information.</returns>
    List<Exercise> GetExercises();
    
    // Add exercise
    void AddExercise(Exercise exercise);
    // Remove exercise
    void RemoveExercise(string exerciseName);


    // Goal

    // Get goal
    Goal GetGoal();
    // Update goal
    void UpdateGoal(Goal goal);



    // User

    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <returns>A list of <see cref="User"/> objects.</returns>
    List<User> GetUsers();

    /// <summary>
    /// Checks if the provided username and password match.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <param name="password">The password to check.</param>
    /// <returns>
    /// <c>true</c> if the username and password match; otherwise, <c>false</c>.
    /// </returns>
    bool DoesUserMatchPassword(string username, string password);

    /// <summary>
    /// Checks if the provided username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>
    /// <c>true</c> if the username exists; otherwise, <c>false</c>.
    /// </returns>
    bool DoesUsernameExist(string username);

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">The user to add.</param>
    void AddUser(User user);

    // Get Logs
    List<Log> GetLogs();
    // Add log
    void AddLog(Log log);
    // Delete log
    void DeleteLog(int logId);

    void DeleteLogFood(int idLogDate, int idLog);

    void DeleteLogExercise(int idLogDate, int idLog);

    List<Log> GetLogWithPagination(int numberItemOffset, DateOnly endDate);
    List<Log> GetLogWithPagination(int n, int numberItemOffset, DateOnly endDate);
    void UpdateTotalCalories(int logId, double totalCalories);
}
