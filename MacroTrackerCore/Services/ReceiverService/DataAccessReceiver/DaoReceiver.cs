using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;

/// <summary>
/// Provides methods to interact with the data access layer and perform various operations.
/// </summary>
public class DaoReceiver
{
    public ServiceProvider ServiceProvider { get; set; }

    private IDao _dao { get; set; }

    private JsonSerializerOptions Options { get; } = new() 
    { 
        IncludeFields = true,
    };

    public DaoReceiver()
    {
        ServiceProvider = ProviderCore.GetServiceProvider();
        _dao = ServiceProvider.GetRequiredService<IDao>();
    }

    public DaoReceiver(ServiceProvider serviceProvider) 
    {
        ServiceProvider = serviceProvider;
        _dao = ServiceProvider.GetRequiredService<IDao>();
    }

    /// <summary>
    /// Retrieves a list of foods.
    /// </summary>
    /// <returns>A JSON string representing a list of <see cref="Food"/> objects.</returns>
    public string GetFoods()
    {
        JsonSerializerOptions options = new()
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        return JsonSerializer.Serialize(_dao.GetFoods(), options);
    }
    // Add food
    public void AddFood(string foodJson)
    {
        Food? food = JsonSerializer.Deserialize<Food>(foodJson) 
            ?? throw new ArgumentNullException();
        _dao.AddFood(food);
    }

    // Remove food
    public void RemoveFood(string foodNameJson)
    {
        string? foodName = JsonSerializer.Deserialize<string>(foodNameJson) 
            ?? throw new ArgumentNullException();
        _dao.RemoveFood(foodName);
    }


    /// <summary>
    /// Retrieves a collection of exercises.
    /// </summary>
    /// <returns>A JSON string representing an <see cref="ObservableCollection{ExerciseInfo}"/> containing exercise information.</returns>
    public string GetExercises()
    {
        JsonSerializerOptions options = new()
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        return JsonSerializer.Serialize(_dao.GetExercises(), options);
    }
    // Add exercise
    public void AddExercise(string exerciseJson)
    {
        Exercise? exercise = JsonSerializer.Deserialize<Exercise>(exerciseJson) 
            ?? throw new ArgumentNullException();
        _dao.AddExercise(exercise);
    }
    // Remove exercise
    public void RemoveExercise(string exerciseNameJson)
    {
        string? exerciseName = JsonSerializer.Deserialize<string>(exerciseNameJson);
        if (exerciseName == null)
        {
            throw new ArgumentNullException();
        }
        _dao.RemoveExercise(exerciseName);
    }

    /// <summary>
    /// Retrieves the goal.
    /// </summary>
    /// <returns>A JSON string representing the <see cref="Goal"/> object.</returns>
    public string GetGoal()
    {
        return JsonSerializer.Serialize(_dao.GetGoal());
    }
    // Update goal
    public void UpdateGoal (string goalJson)
    {
        Goal? goal = JsonSerializer.Deserialize<Goal>(goalJson) ?? throw new ArgumentNullException();
        _dao.UpdateGoal(goal);
    }

    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <returns>A JSON string representing a list of <see cref="User"/> objects.</returns>
    public string GetUsers()
    {
        return JsonSerializer.Serialize(_dao.GetUsers());
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
        return username == null || password == null
            ? throw new ArgumentNullException()
            : JsonSerializer.Serialize(_dao.DoesUserMatchPassword(username, password));
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
        return username == null 
            ? throw new ArgumentNullException() 
            : JsonSerializer.Serialize(_dao.DoesUsernameExist(username));
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

        IPasswordEncryption passwordEncryption = 
            ServiceProvider.GetRequiredService<IPasswordEncryption>();

        _dao.AddUser(new User
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
        JsonSerializerOptions options = new()
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };
        return JsonSerializer.Serialize(
            _dao.GetLogs(),
            options
        );
    }

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="logJson">A JSON string representing the log to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when the log is null.</exception>
    public void AddLog(string logJson)
    {
        Log? log = JsonSerializer.Deserialize<Log>(logJson) ?? throw new ArgumentNullException();
        _dao.AddLog(log);
    }

    /// <summary>
    /// Deletes a log by its ID.
    /// </summary>
    /// <param name="logIdJson">A JSON string representing the ID of the log to delete.</param>
    public void DeleteLog(string logIdJson)
    {
        _dao.DeleteLog(JsonSerializer.Deserialize<int>(logIdJson));
    }

    /// <summary>
    /// Deletes a log food item by log date and log ID.
    /// </summary>
    /// <param name="idDeleteJson">A JSON string containing the log date ID and log ID.</param>
    public void DeleteLogFood(string idDeleteJson)
    {
        (int idLogDate, int idLog) = JsonSerializer.Deserialize<(int, int)>(idDeleteJson, Options);
        _dao.DeleteLogFood(idLogDate, idLog);
    }

    /// <summary>
    /// Deletes a log exercise item by log date and log ID.
    /// </summary>
    /// <param name="idDeleteJson">A JSON string containing the log date ID and log ID.</param>
    public void DeleteLogExercise(string idDeleteJson)
    {
        (int idLogDate, int idLog) = JsonSerializer.Deserialize<(int, int)>(idDeleteJson, Options);
        _dao.DeleteLogExercise(idLogDate, idLog);
    }

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="pageOffsetJson">A JSON string containing the number of items to offset and the end date.</param>
    /// <returns>A JSON string representing a list of <see cref="Log"/> objects.</returns>
    public string GetLogWithPagination(string pageOffsetJson)
    {
        JsonSerializerOptions options = new()
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        (int numberItemOffset, DateOnly endDate) = JsonSerializer.Deserialize<(int, DateOnly)>(pageOffsetJson, Options);
        return JsonSerializer.Serialize(_dao.GetLogWithPagination(numberItemOffset, endDate), options);
    }

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="pageOffsetJson">A JSON string containing the number of items to retrieve, the number of items to offset, and the end date.</param>
    /// <returns>A JSON string representing a list of <see cref="Log"/> objects.</returns>
    public string GetNLogWithPagination(string pageOffsetJson)
    {
        JsonSerializerOptions options = new()
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        (int n, int numberItemOffset, DateOnly endDate) =
            JsonSerializer.Deserialize<(int, int, DateOnly)>(pageOffsetJson, Options);
        return JsonSerializer.Serialize(_dao.GetLogWithPagination(n, numberItemOffset, endDate), options);
    }

    /// <summary>
    /// Updates the total calories for a log.
    /// </summary>
    /// <param name="logIdJson">A JSON string containing the log ID and the new total calories.</param>
    public void UpdateTotalCalories(string logIdJson)
    {
        (int logId, double totalCalories) = JsonSerializer.Deserialize<(int, double)>(logIdJson, Options);
        _dao.UpdateTotalCalories(logId, totalCalories);
    }
}
