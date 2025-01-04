using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for managing logs.
/// </summary>
public class LogViewModel
{
    /// <summary>
    /// Gets or sets the list of logs.
    /// </summary>
    public ObservableCollection<Log> LogList { get; set; } = new();

    /// <summary>
    /// Gets the data access sender.
    /// </summary>
    private IDaoSender Sender { get; }

    private IServiceProvider Provider { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogViewModel"/> class.
    /// </summary>
    public LogViewModel()
    {
        Provider = ProviderUI.GetServiceProvider();
        Sender = Provider.GetService<IDaoSender>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogViewModel"/> class with a specified service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    public LogViewModel(IServiceProvider provider)
    {
        Provider = provider;
        Sender = Provider.GetService<IDaoSender>();
    }

    /// <summary>
    /// Gets or sets the paging size.
    /// </summary>
    public int PagingSize { get; set; }

    private DateTime _endDate = DateTime.Now.Date;

    /// <summary>
    /// Gets or sets the end date for log retrieval.
    /// </summary>
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            LogList.Clear();
            GetNextLogsPage();
        }
    }

    /// <summary>
    /// Retrieves the next page of logs.
    /// </summary>
    public void GetNextLogsPage()
    {
        var logs = Sender.GetLogWithPagination(LogList.Count, DateOnly.FromDateTime(EndDate));

        if (logs.Count == 0)
            return;

        foreach (var log in logs)
            LogList.Add(log);
    }

    /// <summary>
    /// Retrieves the next set of log items.
    /// </summary>
    /// <param name="numItem">The number of items to retrieve.</param>
    public void GetNextLogsItem(int numItem)
    {
        var logs = Sender.GetNLogWithPagination(numItem, numItem, DateOnly.FromDateTime(EndDate));

        if (logs.Count == 0)
            return;

        foreach (var log in logs)
            LogList.Add(log);
    }

    /// <summary>
    /// Adds a new log.
    /// </summary>
    /// <param name="log">The log to add.</param>
    public void AddLog(Log log)
    {
        Sender.AddLog(log);
        LogList.Insert(0, log);
    }

    /// <summary>
    /// Checks if the log list contains a log with the specified date.
    /// </summary>
    /// <param name="date">The date to check.</param>
    /// <returns>True if the log list contains a log with the specified date; otherwise, false.</returns>
    public bool DoesContainDate(DateOnly date)
    {
        return LogList.Any(log => log.LogDate.HasValue && log.LogDate == date);
    }

    /// <summary>
    /// Deletes a log by its ID.
    /// </summary>
    /// <param name="id">The ID of the log to delete.</param>
    public void DeleteLog(int id)
    {
        Sender.DeleteLog(id);
        LogList.Remove(LogList.First(log => log.LogId == id));
    }

    /// <summary>
    /// Deletes a log food item by log ID and log food ID.
    /// </summary>
    /// <param name="logId">The log ID.</param>
    /// <param name="logFoodId">The log food ID.</param>
    public void DeleteLogFood(int logId, int logFoodId)
    {
        Sender.DeleteLogFood(logId, logFoodId);

        var log = LogList.First(log => log.LogId == logId);
        log.LogFoodItems.Remove(log.LogFoodItems.First(logFood => logFood.LogFoodId == logFoodId));
    }

    /// <summary>
    /// Deletes a log exercise item by log ID and log exercise ID.
    /// </summary>
    /// <param name="logId">The log ID.</param>
    /// <param name="logExerciseId">The log exercise ID.</param>
    public void DeleteLogExercise(int logId, int logExerciseId)
    {
        Sender.DeleteLogExercise(logId, logExerciseId);

        var log = LogList.First(log => log.LogId == logId);
        log.LogExerciseItems.Remove(log.LogExerciseItems.First(logExercise => logExercise.LogExerciseId == logExerciseId));
    }
}
