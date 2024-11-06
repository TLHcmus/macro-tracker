using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroTracker.Model;
using MacroTracker.Helpers;

namespace MacroTracker.Service.DataAcess;
public class MockDao : IDao
{
    public List<Exercise> GetExercises() => throw new NotImplementedException();

    public List<Food> GetFoods() => throw new NotImplementedException();

    private List<User> UserList { get; set; } = new List<User>()
        {
            new User { Username = "admin", EncryptedPassword = PasswordEncryptionHelper.EncryptPasswordToDatabase("123") }
        };
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
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
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
    /// <param name="users"></param>
    /// <param name="userName"></param>
    /// <returns>
    /// If found, return the index of the username
    /// If not found, return -1
    /// </returns>
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

    public bool DoesUsernameExist(string username)
    {
        var users = GetUsers();
        return FindUsernameIndex(users, username) != -1;
    }

    /// <summary>
    /// Add a user to the list of mock users
    /// </summary>
    /// <param name="user"></param>
    public void AddUser(User user)
    {
        UserList.Add(user);
    }
}