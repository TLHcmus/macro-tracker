using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Exercise : Item, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
}
