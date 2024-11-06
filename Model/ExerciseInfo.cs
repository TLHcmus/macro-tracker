using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.Model;

public class ExerciseInfo : INotifyPropertyChanged
{
    public string IconFileName { get; set; }

    public string Name { get; set; } 

    public event PropertyChangedEventHandler PropertyChanged;  
}
