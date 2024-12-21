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

    public void GetAllLogs()
    {
        LogList = new(Sender.GetLogs());
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

    public void DeleteLog(int Id)
    {
        Sender.DeleteLog(Id);
        LogList.Remove(LogList.First(log => log.LogId == Id));
    }
}
