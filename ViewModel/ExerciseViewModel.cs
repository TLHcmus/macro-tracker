using MacroTracker.Model;
using MacroTracker.Service.DataAcess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.ViewModel
{
    public class ExerciseViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ExerciseInfo> Exercises { get; set; }

        private IDao Dao { get; } = Service.ServiceRegistry.RegisteredService["IDao"] as IDao;

        public ExerciseViewModel()
        {
            Exercises = Dao.GetExercises();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
