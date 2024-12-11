using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.Entities;

public class LogExercise : Log
{
    public Exercise Exercise { get; set; }
    public float Calories { get; set; }
    public int Minutes { get; set; }
}
