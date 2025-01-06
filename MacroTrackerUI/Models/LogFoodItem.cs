using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

public class LogFoodItem : INotifyPropertyChanged
{
    public int LogFoodId { get; set; }

    public int FoodId { get; set; }

    public double NumberOfServings { get; set; }

    public double TotalCalories { get; set; }
    public Food Food { get; set; }

    public event PropertyChangedEventHandler PropertyChanged; 
}
