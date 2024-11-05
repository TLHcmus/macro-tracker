using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroTracker.Model;

namespace MacroTracker.ViewModel;
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
