using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

public class LogViewModel
{
    public ObservableCollection<Log> LogList { get; set; } = [];
    private DaoSender Sender { get; } = ProviderUI.GetServiceProvider().GetRequiredService<DaoSender>();
    public int PagingSize { get; set; }

    private DateTime _endDate = DateTime.Now.Date;

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

    public void GetNextLogsPage()
    {
        var logs = Sender.GetLogWithPagination(LogList.Count, DateOnly.FromDateTime(EndDate));
        
        if (logs.Count == 0)
            return;
        
        foreach (Log log in logs)
            LogList.Add(log);
    }

    public void GetNextLogsItem(int numItem)
    {
        var logs = Sender.GetNLogWithPagination(numItem, numItem, DateOnly.FromDateTime(EndDate));

        if (logs.Count == 0)
            return;

        foreach (Log log in logs)
            LogList.Add(log);
    }

    public void AddLog(Log log)
    {
        Sender.AddLog(log);
        LogList.Insert(0, log);
    }

    public bool DoesContainDate(DateOnly date)
    {
        return LogList.Any(log => log.LogDate.HasValue && log.LogDate == date);
    }

    public void DeleteLog(int iD)
    {
        Sender.DeleteLog(iD);
        LogList.Remove(LogList.First(log => log.LogId == iD));
    }

    public void DeleteLogFood(int logID, int logFoodID)
    {
        Sender.DeleteLogFood(logID, logFoodID);

        Log log = LogList.First(log => log.LogId == logID);
        log.LogFoodItems.Remove(log.LogFoodItems.First(logFood => logFood.LogFoodId == logFoodID));
    }

    public void DeleteLogExercise(int logID, int logFoodID)
    {
        Sender.DeleteLogExercise(logID, logFoodID);

        Log log = LogList.First(logDate => logDate.LogId == logID);
        log.LogExerciseItems.Remove(log.LogExerciseItems.First(
            logExercise => logExercise.LogExerciseId == logFoodID)
        );
    }
}
