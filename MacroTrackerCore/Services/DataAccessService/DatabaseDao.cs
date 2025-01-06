using Microsoft.Extensions.DependencyInjection;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MacroTrackerCore.DTOs;
using System.Collections.ObjectModel;

namespace MacroTrackerCore.Services.DataAccessService;

public class DatabaseDao : IDao
{

    private readonly MacroTrackerContext _context;

    public DatabaseDao()
    {
        _context = new MacroTrackerContext();
    }

    // Food
    public List<FoodDTO> GetFoods()
    {
        return _context.Foods
            .Select(food => new FoodDTO
            {
                FoodId = food.FoodId,
                Name = food.Name ?? string.Empty,
                CaloriesPer100g = food.CaloriesPer100g ?? 0,
                ProteinPer100g = food.ProteinPer100g ?? 0,
                CarbsPer100g = food.CarbsPer100g ?? 0,
                FatPer100g = food.FatPer100g ?? 0,
                Image = food.Image ?? Array.Empty<byte>()
            })
            .ToList();
    }


    // Add food tra ve id cua mon an vua them
    public int AddFood(Food food)
    {
        _context.Foods.Add(food);

        _context.SaveChanges();

        // Trả về món ăn đã có ID từ cơ sở dữ liệu
        return food.FoodId;
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
    // Get Food by Id
    public FoodDTO GetFoodById(int foodId)
    {
        var food = _context.Foods.Find(foodId);

        if (food == null)
        {
            throw new Exception("Food not found");
        }

        return new FoodDTO
        {
            FoodId = food.FoodId,
            Name = food.Name ?? string.Empty,
            CaloriesPer100g = food.CaloriesPer100g ?? 0,
            ProteinPer100g = food.ProteinPer100g ?? 0,
            CarbsPer100g = food.CarbsPer100g ?? 0,
            FatPer100g = food.FatPer100g ?? 0,
            Image = food.Image ?? Array.Empty<byte>()
        };
    }


    // Exercise
    public List<ExerciseDTO> GetExercises()
    {
        return _context.Exercises
            .Select(exercise => new ExerciseDTO
            {
                ExerciseId = exercise.ExerciseId,
                Name = exercise.Name ?? string.Empty,
                CaloriesPerMinute = exercise.CaloriesPerMinute ?? 0,
                Image = exercise.Image ?? Array.Empty<byte>()
            })
            .ToList();
    }

    // Get Exercise by Id
    public ExerciseDTO GetExerciseById(int exerciseId)
    {
        var exercise = _context.Exercises.Find(exerciseId);

        if (exercise == null)
        {
            throw new Exception("Exercise not found");
        }

        return new ExerciseDTO
        {
            ExerciseId = exercise.ExerciseId,
            Name = exercise.Name ?? string.Empty,
            CaloriesPerMinute = exercise.CaloriesPerMinute ?? 0,
            Image = exercise.Image ?? Array.Empty<byte>()
        };
    }


    // Add exercise tra ve id cua bai tap vua them
    public int AddExercise(Exercise exercise)
    {
        _context.Exercises.Add(exercise);

        _context.SaveChanges();

        return exercise.ExerciseId;
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
    public GoalDTO GetGoal()
    {
        var userId = CurrentUser.UserId;

        var goal = _context.Goals.FirstOrDefault(goal => goal.UserId == userId);

        if (goal == null)
        {
            // Tra ve Goal mac dinh
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

    // Update goal
    public void UpdateGoal(Goal goal)
    {
        var userId = CurrentUser.UserId;

        var existingGoal = _context.Goals.FirstOrDefault(g => g.UserId == userId);
        // Neu user chua co goal
        if (existingGoal == null)
        {
            // Gan user id cho goal moi
            goal.UserId = userId;
            _context.Goals.Add(goal);
        }
        else
        {
            // Cap nhat goal
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
    //public List<Log> GetLogs()
    //{
    //    var userId = CurrentUser.UserId;
    //    // Debug
    //    Debug.WriteLine("UserId in GetLogs: " + userId);

    //    return _context.Logs.Where(log => log.UserId == userId)
    //        .Include(log => log.LogFoodItems)
    //        .Include(log => log.LogExerciseItems)
    //        .ToList();
    //}
    public List<LogDTO> GetLogs()
    {
        var userId = CurrentUser.UserId;

        return _context.Logs.Where(log => log.UserId == userId)
            .Select(log => new LogDTO
            {
                LogId = log.LogId,
                LogDate = log.LogDate,
                TotalCalories = log.TotalCalories ?? 0,
                LogExerciseItems = new List<LogExerciseItemDTO>(
                    log.LogExerciseItems.Select(exerciseItem => new LogExerciseItemDTO
                    {
                        LogExerciseId = exerciseItem.LogExerciseId,
                        ExerciseId = exerciseItem.ExerciseId ?? 0,
                        Duration = exerciseItem.Duration ?? 0,
                        TotalCalories = exerciseItem.TotalCalories ?? 0
                    })
                ),
                LogFoodItems = new List<LogFoodItemDTO>(
                    log.LogFoodItems.Select(foodItem => new LogFoodItemDTO
                    {
                        LogFoodId = foodItem.LogFoodId,
                        FoodId = foodItem.FoodId ?? 0,
                        NumberOfServings = foodItem.NumberOfServings ?? 0,
                        TotalCalories = foodItem.TotalCalories ?? 0
                    })
                )
            }).ToList();
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

    // Get log by date
    public LogDTO GetLogByDate(DateOnly date)
    {
        var userId = CurrentUser.UserId;

        Log log = _context.Logs
                            .Include(l => l.LogExerciseItems)
                            .Include(l => l.LogFoodItems)
                            .FirstOrDefault(log => log.UserId == userId && log.LogDate == date);

        if (log == null)
        {
            return null;
        }

        var logDto = new LogDTO
        {
            LogId = log.LogId,
            LogDate = log.LogDate,
            TotalCalories = log.TotalCalories ?? 0,
            LogExerciseItems = new List<LogExerciseItemDTO>(
                     log.LogExerciseItems.Select(exerciseItem => new LogExerciseItemDTO
                     {
                        LogExerciseId = exerciseItem.LogExerciseId,
                        ExerciseId = exerciseItem.ExerciseId ?? 0,
                        Duration = exerciseItem.Duration ?? 0,
                        TotalCalories = exerciseItem.TotalCalories ?? 0
                     })
            ), 
            LogFoodItems = new List<LogFoodItemDTO>(
                     log.LogFoodItems.Select(foodItem => new LogFoodItemDTO
                     {
                        LogFoodId = foodItem.LogFoodId,
                        FoodId = foodItem.FoodId ?? 0,
                        NumberOfServings = foodItem.NumberOfServings ?? 0,
                        TotalCalories = foodItem.TotalCalories ?? 0
                     })
            )
        };
        return logDto;
    }

    // Update Log
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
            // Cập nhật các thuộc tính cơ bản
            existingLog.LogDate = log.LogDate;
            existingLog.TotalCalories = log.TotalCalories;

            // Đồng bộ LogFoodItems
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
                    // Cập nhật mục hiện có
                    existingItem.FoodId = item.FoodId;  
                    existingItem.NumberOfServings = item.NumberOfServings;
                    existingItem.TotalCalories = item.TotalCalories;
                }
            }

            // Đồng bộ LogExerciseItems (tương tự như trên)
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
                    // Cập nhật mục hiện có
                    existingItem.ExerciseId = item.ExerciseId;
                    existingItem.Duration = item.Duration;
                    existingItem.TotalCalories = item.TotalCalories;
                }
            }
        }
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
