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
    public ObservableCollection<Log> LogList { get; set; } = new ObservableCollection<Log>();

    /// <summary>
    /// Gets the data access sender.
    /// </summary>
    private DaoSender Sender { get; } = ProviderUI.GetServiceProvider().GetRequiredService<DaoSender>();

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
        get { return _endDate; }
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

        foreach (Log log in logs)
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

        foreach (Log log in logs)
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
    /// <param name="iD">The ID of the log to delete.</param>
    public void DeleteLog(int iD)
    {
        Sender.DeleteLog(iD);
        LogList.Remove(LogList.First(log => log.LogId == iD));
    }

    /// <summary>
    /// Deletes a log food item by log ID and log food ID.
    /// </summary>
    /// <param name="logID">The log ID.</param>
    /// <param name="logFoodID">The log food ID.</param>
    public void DeleteLogFood(int logID, int logFoodID)
    {
        Sender.DeleteLogFood(logID, logFoodID);

        Log log = LogList.First(log => log.LogId == logID);
        log.LogFoodItems.Remove(log.LogFoodItems.First(logFood => logFood.LogFoodId == logFoodID));
    }

    /// <summary>
    /// Deletes a log exercise item by log ID and log exercise ID.
    /// </summary>
    /// <param name="logID">The log ID.</param>
    /// <param name="logExerciseID">The log exercise ID.</param>
    public void DeleteLogExercise(int logID, int logExerciseID)
    {
        Sender.DeleteLogExercise(logID, logExerciseID);

        Log log = LogList.First(logDate => logDate.LogId == logID);
        log.LogExerciseItems.Remove(log.LogExerciseItems.First(
            logExercise => logExercise.LogExerciseId == logExerciseID)
        );
    }
}
