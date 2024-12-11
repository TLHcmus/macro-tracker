using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.SmartCards;

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
}
