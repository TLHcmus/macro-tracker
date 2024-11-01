using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.Model;

public class Food : INotifyPropertyChanged
{
    public string Name { get; set; }
    public int ServingSize { get; set; } // Vi du: 100g
    public int Calories { get; set; }
    public int Protein { get; set; }
    public int Carbs { get; set; }
    public int Fat { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;  
}

