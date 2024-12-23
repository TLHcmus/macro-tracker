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

    // Exercise

    /// <summary>  
    /// Collection of mock exercises  
    /// </summary>  
    public List<Exercise> Exercises = new()
    {
        new Exercise
        {
            IconFileName = "ExerciseIcons/basketball.png",
            Name = "Basketball",
            CaloriesPerMinute = 7.5,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/climbing.png",
            Name = "Climbing",
            CaloriesPerMinute = 8.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/martialarts.png",
            Name = "Martial Arts",
            CaloriesPerMinute = 6.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/running.png",
            Name = "Running",
            CaloriesPerMinute = 11.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/swimming.png",
            Name = "Swimming",
            CaloriesPerMinute = 9.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/pickleball.png",
            Name = "Pickle Ball",
            CaloriesPerMinute = 6.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/tennis.png",
            Name = "Tennis",
            CaloriesPerMinute = 7.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/volleyball.png",
            Name = "Volleyball",
            CaloriesPerMinute = 6.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/walking.png",
            Name = "Walking",
            CaloriesPerMinute = 4.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/weightlifting.png",
            Name = "Weight Lifting",
            CaloriesPerMinute = 5.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/yoga.png",
            Name = "Yoga",
            CaloriesPerMinute = 3.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/pilates.png",
            Name = "Pilates",
            CaloriesPerMinute = 3.0,
        },
        new Exercise
        {
            IconFileName = "ExerciseIcons/baseball.png",
            Name = "Baseball",
        }
    };

    public List<Food> Foods = new()
    {
        new Food
        {
            IconFileName = "FoodImages/apple.png",
            Name = "Apple",
            CaloriesPer100g = 52,
            ProteinPer100g = 0.3,
            CarbsPer100g = 14,
            FatPer100g = 0.2,
        },
        new Food
        {
            IconFileName = "FoodImages/banana.png",
            Name = "Banana",
            CaloriesPer100g = 89,
            ProteinPer100g = 1.1,
            CarbsPer100g = 23,
            FatPer100g = 0.3,
        },
        new Food
        {
            IconFileName = "FoodImages/banhmi.png",
            Name = "Banh Mi",
            CaloriesPer100g = 250,
            ProteinPer100g = 8,
            CarbsPer100g = 45,
            FatPer100g = 5,
        },
        new Food
        {
            IconFileName = "FoodImages/beer.png",
            Name = "Beer",
            CaloriesPer100g = 43,
            ProteinPer100g = 0.5,
            CarbsPer100g = 3.6,
            FatPer100g = 0,
        },
        new Food
        {
            IconFileName = "FoodImages/cabbage.png",
            Name = "Cabbage",
            CaloriesPer100g = 25,
            ProteinPer100g = 1.3,
            CarbsPer100g = 6,
            FatPer100g = 0.1,
        },
        new Food
        {
            IconFileName = "FoodImages/cake.png",
            Name = "Cake",
            CaloriesPer100g = 350,
            ProteinPer100g = 4,
            CarbsPer100g = 50,
            FatPer100g = 15,
        },
        new Food
        {
            IconFileName = "FoodImages/carrot.png",
            Name = "Carrot",
            CaloriesPer100g = 35,
            ProteinPer100g = 1,
            CarbsPer100g = 5,
            FatPer100g = 0,
        },
        new Food
        {
            IconFileName = "FoodImages/cheese.png",
            Name = "Cheese",
            CaloriesPer100g = 400,
            ProteinPer100g = 25,
            CarbsPer100g = 1,
            FatPer100g = 33,
        },
        new Food
        {
            IconFileName = "FoodImages/chocolate.png",
            Name = "Chocolate",
            CaloriesPer100g = 546,
            ProteinPer100g = 7.6,
            CarbsPer100g = 61,
            FatPer100g = 31,
        },
        new Food
        {
            IconFileName = "FoodImages/coca.png",
            Name = "Coca",
            CaloriesPer100g = 42,
            ProteinPer100g = 0,
            CarbsPer100g = 10.6,
            FatPer100g = 0,
        },
        new Food
        {
            IconFileName = "FoodImages/coconut.png",
            Name = "Coconut",
            CaloriesPer100g = 354,
            ProteinPer100g = 3.3,
            CarbsPer100g = 15,
            FatPer100g = 33,
        },
        new Food
        {
            IconFileName = "FoodImages/dumpling.png",
            Name = "Dumpling",
            CaloriesPer100g = 155,
            ProteinPer100g = 6,
            CarbsPer100g = 23,
            FatPer100g = 3,
        },
        new Food
        {
            IconFileName = "FoodImages/egg.png",
            Name = "Egg",
            CaloriesPer100g = 155,
            ProteinPer100g = 13,
            CarbsPer100g = 1.1,
            FatPer100g = 11,
        },
        new Food
        {
            IconFileName = "FoodImages/hotdog.png",
            Name = "Hot Dog",
            CaloriesPer100g = 290,
            ProteinPer100g = 10,
            CarbsPer100g = 23,
            FatPer100g = 19,
        },
        new Food
        {
            IconFileName = "FoodImages/milk.png",
            Name = "Milk",
            CaloriesPer100g = 42,
            ProteinPer100g = 3.4,
            CarbsPer100g = 5,
            FatPer100g = 1,
        },
        new Food
        {
            IconFileName = "FoodImages/orange.png",
            Name = "Orange",
            CaloriesPer100g = 47,
            ProteinPer100g = 0.9,
            CarbsPer100g = 12,
            FatPer100g = 0.1,
        },
        new Food
        {
            IconFileName = "FoodImages/pho.png",
            Name = "Pho",
            CaloriesPer100g = 45,
            ProteinPer100g = 2,
            CarbsPer100g = 8,
            FatPer100g = 1,
        },
        new Food
        {
            IconFileName = "FoodImages/pineapple.png",
            Name = "Pineapple",
            CaloriesPer100g = 50,
            ProteinPer100g = 0.5,
            CarbsPer100g = 13,
            FatPer100g = 0.1,
        },
        new Food
        {
            IconFileName = "FoodImages/pizza.png",
            Name = "Pizza",
            CaloriesPer100g = 266,
            ProteinPer100g = 11,
            CarbsPer100g = 33,
            FatPer100g = 10,
        },
        new Food
        {
            IconFileName = "FoodImages/potato.png",
            Name = "Potato",
            CaloriesPer100g = 77,
            ProteinPer100g = 2,
            CarbsPer100g = 17,
            FatPer100g = 0.1,
        },
        new Food
        {
            IconFileName = "FoodImages/rice.png",
            Name = "Rice",
            CaloriesPer100g = 130,
            ProteinPer100g = 2.7,
            CarbsPer100g = 28,
            FatPer100g = 0.3,
        },
        new Food
        {
            IconFileName = "FoodImages/skewer.png",
            Name = "Skewer",
            CaloriesPer100g = 180,
            ProteinPer100g = 10,
            CarbsPer100g = 5,
            FatPer100g = 14,
        },
        new Food
        {
            IconFileName = "FoodImages/spaghetti.png",
            Name = "Spaghetti",
            CaloriesPer100g = 158,
            ProteinPer100g = 5.8,
            CarbsPer100g = 31,
            FatPer100g = 1,
        },
        new Food
        {
            IconFileName = "FoodImages/steak.png",
            Name = "Steak",
            CaloriesPer100g = 271,
            ProteinPer100g = 25,
            CarbsPer100g = 0,
            FatPer100g = 19,
        },
        new Food
        {
            IconFileName = "FoodImages/strawberry.png",
            Name = "Strawberry",
            CaloriesPer100g = 33,
            ProteinPer100g = 0.7,
            CarbsPer100g = 8,
            FatPer100g = 0.3,
        },
        new Food
        {
            IconFileName = "FoodImages/tea.png",
            Name = "Tea",
            CaloriesPer100g = 1,
            ProteinPer100g = 0,
            CarbsPer100g = 0.3,
            FatPer100g = 0,
        },
        new Food
        {
            IconFileName = "FoodImages/tomato.png",
            Name = "Tomato",
            CaloriesPer100g = 18,
            ProteinPer100g = 0.9,
            CarbsPer100g = 3.9,
            FatPer100g = 0.2,
        },
    };


    /// <summary>  
    /// Gets the collection of exercises  
    /// </summary>  
    /// <returns>Collection of exercises</returns>  
    public List<Exercise> GetExercises() => Exercises;

    /// <summary>  
    /// Throws a NotImplementedException  
    /// </summary>  
    /// <returns>Throws NotImplementedException</returns>  
    public List<Food> GetFoods() => Foods;
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
            TotalCalories = 0,
        }
    };

