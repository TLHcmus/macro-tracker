using System.Collections.ObjectModel;
using MacroTrackerCore.Entities;

/// <summary>
/// Interface for Data Access Object (DAO) services
/// </summary>
namespace MacroTrackerCore.Services.DataAccessService;
public interface IDao
{
    /// <summary>
    /// Retrieves a list of foods.
    /// </summary>
    /// <returns>A list of <see cref="Food"/> objects.</returns>
    List<Food> GetFoods();

    /// <summary>
    /// Adds a new food item.
    /// </summary>
    /// <param name="food">The food item to add.</param>
    void AddFood(Food food);

    /// <summary>
    /// Removes a food item by name.
    /// </summary>
    /// <param name="foodName">The name of the food item to remove.</param>
    void RemoveFood(string foodName);

    /// <summary>
    /// Retrieves a collection of exercises.
    /// </summary>
    /// <returns>A list of <see cref="Exercise"/> objects.</returns>
    List<Exercise> GetExercises();

    /// <summary>
    /// Adds a new exercise.
    /// </summary>
    /// <param name="exercise">The exercise to add.</param>
    void AddExercise(Exercise exercise);

    /// <summary>
    /// Removes an exercise by name.
    /// </summary>
    /// <param name="exerciseName">The name of the exercise to remove.</param>
    void RemoveExercise(string exerciseName);

    /// <summary>
    /// Retrieves the goal.
    /// </summary>
    /// <returns>A <see cref="Goal"/> object.</returns>
    Goal GetGoal();

    /// <summary>
    /// Updates the goal.
    /// </summary>
    /// <param name="goal">The goal to update.</param>
    void UpdateGoal(Goal goal);

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
    /// <returns><c>true</c> if the username and password match; otherwise, <c>false</c>.</returns>
    bool DoesUserMatchPassword(string username, string password);

    /// <summary>
    /// Checks if the provided username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns><c>true</c> if the username exists; otherwise, <c>false</c>.</returns>
    bool DoesUsernameExist(string username);

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">The user to add.</param>
    void AddUser(User user);

    /// <summary>
    /// Retrieves a list of logs.
    /// </summary>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    List<Log> GetLogs();

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="log">The log to add.</param>
    void AddLog(Log log);

    /// <summary>
    /// Deletes a log by its ID.
    /// </summary>
    /// <param name="logId">The ID of the log to delete.</param>
    void DeleteLog(int logId);

    /// <summary>
    /// Deletes a log food item by log date and log ID.
    /// </summary>
    /// <param name="idLogDate">The log date ID.</param>
    /// <param name="idLog">The log ID.</param>
    void DeleteLogFood(int idLogDate, int idLog);

    /// <summary>
    /// Deletes a log exercise item by log date and log ID.
    /// </summary>
    /// <param name="idLogDate">The log date ID.</param>
    /// <param name="idLog">The log ID.</param>
    void DeleteLogExercise(int idLogDate, int idLog);

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="numberItemOffset">The number of items to offset.</param>
    /// <param name="endDate">The end date for the logs.</param>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    List<Log> GetLogWithPagination(int numberItemOffset, DateOnly endDate);

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="n">The number of items to retrieve.</param>
    /// <param name="numberItemOffset">The number of items to offset.</param>
    /// <param name="endDate">The end date for the logs.</param>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    List<Log> GetLogWithPagination(int n, int numberItemOffset, DateOnly endDate);

    /// <summary>
    /// Updates the total calories for a log.
    /// </summary>
    /// <param name="logId">The ID of the log to update.</param>
    /// <param name="totalCalories">The new total calories.</param>
    void UpdateTotalCalories(int logId, double totalCalories);
}
