using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace MacroTracker.Model;
public class Goal : INotifyPropertyChanged
{
    private int calories;
    public int Calories
    {
        get => calories;
        set
        {
            if (calories != value)
            {
                calories = value;
                UpdateMacros();
            }
        }

    }
    public int Protein
    {
        get; set;
    }
    public int Carbs
    {
        get; set;
    }
    public int Fat
    {
        get; set;
    }

    // Update Macro
    private void UpdateMacros()
    {
        Protein = (int)(Calories * 0.2)/4; // 20% from protein
        Carbs = (int)(Calories * 0.5)/4;   // 50% from carbs
        Fat = (int)(Calories * 0.3)/9;     // 30% from fat
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
