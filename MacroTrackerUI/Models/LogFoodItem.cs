using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

public class LogFoodItem : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the unique identifier for the logged food item.
    /// </summary>
    public int LogFoodId { get; set; }

    public int FoodId { get; set; }

    /// <summary>
    /// Gets or sets the number of servings of the food.
    /// </summary>
    public double NumberOfServings { get; set; }

    /// <summary>
    /// Gets or sets the total calories of the food.
    /// </summary>
    public double TotalCalories { get; set; }
    public Food Food { get; set; }

    public event PropertyChangedEventHandler PropertyChanged; 
}
