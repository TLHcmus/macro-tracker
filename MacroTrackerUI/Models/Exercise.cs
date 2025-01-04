using System.ComponentModel;
using System.IO;

namespace MacroTrackerUI.Models;

public class Exercise : INotifyPropertyChanged
{
    public int ExerciseId { get; set; }
    public string Name { get; set; }

    public double CaloriesPerMinute { get; set; }

    public byte[] Image { get; set; }

    // Get calories burned
    public double GetCaloriesBurned(double duration)
    {
        return CaloriesPerMinute * duration;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
