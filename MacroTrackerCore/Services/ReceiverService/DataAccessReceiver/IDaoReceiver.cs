using MacroTrackerCore.Services.DataAccessService;
using Microsoft.Extensions.Options;

using System.Text.Json;

/// <summary>
/// Interface for data access operations related to the receiver service.
/// </summary>
public interface IDaoReceiver
{
    /// <summary>
    /// Retrieves all foods.
    /// </summary>
    /// <returns>A JSON string representing all foods.</returns>
    string GetFoods();

    /// <summary>
    /// Adds a new food.
    /// </summary>
    /// <param name="foodJson">A JSON string representing the food to add.</param>
    /// <returns>The ID of the added food.</returns>
    int AddFood(string foodJson);

    /// <summary>
    /// Removes a food.
    /// </summary>
    /// <param name="foodIdJson">A JSON string representing the ID of the food to remove.</param>
    void RemoveFood(string foodIdJson);

    /// <summary>
    /// Retrieves a food by its ID.
    /// </summary>
    /// <param name="foodId">The ID of the food to retrieve.</param>
    /// <returns>A JSON string representing the food.</returns>
    string GetFoodById(int foodId);

    /// <summary>
    /// Retrieves all exercises.
    /// </summary>
    /// <returns>A JSON string representing all exercises.</returns>
    string GetExercises();

    /// <summary>
    /// Retrieves an exercise by its ID.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise to retrieve.</param>
    /// <returns>A JSON string representing the exercise.</returns>
    string GetExerciseById(int exerciseId);

    /// <summary>
    /// Adds a new exercise.
    /// </summary>
    /// <param name="exerciseJson">A JSON string representing the exercise to add.</param>
    /// <returns>The ID of the added exercise.</returns>
    int AddExercise(string exerciseJson);

    /// <summary>
    /// Removes an exercise.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise to remove.</param>
    void RemoveExercise(int exerciseId);

    /// <summary>
    /// Retrieves the goal.
    /// </summary>
    /// <returns>A JSON string representing the goal.</returns>
    string GetGoal();

    /// <summary>
    /// Updates the goal.
    /// </summary>
    /// <param name="goalJson">A JSON string representing the goal to update.</param>
    void UpdateGoal(string goalJson);

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A JSON string representing all users.</returns>
    string GetUsers();

    /// <summary>
    /// Checks if the username and password match.
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
    /// <param name="user">A tuple containing the username and password of the user to add.</param>
    void AddUser((string, string) user);

    /// <summary>
    /// Retrieves all logs.
    /// </summary>
    /// <returns>A JSON string representing all logs.</returns>
    string GetLogs();

    /// <summary>
    /// Retrieves a log by its date.
    /// </summary>
    /// <param name="date">The date of the log to retrieve.</param>
    /// <returns>A JSON string representing the log.</returns>
    string GetLogByDate(DateOnly date);

    /// <summary>
    /// Updates a log.
    /// </summary>
    /// <param name="logJson">A JSON string representing the log to update.</param>
    void UpdateLog(string logJson);

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="logJson">A JSON string representing the log to add.</param>
    void AddLog(string logJson);

    /// <summary>
    /// Deletes a log.
    /// </summary>
    /// <param name="logId">The ID of the log to delete.</param>
    void DeleteLog(int logId);

    /// <summary>
    /// Deletes a food from a log.
    /// </summary>
    /// <param name="idDeleteJson">A JSON string representing the ID of the food to delete from the log.</param>
    void DeleteLogFood(string idDeleteJson);

    /// <summary>
    /// Deletes an exercise from a log.
    /// </summary>
    /// <param name="idDeleteJson">A JSON string representing the ID of the exercise to delete from the log.</param>
    void DeleteLogExercise(string idDeleteJson);

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="pageOffsetJson">A JSON string representing the page offset for pagination.</param>
    /// <returns>A JSON string representing the logs with pagination.</returns>
    string GetLogWithPagination(string pageOffsetJson);

    /// <summary>
    /// Retrieves N logs with pagination.
    /// </summary>
    /// <param name="pageOffsetJson">A JSON string representing the page offset for pagination.</param>
    /// <returns>A JSON string representing the N logs with pagination.</returns>
    string GetNLogWithPagination(string pageOffsetJson);

    public void UpdateTotalCalories(string logIdJson);
}
