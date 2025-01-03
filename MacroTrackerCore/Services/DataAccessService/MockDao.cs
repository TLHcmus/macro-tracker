using Microsoft.Extensions.DependencyInjection;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.EncryptionService;
using System.Collections.ObjectModel;
using MacroTrackerCore.Services.ConfigurationService;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;

namespace MacroTrackerCore.Services.DataAccessService;
/// <summary>
/// Mock data access object for testing purposes.
/// </summary>
public class MockDao : IDao
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MockDao"/> class.
    /// </summary>
    public MockDao()
    {
        Logs.Sort((a, b) => b.LogDate.Value.CompareTo(a.LogDate.Value));
        UpdateTotalCalories();
    }

    /// <summary>  
    /// Throws a NotImplementedException.  
    /// </summary>  
    /// <returns>Throws NotImplementedException</returns>  
    public List<Food> GetFoods() => throw new NotImplementedException();

    // Add food
    public void AddFood(Food food)
    {
        throw new NotImplementedException();
    }

    // Remove food
    public void RemoveFood(string foodName)
    {
        throw new NotImplementedException();
    }

    // Exercise

    /// <summary>  
    /// Collection of mock exercises.  
    /// </summary>  
    public List<Exercise> Exercises = new()
    {
        new Exercise
        {
            IconFileName = "basketball.png",
            Name = "Basketball",
            CaloriesPerMinute = 7.5,
        },
        new Exercise
        {
            IconFileName = "climbing.png",
            Name = "Climbing",
            CaloriesPerMinute = 8.0,
        },
        new Exercise
        {
            IconFileName = "martialarts.png",
            Name = "Martial Arts",
            CaloriesPerMinute = 6.0,
        },
        new Exercise
        {
            IconFileName = "running.png",
            Name = "Running",
            CaloriesPerMinute = 11.0,
        },
        new Exercise
        {
            IconFileName = "swimming.png",
            Name = "Swimming",
            CaloriesPerMinute = 9.0,
        },
        new Exercise
        {
            IconFileName = "pickleball.png",
            Name = "Pickle Ball",
            CaloriesPerMinute = 6.0,
        },
        new Exercise
        {
            IconFileName = "tennis.png",
            Name = "Tennis",
            CaloriesPerMinute = 7.0,
        },
        new Exercise
        {
            IconFileName = "volleyball.png",
            Name = "Volleyball",
            CaloriesPerMinute = 6.0,
        },
        new Exercise
        {
            IconFileName = "walking.png",
            Name = "Walking",
            CaloriesPerMinute = 4.0,
        },
        new Exercise
        {
            IconFileName = "weightlifting.png",
            Name = "Weight Lifting",
            CaloriesPerMinute = 5.0,
        },
        new Exercise
        {
            IconFileName = "yoga.png",
            Name = "Yoga",
            CaloriesPerMinute = 3.0,
        },
        new Exercise
        {
            IconFileName = "pilates.png",
            Name = "Pilates",
            CaloriesPerMinute = 3.0,
        },
        new Exercise
        {
            IconFileName = "baseball.png",
            Name = "Baseball",
            CaloriesPerMinute = 5.0,
        },
    };

    /// <summary>  
    /// Gets the collection of exercises.  
    /// </summary>  
    /// <returns>Collection of exercises</returns>  
    public List<Exercise> GetExercises()
    {
        return Exercises;
    }

    // Add exercise
    public void AddExercise(Exercise exercise)
    {
        throw new NotImplementedException();
    }
    // Remove exercise
    public void RemoveExercise(string exerciseName)
    {
        throw new NotImplementedException();
    }

    // Goal

    // Get Goal
    /// <summary>
    /// Gets the goal.
    /// </summary>
    /// <returns>A <see cref="Goal"/> object.</returns>
    public Goal GetGoal()
    {
        return new Goal()
        {
            Calories = 2500,
            Protein = 156,
            Carbs = 313,
            Fat = 69
        };
    }
    // Update goal
    public void UpdateGoal(Goal goal)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Collection of mock users.
    /// </summary>
    private List<User> UserList { get; set; } = new()
    {
        new User
        {
            Username = "admin",
            EncryptedPassword =
                ProviderCore.GetServiceProvider()
                            .GetRequiredService<IPasswordEncryption>()
                            .EncryptPasswordToDatabase("123")
        },
    };

    /// <summary>  
    /// Mock method to get a list of users.  
    /// </summary>  
    /// <returns>Return a list of mock users</returns>  
    public List<User> GetUsers()
    {
        return UserList;
    }

    /// <summary>  
    /// Mock method to check if username and password match.  
    /// Username: admin  
    /// Password: 123  
    /// </summary>  
    /// <param name="username">The username to check</param>  
    /// <param name="password">The password to check</param>  
    /// <returns>True if the username and password match, otherwise false</returns>  
    public bool DoesUserMatchPassword(string username, string password)
    {
        var users = GetUsers();

        int indexUsername = FindUsernameIndex(users, username);
        if (indexUsername == -1)
            return false;

        IPasswordEncryption passwordEncryption =
            ProviderCore.GetServiceProvider().GetRequiredService<IPasswordEncryption>();

        return users[indexUsername].EncryptedPassword == passwordEncryption.EncryptPasswordToDatabase(password);
    }

    /// <summary>  
    /// Find the index of a username in a list of users.  
    /// </summary>  
    /// <param name="users">The list of users</param>  
    /// <param name="userName">The username to find</param>  
    /// <returns>If found, return the index of the username. If not found, return -1</returns>  
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
    /// Checks if a username exists in the list of users.  
    /// </summary>  
    /// <param name="username">The username to check</param>  
    /// <returns>True if the username exists, otherwise false</returns>  
    public bool DoesUsernameExist(string username)
    {
        var users = GetUsers();
        return FindUsernameIndex(users, username) != -1;
    }

    /// <summary>  
    /// Add a user to the list of mock users.  
    /// </summary>  
    /// <param name="user">The user to add</param>  
    public void AddUser(User user)
    {
        UserList.Add(user);
    }

    /// <summary>
    /// Gets the list of logs.
    /// </summary>
    /// <returns>A list of logs.</returns>
    public List<Log> GetLogs()
    {
        UpdateTotalCalories();
        return Logs;
    }

    /// <summary>
    /// Updates the total calories for each log.
    /// </summary>
    private void UpdateTotalCalories()
    {
        foreach (var log in Logs)
        {
            double totalCalories = 0;
            foreach (var food in log.LogFoodItems)
            {
                totalCalories += food.TotalCalories ?? 0;
            }
            foreach (var exercise in log.LogExerciseItems)
            {
                totalCalories += exercise.TotalCalories ?? 0;
            }
            log.TotalCalories = totalCalories;
        }
    }

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="log">The log to add.</param>
    public void AddLog(Log log)
    {
        Logs.Add(log);
    }

    /// <summary>
    /// Deletes a log by its ID.
    /// </summary>
    /// <param name="logId">The ID of the log to delete.</param>
    public void DeleteLog(int logId)
    {
        Logs.Remove(Logs.First(log => log.LogId == logId));
    }

    /// <summary>
    /// Collection of mock logs.
    /// </summary>
    private List<Log> Logs =
    [
        new Log
        {
            LogId = 1,
            LogDate = new(2024, 5, 5),
            LogFoodItems = 
            [
                new LogFoodItem() {
                    LogId = 1,
                    LogFoodId = 1,
                    FoodName = "Apple",
                    NumberOfServings = 2,
                    TotalCalories = 24
                },
                new LogFoodItem() {
                    LogId = 1,
                    LogFoodId = 2,
                    FoodName = "Banana",
                    NumberOfServings = 3,
                    TotalCalories = 48
                },
            ],
            LogExerciseItems = [],
            TotalCalories = 0
        },
        new Log
        {
            LogId = 2,
            LogDate = new(2024, 4, 2),
            LogExerciseItems =
            [
                new LogExerciseItem() {
                    LogId = 2,
                    LogExerciseId = 1,
                    ExerciseName = "Basketball",
                    Duration = 15,
                    TotalCalories = -23,
                }
            ],
            LogFoodItems =
            [
                new LogFoodItem() {
                    LogId = 2,
                    LogFoodId = 1,
                    FoodName = "Coconut",
                    NumberOfServings = 2,
                    TotalCalories = 40
                }
            ],
            TotalCalories = 0
        },
        new Log
        {
            LogId = 3,
            LogDate = new(2024, 4, 1),
            LogExerciseItems =
            [
                new LogExerciseItem() {
                    LogId = 3,
                    LogExerciseId = 1,
                    ExerciseName = "Yoga",
                    Duration = 30,
                    TotalCalories = -14.4f,
                }
            ],
            LogFoodItems =
            [
                new LogFoodItem() {
                    LogId = 3,
                    LogFoodId = 1,
                    FoodName = "Meme",
                    NumberOfServings = 10,
                    TotalCalories = 100
                },
                new LogFoodItem() {
                    LogId = 3,
                    LogFoodId = 2,
                    FoodName = "Coke",
                    NumberOfServings = 3,
                    TotalCalories = 300
                },
                new LogFoodItem() {
                    LogId = 3,
                    LogFoodId = 3,
                    FoodName = "Pasta",
                    NumberOfServings = 1,
                    TotalCalories = 150
                }
            ],
            TotalCalories = 0
        },
        new Log
        {
            LogId = 4,
            LogDate = new(2024, 12, 5),
            LogFoodItems =
            [
                new LogFoodItem() {
                    LogId = 1,
                    LogFoodId = 1,
                    FoodName = "Apple",
                    NumberOfServings = 2,
                    TotalCalories = 24
                },
                new LogFoodItem() {
                    LogId = 1,
                    LogFoodId = 2,
                    FoodName = "Banana",
                    NumberOfServings = 3,
                    TotalCalories = 48
                },
            ],
            LogExerciseItems = [],
            TotalCalories = 0
        },
        new Log
        {
            LogId = 5,
            LogDate = new(2024, 8, 2),
            LogExerciseItems =
            [
                new LogExerciseItem() {
                    LogId = 2,
                    LogExerciseId = 1,
                    ExerciseName = "Basketball",
                    Duration = 15,
                    TotalCalories = -23,
                }
            ],
            LogFoodItems =
            [
                new LogFoodItem() {
                    LogId = 2,
                    LogFoodId = 1,
                    FoodName = "Coconut",
                    NumberOfServings = 2,
                    TotalCalories = 40
                }
            ],
            TotalCalories = 0
        },
        new Log
        {
            LogId = 6,
            LogDate = new(2024, 6, 1),
            LogExerciseItems =
            [
                new LogExerciseItem() {
                    LogId = 3,
                    LogExerciseId = 1,
                    ExerciseName = "Yoga",
                    Duration = 30,
                    TotalCalories = -14.4f,
                }
            ],
            LogFoodItems =
            [
                new LogFoodItem() {
                    LogId = 3,
                    LogFoodId = 1,
                    FoodName = "Meme",
                    NumberOfServings = 10,
                    TotalCalories = 100
                },
                new LogFoodItem() {
                    LogId = 3,
                    LogFoodId = 2,
                    FoodName = "Coke",
                    NumberOfServings = 3,
                    TotalCalories = 300
                },
                new LogFoodItem() {
                    LogId = 3,
                    LogFoodId = 3,
                    FoodName = "Pasta",
                    NumberOfServings = 1,
                    TotalCalories = 150
                }
            ],
            TotalCalories = 0
        }
    ];

    /// <summary>
    /// Deletes a log food item by log ID and log food ID.
    /// </summary>
    /// <param name="idLog">The log ID.</param>
    /// <param name="idLogFood">The log food ID.</param>
    public void DeleteLogFood(int idLog, int idLogFood)
    {
        Log log = Logs.First(log => log.LogId == idLog);
        log.LogFoodItems.Remove(log.LogFoodItems.First(logFood => logFood.LogFoodId == idLogFood));
    }

    /// <summary>
    /// Deletes a log exercise item by log ID and log exercise ID.
    /// </summary>
    /// <param name="idLog">The log ID.</param>
    /// <param name="idLogExercise">The log exercise ID.</param>
    public void DeleteLogExercise(int idLog, int idLogExercise)
    {
        Log log = Logs.First(log => log.LogId == idLog);
        log.LogExerciseItems.Remove(log.LogExerciseItems.First(
            logExercise => logExercise.LogExerciseId == idLogExercise)
        );
    }

    /// <summary>
    /// Retrieves logs with pagination.
    /// </summary>
    /// <param name="numberItemOffset">The number of items to offset.</param>
    /// <param name="endDate">The end date for the logs.</param>
    /// <returns>A list of logs.</returns>
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
    /// <returns>A list of logs.</returns>
    public List<Log> GetLogWithPagination(int n, int numberItemOffset, DateOnly endDate)
    {
        return Logs.OrderByDescending(log => log.LogDate)
                   .Where(log => log.LogDate <= endDate)
                   .Skip(numberItemOffset)
                   .Take(n)
                   .ToList();
    }

    /// <summary>
    /// Updates the total calories for a log.
    /// </summary>
    /// <param name="logId">The ID of the log to update.</param>
    /// <param name="totalCalories">The new total calories.</param>
    public void UpdateTotalCalories(int logId, double totalCalories)
    {
        Logs.First(log => log.LogId == logId).TotalCalories = totalCalories;
    }
}
