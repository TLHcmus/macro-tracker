using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;

public class DaoReceiver
{
    private IDao Dao { get; } = ProviderCore.GetServiceProvider().GetRequiredService<IDao>();
    private JsonSerializerOptions Options { get; } = new() 
    { 
        IncludeFields = true,
        
    };

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

        return JsonSerializer.Serialize(Dao.GetFoods(), options);
    }
    // Add food
    public void AddFood(string foodJson)
    {
        Food? food = JsonSerializer.Deserialize<Food>(foodJson);
        if (food == null)
        {
            throw new ArgumentNullException();
        }
        Dao.AddFood(food);
    }

    // Remove food
    public void RemoveFood(string foodNameJson)
    {
        string? foodName = JsonSerializer.Deserialize<string>(foodNameJson);
        if (foodName == null)
        {
            throw new ArgumentNullException();
        }
        Dao.RemoveFood(foodName);
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

        return JsonSerializer.Serialize(Dao.GetExercises(), options);
    }
    // Add exercise
    public void AddExercise(string exerciseJson)
    {
        Exercise? exercise = JsonSerializer.Deserialize<Exercise>(exerciseJson);
        if (exercise == null)
        {
            throw new ArgumentNullException();
        }
        Dao.AddExercise(exercise);
    }
    // Remove exercise
    public void RemoveExercise(string exerciseNameJson)
    {
        string? exerciseName = JsonSerializer.Deserialize<string>(exerciseNameJson);
        if (exerciseName == null)
        {
            throw new ArgumentNullException();
        }
        Dao.RemoveExercise(exerciseName);
    }

    // Get Goal
    public string GetGoal()
    {
        return JsonSerializer.Serialize(Dao.GetGoal());
    }
    // Update goal
    public void UpdateGoal (string goalJson)
    {
        Goal? goal = JsonSerializer.Deserialize<Goal>(goalJson);
        if (goal == null)
        {
            throw new ArgumentNullException();
        }

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
        JsonSerializerOptions options = new()
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };
        return JsonSerializer.Serialize(
            Dao.GetLogs(),
            options
        );
    }

    //public List<Log> GetLogs()
    //{
    //    return Dao.GetLogs();
    //}

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
        JsonSerializerOptions options = new()
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        (int numberItemOffset, DateOnly endDate) = JsonSerializer.Deserialize<(int, DateOnly)>(pageOffsetJson, Options);
        return JsonSerializer.Serialize(Dao.GetLogWithPagination(numberItemOffset, endDate), options);
    }

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
        return JsonSerializer.Serialize(Dao.GetLogWithPagination(n, numberItemOffset, endDate), options);
    }

    public void UpdateTotalCalories(string logIdJson)
    {
        (int logId, double totalCalories) = JsonSerializer.Deserialize<(int, double)>(logIdJson, Options);
        Dao.UpdateTotalCalories(logId, totalCalories);
    }
}
