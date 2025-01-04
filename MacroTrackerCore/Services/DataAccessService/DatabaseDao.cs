using Microsoft.Extensions.DependencyInjection;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

    // Add food
    public void AddFood(Food food)
    {
        _context.Foods.Add(food);

        _context.SaveChanges();
    }

    // Remove food
    public void RemoveFood(int foodId)
    {
        var food = _context.Foods.Find(foodId);

        if (food == null)
        {
            throw new Exception("Food not found");
        }

        _context.Foods.Remove(food);

        _context.SaveChanges();
    }

    // Exercise
    public List<Exercise> GetExercises()
    {
        return _context.Exercises.ToList();
    }

    // Add exercise
    public void AddExercise(Exercise exercise)
    {
        _context.Exercises.Add(exercise);

        _context.SaveChanges();
    }

    // Remove exercise
    public void RemoveExercise(int exerciseId)
    {
        var exercise = _context.Exercises.Find(exerciseId);

        if (exercise == null)
        {
            throw new Exception("Exercise not found");
        }

        _context.Exercises.Remove(exercise);

        _context.SaveChanges();
    }

    // Goal
    public Goal GetGoal()
    {
        var userId = CurrentUser.UserId;

        return _context.Goals.FirstOrDefault(goal => goal.UserId == userId);
    }
    // Update goal
    public void UpdateGoal(Goal goal)
    {
        var userId = CurrentUser.UserId;

        var existingGoal = _context.Goals.FirstOrDefault(g => g.UserId == userId);
        
        if (existingGoal == null)
        {
            // Gan user id cho goal moi
            goal.UserId = userId;
            _context.Goals.Add(goal);
        }
        else
        {    
            existingGoal.Calories = goal.Calories;
            existingGoal.Protein = goal.Protein;
            existingGoal.Carbs = goal.Carbs;
            existingGoal.Fat = goal.Fat;
        }   

        _context.SaveChanges();
    }


    // User
    public List<User> GetUsers()
    {
        var users = _context.Users.ToList();

        // Ma hoa mat khau
        //foreach (var user in users)
        //{
        //    user.EncryptedPassword =
        //        ProviderCore.GetServiceProvider()
        //                    .GetRequiredService<IPasswordEncryption>()
        //                    .EncryptPasswordToDatabase(user.EncryptedPassword);
        //}

        return users;
    }

    // Check user match password

    public bool DoesUserMatchPassword(string username, string password)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Username == username); 
        if (user == null)
            return false;

        // Kiểm tra mật khẩu
        IPasswordEncryption passwordEncryption =
            ProviderCore.GetServiceProvider().GetRequiredService<IPasswordEncryption>();
        
        // Luu user id
        CurrentUser.UserId = user.UserId;

        Debug.WriteLine($"UserId after login: {CurrentUser.UserId}");

        return user.EncryptedPassword == passwordEncryption.EncryptPasswordToDatabase(password);
    }

    public bool DoesUsernameExist(string username)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Username == username); 
        return user != null;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);

        _context.SaveChanges();
    }

    // Log
    public List<Log> GetLogs()
    {
        var userId = CurrentUser.UserId;
        // Debug
        Debug.WriteLine("UserId in GetLogs: " + userId);

        return _context.Logs.Where(log => log.UserId == userId)
            .Include(log => log.LogFoodItems)
            .Include(log => log.LogExerciseItems)
            .ToList();
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

    public List<Log> GetLogWithPagination(int numberItemOffset, DateOnly endDate)
    {
        return GetLogWithPagination(Configuration.PAGINATION_NUMBER, numberItemOffset, endDate);
    }

    public List<Log> GetLogWithPagination(int n, int numberItemOffset, DateOnly endDate)
    {
        return _context.Logs.OrderByDescending(log => log.LogDate)
                   .Where(log => log.LogDate <= endDate)
                   .Skip(numberItemOffset)
                   .Take(n)
                   .ToList();
    }
    public void UpdateTotalCalories(int logId, double totalCalories) => throw new NotImplementedException();
}
