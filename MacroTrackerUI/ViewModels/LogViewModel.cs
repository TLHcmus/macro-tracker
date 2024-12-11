using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

class LogViewModel
{
    public ObservableCollection<LogDate> LogList { get; set; }
    private DaoSender Sender { get; } = ProviderUI.GetServiceProvider().GetRequiredService<DaoSender>();

    public void GetAllLogs()
    {
        LogList = new(Sender.GetAllLogDates());
    }

    public void AddLogDate(LogDate log)
    {
        Sender.AddLogDate(log);
        LogList.Insert(0, log);
    }

    public bool DoesContainDate(DateTime date)
    {
        return LogList.Any(log => log.Date.Date == date.Date);
    }

    public void AddDefaultLogDate()
    {
        LogList.Insert(0, Sender.AddDefaultLogDate());
    }

    public void DeleteLogDate(int Id)
    {
        Sender.DeleteLogDate(Id);
        LogList.Remove(LogList.First(log => log.ID == Id));
    }
}
