using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Data;
using System.Reflection.Metadata;

namespace MacroTrackerCore.Services.DataAccessService;

public class DatabaseDao : IDao
{

    private readonly MacroTrackerContext _context;

    public DatabaseDao()
    {
        _context = new MacroTrackerContext();
    }

    // Food
    public List<Food> GetFoods()
    {
        return _context.Foods.ToList();
    }

    // Exercise
    public List<Exercise> GetExercises()
    {
        return _context.Exercises.ToList();
    }

    
    // Goal
    public Goal GetGoal()
    {
        return _context.Goals.FirstOrDefault();
    }


    // User
    public List<User> GetUsers()
    {
        var users = _context.Users.ToList();

        // Ma hoa mat khau
        foreach (var user in users)
        {
            user.EncryptedPassword =
                ProviderCore.GetServiceProvider()
                            .GetRequiredService<IPasswordEncryption>()
                            .EncryptPasswordToDatabase(user.EncryptedPassword);
        }

        return users;
    }

    // Check user match password
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

    public bool DoesUsernameExist(string username)
    {
        var users = GetUsers();
        return FindUsernameIndex(users, username) != -1;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);

        _context.SaveChanges();
    }

    // Log
    public List<Log> GetLogs()
    {
        return _context.Logs.ToList();
    }

    public void AddLog(Log log)
    {
        _context.Logs.Add(log);

        _context.SaveChanges();
    }
    
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
    public void DeleteLogFood(int logDateID, int logID) => throw new NotImplementedException();

    public void DeleteLogExercise(int logDateID, int logID) => throw new NotImplementedException();

    public List<Log> GetLogDateWithPagination(int pageOffset, DateOnly endDate) => throw new NotImplementedException();
}
