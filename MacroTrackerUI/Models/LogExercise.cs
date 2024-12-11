using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class LogExercise : Log
{
    public Exercise Exercise { get; set; }
    public float Calories { get; set; }
    public int Minutes { get; set; }
}
