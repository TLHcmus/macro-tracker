using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Item : INotifyPropertyChanged
{
    public string IconFileName { get; set; }

    public string Name { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
