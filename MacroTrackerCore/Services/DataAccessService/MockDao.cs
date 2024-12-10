using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.EncryptionService;

namespace MacroTrackerCore.Services.DataAccessService;
public class MockDao : IDao
{
    /// <summary>  
    /// Collection of mock exercises  
    /// </summary>  
    public ObservableCollection<Exercise> Exercises =
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
    public ObservableCollection<Exercise> GetExercises()
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
    public bool DoesUserMatchPassword(string username, string endcryptedPassword)
    {
        var users = GetUsers();
         
        int indexUsername = FindUsernameIndex(users, username);
        if (indexUsername == -1)
            return false;

        if (users[indexUsername].EncryptedPassword == endcryptedPassword)
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
}
