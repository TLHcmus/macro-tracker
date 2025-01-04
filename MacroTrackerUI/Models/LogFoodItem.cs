using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

public class LogFoodItem
{
    public int LogFoodId { get; set; }

    public int FoodId { get; set; }

    public double NumberOfServings { get; set; }

    public double TotalCalories { get; set; }
}
