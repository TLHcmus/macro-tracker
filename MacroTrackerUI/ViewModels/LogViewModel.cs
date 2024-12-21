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
    public ObservableCollection<Log> LogList { get; set; }
    private DaoSender Sender { get; } = ProviderUI.GetServiceProvider().GetRequiredService<DaoSender>();
    private int NumberItem { get; set; } = 0;

    private DateTime _endDate = DateTime.Now.Date;

    public DateTime EndDate
    {
        get { return _endDate; }
        set
        {
            _endDate = value;
            NumberItem = 0;
            LogList.Clear();
            GetNextLogsPage();
        }
    }

    public void GetNextLogsPage()
    {
        var logs = Sender.GetLogDateWithPagination(NumberItem, DateOnly.FromDateTime(EndDate));
        foreach (Log log in logs)
        {
            LogList.Add(log);
            NumberItem += 1;
        }
    }

    public void GetAllLogs()
    {
        LogList = new(Sender.GetLogs());
    }

    public void AddLog(Log log)
    {
        Sender.AddLog(log);
        LogList.Insert(0, log);
        NumberItem += 1;
    }

    public bool DoesContainDate(DateOnly date)
    {
        return LogList.Any(log => log.LogDate.HasValue && log.LogDate == date);
    }

    //public void DeleteLogDate(int iD)
    //{
    //    Sender.DeleteLogDate(iD);
    //    LogList.Remove(LogList.First(log => log.ID == iD));
    //    NumberItem -= 1;
    //}

    //public void DeleteLogFood(int logDateID, int logID)
    //{
    //    Sender.DeleteLogFood(logDateID, logID);

    //    LogDate logDate = LogList.First(logDate => logDate.ID == logDateID);
    //    logDate.LogFood.Remove(logDate.LogFood.First(log => log.ID == logID));
    //}

    //public void DeleteLogExercise(int logDateID, int logID)
    //{
    //    Sender.DeleteLogExercise(logDateID, logID);

    //    LogDate logDate = LogList.First(logDate => logDate.ID == logDateID);
    //    logDate.LogExercise.Remove(logDate.LogExercise.First(log => log.ID == logID));
    //}
    //public void DeleteLog(int Id)
    //{ 
    //    Sender.DeleteLog(Id);
    //    LogList.Remove(LogList.First(log => log.LogId == Id));
    //}
}
