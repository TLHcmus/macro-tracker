using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.Model;

public class Exercise : INotifyPropertyChanged
{
    public string Name { get; set; } // Vi du: Bench Press
    public int Sets { get; set; } // Vi du: 3 Sets
    public int Reps { get; set; } // Vi du: 10 Reps

    public event PropertyChangedEventHandler PropertyChanged;  
}
