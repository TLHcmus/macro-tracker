using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.Entities;

public class LogFood : Log
{
    public Food Food { get; set; }
    public float Calories { get; set; }
    public int Quantity { get; set; }
}
