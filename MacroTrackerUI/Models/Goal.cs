using System.ComponentModel;

namespace MacroTrackerUI.Models;
public class Goal : INotifyPropertyChanged
{
    public int GoalId { get; set; }
    public int Calories { get; set; }
    public int Protein { get; set; }
    public int Carbs { get; set; }
    public int Fat { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
