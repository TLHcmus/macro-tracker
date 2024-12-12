using Microsoft.Extensions.DependencyInjection;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.EncryptionService;
using System.Collections.ObjectModel;

namespace MacroTrackerCore.Services.DataAccessService;
public class MockDao : IDao
{
    /// <summary>  
    /// Collection of mock exercises  
    /// </summary>  
    public List<Exercise> Exercises =
    [
        new Exercise
        {
            IconFileName = "basketball.png",
            Name = "Basketball",
        },
        new Exercise
        {
            IconFileName = "climbing.png",
            Name = "Climbing",
        },
        new Exercise
        {
            IconFileName = "martialarts.png",
            Name = "Martial Arts",
        },
        new Exercise
        {
            IconFileName = "running.png",
            Name = "Running",
        },
        new Exercise
        {
            IconFileName = "swimming.png",
            Name = "Swimming",
        },
        new Exercise
        {
            IconFileName = "pickleball.png",
            Name = "Pickle Ball",
        },
        new Exercise
        {
            IconFileName = "tennis.png",
            Name = "Tennis",
        },
        new Exercise
        {
            IconFileName = "volleyball.png",
            Name = "Volleyball",
        },
        new Exercise
        {
            IconFileName = "walking.png",
            Name = "Walking",
        },
        new Exercise
        {
            IconFileName = "weightlifting.png",
            Name = "Weight Lifting",
        },
        new Exercise
        {
            IconFileName = "yoga.png",
            Name = "Yoga",
        },
        new Exercise
        {
            IconFileName = "pilates.png",
            Name = "Pilates",
        },
        new Exercise
        {
            IconFileName = "baseball.png",
            Name = "Baseball",
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

    /// <summary>  
    /// Throws a NotImplementedException  
    /// </summary>  
    /// <returns>Throws NotImplementedException</returns>  
    public ObservableCollection<Food> GetFoods() => throw new NotImplementedException();

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

    private List<LogDate> DateLogs = new()
    {
        new LogDate {
            Date = new(2024, 5, 5),
            LogFood = [
                new LogFood() {
                    Time = new(2023, 10, 5, 1, 2, 4),
                    Food = new Food() {
                        Name = "Apple",
                        CaloriesPer100g = 95,
                        ProteinPer100g = 5,
                        CarbsPer100g = 25,
                        FatPer100g = 3,
                    },
                    Calories = 13.2f,
                    Quantity = 1,
                },
                new LogFood() {
                    Time = new(2023, 10, 5, 2, 4, 5),
                    Food = new Food() {
                        Name = "Banana",
                        CaloriesPer100g = 105,
                        ProteinPer100g = 13,
                        CarbsPer100g = 27,
                        FatPer100g = 4,
                    },
                    Calories = 15.2f,
                    Quantity = 2,
                }
            ],
            LogExercise = []
        },
        new LogDate {
            Date = new(2024, 4, 2),
            LogExercise = [
                new LogExercise() {
                    Time = new(2024, 4, 2, 2, 1, 3),
                    Exercise = new Exercise {
                        IconFileName = "basketball.png",
                        Name = "Basketball",
                    },
                    Calories = -24.2f,
                    Minutes = 15,
                }
            ],
            LogFood = [
                new LogFood() {
                    Time = new(2024, 4, 2, 2, 3, 45),
                    Food = new Food {
                        Name = "Coconut",
                        CaloriesPer100g = 15,
                        ProteinPer100g = 131,
                        CarbsPer100g = 273,
                        FatPer100g = 44,
                    },
                    Calories = 55f,
                    Quantity = 2,
                }
            ]
        },
        new LogDate {
            Date = new(2024, 4, 2),
            LogExercise = [
                new LogExercise() {
                    Time = new(2024, 4, 2, 2, 1, 3),
                    Exercise = new Exercise {
                        IconFileName = "yoga.png",
                        Name = "Yoga",
                    },
                    Calories = -14.4f,
                    Minutes = 30,
                }
            ],
            LogFood = [
                new LogFood() {
                    Time = new(2024, 4, 2, 2, 3, 45),
                    Food = new Food {
                        Name = "Meme",
                        CaloriesPer100g = 15,
                        ProteinPer100g = 131,
                        CarbsPer100g = 273,
                        FatPer100g = 44,
                    },
                    Calories = 65f,
                    Quantity = 4,
                },
                new LogFood() {
                    Time = new(2024, 4, 2, 1, 2, 4),
                    Food = new Food() {
                        Name = "Pepsi",
                        CaloriesPer100g = 95,
                        ProteinPer100g = 5,
                        CarbsPer100g = 25,
                        FatPer100g = 3,
                    },
                    Calories = 44f,
                    Quantity = 1,
                },
                new LogFood() {
                    Time = new(2024, 4, 2, 2, 4, 5),
                    Food = new Food() {
                        Name = "Cacao",
                        CaloriesPer100g = 105,
                        ProteinPer100g = 13,
                        CarbsPer100g = 27,
                        FatPer100g = 4,
                    },
                    Calories = 55.2f,
                    Quantity = 1,
                }
            ]
        },
        new LogDate {
            Date = new(2024, 6, 10),
            LogFood = [
                new LogFood() {
                    Time = new(2024, 6, 10, 8, 15, 0),
                    Food = new Food() {
                        Name = "Oatmeal",
                        CaloriesPer100g = 68,
                        ProteinPer100g = 2.4,
                        CarbsPer100g = 12,
                        FatPer100g = 1.4,
                    },
                    Calories = 204f,
                    Quantity = 3,
                },
                new LogFood() {
                    Time = new(2024, 6, 10, 12, 30, 0),
                    Food = new Food() {
                        Name = "Grilled Chicken Breast",
                        CaloriesPer100g = 165,
                        ProteinPer100g = 31,
                        CarbsPer100g = 0,
                        FatPer100g = 3.6,
                    },
                    Calories = 330f,
                    Quantity = 2,
                },
                new LogFood() {
                    Time = new(2024, 6, 10, 19, 0, 0),
                    Food = new Food() {
                        Name = "Mixed Vegetables",
                        CaloriesPer100g = 45,
                        ProteinPer100g = 3,
                        CarbsPer100g = 8,
                        FatPer100g = 1,
                    },
                    Calories = 90f,
                    Quantity = 2,
                }
            ],
            LogExercise = [
                new LogExercise() {
                    Time = new(2024, 6, 10, 9, 0, 0),
                    Exercise = new Exercise() {
                        IconFileName = "running.png",
                        Name = "Running",
                    },
                    Calories = -300f,
                    Minutes = 30,
                },
                new LogExercise() {
                    Time = new(2024, 6, 10, 18, 0, 0),
                    Exercise = new Exercise() {
                        IconFileName = "weightlifting.png",
                        Name = "Weightlifting",
                    },
                    Calories = -100f,
                    Minutes = 45,
                }
            ]
        },
        new LogDate {
            Date = new(2024, 7, 15),
            LogFood = [
                new LogFood() {
                    Time = new(2024, 7, 15, 7, 0, 0),
                    Food = new Food() {
                        Name = "Greek Yogurt",
                        CaloriesPer100g = 59,
                        ProteinPer100g = 10,
                        CarbsPer100g = 3.6,
                        FatPer100g = 0.4,
                    },
                    Calories = 118f,
                    Quantity = 2,
                },
                new LogFood() {
                    Time = new(2024, 7, 15, 13, 0, 0),
                    Food = new Food() {
                        Name = "Turkey Sandwich",
                        CaloriesPer100g = 200,
                        ProteinPer100g = 18,
                        CarbsPer100g = 30,
                        FatPer100g = 4,
                    },
                    Calories = 400f,
                    Quantity = 2,
                },
                new LogFood() {
                    Time = new(2024, 7, 15, 20, 0, 0),
                    Food = new Food() {
                        Name = "Dark Chocolate",
                        CaloriesPer100g = 600,
                        ProteinPer100g = 7.8,
                        CarbsPer100g = 45,
                        FatPer100g = 42,
                    },
                    Calories = 120f,
                    Quantity = 4,
                }
            ],
            LogExercise = [
                new LogExercise() {
                    Time = new(2024, 7, 15, 6, 30, 0),
                    Exercise = new Exercise() {
                        IconFileName = "cycling.png",
                        Name = "Cycling",
                    },
                    Calories = -250f,
                    Minutes = 40,
                },
                new LogExercise() {
                    Time = new(2024, 7, 15, 17, 0, 0),
                    Exercise = new Exercise() {
                        IconFileName = "cycling.png",
                        Name = "Cycling",
                    },
                    Calories = -20f,
                    Minutes = 5,
                }
            ]
        }
    };

    public List<LogDate> GetAllLogs()
    {
        return DateLogs;
    }

    public void AddLogDate(LogDate date)
    {
        DateLogs.Insert(0, date);
    }

    public LogDate AddDefaultLogDate()
    {
        LogDate date = new()
        {
            Date = DateTime.Now,
            LogExercise = [],
            LogFood = []
        };
        DateLogs.Insert(0, date);
        return date;
    }

    public void DeleteLogDate(int Id)
    {
        DateLogs.Remove(DateLogs.First(log => log.ID == Id));
    }

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

    public void DeleteLogFood(int idLogDate, int idLog)
    {
        LogDate logDate = DateLogs.First(logDate => logDate.ID == idLogDate);
        logDate.LogFood.Remove(logDate.LogFood.First(log => log.ID == idLog));
    }

    public void DeleteLogExercise(int idLogDate, int idLog)
    {
        LogDate logDate = DateLogs.First(logDate => logDate.ID == idLogDate);
        logDate.LogExercise.Remove(logDate.LogExercise.First(log => log.ID == idLog));
    }
}
