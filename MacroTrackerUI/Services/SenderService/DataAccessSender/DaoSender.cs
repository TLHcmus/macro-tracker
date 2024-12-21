﻿using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace MacroTrackerUI.Services.SenderService.DataAccessSender;

public class DaoSender
{
    private DaoReceiver Receiver { get; } =
        ProviderUI.GetServiceProvider().GetService<DaoReceiver>();
    private JsonSerializerOptions Options { get; } = new() { IncludeFields = true };

    /// <summary>
    /// Retrieves a list of foods.
    /// </summary>
    /// <returns>A list of <see cref="Food"/> objects.</returns>
    public List<Food> GetFoods()
    {
        return JsonSerializer.Deserialize<List<Food>>(Receiver.GetFoods());
    }

    /// <summary>
    /// Retrieves a collection of exercises.
    /// </summary>
    /// <returns>A list of <see cref="Exercise"/> objects.</returns>
    public List<Exercise> GetExercises()
    {
        return JsonSerializer.Deserialize<List<Exercise>>(Receiver.GetExercises());
    }

    // Get Goal
    public Goal GetGoal()
    {
        return JsonSerializer.Deserialize<Goal>(Receiver.GetGoal());
    }

    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <returns>A list of <see cref="User"/> objects.</returns>
    public List<User> GetUsers()
    {
        return JsonSerializer.Deserialize<List<User>>(Receiver.GetUsers());
    }

    /// <summary>
    /// Checks if the provided username and password match.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <param name="password">The password to check.</param>
    /// <returns>
    /// <c>true</c> if the username and password match; otherwise, <c>false</c>.
    /// </returns>
    public bool DoesUserMatchPassword(string username, string password)
    {

        string signInJson = JsonSerializer.Serialize((username, password), Options);
        return JsonSerializer.Deserialize<bool>(
            Receiver.DoesUserMatchPassword(signInJson)
        );
    }

    /// <summary>
    /// Checks if the provided username exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>
    /// <c>true</c> if the username exists; otherwise, <c>false</c>.
    /// </returns>
    public bool DoesUsernameExist(string username)
    {
        string usernameJson = JsonSerializer.Serialize(username);
        return JsonSerializer.Deserialize<bool>(
            Receiver.DoesUsernameExist(usernameJson)
        );
    }

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">The user to add.</param>
    public void AddUser((string, string) user)
    {
        Receiver.AddUser(
            JsonSerializer.Serialize(user, Options)
        );
    }

    public List<Log> GetLogs()
    {
        return JsonSerializer.Deserialize<List<Log>>(
            Receiver.GetLogs()
        );
    }

    public void AddLog(Log log)
    {
        Receiver.AddLog(JsonSerializer.Serialize(log));
    }

    public void DeleteLog(int logId)
    {
        Receiver.DeleteLog(
            JsonSerializer.Serialize(logId)
        );
    }

    public void DeleteLogFood(int logDateID, int logID)
    {
        Receiver.DeleteLogFood(
            JsonSerializer.Serialize((logDateID, logID), Options)
        );
    }

    public void DeleteLogExercise(int logDateID, int logID)
    {
        Receiver.DeleteLogExercise(
            JsonSerializer.Serialize((logDateID, logID), Options)
        );
    }

    public List<Log> GetLogDateWithPagination(int numberItemOffset, DateOnly endDate)
    {
        return JsonSerializer.Deserialize<List<Log>>(
            Receiver.GetLogDateWithPagination(
                JsonSerializer.Serialize((numberItemOffset, endDate), Options)
            )
        );
    }
}
