using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        return JsonSerializer.Serialize(Dao.GetFoods());
    }

    /// <summary>
    /// Adds a new food item.
    /// </summary>
    /// <param name="foodJson">A JSON string representing the food to add.</param>
    /// <returns>The ID of the added food item.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the food is null.</exception>
    public int AddFood(string foodJson)
    {
        Food? food = JsonSerializer.Deserialize<Food>(foodJson);
        return food == null ? throw new ArgumentNullException() : Dao.AddFood(food);
    }

    /// <summary>
    /// Removes a food item.
    /// </summary>
    /// <param name="foodIdJson">A JSON string representing the ID of the food to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when the food ID is invalid.</exception>
    public void RemoveFood(string foodIdJson)
    {
        int foodId = JsonSerializer.Deserialize<int>(foodIdJson);
        if (foodId <= 0)
        {
            throw new ArgumentNullException();
        }
        Dao.RemoveFood(foodId);
    }

    /// <summary>
    /// Retrieves a food item by its ID.
    /// </summary>
    /// <param name="foodId">The ID of the food item to retrieve.</param>
    /// <returns>A JSON string representing the <see cref="FoodDTO"/> object.</returns>
    public string GetFoodById(int foodId)
    {
        return JsonSerializer.Serialize(Dao.GetFoodById(foodId));
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
    /// Retrieves an exercise item by its ID.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise item to retrieve.</param>
    /// <returns>A JSON string representing the <see cref="ExerciseDTO"/> object.</returns>
    public string GetExerciseById(int exerciseId)
    {
        return JsonSerializer.Serialize(Dao.GetExerciseById(exerciseId));
    }

    /// <summary>
    /// Adds a new exercise item.
    /// </summary>
    /// <param name="exerciseJson">A JSON string representing the exercise to add.</param>
    /// <returns>The ID of the added exercise item.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the exercise is null.</exception>
    public int AddExercise(string exerciseJson)
    {
        Exercise? exercise = JsonSerializer.Deserialize<Exercise>(exerciseJson);
        return exercise == null ? throw new ArgumentNullException() : Dao.AddExercise(exercise);
    }

    /// <summary>
    /// Removes an exercise item.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise item to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when the exercise ID is invalid.</exception>
    public void RemoveExercise(int exerciseId)
    {
        if (exerciseId <= 0)
        {
            throw new ArgumentNullException();
        }
        Dao.RemoveExercise(exerciseId);
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
    /// <param name="username">The username to check.</param>
    /// <param name="password">The password to check.</param>
    /// <returns><c>true</c> if the username and password match; otherwise, <c>false</c>.</returns>
    public bool DoesUserMatchPassword(string username, string password)
    {
        return Dao.DoesUserMatchPassword(username, password);
    }

    /// <summary>
    /// Checks if the provided username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns><c>true</c> if the username exists; otherwise, <c>false</c>.</returns>
    public bool DoesUsernameExist(string username)
    {
        return Dao.DoesUsernameExist(username);
    }

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">A tuple containing the username and password of the user to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when the username or password is null.</exception>
    public void AddUser((string, string) user)
    {
        (string username, string password) = user;
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
        var json = JsonSerializer.Serialize(Dao.GetLogs());
        Debug.WriteLine($"JSON size: {json.Length / 1024.0} KB");
        return json;
    }

    /// <summary>
    /// Retrieves a log entry by its date.
    /// </summary>
    /// <param name="date">The date of the log entry to retrieve.</param>
    /// <returns>A JSON string representing the <see cref="LogDTO"/> object.</returns>
    public string GetLogByDate(DateOnly date)
    {
        return JsonSerializer.Serialize(Dao.GetLogByDate(date));
    }

    /// <summary>
    /// Updates an existing log entry.
    /// </summary>
    /// <param name="logJson">A JSON string representing the log entry to update.</param>
    /// <exception cref="ArgumentNullException">Thrown when the log is null.</exception>
    public void UpdateLog(string logJson)
    {
        Log? log = JsonSerializer.Deserialize<Log>(logJson) ?? throw new ArgumentNullException();
        Dao.UpdateLog(log);
    }

    /// <summary>
    /// Adds a new log entry.
    /// </summary>
    /// <param name="logJson">A JSON string representing the log entry to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when the log is null.</exception>
    public void AddLog(string logJson)
    {
        Log? log = JsonSerializer.Deserialize<Log>(logJson)
            ?? throw new ArgumentNullException();
        Dao.AddLog(log);
    }

    /// <summary>
    /// Deletes a log entry.
    /// </summary>
    /// <param name="logId">The ID of the log entry to delete.</param>
    public void DeleteLog(int logId)
    {
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
    /// Gets the JSON serializer options.
    /// </summary>
    /// <returns>The JSON serializer options.</returns>
    private JsonSerializerOptions GetJsonSerializerOptions() => new()
    {
        IncludeFields = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        WriteIndented = true
    };

    public void UpdateTotalCalories(string logIdJson)
    {
        (int logId, double totalCalories) = JsonSerializer.Deserialize<(int, double)>(logIdJson, Options);
        Dao.UpdateTotalCalories(logId, totalCalories);
    }
}
