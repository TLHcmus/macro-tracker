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
    public List<Exercise> Exercises = new()
    {
        new Exercise
        {
            IconFile = "ExerciseIcons/basketball.png",
            Title = "Basketball",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/climbing.png",
            Title = "Climbing",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/martialarts.png",
            Title = "Martial Arts",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/running.png",
            Title = "Running",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/swimming.png",
            Title = "Swimming",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/pickleball.png",
            Title = "Pickle Ball",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/tennis.png",
            Title = "Tennis",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/volleyball.png",
            Title = "Volleyball",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/walking.png",
            Title = "Walking",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/weightlifting.png",
            Title = "Weight Lifting",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/yoga.png",
            Title = "Yoga",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/pilates.png",
            Title = "Pilates",
        },
        new Exercise
        {
            IconFile = "ExerciseIcons/baseball.png",
            Title = "Baseball",
        }
    };

    public List<Food> Foods = new()
    {
        new Food
        {
            IconFile = "FoodImages/apple.png",
            Title = "Apple",
            CaloriesPer100g = 52,
            ProteinPer100g = 0.3,
            CarbsPer100g = 14,
            FatPer100g = 0.2,
        },
        new Food
        {
            IconFile = "FoodImages/banana.png",
            Title = "Banana",
            CaloriesPer100g = 89,
            ProteinPer100g = 1.1,
            CarbsPer100g = 23,
            FatPer100g = 0.3,
        },
        new Food
        {
            IconFile = "FoodImages/banhmi.png",
            Title = "Banh Mi",
            CaloriesPer100g = 250,
            ProteinPer100g = 8,
            CarbsPer100g = 45,
            FatPer100g = 5,
        },
        new Food
        {
            IconFile = "FoodImages/beer.png",
            Title = "Beer",
            CaloriesPer100g = 43,
            ProteinPer100g = 0.5,
            CarbsPer100g = 3.6,
            FatPer100g = 0,
        },
        new Food
        {
            IconFile = "FoodImages/cabbage.png",
            Title = "Cabbage",
            CaloriesPer100g = 25,
            ProteinPer100g = 1.3,
            CarbsPer100g = 6,
            FatPer100g = 0.1,
        },
        new Food
        {
            IconFile = "FoodImages/cake.png",
            Title = "Cake",
            CaloriesPer100g = 350,
            ProteinPer100g = 4,
            CarbsPer100g = 50,
            FatPer100g = 15,
        },
        new Food
        {
            IconFile = "FoodImages/carrot.png",
            Title = "Carrot",
            CaloriesPer100g = 35,
            ProteinPer100g = 1,
            CarbsPer100g = 5,
            FatPer100g = 0,
        },
        new Food
        {
            IconFile = "FoodImages/cheese.png",
            Title = "Cheese",
            CaloriesPer100g = 400,
            ProteinPer100g = 25,
            CarbsPer100g = 1,
            FatPer100g = 33,
        },
        new Food
        {
            IconFile = "FoodImages/chocolate.png",
            Title = "Chocolate",
            CaloriesPer100g = 546,
            ProteinPer100g = 7.6,
            CarbsPer100g = 61,
            FatPer100g = 31,
        },
        new Food
        {
            IconFile = "FoodImages/coca.png",
            Title = "Coca",
            CaloriesPer100g = 42,
            ProteinPer100g = 0,
            CarbsPer100g = 10.6,
            FatPer100g = 0,
        },
        new Food
        {
            IconFile = "FoodImages/coconut.png",
            Title = "Coconut",
            CaloriesPer100g = 354,
            ProteinPer100g = 3.3,
            CarbsPer100g = 15,
            FatPer100g = 33,
        },
        new Food
        {
            IconFile = "FoodImages/dumpling.png",
            Title = "Dumpling",
            CaloriesPer100g = 155,
            ProteinPer100g = 6,
            CarbsPer100g = 23,
            FatPer100g = 3,
        },
        new Food
        {
            IconFile = "FoodImages/egg.png",
            Title = "Egg",
            CaloriesPer100g = 155,
            ProteinPer100g = 13,
            CarbsPer100g = 1.1,
            FatPer100g = 11,
        },
        new Food
        {
            IconFile = "FoodImages/hotdog.png",
            Title = "Hot Dog",
            CaloriesPer100g = 290,
            ProteinPer100g = 10,
            CarbsPer100g = 23,
            FatPer100g = 19,
        },
        new Food
        {
            IconFile = "FoodImages/milk.png",
            Title = "Milk",
            CaloriesPer100g = 42,
            ProteinPer100g = 3.4,
            CarbsPer100g = 5,
            FatPer100g = 1,
        },
        new Food
        {
            IconFile = "FoodImages/orange.png",
            Title = "Orange",
            CaloriesPer100g = 47,
            ProteinPer100g = 0.9,
            CarbsPer100g = 12,
            FatPer100g = 0.1,
        },
        new Food
        {
            IconFile = "FoodImages/pho.png",
            Title = "Pho",
            CaloriesPer100g = 45,
            ProteinPer100g = 2,
            CarbsPer100g = 8,
            FatPer100g = 1,
        },
        new Food
        {
            IconFile = "FoodImages/pineapple.png",
            Title = "Pineapple",
            CaloriesPer100g = 50,
            ProteinPer100g = 0.5,
            CarbsPer100g = 13,
            FatPer100g = 0.1,
        },
        new Food
        {
            IconFile = "FoodImages/pizza.png",
            Title = "Pizza",
            CaloriesPer100g = 266,
            ProteinPer100g = 11,
            CarbsPer100g = 33,
            FatPer100g = 10,
        },
        new Food
        {
            IconFile = "FoodImages/potato.png",
            Title = "Potato",
            CaloriesPer100g = 77,
            ProteinPer100g = 2,
            CarbsPer100g = 17,
            FatPer100g = 0.1,
        },
        new Food
        {
            IconFile = "FoodImages/rice.png",
            Title = "Rice",
            CaloriesPer100g = 130,
            ProteinPer100g = 2.7,
            CarbsPer100g = 28,
            FatPer100g = 0.3,
        },
        new Food
        {
            IconFile = "FoodImages/skewer.png",
            Title = "Skewer",
            CaloriesPer100g = 180,
            ProteinPer100g = 10,
            CarbsPer100g = 5,
            FatPer100g = 14,
        },
        new Food
        {
            IconFile = "FoodImages/spaghetti.png",
            Title = "Spaghetti",
            CaloriesPer100g = 158,
            ProteinPer100g = 5.8,
            CarbsPer100g = 31,
            FatPer100g = 1,
        },
        new Food
        {
            IconFile = "FoodImages/steak.png",
            Title = "Steak",
            CaloriesPer100g = 271,
            ProteinPer100g = 25,
            CarbsPer100g = 0,
            FatPer100g = 19,
        },
        new Food
        {
            IconFile = "FoodImages/strawberry.png",
            Title = "Strawberry",
            CaloriesPer100g = 33,
            ProteinPer100g = 0.7,
            CarbsPer100g = 8,
            FatPer100g = 0.3,
        },
        new Food
        {
            IconFile = "FoodImages/tea.png",
            Title = "Tea",
            CaloriesPer100g = 1,
            ProteinPer100g = 0,
            CarbsPer100g = 0.3,
            FatPer100g = 0,
        },
        new Food
        {
            IconFile = "FoodImages/tomato.png",
            Title = "Tomato",
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
                        Title = "Apple",
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
                        Title = "Banana",
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
            Date = new(2024, 4, 1),
            LogExercise = [
                new LogExercise() {
                    Time = new(2024, 4, 2, 2, 1, 3),
                    Exercise = new Exercise {
                        IconFile = "basketball.png",
                        Title = "Basketball",
                    },
                    Calories = -24.2f,
                    Minutes = 15,
                }
            ],
            LogFood = [
                new LogFood() {
                    Time = new(2024, 4, 2, 2, 3, 45),
                    Food = new Food {
                        Title = "Coconut",
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
                        IconFile = "yoga.png",
                        Title = "Yoga",
                    },
                    Calories = -14.4f,
                    Minutes = 30,
                }
            ],
            LogFood = [
                new LogFood() {
                    Time = new(2024, 4, 2, 2, 3, 45),
                    Food = new Food {
                        Title = "Meme",
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
                        Title = "Pepsi",
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
                        Title = "Cacao",
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
                        Title = "Oatmeal",
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
                        Title = "Grilled Chicken Breast",
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
                        Title = "Mixed Vegetables",
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
                        IconFile = "running.png",
                        Title = "Running",
                    },
                    Calories = -300f,
                    Minutes = 30,
                },
                new LogExercise() {
                    Time = new(2024, 6, 10, 18, 0, 0),
                    Exercise = new Exercise() {
                        IconFile = "weightlifting.png",
                        Title = "Weightlifting",
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
                        Title = "Greek Yogurt",
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
                        Title = "Turkey Sandwich",
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
                        Title = "Dark Chocolate",
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
                        IconFile = "cycling.png",
                        Title = "Cycling",
                    },
                    Calories = -250f,
                    Minutes = 40,
                },
                new LogExercise() {
                    Time = new(2024, 7, 15, 17, 0, 0),
                    Exercise = new Exercise() {
                        IconFile = "cycling.png",
                        Title = "Cycling",
                    }
                }
            ],
        },
        new LogDate {
            Date = new(2024, 4, 2),
            LogExercise = [
                new LogExercise() {
                    Time = new(2024, 4, 2, 2, 1, 3),
                    Exercise = new Exercise {
                        IconFile = "yoga.png",
                        Title = "Yoga",
                    },
                    Calories = -14.4f,
                    Minutes = 30,
                }
            ],
            LogFood = [
                new LogFood() {
                    Time = new(2024, 4, 2, 2, 3, 45),
                    Food = new Food {
                        Title = "Meme",
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
                        Title = "Pepsi",
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
                        Title = "Cacao",
                        CaloriesPer100g = 105,
                        ProteinPer100g = 13,
                        CarbsPer100g = 27,
                        FatPer100g = 4,
                    },
                    Calories = 55.2f,
                    Quantity = 1,
                },
                new LogFood() {
                    Time = new(2024, 4, 2, 2, 3, 45),
                    Food = new Food {
                        Title = "Coconut",
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
            Date = new(2024, 6, 10),
            LogFood = [
                new LogFood() {
                    Time = new(2024, 6, 10, 8, 15, 0),
                    Food = new Food() {
                        Title = "Oatmeal",
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
                        Title = "Grilled Chicken Breast",
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
                        Title = "Mixed Vegetables",
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
                        IconFile = "running.png",
                        Title = "Running",
                    },
                    Calories = -300f,
                    Minutes = 30,
                },
                new LogExercise() {
                    Time = new(2024, 6, 10, 18, 0, 0),
                    Exercise = new Exercise() {
                        IconFile = "weightlifting.png",
                        Title = "Weightlifting",
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
                        Title = "Greek Yogurt",
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
                        Title = "Turkey Sandwich",
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
                        Title = "Dark Chocolate",
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
                        IconFile = "cycling.png",
                        Title = "Cycling",
                    },
                    Calories = -250f,
                    Minutes = 40,
                },
                new LogExercise() {
                    Time = new(2024, 7, 15, 17, 0, 0),
                    Exercise = new Exercise() {
                        IconFile = "cycling.png",
                        Title = "Cycling",
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

    public List<LogDate> GetLogDateWithPagination(int numberItemOffset, DateTime endDate)
    {
        return DateLogs.OrderByDescending(log => log.Date)
                       .Where(log => log.Date <= endDate)
                       .Skip(numberItemOffset)
                       .Take(Configuration.PAGINATION_NUMBER)
                       .ToList();
    }
}
