using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

public class Item : INotifyPropertyChanged
{
    public string IconFile { get; set; }

    public string Title { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
