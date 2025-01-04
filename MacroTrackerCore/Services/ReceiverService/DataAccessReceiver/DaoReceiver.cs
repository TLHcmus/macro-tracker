using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;

/// <summary>
/// Provides methods to interact with the data access layer and perform various operations.
/// </summary>
public class DaoReceiver : IDaoReceiver
{
    public ServiceProvider ServiceProvider { get; private set; }
    public IDao Dao { get; private set; }

    private JsonSerializerOptions Options { get; } = new()
    {
        IncludeFields = true,
    };

    public DaoReceiver()
    {
        ServiceProvider = ProviderCore.GetServiceProvider();
        Dao = ServiceProvider.GetRequiredService<IDao>();
    }

    public DaoReceiver(ServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Dao = ServiceProvider.GetRequiredService<IDao>();
    }

    /// <summary>
    /// Retrieves a list of foods.
    /// </summary>
    /// <returns>A JSON string representing a list of <see cref="Food"/> objects.</returns>
    public string GetFoods()
    {
        return JsonSerializer.Serialize(Dao.GetFoods(), GetJsonSerializerOptions());
    }

    /// <summary>
    /// Adds a new food item.
    /// </summary>
    /// <param name="foodJson">A JSON string representing the food to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when the food is null.</exception>
    public void AddFood(string foodJson)
    {
        Food? food = JsonSerializer.Deserialize<Food>(foodJson) 
            ?? throw new ArgumentNullException();
        Dao.AddFood(food);
    }

    /// <summary>
    /// Removes a food item by name.
    /// </summary>
    /// <param name="foodNameJson">A JSON string representing the name of the food to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when the food name is null.</exception>
    public void RemoveFood(string foodNameJson)
    {
        string? foodName = JsonSerializer.Deserialize<string>(foodNameJson) 
            ?? throw new ArgumentNullException();
        Dao.RemoveFood(foodName);
    }

    /// <summary>
    /// Retrieves a collection of exercises.
    /// </summary>
    /// <returns>A JSON string representing an <see cref="ObservableCollection{ExerciseInfo}"/> containing exercise information.</returns>
    public string GetExercises()
    {
        return JsonSerializer.Serialize(Dao.GetExercises(), GetJsonSerializerOptions());
    }

    /// <summary>
    /// Adds a new exercise.
    /// </summary>
    /// <param name="exerciseJson">A JSON string representing the exercise to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when the exercise is null.</exception>
    public void AddExercise(string exerciseJson)
    {
        Exercise? exercise = JsonSerializer.Deserialize<Exercise>(exerciseJson) 
            ?? throw new ArgumentNullException();
        Dao.AddExercise(exercise);
    }

    /// <summary>
    /// Removes an exercise by name.
    /// </summary>
    /// <param name="exerciseNameJson">A JSON string representing the name of the exercise to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when the exercise name is null.</exception>
    public void RemoveExercise(string exerciseNameJson)
    {
        string? exerciseName = JsonSerializer.Deserialize<string>(exerciseNameJson) 
            ?? throw new ArgumentNullException();
        Dao.RemoveExercise(exerciseName);
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
    /// Updates the goal.
    /// </summary>
    /// <param name="goalJson">A JSON string representing the goal to update.</param>
    /// <exception cref="ArgumentNullException">Thrown when the goal is null.</exception>
    public void UpdateGoal(string goalJson)
    {
        Goal? goal = JsonSerializer.Deserialize<Goal>(goalJson) 
            ?? throw new ArgumentNullException();
        Dao.UpdateGoal(goal);
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
        string? username = JsonSerializer.Deserialize<string>(usernameJson) 
            ?? throw new ArgumentNullException();
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

        IPasswordEncryption passwordEncryption = ServiceProvider.GetRequiredService<IPasswordEncryption>();
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
        return JsonSerializer.Serialize(Dao.GetLogs(), GetJsonSerializerOptions());
    }

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="logJson">A JSON string representing the log to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when the log is null.</exception>
    public void AddLog(string logJson)
    {
        Log? log = JsonSerializer.Deserialize<Log>(logJson) 
            ?? throw new ArgumentNullException();
        Dao.AddLog(log);
    }

    /// <summary>
    /// Deletes a log by its ID.
    /// </summary>
    /// <param name="logIdJson">A JSON string representing the ID of the log to delete.</param>
    public void DeleteLog(string logIdJson)
    {
        int logId = JsonSerializer.Deserialize<int>(logIdJson);
        Dao.DeleteLog(logId);
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
        return JsonSerializer.Serialize(Dao.GetLogWithPagination(numberItemOffset, endDate), GetJsonSerializerOptions());
    }

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="pageOffsetJson">A JSON string containing the number of items to retrieve, the number of items to offset, and the end date.</param>
    /// <returns>A JSON string representing a list of <see cref="Log"/> objects.</returns>
    public string GetNLogWithPagination(string pageOffsetJson)
    {
        (int n, int numberItemOffset, DateOnly endDate) = JsonSerializer.Deserialize<(int, int, DateOnly)>(pageOffsetJson, Options);
        return JsonSerializer.Serialize(Dao.GetLogWithPagination(n, numberItemOffset, endDate), GetJsonSerializerOptions());
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

    /// <summary>
    /// Gets the JSON serializer options.
    /// </summary>
    /// <returns>The JSON serializer options.</returns>
    private JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };
    }
}
