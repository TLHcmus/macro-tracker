using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Exercise : INotifyPropertyChanged
{
    public string Name { get; set; }

    public double CaloriesPerMinute { get; set; }

    public string IconFileName { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
