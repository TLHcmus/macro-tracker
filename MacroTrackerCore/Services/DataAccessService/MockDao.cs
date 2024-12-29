using Microsoft.Extensions.DependencyInjection;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.EncryptionService;
using System.Collections.ObjectModel;

namespace MacroTrackerCore.Services.DataAccessService;
public class MockDao : IDao
{
    public MockDao()
    {
        Logs.Sort((a, b) => b.LogDate.Value.CompareTo(a.LogDate.Value));
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

    // Food 

    /// <summary>  
    /// Throws a NotImplementedException  
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
    /// Collection of mock exercises  
    /// </summary>  
    public List<Exercise> Exercises =
    [
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
    ];

    /// <summary>  
    /// Gets the collection of exercises  
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

    private List<User> UserList { get; set; } =
    [
        new User
        {
            Username = "admin",
            EncryptedPassword =
                ProviderCore.GetServiceProvider()
                            .GetRequiredService<IPasswordEncryption>()
                            .EncryptPasswordToDatabase("123")
        },
    ];

    // User

    /// <summary>  
    /// Mock method to get a list of users  
    /// </summary>  
    /// <returns>Return a list of mock users</returns>  
    public List<User> GetUsers()
    {
        return UserList;
    }

    /// <summary>  
    /// Mock method to check if username and password match  
    /// Username: admin  
    /// Password: 123  
    /// </summary>  
    /// <param name="username">The username to check</param>  
    /// <param name="endcryptedPassword">The encrypted password to check</param>  
    /// <returns>True if the username and password match, otherwise false</returns>  
    public bool DoesUserMatchPassword(string username, string password)
    {
        var users = GetUsers();
         
        int indexUsername = FindUsernameIndex(users, username);
        if (indexUsername == -1)
            return false;

        IPasswordEncryption passwordEncryption =
            ProviderCore.GetServiceProvider().GetRequiredService<IPasswordEncryption>();

        if (users[indexUsername].EncryptedPassword == passwordEncryption.EncryptPasswordToDatabase(password))
            return true;
        return false;
    }

    /// <summary>  
    /// Find the index of a username in a list of users  
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
    /// Checks if a username exists in the list of users  
    /// </summary>  
    /// <param name="username">The username to check</param>  
    /// <returns>True if the username exists, otherwise false</returns>  
    public bool DoesUsernameExist(string username)
    {
        var users = GetUsers();
        return FindUsernameIndex(users, username) != -1;
    }

    /// <summary>  
    /// Add a user to the list of mock users  
    /// </summary>  
    /// <param name="user">The user to add</param>  
    public void AddUser(User user)
    {
        UserList.Add(user);
    }

    public List<Log> GetLogs()
    {
        UpdateTotalCalories();
        return Logs;
    }

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

    public void AddLog(Log log)
    {
        Logs.Add(log);
    }

    public void DeleteLog(int logId)
    {
        Logs.Remove(Logs.First(log => log.LogId == logId));
    }

    private List<Log> Logs = new()
    {
        new Log
        {
            LogId = 1,
            LogDate = new(2024, 5, 5),
            LogFoodItems = [
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
            LogExerciseItems = [
                new LogExerciseItem() {
                    LogId = 2,
                    LogExerciseId = 1,
                    ExerciseName = "Basketball",
                    Duration = 15,
                    TotalCalories = -23,
                }
            ],
            LogFoodItems = [
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
            LogExerciseItems = [
                new LogExerciseItem() {
                    LogId = 3,
                    LogExerciseId = 1,
                    ExerciseName = "Yoga",
                    Duration = 30,
                    TotalCalories = -14.4f,
                }
            ],
            LogFoodItems = [
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
            LogFoodItems = [
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
            LogExerciseItems = [
                new LogExerciseItem() {
                    LogId = 2,
                    LogExerciseId = 1,
                    ExerciseName = "Basketball",
                    Duration = 15,
                    TotalCalories = -23,
                }
            ],
            LogFoodItems = [
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
            LogExerciseItems = [
                new LogExerciseItem() {
                    LogId = 3,
                    LogExerciseId = 1,
                    ExerciseName = "Yoga",
                    Duration = 30,
                    TotalCalories = -14.4f,
                }
            ],
            LogFoodItems = [
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
    };

    public void DeleteLogFood(int idLog, int idLogFood)
    {
        Log log = Logs.First(log => log.LogId == idLog);
        log.LogFoodItems.Remove(log.LogFoodItems.First(logFood => logFood.LogFoodId == idLogFood));
    }

    public void DeleteLogExercise(int idLog, int idLogExercise)
    {
        Log log = Logs.First(log => log.LogId == idLog);
        log.LogExerciseItems.Remove(log.LogExerciseItems.First(
            logExercise => logExercise.LogExerciseId == idLogExercise)
        );
    }

    public List<Log> GetLogWithPagination(int numberItemOffset, DateOnly endDate)
    {
        return GetLogWithPagination(Configuration.PAGINATION_NUMBER, numberItemOffset, endDate);
    }

    public List<Log> GetLogWithPagination(int n, int numberItemOffset, DateOnly endDate)
    {
        return Logs.OrderByDescending(log => log.LogDate)
                   .Where(log => log.LogDate <= endDate)
                   .Skip(numberItemOffset)
                   .Take(n)
                   .ToList();
    }

    public void UpdateTotalCalories(int logId, double totalCalories)
    {
        Logs.First(log => log.LogId == logId).TotalCalories = totalCalories;
    }
}
