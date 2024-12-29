using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Exercise : INotifyPropertyChanged
{
    public string Name { get; set; }

    public double CaloriesPerMinute { get; set; }

    public string IconFileName { get; set; }

    // Get calories burned
    public double GetCaloriesBurned(double duration)
    {
        return CaloriesPerMinute * duration;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
