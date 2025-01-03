using System.Collections.Generic;
using System;
using MacroTrackerUI.Models;

namespace MacroTrackerUI.Services.SenderService.DataAccessSender;

public interface IDaoSender
{
    List<Food> GetFoods();

    void AddFood(Food food);

    void RemoveFood(string foodName);

    List<Exercise> GetExercises();

    void AddExercise(Exercise exercise);

    void RemoveExercise(string exerciseName);

    Goal GetGoal();

    void UpdateGoal(Goal goal);

    List<User> GetUsers();

    bool DoesUserMatchPassword(string username, string password);

    bool DoesUsernameExist(string username);

    void AddUser((string, string) user);

    List<Log> GetLogs();

    void AddLog(Log log);

    void DeleteLog(int logId);

    void DeleteLogFood(int logDateID, int logID);

    void DeleteLogExercise(int logDateID, int logID);

    List<Log> GetLogWithPagination(int numberItemOffset, DateOnly endDate);

    List<Log> GetNLogWithPagination(int n, int numberItemOffset, DateOnly endDate);

    void UpdateTotalCalories(int logId, double totalCalories);
}