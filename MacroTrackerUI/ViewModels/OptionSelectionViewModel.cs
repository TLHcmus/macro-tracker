using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MacroTrackerUI.ViewModels;

public class OptionSelectionViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
}
