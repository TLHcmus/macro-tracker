using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Exercise : INotifyPropertyChanged
{
    required public string IconFileName { get; set; }

    required public string Name { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
