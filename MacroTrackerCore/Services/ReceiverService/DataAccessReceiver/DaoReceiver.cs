using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;

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
        return JsonSerializer.Serialize(Dao.GetFoods(), Options);
    }

    /// <summary>
    /// Retrieves a collection of exercises.
    /// </summary>
    /// <returns>A JSON string representing an <see cref="ObservableCollection{ExerciseInfo}"/> containing exercise information.</returns>
    public string GetExercises()
    {
        return JsonSerializer.Serialize(Dao.GetExercises(), Options);
    }

    // Get Goal
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
    /// <param name="username">The username to check.</param>
    /// <param name="password">The password to check.</param>
    /// <returns>
    /// A JSON string representing <c>true</c> if the username and password match; otherwise, <c>false</c>.
    /// </returns>
    public string DoesUserMatchPassword(string userJson)
    {
        (string username, string password) = 
            JsonSerializer.Deserialize<(string, string)>(userJson, Options);
        if (username == null || password == null)
        {
            throw new ArgumentNullException();
        }

        return JsonSerializer.Serialize(Dao.DoesUserMatchPassword(username, password));
    }

    /// <summary>
    /// Checks if the provided username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>
    /// A JSON string representing <c>true</c> if the username exists; otherwise, <c>false</c>.
    /// </returns>
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
    /// <param name="user">The user to add.</param>
    public void AddUser(string userJson)
    {
        (string username, string password) = JsonSerializer.Deserialize<(string, string)>(userJson, Options);
        if (username == null || password == null)
        {
            throw new ArgumentNullException();
        }

        IPasswordEncryption passwordEncryption =
            ProviderCore.GetServiceProvider().GetRequiredService<IPasswordEncryption>();

        Dao.AddUser(new User
            {
                Username = username,
                EncryptedPassword = passwordEncryption.EncryptPasswordToDatabase(password)
            }
        );
    }

    public string GetLogs()
    {
        return JsonSerializer.Serialize(
            Dao.GetLogs(),
            Options
        );
    }

    public void AddLog(string logJson)
    {
        Log? log = JsonSerializer.Deserialize<Log>(logJson);
        if (log == null)
        {
            throw new ArgumentNullException();
        }
        Dao.AddLog(log);
    }

    public void DeleteLog(string logIdJson)
    {
        Dao.DeleteLog(JsonSerializer.Deserialize<int>(logIdJson));
    }

    public void DeleteLogFood(string idDeleteJson)
    {
        (int idLogDate, int idLog) = JsonSerializer.Deserialize<(int, int)>(idDeleteJson, Options);
        Dao.DeleteLogFood(idLogDate, idLog);
    }

    public void DeleteLogExercise(string idDeleteJson)
    {
        (int idLogDate, int idLog) = JsonSerializer.Deserialize<(int, int)>(idDeleteJson, Options);
        Dao.DeleteLogExercise(idLogDate, idLog);
    }

    public string GetLogWithPagination(string pageOffsetJson)
    {
        (int numberItemOffset, DateOnly endDate) = JsonSerializer.Deserialize<(int, DateOnly)>(pageOffsetJson, Options);
        return JsonSerializer.Serialize(Dao.GetLogWithPagination(numberItemOffset, endDate));
    }

    public string GetNLogWithPagination(string pageOffsetJson)
    {
        (int n, int numberItemOffset, DateOnly endDate) =
            JsonSerializer.Deserialize<(int, int, DateOnly)>(pageOffsetJson, Options);
        return JsonSerializer.Serialize(Dao.GetLogWithPagination(n, numberItemOffset, endDate));
    }

    public void UpdateTotalCalories(string logIdJson)
    {
        (int logId, double totalCalories) = JsonSerializer.Deserialize<(int, double)>(logIdJson, Options);
        Dao.UpdateTotalCalories(logId, totalCalories);
    }
}
