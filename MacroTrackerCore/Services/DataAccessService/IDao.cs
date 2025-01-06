using System.Collections.ObjectModel;
using MacroTrackerCore.DTOs;
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
    /// <returns>A list of <see cref="FoodDTO"/> objects.</returns>
    List<FoodDTO> GetFoods();

    /// <summary>
    /// Adds a new food item.
    /// </summary>
    /// <param name="food">The food item to add.</param>
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
    /// <returns>A <see cref="FoodDTO"/> object.</returns>
    FoodDTO GetFoodById(int foodId);

    /// <summary>
    /// Retrieves an exercise item by its ID.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise item to retrieve.</param>
    /// <returns>An <see cref="ExerciseDTO"/> object.</returns>
    ExerciseDTO GetExerciseById(int exerciseId);

    /// <summary>
    /// Retrieves a collection of exercises.
    /// </summary>
    /// <returns>A list of <see cref="ExerciseDTO"/> objects.</returns>
    List<ExerciseDTO> GetExercises();

    /// <summary>
    /// Adds a new exercise item.
    /// </summary>
    /// <param name="exercise">The exercise item to add.</param>
    /// <returns>The ID of the added exercise item.</returns>
    int AddExercise(Exercise exercise);

    /// <summary>
    /// Removes an exercise item by its ID.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise item to remove.</param>
    void RemoveExercise(int exerciseId);

    /// <summary>
    /// Retrieves the current goal.
    /// </summary>
    /// <returns>A <see cref="GoalDTO"/> object.</returns>
    GoalDTO GetGoal();

    /// <summary>
    /// Updates the current goal.
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

    /// <summary>
    /// Retrieves a list of logs.
    /// </summary>
    /// <returns>A list of <see cref="LogDTO"/> objects.</returns>
    List<LogDTO> GetLogs();

    /// <summary>
    /// Adds a new log entry.
    /// </summary>
    /// <param name="log">The log entry to add.</param>
    void AddLog(Log log);

    /// <summary>
    /// Deletes a log entry by its ID.
    /// </summary>
    /// <param name="logId">The ID of the log entry to delete.</param>
    void DeleteLog(int logId);

    /// <summary>
    /// Retrieves a log entry by its date.
    /// </summary>
    /// <param name="date">The date of the log entry to retrieve.</param>
    /// <returns>A <see cref="LogDTO"/> object.</returns>
    LogDTO GetLogByDate(DateOnly date);

    /// <summary>
    /// Updates an existing log entry.
    /// </summary>
    /// <param name="log">The log entry to update.</param>
    void UpdateLog(Log log);

    /// <summary>
    /// Deletes a food item from a log entry.
    /// </summary>
    /// <param name="idLogDate">The date ID of the log entry.</param>
    /// <param name="idLog">The ID of the log entry.</param>
    void DeleteLogFood(int idLogDate, int idLog);

    /// <summary>
    /// Deletes an exercise item from a log entry.
    /// </summary>
    /// <param name="idLogDate">The date ID of the log entry.</param>
    /// <param name="idLog">The ID of the log entry.</param>
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
    /// Updates the total calories for a log entry.
    /// </summary>
    /// <param name="logId">The ID of the log entry.</param>
    /// <param name="totalCalories">The total calories to update.</param>
    void UpdateTotalCalories(int logId, double totalCalories);
}
