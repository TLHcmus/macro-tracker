﻿using Microsoft.Extensions.DependencyInjection;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MacroTrackerCore.DTOs;
using System.Collections.ObjectModel;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseDao"/> class for testing.
    /// </summary>
    /// <param name="isTest">Indicates if the instance is for testing.</param>
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

    /// <summary>
    /// Finalizes an instance of the <see cref="DatabaseDao"/> class.
    /// </summary>
    ~DatabaseDao()
    {
        _context.DisposeSqliteForTest(_context.Database.GetDbConnection());
    }

    /// <summary>
    /// Retrieves a list of foods.
    /// </summary>
    /// <returns>A list of <see cref="FoodDTO"/> objects.</returns>
    public List<FoodDTO> GetFoods()
    {
        return [.. _context.Foods
            .Select(food => new FoodDTO
            {
                FoodId = food.FoodId,
                Name = food.Name ?? string.Empty,
                CaloriesPer100g = food.CaloriesPer100g ?? 0,
                ProteinPer100g = food.ProteinPer100g ?? 0,
                CarbsPer100g = food.CarbsPer100g ?? 0,
                FatPer100g = food.FatPer100g ?? 0,
                Image = food.Image ?? Array.Empty<byte>()
            })];
    }

    /// <summary>
    /// Adds a new food.
    /// </summary>
    /// <param name="food">The food to add.</param>
    /// <returns>The ID of the added food.</returns>
    public int AddFood(Food food)
    {
        _context.Foods.Add(food);
        _context.SaveChanges();
        return food.FoodId;
    }

    /// <summary>
    /// Removes a food by its ID.
    /// </summary>
    /// <param name="foodId">The ID of the food to remove.</param>
    /// <exception cref="Exception">Thrown when the food is not found.</exception>
    public void RemoveFood(int foodId)
    {
        var food = _context.Foods.Find(foodId) ?? throw new Exception("Food not found");
        _context.Foods.Remove(food);
        _context.SaveChanges();
    }

    /// <summary>
    /// Retrieves a food by its ID.
    /// </summary>
    /// <param name="foodId">The ID of the food to retrieve.</param>
    /// <returns>A <see cref="FoodDTO"/> object.</returns>
    /// <exception cref="Exception">Thrown when the food is not found.</exception>
    public FoodDTO GetFoodById(int foodId)
    {
        var food = _context.Foods.Find(foodId);
        return food == null
            ? throw new Exception("Food not found")
            : new FoodDTO
        {
            FoodId = food.FoodId,
            Name = food.Name ?? string.Empty,
            CaloriesPer100g = food.CaloriesPer100g ?? 0,
            ProteinPer100g = food.ProteinPer100g ?? 0,
            CarbsPer100g = food.CarbsPer100g ?? 0,
            FatPer100g = food.FatPer100g ?? 0,
            Image = food.Image ?? []
            };
    }

    /// <summary>
    /// Retrieves a list of exercises.
    /// </summary>
    /// <returns>A list of <see cref="ExerciseDTO"/> objects.</returns>
    public List<ExerciseDTO> GetExercises()
    {
        return [.. _context.Exercises
            .Select(exercise => new ExerciseDTO
            {
                ExerciseId = exercise.ExerciseId,
                Name = exercise.Name ?? string.Empty,
                CaloriesPerMinute = exercise.CaloriesPerMinute ?? 0,
                Image = exercise.Image ?? Array.Empty<byte>()
            })];
    }

    /// <summary>
    /// Retrieves an exercise by its ID.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise to retrieve.</param>
    /// <returns>An <see cref="ExerciseDTO"/> object.</returns>
    /// <exception cref="Exception">Thrown when the exercise is not found.</exception>
    public ExerciseDTO GetExerciseById(int exerciseId)
    {
        var exercise = _context.Exercises.Find(exerciseId);
        return exercise == null
            ? throw new Exception("Exercise not found")
            : new ExerciseDTO
        {
            ExerciseId = exercise.ExerciseId,
            Name = exercise.Name ?? string.Empty,
            CaloriesPerMinute = exercise.CaloriesPerMinute ?? 0,
            Image = exercise.Image ?? []
            };
    }

    /// <summary>
    /// Adds a new exercise.
    /// </summary>
    /// <param name="exercise">The exercise to add.</param>
    /// <returns>The ID of the added exercise.</returns>
    public int AddExercise(Exercise exercise)
    {
        _context.Exercises.Add(exercise);
        _context.SaveChanges();
        return exercise.ExerciseId;
    }

    /// <summary>
    /// Removes an exercise by its ID.
    /// </summary>
    /// <param name="exerciseId">The ID of the exercise to remove.</param>
    /// <exception cref="Exception">Thrown when the exercise is not found.</exception>
    public void RemoveExercise(int exerciseId)
    {
        var exercise = _context.Exercises.Find(exerciseId) ?? throw new Exception("Exercise not found");
        _context.Exercises.Remove(exercise);
        _context.SaveChanges();
    }

    /// <summary>
    /// Retrieves the goal for the current user.
    /// </summary>
    /// <returns>A <see cref="GoalDTO"/> object.</returns>
    public GoalDTO GetGoal()
    {
        var userId = CurrentUser.UserId;
        var goal = _context.Goals.FirstOrDefault(goal => goal.UserId == userId);
        if (goal == null)
        {
            return new GoalDTO
            {
                Calories = 0,
                Protein = 0,
                Carbs = 0,
                Fat = 0
            };
        }
        return new GoalDTO
        {
            GoalId = goal.GoalId,
            Calories = goal.Calories ?? 0,
            Protein = goal.Protein ?? 0,
            Carbs = goal.Carbs ?? 0,
            Fat = goal.Fat ?? 0
        };
    }

    /// <summary>
    /// Updates the goal for the current user.
    /// </summary>
    /// <param name="goal">The goal to update.</param>
    public void UpdateGoal(Goal goal)
    {
        var userId = CurrentUser.UserId;
        var existingGoal = _context.Goals.FirstOrDefault(g => g.UserId == userId);
        if (existingGoal == null)
        {
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

    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <returns>A list of <see cref="User"/> objects.</returns>
    public List<User> GetUsers()
    {
        return [.. _context.Users];
    }

    /// <summary>
    /// Checks if the provided username and password match.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <param name="password">The password to check.</param>
    /// <returns><c>true</c> if the username and password match; otherwise, <c>false</c>.</returns>
    public bool DoesUserMatchPassword(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
            return false;

        IPasswordEncryption passwordEncryption =
            ProviderCore.GetServiceProvider().GetRequiredService<IPasswordEncryption>();

        CurrentUser.UserId = user.UserId;
        Debug.WriteLine($"UserId after login: {CurrentUser.UserId}");

        return user.EncryptedPassword == passwordEncryption.EncryptPasswordToDatabase(password);
    }

    /// <summary>
    /// Checks if the provided username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns><c>true</c> if the username exists; otherwise, <c>false</c>.</returns>
    public bool DoesUsernameExist(string username)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        return user != null;
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
    /// Retrieves a list of logs for the current user.
    /// </summary>
    /// <returns>A list of <see cref="LogDTO"/> objects.</returns>
    public List<LogDTO> GetLogs()
    {
        var userId = CurrentUser.UserId;
        return [.. _context.Logs.Where(log => log.UserId == userId)
            .Select(log => new LogDTO
            {
                LogId = log.LogId,
                LogDate = log.LogDate,
                TotalCalories = log.TotalCalories ?? 0,
                LogExerciseItems = log.LogExerciseItems.Select(exerciseItem => new LogExerciseItemDTO
                {
                    LogExerciseId = exerciseItem.LogExerciseId,
                    ExerciseId = exerciseItem.ExerciseId ?? 0,
                    Duration = exerciseItem.Duration ?? 0,
                    TotalCalories = exerciseItem.TotalCalories ?? 0
                }).ToList(),
                LogFoodItems = log.LogFoodItems.Select(foodItem => new LogFoodItemDTO
                {
                    LogFoodId = foodItem.LogFoodId,
                    FoodId = foodItem.FoodId ?? 0,
                    NumberOfServings = foodItem.NumberOfServings ?? 0,
                    TotalCalories = foodItem.TotalCalories ?? 0
                }).ToList()
            })];
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
        var log = _context.Logs.Find(logId) ?? throw new Exception("Log not found");
        _context.Logs.Remove(log);
        _context.SaveChanges();
    }

    /// <summary>
    /// Retrieves a log by its date.
    /// </summary>
    /// <param name="date">The date of the log to retrieve.</param>
    /// <returns>A <see cref="LogDTO"/> object.</returns>
    public LogDTO GetLogByDate(DateOnly date)
    {
        var userId = CurrentUser.UserId;
        var log = _context.Logs
            .Include(l => l.LogExerciseItems)
            .Include(l => l.LogFoodItems)
            .FirstOrDefault(log => log.UserId == userId && log.LogDate == date);

        if (log == null)
        {
            return null;
        }

        return new LogDTO
        {
            LogId = log.LogId,
            LogDate = log.LogDate,
            TotalCalories = log.TotalCalories ?? 0,
            LogExerciseItems = log.LogExerciseItems.Select(exerciseItem => new LogExerciseItemDTO
            {
                LogExerciseId = exerciseItem.LogExerciseId,
                ExerciseId = exerciseItem.ExerciseId ?? 0,
                Duration = exerciseItem.Duration ?? 0,
                TotalCalories = exerciseItem.TotalCalories ?? 0
            }).ToList(),
            LogFoodItems = log.LogFoodItems.Select(foodItem => new LogFoodItemDTO
            {
                LogFoodId = foodItem.LogFoodId,
                FoodId = foodItem.FoodId ?? 0,
                NumberOfServings = foodItem.NumberOfServings ?? 0,
                TotalCalories = foodItem.TotalCalories ?? 0
            }).ToList()
        };
    }

    /// <summary>
    /// Updates a log.
    /// </summary>
    /// <param name="log">The log to update.</param>
    public void UpdateLog(Log log)
    {
        var userId = CurrentUser.UserId;
        var logId = log.LogId;
        var existingLog = _context.Logs
            .Include(l => l.LogFoodItems)
            .Include(l => l.LogExerciseItems)
            .FirstOrDefault(l => l.LogId == logId);

        if (existingLog == null)
        {
            log.UserId = userId;
            _context.Logs.Add(log);
        }
        else
        {
            existingLog.LogDate = log.LogDate;
            existingLog.TotalCalories = log.TotalCalories;

            foreach (var item in existingLog.LogFoodItems.ToList())
            {
                if (!log.LogFoodItems.Any(f => f.LogFoodId == item.LogFoodId))
                {
                    _context.Remove(item);
                }
            }

            foreach (var item in log.LogFoodItems)
            {
                var existingItem = existingLog.LogFoodItems.FirstOrDefault(f => f.LogFoodId == item.LogFoodId);
                if (existingItem == null)
                {
                    existingLog.LogFoodItems.Add(item);
                }
                else
                {
                    existingItem.FoodId = item.FoodId;
                    existingItem.NumberOfServings = item.NumberOfServings;
                    existingItem.TotalCalories = item.TotalCalories;
                }
            }

            foreach (var item in existingLog.LogExerciseItems.ToList())
            {
                if (!log.LogExerciseItems.Any(e => e.LogExerciseId == item.LogExerciseId))
                {
                    _context.Remove(item);
                }
            }

            foreach (var item in log.LogExerciseItems)
            {
                var existingItem = existingLog.LogExerciseItems.FirstOrDefault(e => e.LogExerciseId == item.LogExerciseId);
                if (existingItem == null)
                {
                    existingLog.LogExerciseItems.Add(item);
                }
                else
                {
                    existingItem.ExerciseId = item.ExerciseId;
                    existingItem.Duration = item.Duration;
                    existingItem.TotalCalories = item.TotalCalories;
                }
            }
        }
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
