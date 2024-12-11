using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
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
    public void AddUser(object user)
    {
        Receiver.AddUser(
            JsonSerializer.Serialize(user)
        );
    }

    public List<LogDate> GetAllLogDates()
    {
        return JsonSerializer.Deserialize<List<LogDate>>(
            Receiver.GetAllLogDates()
        );
    }

    public void AddLogDate(LogDate logDate)
    {
        Receiver.AddLogDate(JsonSerializer.Serialize(logDate));
    }
}