//    new LogDate {
//            Date = new (2024, 5, 5),
//            LogFood = [
//                new LogFood()
//    {
//        Time = new(2023, 10, 5, 1, 2, 4),
//                    Food = new Food()
//                    {
//                        Title = "Apple",
//                        CaloriesPer100g = 95,
//                        ProteinPer100g = 5,
//                        CarbsPer100g = 25,
//                        FatPer100g = 3,
//                    },
//                    Calories = 13.2f,
//                    Quantity = 1,
//                },
//                new LogFood()
//    {
//        Time = new(2023, 10, 5, 2, 4, 5),
//                    Food = new Food()
//                    {
//                        Title = "Banana",
//                        CaloriesPer100g = 105,
//                        ProteinPer100g = 13,
//                        CarbsPer100g = 27,
//                        FatPer100g = 4,
//                    },
//                    Calories = 15.2f,
//                    Quantity = 2,
//                }
//            ],
//            LogExercise = []
//},
//        new LogDate {
//            Date = new (2024, 4, 1),
//            LogExercise = [
//                new LogExercise()
//{
//    Time = new(2024, 4, 2, 2, 1, 3),
//                    Exercise = new Exercise
//                    {
//                        IconFile = "basketball.png",
//                        Title = "Basketball",
//                    },
//                    Calories = -24.2f,
//                    Minutes = 15,
//                }
//            ],
//            LogFood = [
//                new LogFood()
//{
//    Time = new(2024, 4, 2, 2, 3, 45),
//                    Food = new Food
//                    {
//                        Title = "Coconut",
//                        CaloriesPer100g = 15,
//                        ProteinPer100g = 131,
//                        CarbsPer100g = 273,
//                        FatPer100g = 44,
//                    },
//                    Calories = 55f,
//                    Quantity = 2,
//                }
//            ]
//        },
//        new LogDate
//        {
//            Date = new(2024, 4, 2),
//            LogExercise = [
//                new LogExercise() {
//                    Time = new(2024, 4, 2, 2, 1, 3),
//                    Exercise = new Exercise {
//                        IconFile = "yoga.png",
//                        Title = "Yoga",
//                    },
//                    Calories = -14.4f,
//                    Minutes = 30,
//                }
//            ],
//            LogFood = [
//                new LogFood() {
//                    Time = new(2024, 4, 2, 2, 3, 45),
//                    Food = new Food {
//                        Title = "Meme",
//                        CaloriesPer100g = 15,
//                        ProteinPer100g = 131,
//                        CarbsPer100g = 273,
//                        FatPer100g = 44,
//                    },
//                    Calories = 65f,
//                    Quantity = 4,
//                },
//                new LogFood() {
//                    Time = new(2024, 4, 2, 1, 2, 4),
//                    Food = new Food() {
//                        Title = "Pepsi",
//                        CaloriesPer100g = 95,
//                        ProteinPer100g = 5,
//                        CarbsPer100g = 25,
//                        FatPer100g = 3,
//                    },
//                    Calories = 44f,
//                    Quantity = 1,
//                },
//                new LogFood() {
//                    Time = new(2024, 4, 2, 2, 4, 5),
//                    Food = new Food() {
//                        Title = "Cacao",
//                        CaloriesPer100g = 105,
//                        ProteinPer100g = 13,
//                        CarbsPer100g = 27,
//                        FatPer100g = 4,
//                    },
//                    Calories = 55.2f,
//                    Quantity = 1,
//                }
//            ]
//        },
//        new LogDate
//        {
//            Date = new(2024, 6, 10),
//            LogFood = [
//                new LogFood() {
//                    Time = new(2024, 6, 10, 8, 15, 0),
//                    Food = new Food() {
//                        Title = "Oatmeal",
//                        CaloriesPer100g = 68,
//                        ProteinPer100g = 2.4,
//                        CarbsPer100g = 12,
//                        FatPer100g = 1.4,
//                    },
//                    Calories = 204f,
//                    Quantity = 3,
//                },
//                new LogFood() {
//                    Time = new(2024, 6, 10, 12, 30, 0),
//                    Food = new Food() {
//                        Title = "Grilled Chicken Breast",
//                        CaloriesPer100g = 165,
//                        ProteinPer100g = 31,
//                        CarbsPer100g = 0,
//                        FatPer100g = 3.6,
//                    },
//                    Calories = 330f,
//                    Quantity = 2,
//                },
//                new LogFood() {
//                    Time = new(2024, 6, 10, 19, 0, 0),
//                    Food = new Food() {
//                        Title = "Mixed Vegetables",
//                        CaloriesPer100g = 45,
//                        ProteinPer100g = 3,
//                        CarbsPer100g = 8,
//                        FatPer100g = 1,
//                    },
//                    Calories = 90f,
//                    Quantity = 2,
//                }
//            ],
//            LogExercise = [
//                new LogExercise() {
//                    Time = new(2024, 6, 10, 9, 0, 0),
//                    Exercise = new Exercise() {
//                        IconFile = "running.png",
//                        Title = "Running",
//                    },
//                    Calories = -300f,
//                    Minutes = 30,
//                },
//                new LogExercise() {
//                    Time = new(2024, 6, 10, 18, 0, 0),
//                    Exercise = new Exercise() {
//                        IconFile = "weightlifting.png",
//                        Title = "Weightlifting",
//                    },
//                    Calories = -100f,
//                    Minutes = 45,
//                }
//            ]
//        },
//        new LogDate
//        {
//            Date = new(2024, 7, 15),
//            LogFood = [
//                new LogFood() {
//                    Time = new(2024, 7, 15, 7, 0, 0),
//                    Food = new Food() {
//                        Title = "Greek Yogurt",
//                        CaloriesPer100g = 59,
//                        ProteinPer100g = 10,
//                        CarbsPer100g = 3.6,
//                        FatPer100g = 0.4,
//                    },
//                    Calories = 118f,
//                    Quantity = 2,
//                },
//                new LogFood() {
//                    Time = new(2024, 7, 15, 13, 0, 0),
//                    Food = new Food() {
//                        Title = "Turkey Sandwich",
//                        CaloriesPer100g = 200,
//                        ProteinPer100g = 18,
//                        CarbsPer100g = 30,
//                        FatPer100g = 4,
//                    },
//                    Calories = 400f,
//                    Quantity = 2,
//                },
//                new LogFood() {
//                    Time = new(2024, 7, 15, 20, 0, 0),
//                    Food = new Food() {
//                        Title = "Dark Chocolate",
//                        CaloriesPer100g = 600,
//                        ProteinPer100g = 7.8,
//                        CarbsPer100g = 45,
//                        FatPer100g = 42,
//                    },
//                    Calories = 120f,
//                    Quantity = 4,
//                }
//            ],

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
