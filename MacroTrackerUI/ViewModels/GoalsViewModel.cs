using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using PropertyChanged;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MacroTrackerUI.ViewModels
{
    public class GoalsViewModel : INotifyPropertyChanged
    {
        public Goal CurrentGoal;

        private DaoSender Dao { get; } =
            ProviderUI.GetServiceProvider().GetService<DaoSender>();

        public GoalsViewModel()
        {
            CurrentGoal = Dao.GetGoal();
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }

}