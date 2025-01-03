using Microsoft.Extensions.DependencyInjection;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Data;
using Microsoft.EntityFrameworkCore;
using MacroTrackerCore.Services.ConfigurationService;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;

namespace MacroTrackerCore.Services.DataAccessService;

/// <summary>
/// Data Access Object for interacting with the database.
/// </summary>
public class DatabaseDao : IDao
{
    private readonly MacroTrackerContext _context;
    public IPasswordEncryption PasswordEncryption { get; set; } = 
        ProviderCore.GetServiceProvider().GetRequiredService<IPasswordEncryption>();

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseDao"/> class.
    /// </summary>
    public DatabaseDao()
    {
        _context = new MacroTrackerContext();
    }

    public DatabaseDao(bool isTest)
    {
        if (!isTest)
        {
            _context = new MacroTrackerContext();
            return;
        }

        _context = new MacroTrackerContext("test");
        _context.Database.EnsureCreated();
        _context.InitSqliteForTest();
    }

    ~DatabaseDao()
    {
        _context.DisposeSqliteForTest(_context.Database.GetDbConnection()); 
    }

    /// <summary>
    /// Retrieves a list of foods.
    /// </summary>
    /// <returns>A list of <see cref="Food"/> objects.</returns>
    public List<Food> GetFoods()
    {
        return [.. _context.Foods];
    }

    // Add food
    public void AddFood(Food food)
    {
        _context.Foods.Add(food);

        _context.SaveChanges();
    }

    // Remove food
    public void RemoveFood(string foodName)
    {
        var food = _context.Foods.Find(foodName);

        if (food == null)
        {
            throw new Exception("Food not found");
        }

        _context.Foods.Remove(food);

        _context.SaveChanges();
    }

    /// <summary>
    /// Retrieves a list of exercises.
    /// </summary>
    /// <returns>A list of <see cref="Exercise"/> objects.</returns>
    public List<Exercise> GetExercises()
    {
        return [.. _context.Exercises];
    }

    // Add exercise
    public void AddExercise(Exercise exercise)
    {
        _context.Exercises.Add(exercise);

        _context.SaveChanges();
    }

    // Remove exercise
    public void RemoveExercise(string exerciseName)
    {
        var exercise = _context.Exercises.Find(exerciseName) ?? 
            throw new Exception("Exercise not found");
        _context.Exercises.Remove(exercise);

        _context.SaveChanges();
    }

    /// <summary>
    /// Retrieves the goal.
    /// </summary>
    /// <returns>A <see cref="Goal"/> object.</returns>
    public Goal GetGoal()
    {
        return _context.Goals.FirstOrDefault();
    }

    public void UpdateGoal(Goal goal)
    {
        var existingGoal = _context.Goals.FirstOrDefault();
        
        if (existingGoal == null)
        {
            _context.Goals.Add(goal);
            return;
        }

        existingGoal.Calories = goal.Calories;
        existingGoal.Protein = goal.Protein;
        existingGoal.Carbs = goal.Carbs;
        existingGoal.Fat = goal.Fat;

        _context.SaveChanges();
    }

    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <returns>A list of <see cref="User"/> objects.</returns>
    public List<User> GetUsers()
    {
        return [.. _context.Users];
    }

    /// <summary>
    /// Finds the index of a username in a list of users.
    /// </summary>
    /// <param name="users">The list of users.</param>
    /// <param name="userName">The username to find.</param>
    /// <returns>The index of the username if found; otherwise, -1.</returns>
    static int FindUsernameIndex(List<User> users, string userName)
    {
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Username.Equals(userName)) // Case-sensitive comparison  
            {
                return i; // Return the index if found  
            }
        }
        return -1; // Return -1 if not found  
    }

    /// <summary>
    /// Checks if the provided username and password match.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <param name="password">The password to check.</param>
    /// <returns><c>true</c> if the username and password match; otherwise, <c>false</c>.</returns>
    public bool DoesUserMatchPassword(string username, string password)
    {
        var users = GetUsers();
        int indexUsername = FindUsernameIndex(users, username);
        if (indexUsername == -1)
            return false;

        return users[indexUsername].EncryptedPassword == PasswordEncryption.EncryptPasswordToDatabase(password);
    }

    /// <summary>
    /// Checks if the provided username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns><c>true</c> if the username exists; otherwise, <c>false</c>.</returns>
    public bool DoesUsernameExist(string username)
    {
        var users = GetUsers();
        return FindUsernameIndex(users, username) != -1;
    }

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">The user to add.</param>
    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    /// <summary>
    /// Retrieves a list of logs.
    /// </summary>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    public List<Log> GetLogs()
    {
        return [.. _context.Logs
                    .Include(log => log.LogExerciseItems)
                    .Include(log => log.LogFoodItems)];
    }

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="log">The log to add.</param>
    public void AddLog(Log log)
    {
        _context.Logs.Add(log);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deletes a log by its ID.
    /// </summary>
    /// <param name="logId">The ID of the log to delete.</param>
    /// <exception cref="Exception">Thrown when the log is not found.</exception>
    public void DeleteLog(int logId)
    {
        var log = _context.Logs.Find(logId);
        if (log == null)
        {
            throw new Exception("Log not found");
        }
        _context.Logs.Remove(log);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deletes a log food item.
    /// </summary>
    /// <param name="logDateID">The log date ID.</param>
    /// <param name="logID">The log ID.</param>
    public void DeleteLogFood(int logDateID, int logID) => throw new NotImplementedException();

    /// <summary>
    /// Deletes a log exercise item.
    /// </summary>
    /// <param name="logDateID">The log date ID.</param>
    /// <param name="logID">The log ID.</param>
    public void DeleteLogExercise(int logDateID, int logID) => throw new NotImplementedException();

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="numberItemOffset">The number of items to offset.</param>
    /// <param name="endDate">The end date for the logs.</param>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    public List<Log> GetLogWithPagination(int numberItemOffset, DateOnly endDate)
    {
        return GetLogWithPagination(Configuration.PAGINATION_NUMBER, numberItemOffset, endDate);
    }

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="n">The number of items to retrieve.</param>
    /// <param name="numberItemOffset">The number of items to offset.</param>
    /// <param name="endDate">The end date for the logs.</param>
    /// <returns>A list of <see cref="Log"/> objects.</returns>
    public List<Log> GetLogWithPagination(int n, int numberItemOffset, DateOnly endDate)
    {
        return [.. _context.Logs.OrderByDescending(log => log.LogDate)
                   .Where(log => log.LogDate <= endDate)
                   .Skip(numberItemOffset)
                   .Take(n)];
    }

    /// <summary>
    /// Updates the total calories for a log.
    /// </summary>
    /// <param name="logId">The ID of the log to update.</param>
    /// <param name="totalCalories">The new total calories.</param>
    public void UpdateTotalCalories(int logId, double totalCalories) => throw new NotImplementedException();
}
