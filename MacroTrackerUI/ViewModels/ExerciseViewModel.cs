using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MacroTrackerUI.ViewModels
{
    public class ExerciseViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ExerciseInfo> Exercises { get; set; }

        private DaoSender Dao { get; } =
            ProviderUI.GetServiceProvider().GetService<DaoSender>();

        public ExerciseViewModel()
        {
            Exercises = new ObservableCollection<ExerciseInfo>(Dao.GetExercises());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
