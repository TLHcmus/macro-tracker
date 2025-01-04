namespace MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;

/// <summary>
/// Interface for data access operations related to the receiver service.
/// </summary>
public interface IDaoReceiver
{
    /// <summary>
    /// Retrieves all foods.
    /// </summary>
    /// <returns>A JSON string representing the foods.</returns>
    string GetFoods();

    /// <summary>
    /// Adds a new food.
    /// </summary>
    /// <param name="foodJson">A JSON string representing the food to add.</param>
    void AddFood(string foodJson);

    /// <summary>
    /// Removes a food.
    /// </summary>
    /// <param name="foodNameJson">A JSON string representing the name of the food to remove.</param>
    void RemoveFood(string foodNameJson);

    /// <summary>
    /// Retrieves all exercises.
    /// </summary>
    /// <returns>A JSON string representing the exercises.</returns>
    string GetExercises();

    /// <summary>
    /// Adds a new exercise.
    /// </summary>
    /// <param name="exerciseJson">A JSON string representing the exercise to add.</param>
    void AddExercise(string exerciseJson);

    /// <summary>
    /// Removes an exercise.
    /// </summary>
    /// <param name="exerciseNameJson">A JSON string representing the name of the exercise to remove.</param>
    void RemoveExercise(string exerciseNameJson);

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
    /// <returns>A JSON string representing the users.</returns>
    string GetUsers();

    /// <summary>
    /// Checks if the user matches the password.
    /// </summary>
    /// <param name="userJson">A JSON string representing the user.</param>
    /// <returns>A JSON string indicating if the user matches the password.</returns>
    string DoesUserMatchPassword(string userJson);

    /// <summary>
    /// Checks if the username exists.
    /// </summary>
    /// <param name="usernameJson">A JSON string representing the username.</param>
    /// <returns>A JSON string indicating if the username exists.</returns>
    string DoesUsernameExist(string usernameJson);

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="userJson">A JSON string representing the user to add.</param>
    void AddUser(string userJson);

    /// <summary>
    /// Retrieves all logs.
    /// </summary>
    /// <returns>A JSON string representing the logs.</returns>
    string GetLogs();

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="logJson">A JSON string representing the log to add.</param>
    void AddLog(string logJson);

    /// <summary>
    /// Deletes a log.
    /// </summary>
    /// <param name="logIdJson">A JSON string representing the ID of the log to delete.</param>
    void DeleteLog(string logIdJson);

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

    /// <summary>
    /// Updates the total calories for a log.
    /// </summary>
    /// <param name="logIdJson">A JSON string representing the ID of the log to update.</param>
    void UpdateTotalCalories(string logIdJson);
}
