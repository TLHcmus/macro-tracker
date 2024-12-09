using MacroTrackerUI.Models;
using System.ComponentModel;

namespace MacroTrackerUI.ViewModels
{
    public class GoalsViewModel : INotifyPropertyChanged
    {
        public Goal CurrentGoal
        {
            get; set;
        }

        public GoalsViewModel()
        {
            CurrentGoal = new Goal();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}