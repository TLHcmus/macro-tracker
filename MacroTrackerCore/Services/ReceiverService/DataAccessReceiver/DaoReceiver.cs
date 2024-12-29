using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;

/// <summary>
/// Provides methods to interact with the data access layer and perform various operations.
/// </summary>
public class DaoReceiver
{
    private IDao Dao { get; } = ProviderCore.GetServiceProvider().GetRequiredService<IDao>();
    private JsonSerializerOptions Options { get; } = new() { IncludeFields = true };

    /// <summary>
    /// Retrieves a list of foods.
    /// </summary>
    /// <returns>A JSON string representing a list of <see cref="Food"/> objects.</returns>
    public string GetFoods()
    {
        return JsonSerializer.Serialize(Dao.GetFoods());
    }

    /// <summary>
    /// Retrieves a collection of exercises.
    /// </summary>
    /// <returns>A JSON string representing an <see cref="ObservableCollection{ExerciseInfo}"/> containing exercise information.</returns>
    public string GetExercises()
    {
        return JsonSerializer.Serialize(Dao.GetExercises());
    }

    /// <summary>
    /// Retrieves the goal.
    /// </summary>
    /// <returns>A JSON string representing the <see cref="Goal"/> object.</returns>
    public string GetGoal()
    {
        return JsonSerializer.Serialize(Dao.GetGoal());
    }

    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <returns>A JSON string representing a list of <see cref="User"/> objects.</returns>
    public string GetUsers()
    {
        return JsonSerializer.Serialize(Dao.GetUsers());
    }

    /// <summary>
    /// Checks if the provided username and password match.
    /// </summary>
    /// <param name="userJson">A JSON string containing the username and password.</param>
    /// <returns>A JSON string representing <c>true</c> if the username and password match; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the username or password is null.</exception>
    public string DoesUserMatchPassword(string userJson)
    {
        (string username, string password) = JsonSerializer.Deserialize<(string, string)>(userJson, Options);
        if (username == null || password == null)
        {
            throw new ArgumentNullException();
        }

        return JsonSerializer.Serialize(Dao.DoesUserMatchPassword(username, password));
    }

    /// <summary>
    /// Checks if the provided username exists.
    /// </summary>
    /// <param name="usernameJson">A JSON string containing the username.</param>
    /// <returns>A JSON string representing <c>true</c> if the username exists; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the username is null.</exception>
    public string DoesUsernameExist(string usernameJson)
    {
        string? username = JsonSerializer.Deserialize<string>(usernameJson);
        if (username == null)
        {
            throw new ArgumentNullException();
        }

        return JsonSerializer.Serialize(Dao.DoesUsernameExist(username));
    }

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="userJson">A JSON string containing the username and password.</param>
    /// <exception cref="ArgumentNullException">Thrown when the username or password is null.</exception>
    public void AddUser(string userJson)
    {
        (string username, string password) = JsonSerializer.Deserialize<(string, string)>(userJson, Options);
        if (username == null || password == null)
        {
            throw new ArgumentNullException();
        }

        IPasswordEncryption passwordEncryption = ProviderCore.GetServiceProvider().GetRequiredService<IPasswordEncryption>();

        Dao.AddUser(new User
        {
            Username = username,
            EncryptedPassword = passwordEncryption.EncryptPasswordToDatabase(password)
        });
    }

    /// <summary>
    /// Retrieves a list of logs.
    /// </summary>
    /// <returns>A JSON string representing a list of <see cref="Log"/> objects.</returns>
    public string GetLogs()
    {
        return JsonSerializer.Serialize(Dao.GetLogs(), Options);
    }

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="logJson">A JSON string representing the log to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when the log is null.</exception>
    public void AddLog(string logJson)
    {
        Log? log = JsonSerializer.Deserialize<Log>(logJson);
        if (log == null)
        {
            throw new ArgumentNullException();
        }
        Dao.AddLog(log);
    }

    /// <summary>
    /// Deletes a log by its ID.
    /// </summary>
    /// <param name="logIdJson">A JSON string representing the ID of the log to delete.</param>
    public void DeleteLog(string logIdJson)
    {
        Dao.DeleteLog(JsonSerializer.Deserialize<int>(logIdJson));
    }

    /// <summary>
    /// Deletes a log food item by log date and log ID.
    /// </summary>
    /// <param name="idDeleteJson">A JSON string containing the log date ID and log ID.</param>
    public void DeleteLogFood(string idDeleteJson)
    {
        (int idLogDate, int idLog) = JsonSerializer.Deserialize<(int, int)>(idDeleteJson, Options);
        Dao.DeleteLogFood(idLogDate, idLog);
    }

    /// <summary>
    /// Deletes a log exercise item by log date and log ID.
    /// </summary>
    /// <param name="idDeleteJson">A JSON string containing the log date ID and log ID.</param>
    public void DeleteLogExercise(string idDeleteJson)
    {
        (int idLogDate, int idLog) = JsonSerializer.Deserialize<(int, int)>(idDeleteJson, Options);
        Dao.DeleteLogExercise(idLogDate, idLog);
    }

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="pageOffsetJson">A JSON string containing the number of items to offset and the end date.</param>
    /// <returns>A JSON string representing a list of <see cref="Log"/> objects.</returns>
    public string GetLogWithPagination(string pageOffsetJson)
    {
        (int numberItemOffset, DateOnly endDate) = JsonSerializer.Deserialize<(int, DateOnly)>(pageOffsetJson, Options);
        return JsonSerializer.Serialize(Dao.GetLogWithPagination(numberItemOffset, endDate));
    }

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="pageOffsetJson">A JSON string containing the number of items to retrieve, the number of items to offset, and the end date.</param>
    /// <returns>A JSON string representing a list of <see cref="Log"/> objects.</returns>
    public string GetNLogWithPagination(string pageOffsetJson)
    {
        (int n, int numberItemOffset, DateOnly endDate) = JsonSerializer.Deserialize<(int, int, DateOnly)>(pageOffsetJson, Options);
        return JsonSerializer.Serialize(Dao.GetLogWithPagination(n, numberItemOffset, endDate));
    }

    /// <summary>
    /// Updates the total calories for a log.
    /// </summary>
    /// <param name="logIdJson">A JSON string containing the log ID and the new total calories.</param>
    public void UpdateTotalCalories(string logIdJson)
    {
        (int logId, double totalCalories) = JsonSerializer.Deserialize<(int, double)>(logIdJson, Options);
        Dao.UpdateTotalCalories(logId, totalCalories);
    }
}
